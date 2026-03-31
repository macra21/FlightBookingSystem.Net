using FlightBookingSystem.Domain;
using FlightBookingSystem.Exceptions;
using FlightBookingSystem.Repository;
using FlightBookingSystem.Validators;

namespace FlightBookingSystem.Service;

/// <summary>
/// Service class for <see cref="Booking"/> entities.
/// <para>
/// Includes CRUD operations for managing flight bookings.
/// </para>
/// </summary>
public class BookingService
{
    private readonly IBookingRepository _bookingRepository;

    /// <summary>
    /// Constructs the service using an interface for easy swapping between
    /// persistence types.
    /// </summary>
    /// <param name="bookingRepository">A repository that implements <see cref="IBookingRepository"/>.</param>
    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    /// <summary>
    /// Validates and saves a new <see cref="Booking"/> to the database.
    /// </summary>
    /// <param name="booking">The entity to persist.</param>
    /// <exception cref="RepositoryException">Thrown if a persistence error occurs.</exception>
    /// <exception cref="ValidationException">Thrown if the given booking object is invalid.</exception>
    public void Save(Booking booking)
    {
        BookingValidator.validate(booking);
        _bookingRepository.Save(booking);
    }

    /// <summary>
    /// Finds a <see cref="Booking"/> based on its ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>The <see cref="Booking"/> if found, or null otherwise.</returns>
    /// <exception cref="RepositoryException">Thrown if a database error occurs.</exception>
    public Booking FindOne(int id)
    {
        return _bookingRepository.FindOne(id);
    }

    /// <summary>
    /// Retrieves all the bookings from the database.
    /// <para>
    /// <strong>WARNING:</strong>
    /// Use this function carefully, because there can be lots of entities in the database.
    /// </para>
    /// </summary>
    /// <returns>An <see cref="IEnumerable{Booking}"/> collection of all bookings.</returns>
    /// <exception cref="RepositoryException">Thrown if a database error occurs.</exception>
    public IEnumerable<Booking> FindAll()
    {
        return _bookingRepository.FindAll();
    }

    /// <summary>
    /// Updates an existing <see cref="Booking"/> based on its ID.
    /// </summary>
    /// <param name="booking">The entity with updated information.</param>
    /// <exception cref="RepositoryException">Thrown if a database error occurs.</exception>
    /// <exception cref="ValidationException">Thrown if the given booking object is invalid.</exception>
    public void Update(Booking booking)
    {
        BookingValidator.validate(booking);
        _bookingRepository.Update(booking);
    }

    /// <summary>
    /// Deletes a <see cref="Booking"/> based on its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to remove.</param>
    /// <exception cref="RepositoryException">Thrown if a database error occurs.</exception>
    public void Delete(int id)
    {
        _bookingRepository.Delete(id);
    }
}
