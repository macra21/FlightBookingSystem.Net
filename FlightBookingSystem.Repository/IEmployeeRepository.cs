using FlightBookingSystem.Model;

namespace FlightBookingSystem.Repository;

/// <summary>
/// Specialized repository interface for Employee related operations.
/// </summary>
public interface IEmployeeRepository : IRepository<int, Employee>
{
    /// <summary>
    /// Retrieves an employee based on login credentials.
    /// </summary>
    /// <param name="username">The employee's username.</param>
    /// <param name="password">The employee's password.</param>
    /// <returns>The employee object if credentials match, otherwise null.</returns>
    Employee FindByUsernameAndPassword(string username, string password);
}