namespace FlightBookingSystem.Exceptions;

/// <summary>
/// Exception class used to signal that a set of data or an entity attribute
/// does not meet the specified validation criteria.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException() : base() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the validation error.</param>
    public ValidationException(string message) : base(message) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified error message 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the validation failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ValidationException(string message, Exception innerException) : base(message, innerException) {}
}