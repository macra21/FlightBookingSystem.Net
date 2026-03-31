using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace FlightBookingSystem.Utils;

public class MySqlConnectionFactory : ConnectionFactory
{
    public override IDbConnection CreateConnection()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FlightBookingDB"].ConnectionString;
        return new MySqlConnection(connectionString);
    }
}