using System.Data;
using FlightBookingSystem.Domain;
using FlightBookingSystem.Exceptions;
using FlightBookingSystem.Utils;
using log4net;

namespace FlightBookingSystem.Repository.AdoNet;

/// <summary>
/// ADO.NET implementation of <see cref="IFlightRepository"/>.
/// This class handles all database related operations from CRUD to custom queries
/// for the <see cref="Flight"/> entity.
/// </summary>
public class FlightAdoNetRepository : IFlightRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(FlightAdoNetRepository));
    private readonly DbUtils dbUtils;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightAdoNetRepository"/> class.
    /// Sets up the database utility for connection management.
    /// </summary>
    public FlightAdoNetRepository()
    {
        logger.Info("Initializing FlightAdoNetRepository");
        this.dbUtils = new DbUtils();
    }
    
    /// <summary>
    /// Saves a new <see cref="Flight"/> to the database and assigns it an auto generated id.
    /// </summary>
    /// <param name="entity">the entity to persist</param>
    /// <exception cref="RepositoryException">if a database error occurs.</exception>
    public void Save(Flight entity)
    {
        logger.Debug($"Enter Save: Saving flight from {entity.DepartureAirport} to {entity.ArrivalAirport}");
        var connection = dbUtils.GetConnection();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO flights (departure_airport, arrival_airport, departure_time, arrival_time, available_seats) " +
                                  "VALUES (@depAirport, @arrAirport, @depTime, @arrTime, @seats); " +
                                  "SELECT LAST_INSERT_ID();";
            AddParameter(command, "@depAirport", entity.DepartureAirport);
            AddParameter(command, "@arrAirport", entity.ArrivalAirport);
            AddParameter(command, "@depTime", entity.DepartureTime);
            AddParameter(command, "@arrTime", entity.ArrivalTime);
            AddParameter(command, "@seats", entity.AvailableSeats);

            try
            {
                var id = Convert.ToInt32(command.ExecuteScalar());
                entity.Id = id;
                logger.Debug($"Exit Save: Saved flight successfully: {entity}");
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to save flight", ex);
                var repoEx = new RepositoryException("DB error while saving flight", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }
    
    /// <summary>
    /// Finds an <see cref="Flight"/> based on its id.
    /// </summary>
    /// <param name="id">the ID of the entity</param>
    /// <returns>the <see cref="Flight"/> if found, or null otherwise</returns>
    /// <exception cref="RepositoryException">if a database error occurs.</exception>
    public Flight FindOne(int id)
    {
        logger.Debug($"Enter FindOne: Finding flight with id={id}");
        Flight flight = null;
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM flights WHERE id = @id";
            AddParameter(command, "@id", id);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        flight = ExtractFlightFromReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Caught Exception: Failed to find flight with id={id}", ex);
                var repoEx = new RepositoryException($"DB error while finding flight with id={id}", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }

        if (flight == null)
            logger.Debug($"Exit FindOne: Flight with id={id} NOT found.");
        else
            logger.Debug($"Exit FindOne: Flight with id={id} found.");
        
        return flight;
    }
    
    /// <summary>
    /// Retrieves all the flights from the database.
    /// <para><strong>WARNING:</strong> Use this function carefully, because there can be lots of entities in the database.</para>
    /// </summary>
    /// <returns>an <see cref="IEnumerable{Flight}"/> collection of all flights</returns>
    /// <exception cref="RepositoryException">if a database error occurs</exception>
    public IEnumerable<Flight> FindAll()
    {
        logger.Debug("Enter FindAll: Finding all flights");
        var flights = new List<Flight>();
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM flights";
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flights.Add(ExtractFlightFromReader(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to find all flights", ex);
                var repoEx = new RepositoryException("DB error while finding all flights", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
        logger.Debug("Exit FindAll: Returned found flights");
        return flights;
    }
    
    /// <summary>
    /// Updates an existing <see cref="Flight"/> based on its id.
    /// </summary>
    /// <param name="entity">the entity with updated information</param>
    /// <exception cref="RepositoryException">if a database error occurs.</exception>
    public void Update(Flight entity)
    {
        logger.Debug($"Enter Update: Updating flight: {entity}");
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE flights SET departure_airport = @depAirport, arrival_airport = @arrAirport, " +
                                  "departure_time = @depTime, arrival_time = @arrTime, available_seats = @seats WHERE id = @id";
            AddParameter(command, "@depAirport", entity.DepartureAirport);
            AddParameter(command, "@arrAirport", entity.ArrivalAirport);
            AddParameter(command, "@depTime", entity.DepartureTime);
            AddParameter(command, "@arrTime", entity.ArrivalTime);
            AddParameter(command, "@seats", entity.AvailableSeats);
            AddParameter(command, "@id", entity.Id);
            try
            {
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    logger.Error($"Failed to update flight with id={entity.Id}");
                    var repoEx = new RepositoryException("DB error while updating flight");
                    logger.Error("Throwing RepositoryException", repoEx);
                    throw repoEx;
                }
                logger.Debug($"Exit Update: Updated flight: {entity}");
            }
            catch (Exception ex)
            {
                logger.Error($"Caught Exception: Failed to update flight", ex);
                var repoEx = new RepositoryException($"DB error while updating flight", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }
    
    /// <summary>
    /// Deletes an <see cref="Flight"/> based on its id.
    /// </summary>
    /// <param name="id">the ID of the entity to remove</param>
    /// <exception cref="RepositoryException">if a database error occurs.</exception>
    public void Delete(int id)
    {
        logger.Debug($"Enter Delete: Deleting flight with id={id}");
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM flights WHERE id = @id";
            AddParameter(command, "@id", id);
            try
            {
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    logger.Error($"Failed to delete flight with id={id}");
                    var repoEx = new RepositoryException("DB error while deleting flights");
                    logger.Error("Throwing RepositoryException", repoEx);
                    throw repoEx;
                }
                logger.Debug($"Exit Delete: Deleted flight: {id}");
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to delete flight", ex);
                var repoEx = new RepositoryException("DB error while deleting flight", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }
    
    /// <summary>
    /// Finds all flights based on their destination and departure date
    /// </summary>
    /// <param name="destination">the arrival airport name</param>
    /// <param name="date">the scheduled departure date</param>
    /// <returns>an <see cref="IEnumerable{Flight}"/> collection of the respective flights</returns>
    /// <exception cref="RepositoryException">if a database error occurs</exception>
    public IEnumerable<Flight> FindByDestinationAndDepartureDate(string destination, DateTime date)
    {
        logger.Debug($"Enter FindByDestinationAndDepartureDate: Finding flights to {destination} on {date.ToString("yyyy-MM-dd")}");
        var flights = new List<Flight>();
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM flights WHERE arrival_airport = @dest AND DATE(departure_time) = DATE(@date)";
            AddParameter(command, "@dest", destination);
            AddParameter(command, "@date", date);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flights.Add(ExtractFlightFromReader(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Caught Exception: Failed to find flights to {destination}", ex);
                var repoEx = new RepositoryException($"DB error while filtering flights to {destination}", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
        logger.Debug($"Exit FindByDestinationAndDepartureDate: Found {flights.Count} matching flights");
        return flights;
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
    /// Helper method that maps a single row from the <see cref="IDataReader"/> into a <see cref="Flight"/> object.
    /// </summary>
    private Flight ExtractFlightFromReader(IDataReader reader)
    {
        int id = reader.GetInt32(reader.GetOrdinal("id"));
        string depAirport = reader.GetString(reader.GetOrdinal("departure_airport"));
        string arrAirport = reader.GetString(reader.GetOrdinal("arrival_airport"));
        DateTime depTime = reader.GetDateTime(reader.GetOrdinal("departure_time"));
        DateTime arrTime = reader.GetDateTime(reader.GetOrdinal("arrival_time"));
        int seats = reader.GetInt32(reader.GetOrdinal("available_seats"));

        return new Flight(id, depAirport, arrAirport, depTime, arrTime, seats);
    }
}
