using System.Data;
using FlightBookingSystem.Domain;
using log4net;
using FlightBookingSystem.Utils;
using FlightBookingSystem.Exceptions;
namespace FlightBookingSystem.Repository.AdoNet;

public class EmployeeAdoNetRepository : IEmployeeRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(EmployeeAdoNetRepository));
    
    private readonly DbUtils dbUtils;

    public EmployeeAdoNetRepository()
    {
        logger.Info("Initializing EmployeeAdoNetRepository");
        this.dbUtils = new DbUtils();
    }

    public void Save(Employee entity)
    {
        logger.Debug($"Enter Save: Saving employee: {entity}");
        var connection = dbUtils.GetConnection();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO employees (username, password) VALUES (@username, @password);" +
                                  "SELECT LAST_INSERT_ID();";
            AddParameter(command, "@username", entity.Username);
            AddParameter(command, "@password", entity.Password);

            try
            {
                var id = Convert.ToInt32(command.ExecuteScalar());
                entity.Id = id;
                logger.Debug($"Exit Save: Saved employee successfully: {entity}");
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to save employee", ex);
                var repoEx = new RepositoryException("DB error while saving employee", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }

    public Employee FindOne(int id)
    {
        logger.Debug($"Enter FindOne: Finding employee with id={id}");
        Employee employee = null;
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM employees WHERE id = @id";
            AddParameter(command, "@id", id);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = ExtractEmployeeFromReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to find employee", ex);
                var repoEx = new RepositoryException("DB error while finding employee", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }

        if (employee == null)
        {
            logger.Debug($"Exit FindOne: Employee with id={id} NOT found");
        }
        else
        {
            logger.Debug($"Exit FindOne: Employee with id={id} found.");
        }
        return employee;
    }

    public IEnumerable<Employee> FindAll()
    {
        logger.Debug($"Enter FindAll: Finding all employees");
        var employees = new List<Employee>();
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM employees;";
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(ExtractEmployeeFromReader(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to find all employees", ex);
                var repoEx = new RepositoryException("DB error while finding all employees", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
        logger.Debug("Exit FindAll: Returned found employees");
        return employees;
    }

    public void Update(Employee entity)
    {
        logger.Debug($"Enter Update: Updating employee: {entity}");
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE employees SET username = @username, password = @password WHERE id = @id";
            AddParameter(command, "@username", entity.Username);
            AddParameter(command, "@password", entity.Password);
            AddParameter(command, "@id", entity.Id);
            try
            {
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    logger.Error($"Failed to update employee with id={entity.Id}");
                    var repoEx = new RepositoryException("DB error while updating employee");
                    logger.Error("Throwing RepositoryException", repoEx);
                    throw repoEx;
                }

                logger.Debug($"Exit Update: Updated employee: {entity}");
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to update employee", ex);
                var repoEx = new RepositoryException("DB error while updating employee", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }

    public void Delete(int id)
    {
        logger.Debug($"Enter Delete: Deleting employee with id={id}");
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM employees WHERE id = @id";
            AddParameter(command, "@id", id);
            try
            {
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    logger.Error($"Failed to delete employee with id={id}");
                    var repoEx = new RepositoryException("DB error while deleting employee");
                    logger.Error("Throwing RepositoryException", repoEx);
                    throw repoEx;
                }

                logger.Debug($"Exit Delete: Deleted employee: {id}");
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to delete employee", ex);
                var repoEx = new RepositoryException("DB error while deleting employee", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }
    }

    public Employee FindByUsernameAndPassword(string username, string password)
    {
        logger.Debug($"Enter FindByUsernameAndPassword: Finding employee with username={username}");
        Employee employee = null;
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM employees WHERE username = @username and password = @password";
            AddParameter(command, "@username", username);
            AddParameter(command, "@password", password);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = ExtractEmployeeFromReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Caught Exception: Failed to find employee", ex);
                var repoEx = new RepositoryException("DB error while finding employee", ex);
                logger.Error("Throwing RepositoryException", repoEx);
                throw repoEx;
            }
        }

        if (employee == null)
        {
            logger.Debug($"Exit FindByUsernameAndPassword: Employee with username={username} NOT found");
        }
        else
        {
            logger.Debug($"Exit FindByUsernameAndPassword: Employee with username={username} found");
        }
        return employee;
    }

    private void AddParameter(IDbCommand command, string name, object value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }
    
    private Employee ExtractEmployeeFromReader(IDataReader reader)
    {
        int id = reader.GetInt32(reader.GetOrdinal("id"));
        string username = reader.GetString(reader.GetOrdinal("username"));
        string password = reader.GetString(reader.GetOrdinal("password"));
        return new Employee(id, username, password);
    }
}