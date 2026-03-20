namespace FlightBookingSystem.Domain;

/// <summary>
/// Represents the Flight entity within the application domain.
/// </summary>
public class Flight: IEntity<int>
{
    public int Id { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    
    /// <summary>
    /// Constructs a fully initialized Flight object.
    /// </summary>
    /// <param name="id">the unique identifier of the flight.</param>
    /// <param name="departureAirport">the departure airport name of the flight.</param>
    /// <param name="arrivalAirport">the arrival airport name of the flight.</param>
    /// <param name="departureTime">the departure date of the flight.</param>
    /// <param name="arrivalTime">the arrival date of the flight.</param>
    public Flight(int id, string departureAirport, string arrivalAirport, DateTime departureTime, DateTime arrivalTime)
    {
        Id = id;
        this.DepartureAirport = departureAirport;
        this.ArrivalAirport = arrivalAirport;
        this.DepartureTime = departureTime;
        this.ArrivalTime = arrivalTime;
    }

}