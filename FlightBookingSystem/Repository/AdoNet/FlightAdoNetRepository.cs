using System.Data;
using FlightBookingSystem.Domain;
using FlightBookingSystem.Exceptions;
using FlightBookingSystem.Utils;
using log4net;

namespace FlightBookingSystem.Repository.AdoNet;

public class FlightAdoNetRepository : IFlightRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(FlightAdoNetRepository));
    private readonly DbUtils dbUtils;

    public FlightAdoNetRepository()
    {
        logger.Info("Initializing FlightAdoNetRepository");
        this.dbUtils = new DbUtils();
    }
    
    public void Save(Flight entity)
    {
        logger.Debug($"Enter Save: Saving flight from {entity.DepartureAirport} to {entity.ArrivalAirport}");
        var connection = dbUtils.GetConnection();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO flights (departure_airport, arrival_airport, departure_time, arrival_time) " +
                                  "VALUES (@depAirport, @arrAirport, @depTime, @arrTime); " +
                                  "SELECT LAST_INSERT_ID();";
            AddParameter(command, "@depAirport", entity.DepartureAirport);
            AddParameter(command, "@arrAirport", entity.ArrivalAirport);
            AddParameter(command, "@depTime", entity.DepartureTime); // ADO.NET mapeaza DateTime automat!
            AddParameter(command, "@arrTime", entity.ArrivalTime);

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
        {
            logger.Debug($"Exit FindOne: Flight with id={id} NOT found.");
        }
        else
        {
            logger.Debug($"Exit FindOne: Flight with id={id} found.");
        }
        return flight;
    }
    
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
    
    public void Update(Flight entity)
    {
        logger.Debug($"Enter Update: Updating flight: {entity}");
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE flights SET departure_airport = @depAirport, arrival_airport = @arrAirport, " +
                                  "departure_time = @depTime, arrival_time = @arrTime WHERE id = @id";
            AddParameter(command, "@depAirport", entity.DepartureAirport);
            AddParameter(command, "@arrAirport", entity.ArrivalAirport);
            AddParameter(command, "@depTime", entity.DepartureTime);
            AddParameter(command, "@arrTime", entity.ArrivalTime);
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
    
    public IEnumerable<Flight> FindByDestinationAndDate(string destination, DateTime date)
    {
        logger.Debug($"Enter FindByDestinationAndDate: Finding flights to {destination} on {date.ToString("yyyy-MM-dd")}");
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
        logger.Debug($"Exit FindByDestinationAndDate: Found {flights.Count} matching flights");
        return flights;
    }
    
    private void AddParameter(IDbCommand command, string name, object value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }

    private Flight ExtractFlightFromReader(IDataReader reader)
    {
        int id = reader.GetInt32(reader.GetOrdinal("id"));
        string depAirport = reader.GetString(reader.GetOrdinal("departure_airport"));
        string arrAirport = reader.GetString(reader.GetOrdinal("arrival_airport"));
        DateTime depTime = reader.GetDateTime(reader.GetOrdinal("departure_time"));
        DateTime arrTime = reader.GetDateTime(reader.GetOrdinal("arrival_time"));

        return new Flight(id, depAirport, arrAirport, depTime, arrTime);
    }
}