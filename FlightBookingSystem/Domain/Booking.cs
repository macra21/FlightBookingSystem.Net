namespace FlightBookingSystem.Domain;

/// <summary>
/// Represents the booking entity within the application domain.
/// </summary>
public class Booking:IEntity<int>
{
    public int Id { get; set; }
    public Flight Flight { get; set; }
    public int NumberOfSeats { get; set; }
    public List<string> TouristNames { get; set; }

    /// <summary>
    /// Constructs a fully initialized Booking object.
    /// </summary>
    /// <param name="id">the unique identifier of the booking.</param>
    /// <param name="flight">the booked flight.</param>
    /// <param name="numberOfSeats">the number of seats booked.</param>
    /// <param name="touristNames">the names of the tourists on the booking.</param>
    public Booking(int id, Flight flight, int numberOfSeats, List<string> touristNames)
    {
        this.Id = id;
        this.Flight = flight;
        this.NumberOfSeats = numberOfSeats;
        this.TouristNames = touristNames;
    }
}