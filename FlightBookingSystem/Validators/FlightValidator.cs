using System.Text;
using FlightBookingSystem.Domain;
using FlightBookingSystem.Exceptions;

namespace FlightBookingSystem.Validators;

/// <summary>
/// Validator class for the <see cref="Flight"/> entity.
/// Ensures that a flight has a valid ID, non-negative available seats, 
/// and a logical timeline (arrival after departure).
/// </summary>
public class FlightValidator
{
    /// <summary>
    /// Validates the attributes of a <see cref="Flight"/> object.
    /// </summary>
    /// <param name="flight">The flight instance to be validated.</param>
    /// <exception cref="ValidationException">
    /// Thrown when one or more validation rules are violated:
    /// <list type="bullet">
    /// <item><description>The Flight ID is negative.</description></item>
    /// <item><description>Available seats are less than 0.</description></item>
    /// <item><description>Arrival time is earlier than departure time.</description></item>
    /// </list>
    /// </exception>
    public static void validate(Flight flight)
    {
        StringBuilder errors = new StringBuilder();

        if (flight.Id < 0)
        {
            errors.AppendLine("Flight ID should be greater than or equal to 0.");
        }
        if (flight.AvailableSeats < 0)
        {
            errors.AppendLine("Available seats should be greater than or equal to 0.");
        }
        if (flight.ArrivalTime < flight.DepartureTime)
        {
            errors.AppendLine("Arrival time should be greater than or equal to departure time.");
        }
        if (errors.Length > 0)
        {
            throw new ValidationException(errors.ToString());
        }
    }
}