using System.Text;
using FlightBookingSystem.Domain;
using FlightBookingSystem.Exceptions;

namespace FlightBookingSystem.Validators;

/// <summary>
/// Validator class for the <see cref="Booking"/> entity.
/// Ensures that a booking has a valid ID, an assigned flight, a positive number of seats, 
/// and a non-empty list of tourist names.
/// </summary>
public class BookingValidator
{
    /// <summary>
    /// Validates the attributes of a <see cref="Booking"/> object.
    /// </summary>
    /// <param name="booking">The booking instance to be validated.</param>
    /// <exception cref="ValidationException">
    /// Thrown when one or more validation rules are violated:
    /// <list type="bullet">
    /// <item><description>The Booking ID is negative.</description></item>
    /// <item><description>The Flight reference is null.</description></item>
    /// <item><description>The number of seats is less than or equal to 0.</description></item>
    /// <item><description>The list of tourist names is empty.</description></item>
    /// </list>
    /// </exception>
    public static void validate(Booking booking) 
    {
        StringBuilder errors = new StringBuilder();

        if (booking.Id < 0)
        {
            errors.AppendLine("Booking ID should be greater than or equal to 0.");
        }
        if (booking.Flight == null)
        {
            errors.AppendLine("Booking should have an assigned flight.");
        }
        if (booking.NumberOfSeats <= 0)
        {
            errors.AppendLine("Number of seats should be greater than 0.");
        }
        if (booking.TouristNames == null || booking.TouristNames.Count == 0)
        {
            errors.AppendLine("Tourist names should be set.");
        }
        if (errors.Length > 0)
        {
            throw new ValidationException(errors.ToString());
        }
    }
}
