using System.Security.Cryptography;
using System.Text;

namespace FlightBookingSystem.Utils;

/// <summary>
/// Utility class for encryption operations.
/// </summary>
public class Encryption
{
    /// <summary>
    /// Computes the SHA256 hash of a given string and returns it as a hex string.
    /// </summary>
    /// <param name="text">The plain text to be hashed.</param>
    /// <returns>A hexadecimal string representing the SHA256 hash.</returns>
    public static string SHA256OneWayHash(string text)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        
        StringBuilder builder = new StringBuilder();
        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}