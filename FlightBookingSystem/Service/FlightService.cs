using FlightBookingSystem.Domain;
using FlightBookingSystem.Exceptions;
using FlightBookingSystem.Repository;
using FlightBookingSystem.Validators;

namespace FlightBookingSystem.Service;

/// <summary>
/// Service class for <see cref="Flight"/> entities.
/// <para>
///     Includes CRUD operations, as well as complex filtering operations.
/// </para>
/// </summary>
public class FlightService
{
    private readonly IFlightRepository flightRepository;

    /// <summary>
    /// Constructs the service using an interface for easy swapping between
    /// persistence types.
    /// </summary>
    /// <param name="flightRepository">A repository that implements <see cref="IFlightRepository"/></param>
    public FlightService(IFlightRepository flightRepository)
    {
        this.flightRepository = flightRepository;
    }

    /// <summary>
    /// Validates and saves a new <see cref="Flight"/> to the database.
    /// </summary>
    /// <param name="flight">The entity to persist</param>
    /// <exception cref="RepositoryException">If a database error occurs.</exception>
    /// <exception cref="ValidationException">If the provided flight data violates validation constraints</exception>
    public void Save(Flight flight)
    {
        FlightValidator.validate(flight);
        this.flightRepository.Save(flight);
    }

    /// <summary>
    /// Finds a <see cref="Flight"/> based on its ID.
    /// </summary>
    /// <param name="id">The ID of the entity</param>
    /// <returns>The <see cref="Flight"/> if found, or null otherwise</returns>
    /// <exception cref="RepositoryException">If a database error occurs.</exception>
    public Flight FindOne(int id)
    {
        return this.flightRepository.FindOne(id);
    }

    /// <summary>
    /// Retrieves all the flights from the database.
    /// <para>
    ///     <strong>WARNING:</strong>
    ///     Use this function carefully, because there can be
    ///     lots of entities in the database.
    /// </para>
    /// </summary>
    /// <returns>An <see cref="IEnumerable{Flight}"/> collection of all flights</returns>
    /// <exception cref="RepositoryException">If a database error occurs</exception>
    public IEnumerable<Flight> FindAll()
    {
        return this.flightRepository.FindAll();
    }

    /// <summary>
    /// Updates an existing <see cref="Flight"/> based on its ID.
    /// </summary>
    /// <param name="flight">The entity with updated information</param>
    /// <exception cref="RepositoryException">If a database error occurs.</exception>
    /// <exception cref="ValidationException">If the provided flight data violates validation constraints</exception>
    public void Update(Flight flight)
    {
        FlightValidator.validate(flight);
        this.flightRepository.Update(flight);
    }

    /// <summary>
    /// Deletes a <see cref="Flight"/> based on its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to remove</param>
    /// <exception cref="RepositoryException">If a database error occurs.</exception>
    public void Delete(int id)
    {
        this.flightRepository.Delete(id);
    }

    /// <summary>
    /// Retrieves flights by destination and departure date.
    /// <para>
    ///     Filters the system to find flights matching the exact expected date
    ///     and landing at the selected airport.
    /// </para>
    /// </summary>
    /// <param name="destination">The arrival airport name</param>
    /// <param name="date">The scheduled departure date</param>
    /// <returns>A collection of matching <see cref="Flight"/> objects</returns>
    /// <exception cref="RepositoryException">If a database error occurs</exception>
    public IEnumerable<Flight> FindByDestinationAndDepartureDate(string destination, DateTime date)
    {
        return this.flightRepository.FindByDestinationAndDepartureDate(destination, date);
    }
}
