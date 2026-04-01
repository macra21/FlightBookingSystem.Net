using FlightBookingSystem.Service;

namespace FlightBookingSystem.Resources.Views;

public partial class LoginView : Form
{
    private EmployeeService _employeeService;
    private FlightService _flightService;
    private BookingService _bookingService;
    
    public LoginView()
    {
        InitializeComponent();
    }
    
    public void Setup(EmployeeService es, FlightService fs, BookingService bs)
    {
        _employeeService = es;
        _flightService = fs;
        _bookingService = bs;
        
        this.LoginButton.Click += LoginButton_Click;
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
        try
        {
            var employee = _employeeService.Login(UsernameField.Text, PasswordField.Text);
            if (employee != null)
            {
                var mainView = new MainView();
                mainView.Setup(employee, _flightService, _bookingService);
                mainView.Show();
                this.Hide();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Login failed: " + ex.Message);
        }
    }
}