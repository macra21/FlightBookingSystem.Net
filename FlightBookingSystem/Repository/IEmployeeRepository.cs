using FlightBookingSystem.Domain;

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
    /// <returns>The employee object if credentials match, otherwise null.</returns>
    Employee FindByUsername(string username);
}