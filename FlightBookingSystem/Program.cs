using FlightBookingSystem.Repository.AdoNet;
using FlightBookingSystem.Service;
using FlightBookingSystem.Resources.Views;
using log4net;

namespace FlightBookingSystem;

static class Program
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

    [STAThread]
    static void Main()
    {
        log4net.Config.XmlConfigurator.Configure();
        logger.Info("Starting WinForms application...");

        ApplicationConfiguration.Initialize();

        try
        {
            var employeeRepo = new EmployeeAdoNetRepository();
            var flightRepo = new FlightAdoNetRepository();
            var bookingRepo = new BookingAdoNetRepository();
            
            var employeeService = new EmployeeService(employeeRepo);
            var flightService = new FlightService(flightRepo);
            var bookingService = new BookingService(bookingRepo);
            
            var loginView = new LoginView();
            loginView.Setup(employeeService, flightService, bookingService);
            
            Application.Run(loginView);
        }
        catch (Exception ex)
        {
            logger.Fatal("A critical error occurred.", ex);
            MessageBox.Show("A critical error occurred. Check logs for details.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}