namespace FlightBookingSystem.Domain;

/// <summary>
/// Represents the Flight entity within the application domain.
/// </summary>
public class Flight : IEntity<int>
{
    /// <summary>
    /// Gets or sets the unique identifier of the flight.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the departure airport name.
    /// </summary>
    public string DepartureAirport { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the arrival airport name.
    /// </summary>
    public string ArrivalAirport { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the scheduled departure date and time.
    /// </summary>
    public DateTime DepartureTime { get; set; }

    /// <summary>
    /// Gets or sets the scheduled arrival date and time.
    /// </summary>
    public DateTime ArrivalTime { get; set; }

    /// <summary>
    /// Gets or sets the number of seats currently available for booking.
    /// </summary>
    public int AvailableSeats { get; set; }

    /// <summary>
    /// Constructs a fully initialized Flight object.
    /// <para>
    /// This constructor is typically used when retrieving the object from
    /// the database and already has an assigned ID.
    /// </para>
    /// </summary>
    /// <param name="id">The unique identifier of the flight.</param>
    /// <param name="departureAirport">The departure airport name.</param>
    /// <param name="arrivalAirport">The arrival airport name.</param>
    /// <param name="departureTime">The scheduled date and time of departure.</param>
    /// <param name="arrivalTime">The scheduled date and time of arrival.</param>
    /// <param name="availableSeats">The number of seats currently available for booking.</param>
    public Flight(int id, string departureAirport, string arrivalAirport, DateTime departureTime, DateTime arrivalTime, int availableSeats)
    {
        Id = id;
        DepartureAirport = departureAirport;
        ArrivalAirport = arrivalAirport;
        DepartureTime = departureTime;
        ArrivalTime = arrivalTime;
        AvailableSeats = availableSeats;
    }

    /// <summary>
    /// Constructs a Flight object without an ID.
    /// <para>
    /// This constructor is typically used before saving the object to
    /// the database and does not have an assigned ID.
    /// </para>
    /// </summary>
    /// <param name="departureAirport">The departure airport name.</param>
    /// <param name="arrivalAirport">The arrival airport name.</param>
    /// <param name="departureTime">The scheduled date and time of departure.</param>
    /// <param name="arrivalTime">The scheduled date and time of arrival.</param>
    /// <param name="availableSeats">The number of seats currently available for booking.</param>
    public Flight(string departureAirport, string arrivalAirport, DateTime departureTime, DateTime arrivalTime, int availableSeats)
    {
        DepartureAirport = departureAirport;
        ArrivalAirport = arrivalAirport;
        DepartureTime = departureTime;
        ArrivalTime = arrivalTime;
        AvailableSeats = availableSeats;
    }
    
    

    /// <summary>
    /// Returns a string representation of the Flight object.
    /// </summary>
    /// <returns>A string containing flight details.</returns>
    public override string ToString()
    {
        return $"Flight{{id={Id}, departureAirport='{DepartureAirport}', arrivalAirport='{ArrivalAirport}', " +
               $"departureTime={DepartureTime}, arrivalTime={ArrivalTime}, availableSeats={AvailableSeats}}}";
    }
}
