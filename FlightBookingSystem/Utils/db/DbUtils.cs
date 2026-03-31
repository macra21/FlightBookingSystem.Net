using System.Data;

namespace FlightBookingSystem.Utils;

public class DbUtils
{
    private IDbConnection instance = null;

    private IDbConnection GetNewConnection()
    {
        return ConnectionFactory.GetInstance().CreateConnection();
    }

    public IDbConnection GetConnection()
    {
        if (instance == null || instance.State == ConnectionState.Closed)
        {
            instance = GetNewConnection();
            instance.Open();
        }
        return instance;
    }
}