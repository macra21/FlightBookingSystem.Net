using System.Text;
using FlightBookingSystem.Domain;
using FlightBookingSystem.Exceptions;

namespace FlightBookingSystem.Validators;

/// <summary>
/// Validator class for the <see cref="Employee"/> entity.
/// Provides methods to ensure an employee object meets all business and data integrity rules.
/// </summary>
public class EmployeeValidator
{
    /// <summary>
    /// Validates the attributes of an <see cref="Employee"/> object.
    /// Checks for valid ID, non-empty username, and password strength requirements.
    /// </summary>
    /// <param name="employee">The employee instance to be validated.</param>
    /// <exception cref="ValidationException">
    /// Thrown when:
    /// <list type="bullet">
    /// <item><description>The ID is negative.</description></item>
    /// <item><description>The username is empty.</description></item>
    /// <item><description>The password is empty or shorter than 8 characters.</description></item>
    /// </list>
    /// </exception>
    public static void validate(Employee employee)
    {
        StringBuilder errors = new StringBuilder();
        if (employee.Id < 0)
        {
            errors.AppendLine("Employee ID should be greater than or equal to 0.\n");
        }
        if (employee.Username.Length == 0)
        {
            errors.AppendLine("Employee username cannot be empty.\n");
        }
        if (employee.Password.Length == 0)
        {
            errors.AppendLine("Employee password cannot be empty.\n");
        } 
        else if (employee.Password.Length < 8)
        {
            errors.AppendLine("Employee password should be at least 8 characters.\n");
        }

        if (errors.Length > 0)
        {
            throw new ValidationException(errors.ToString());
        }
    }
}