using System.Data;
using FlightBookingSystem.Domain;
using log4net;
using FlightBookingSystem.Utils;
using FlightBookingSystem.Exceptions;

namespace FlightBookingSystem.Repository.AdoNet;

/// <summary>
/// ADO.NET implementation of <see cref="IBookingRepository"/>.
/// This class handles all database related operations from CRUD to custom queries
/// for the <see cref="Booking"/> entity using MySql.
/// </summary>
public class BookingAdoNetRepository : IBookingRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(BookingAdoNetRepository));
    private readonly DbUtils dbUtils;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingAdoNetRepository"/> class.
    /// Sets up the database utility for connection management.
    /// </summary>
    public BookingAdoNetRepository()
    {
        logger.Info("Initializing BookingAdoNetRepository");
        this.dbUtils = new DbUtils();
    }

    /// <summary>
    /// Saves a new <see cref="Booking"/> to the database and assigns it an auto-generated ID.
    /// </summary>
    /// <param name="entity">The booking entity to persist.</param>
    /// <exception cref="RepositoryException">Thrown if a database error occurs during the save operation.</exception>
    public void Save(Booking entity)
    {
        logger.Debug($"Enter Save: Saving booking for flight ID: {entity.Flight.Id} with {entity.NumberOfSeats} seats");
        var connection = dbUtils.GetConnection();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO bookings (flight_id, number_of_seats, tourist_names) " +
                                  "VALUES (@flightId, @seats, @tourists); " +
                                  "SELECT LAST_INSERT_ID();";
            AddParameter(command, "@flightId", entity.Flight.Id);
            AddParameter(command, "@seats", entity.NumberOfSeats);
            string touristsJoined = string.Join(",", entity.TouristNames);
            AddParameter(command, "@tourists", touristsJoined);
            try
            {
                var id = Convert.ToInt32(command.ExecuteScalar());
                entity.Id = id;
                logger.Debug($"Exit Save: Saved booking successfully with ID: {id}");
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to save booking", ex);
                var repoEx = new RepositoryException("DB error while saving booking", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }

    /// <summary>
    /// Finds a <see cref="Booking"/> based on its ID, including related Flight information.
    /// </summary>
    /// <param name="id">The ID of the booking to find.</param>
    /// <returns>The <see cref="Booking"/> if found, or null otherwise.</returns>
    /// <exception cref="RepositoryException">Thrown if a database error occurs during the search.</exception>
    public Booking FindOne(int id)
    {
        logger.Debug($"Enter FindOne: Finding booking with id={id}");
        Booking? booking = null;
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"
                SELECT b.id AS booking_id, b.flight_id, b.number_of_seats, b.tourist_names,
                       f.departure_airport, f.arrival_airport, f.departure_time, f.arrival_time
                FROM bookings b
                INNER JOIN flights f ON b.flight_id = f.id
                WHERE b.id = @id";
            AddParameter(command, "@id", id);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        booking = ExtractBookingFromReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Caught Exception: Failed to find booking with id={id}", ex);
                var repoEx = new RepositoryException($"DB error while finding booking with id={id}", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
        if (booking == null)
            logger.Debug($"Exit FindOne: Booking with id={id} NOT found.");
        else
            logger.Debug($"Exit FindOne: Booking with id={id} found.");
        return booking;
    }

    /// <summary>
    /// Retrieves all bookings from the database, including their associated Flight details.
    /// <para><strong>WARNING:</strong> Use this function carefully, as it can return a large number of entities.</para>
    /// </summary>
    /// <returns>An <see cref="IEnumerable{Booking}"/> containing all bookings.</returns>
    /// <exception cref="RepositoryException">Thrown if a database error occurs during retrieval.</exception>
    public IEnumerable<Booking> FindAll()
    {
        logger.Debug("Enter FindAll: Finding all bookings");
        var bookings = new List<Booking>();
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"
                            SELECT b.id, b.number_of_seats, b.tourist_names, 
                                   f.id as flight_id, f.departure_airport, f.arrival_airport, f.departure_time, f.arrival_time, f.available_seats 
                            FROM bookings b 
                            INNER JOIN flights f ON b.flight_id = f.id";
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookings.Add(ExtractBookingFromReader(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to find all bookings", ex);
                var repoEx = new RepositoryException("DB error while finding all bookings", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
        logger.Debug($"Exit FindAll: Returned {bookings.Count} bookings");
        return bookings;
    }

    /// <summary>
    /// Updates an existing <see cref="Booking"/> based on its ID.
    /// </summary>
    /// <param name="entity">The booking entity with updated information.</param>
    /// <exception cref="RepositoryException">Thrown if no booking with the given ID is found or if a database error occurs.</exception>
    public void Update(Booking entity)
    {
        logger.Debug($"Enter Update: Updating booking with id={entity.Id}");
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE bookings SET flight_id = @flightId, number_of_seats = @seats, tourist_names = @tourists WHERE id = @id";
            AddParameter(command, "@flightId", entity.Flight.Id);
            AddParameter(command, "@seats", entity.NumberOfSeats);
            string touristsJoined = string.Join(",", entity.TouristNames);
            AddParameter(command, "@tourists", touristsJoined);
            AddParameter(command, "@id", entity.Id);
            try
            {
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    logger.Warn($"Failed to update booking with id={entity.Id}. No rows affected.");
                    var repoEx = new RepositoryException($"DB error: No booking found to update with id={entity.Id}");
                    logger.Error("Throwing RepositoryException", repoEx);
                    throw repoEx;
                }
                logger.Debug($"Exit Update: Updated booking: {entity.Id}");
            }
            catch (Exception ex)
            {
                logger.Error($"Caught Exception: Failed to update booking with id={entity.Id}", ex);
                var repoEx = new RepositoryException($"DB error while updating booking with id={entity.Id}", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }

    /// <summary>
    /// Deletes a <see cref="Booking"/> based on its ID.
    /// </summary>
    /// <param name="id">The ID of the booking to remove.</param>
    /// <exception cref="RepositoryException">Thrown if no booking with the given ID is found or if a database error occurs.</exception>
    public void Delete(int id)
    {
        logger.Debug($"Enter Delete: Deleting booking with id={id}");
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM bookings WHERE id = @id";
            AddParameter(command, "@id", id);
            try
            {
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    logger.Warn($"Failed to delete booking with id={id}. No rows affected.");
                    var repoEx = new RepositoryException($"DB error: No booking found to delete with id={id}");
                    logger.Error("Throwing RepositoryException", repoEx);
                    throw repoEx;
                }
                logger.Debug($"Exit Delete: Deleted booking: {id}");
            }
            catch (Exception ex)
            {
                logger.Error($"Caught Exception: Failed to delete booking with id={id}", ex);
                var repoEx = new RepositoryException($"DB error while deleting booking with id={id}", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }

    /// <summary>
    /// Helper method to add a parameter to a database command.
    /// </summary>
    private void AddParameter(IDbCommand command, string name, object value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }

    /// <summary>
    /// Helper method that maps a single row from the <see cref="IDataReader"/> into a <see cref="Booking"/> entity.
    /// </summary>
    /// <param name="reader">The IDataReader pointing to the current row.</param>
    /// <returns>A new <see cref="Booking"/> object.</returns>
    private Booking ExtractBookingFromReader(IDataReader reader)
    {
        int flightId = reader.GetInt32(reader.GetOrdinal("flight_id"));
        string depAirport = reader.GetString(reader.GetOrdinal("departure_airport"));
        string arrAirport = reader.GetString(reader.GetOrdinal("arrival_airport"));
        DateTime depTime = reader.GetDateTime(reader.GetOrdinal("departure_time"));
        DateTime arrTime = reader.GetDateTime(reader.GetOrdinal("arrival_time"));
        int availableSeats = reader.GetInt32(reader.GetOrdinal("available_seats"));

        Flight flight = new Flight(flightId, depAirport, arrAirport, depTime, arrTime, availableSeats);
        
        int bookingId = reader.GetInt32(reader.GetOrdinal("id"));
        int seats = reader.GetInt32(reader.GetOrdinal("number_of_seats"));
        string touristsString = reader.GetString(reader.GetOrdinal("tourist_names"));
        List<string> touristNames = new List<string>();
        if (!string.IsNullOrEmpty(touristsString))
        {
            touristNames = touristsString.Split(',').ToList();
        }
        return new Booking(bookingId, flight, seats, touristNames);
    }
}
