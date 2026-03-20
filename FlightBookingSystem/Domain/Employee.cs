namespace FlightBookingSystem.Domain;

public class Employee: IEntity<int>
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public Employee(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
    
    
}