namespace FlightBookingSystem.Domain;

/// <summary>
/// Generic interface for all domain entities that require a unique identifier.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier</typeparam>
public interface IEntity<TId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    TId Id { get; set; }
}