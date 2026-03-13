using FlightBookingSystem.Model;

namespace FlightBookingSystem.Repository;

/// <summary>
/// Specialized repository interface for Flight related operations.
/// </summary>
public interface IFlightRepository : IRepository<int, Flight>
{
    /// <summary>
    /// Retrieves flights by destination and departure date.
    /// </summary>
    /// <param name="destination">The arrival airport/city.</param>
    /// <param name="date">The scheduled departure date.</param>
    /// <returns>A collection of matching flights.</returns>
    IEnumerable<Flight> FindByDestinationAndDate(string destination, DateTime date);
}