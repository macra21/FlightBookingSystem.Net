using System.Data;
using FlightBookingSystem.Domain;
using log4net;
using FlightBookingSystem.Utils;
using FlightBookingSystem.Exceptions;

namespace FlightBookingSystem.Repository.AdoNet;

/// <summary>
/// ADO.NET implementation of <see cref="IEmployeeRepository"/>.
/// This class handles all database related operations from CRUD to custom queries
/// for the <see cref="Employee"/> entity.
/// </summary>
public class EmployeeAdoNetRepository : IEmployeeRepository
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(EmployeeAdoNetRepository));
    
    private readonly DbUtils dbUtils;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeAdoNetRepository"/> class.
    /// Sets up the database utility for connection management.
    /// </summary>
    public EmployeeAdoNetRepository()
    {
        logger.Info("Initializing EmployeeAdoNetRepository");
        this.dbUtils = new DbUtils();
    }

    /// <summary>
    /// Saves a new <see cref="Employee"/> to the database and assigns it an auto-generated ID.
    /// </summary>
    /// <param name="entity">The employee entity to persist.</param>
    /// <exception cref="RepositoryException">Thrown if a database error occurs during the save operation.</exception>
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

    /// <summary>
    /// Finds an <see cref="Employee"/> based on their ID.
    /// </summary>
    /// <param name="id">The ID of the employee to find.</param>
    /// <returns>The <see cref="Employee"/> if found, or null otherwise.</returns>
    /// <exception cref="RepositoryException">Thrown if a database error occurs during the search.</exception>
    public Employee FindOne(int id)
    {
        logger.Debug($"Enter FindOne: Finding employee with id={id}");
        Employee? employee = null;
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

    /// <summary>
    /// Retrieves all the employees from the database.
    /// <para><strong>WARNING:</strong> Use this function carefully, because there can be lots of entities in the database.</para>
    /// </summary>
    /// <returns>An <see cref="IEnumerable{Employee}"/> collection of all employees.</returns>
    /// <exception cref="RepositoryException">Thrown if a database error occurs during retrieval.</exception>
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

    /// <summary>
    /// Updates an existing <see cref="Employee"/> based on its ID.
    /// </summary>
    /// <param name="entity">The employee entity with updated information.</param>
    /// <exception cref="RepositoryException">Thrown if no employee with the given ID is found or if a database error occurs.</exception>
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
                    var repoEx = new RepositoryException("DB error while updating employee, no rows affected.");
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

    /// <summary>
    /// Deletes an <see cref="Employee"/> based on its ID.
    /// </summary>
    /// <param name="id">The ID of the employee to remove.</param>
    /// <exception cref="RepositoryException">Thrown if no employee with the given ID is found or if a database error occurs.</exception>
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
                    var repoEx = new RepositoryException("DB error while deleting employee, no rows affected.");
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

    /// <summary>
    /// Finds an <see cref="Employee"/> based on their username.
    /// <para>This is used in the Authentication service for login.</para>
    /// </summary>
    /// <param name="username">The account username.</param>
    /// <returns>The <see cref="Employee"/> if credentials match, or null otherwise.</returns>
    /// <exception cref="RepositoryException">Thrown if a database error occurs during the search.</exception>
    public Employee FindByUsername(string username)
    {
        logger.Debug($"Enter FindByUsername: Finding employee with username={username}");
        Employee? employee = null;
        var connection = dbUtils.GetConnection();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM employees WHERE username = @username";
            AddParameter(command, "@username", username);
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
            logger.Debug($"Exit FindByUsername: Employee with username={username} NOT found");
        }
        else
        {
            logger.Debug($"Exit FindByUsername: Employee with username={username} found");
        }
        return employee;
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
    /// Helper method that maps a single row from the <see cref="IDataReader"/> into an <see cref="Employee"/> entity.
    /// </summary>
    /// <param name="reader">The IDataReader pointing to the current row.</param>
    /// <returns>A new <see cref="Employee"/> object.</returns>
    private Employee ExtractEmployeeFromReader(IDataReader reader)
    {
        int id = reader.GetInt32(reader.GetOrdinal("id"));
        string username = reader.GetString(reader.GetOrdinal("username"));
        string password = reader.GetString(reader.GetOrdinal("password"));
        return new Employee(id, username, password);
    }
}
