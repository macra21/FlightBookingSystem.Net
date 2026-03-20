using FlightBookingSystem.Domain;

namespace FlightBookingSystem.Repository;

/// <summary>
/// Specialized repository interface for Booking related operations.
/// </summary>
public interface IBookingRepository : IRepository<int, Booking>
{
    
}