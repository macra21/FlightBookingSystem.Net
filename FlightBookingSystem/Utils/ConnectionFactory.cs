using System.Configuration;
using System.Data;

namespace FlightBookingSystem.Utils;

public abstract class ConnectionFactory
{
    private static ConnectionFactory instance = null;

    public static ConnectionFactory GetInstance()
    {
        if (instance == null)
        {
            String factoryClassName = ConfigurationManager.AppSettings["ConnectionFactory"];
            if (string.IsNullOrEmpty(factoryClassName))
            {
                throw new Exception("ConnectionFactory not set in App.condig");
            }
            
            Type factoryType = Type.GetType(factoryClassName);
            if (factoryType == null)
            {
                throw new Exception($"Class {factoryClassName} does not exist has not been found!");
            }
            
            instance = (ConnectionFactory)Activator.CreateInstance(factoryType);
        }
        return instance;
    }
    
    public abstract IDbConnection CreateConnection();
}