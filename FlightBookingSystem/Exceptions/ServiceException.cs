namespace FlightBookingSystem.Exceptions;

/// <summary>
/// Exception class designed for the Service (Business Logic) layer.
/// Used to signal business Rule violations, validation errors, or to wrap 
/// underlying repository exceptions for the UI layer.
/// </summary>
public class ServiceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceException"/> class.
    /// </summary>
    public ServiceException() : base() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the specific business logic error.</param>
    public ServiceException(string message) : base(message) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceException"/> class with a specified error message 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception (e.g., a RepositoryException).</param>
    public ServiceException(string message, Exception innerException) : base(message, innerException) {}
}