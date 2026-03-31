using FlightBookingSystem.Domain;
using FlightBookingSystem.Exceptions;
using FlightBookingSystem.Repository;
using FlightBookingSystem.Utils;
using FlightBookingSystem.Validators;

namespace FlightBookingSystem.Service;

/// <summary>
/// Service class for <see cref="Employee"/> entities.
/// <para>
///     Includes CRUD operations, as well as more complex ones like login and registration.
/// </para>
/// </summary>
public class EmployeeService
{
    private readonly IEmployeeRepository employeeRepository;

    /// <summary>
    /// Constructs the service using an interface for easy swapping between
    /// persistence types.
    /// </summary>
    /// <param name="employeeRepository">A repository that implements <see cref="IEmployeeRepository"/></param>
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Validates and saves a new <see cref="Employee"/> to the database.
    /// <para>
    ///     <strong>WARNING:</strong>
    ///     Hash the users password before calling this function, because
    ///     this function does NOT hash the password so use it carefully.
    /// </para>
    /// </summary>
    /// <param name="employee">The entity to persist</param>
    /// <exception cref="RepositoryException">If a persistence error occurs.</exception>
    /// <exception cref="ValidationException">If the given employee object is invalid</exception>
    public void Save(Employee employee)
    {
        EmployeeValidator.validate(employee);
        this.employeeRepository.Save(employee);
    }

    /// <summary>
    /// Finds an <see cref="Employee"/> based on their ID.
    /// </summary>
    /// <param name="id">The ID of the entity</param>
    /// <returns>The <see cref="Employee"/> if found, or null otherwise</returns>
    /// <exception cref="RepositoryException">If a database error occurs.</exception>
    public Employee FindOne(int id)
    {
        return this.employeeRepository.FindOne(id);
    }

    /// <summary>
    /// Retrieves all the employees from the database.
    /// <para>
    ///     <strong>WARNING:</strong>
    ///     Use this function carefully, because there can be
    ///     lots of entities in the database.
    /// </para>
    /// </summary>
    /// <returns>An <see cref="IEnumerable{Employee}"/> collection of all employees</returns>
    /// <exception cref="RepositoryException">If a database error occurs</exception>
    public IEnumerable<Employee> FindAll()
    {
        return this.employeeRepository.FindAll();
    }

    /// <summary>
    /// Updates an existing <see cref="Employee"/> based on their ID.
    /// <para>
    ///     <strong>WARNING:</strong>
    ///     Hash the users password before calling this function, because
    ///     this function does NOT hash the password so use it carefully.
    /// </para>
    /// </summary>
    /// <param name="employee">The entity with updated information</param>
    /// <exception cref="RepositoryException">If a database error occurs.</exception>
    /// <exception cref="ValidationException">If the given employee object is invalid</exception>
    public void Update(Employee employee)
    {
        EmployeeValidator.validate(employee);
        this.employeeRepository.Update(employee);
    }

    /// <summary>
    /// Deletes an <see cref="Employee"/> based on their ID.
    /// </summary>
    /// <param name="id">The ID of the entity to remove</param>
    /// <exception cref="RepositoryException">If a database error occurs.</exception>
    public void Delete(int id)
    {
        this.employeeRepository.Delete(id);
    }

    /// <summary>
    /// Authenticates an <see cref="Employee"/> based on their username and password.
    /// <para>
    ///     This method retrieves the employee from the database and verifies if the
    ///     provided plain-text password matches the stored SHA-256 hash.
    /// </para>
    /// </summary>
    /// <param name="username">The employee's login username</param>
    /// <param name="password">The employee's plain-text password</param>
    /// <returns>The authenticated <see cref="Employee"/> object</returns>
    /// <exception cref="ServiceException">If the employee is not found or the password is invalid</exception>
    /// <exception cref="RepositoryException">If a database error occurs</exception>
    public Employee Login(string username, string password)
    {
        // Presupunem că IEmployeeRepository are o metodă FindByUsername (din contextul tău anterior)
        Employee employee = this.employeeRepository.FindByUsername(username);
        if (employee == null)
        {
            throw new ServiceException("Employee not found");
        }
        
        if (employee.Password != Encryption.SHA256OneWayHash(password))
        {
            throw new ServiceException("Invalid password");
        }
        return employee;
    }

    /// <summary>
    /// Validates and registers a new <see cref="Employee"/>.
    /// <para>
    ///     Unlike the standard <see cref="Save(Employee)"/> method, this function
    ///     automatically hashes the employee's plain-text password using SHA-256
    ///     before persisting the entity to the database.
    /// </para>
    /// </summary>
    /// <param name="employee">The entity to register, containing a plain-text password</param>
    /// <exception cref="ValidationException">If the provided employee data violates validation constraints</exception>
    /// <exception cref="RepositoryException">If a database error occurs</exception>
    public void Register(Employee employee)
    {
        EmployeeValidator.validate(employee);
        employee.Password = Encryption.SHA256OneWayHash(employee.Password);
        this.employeeRepository.Save(employee);
    }
}
