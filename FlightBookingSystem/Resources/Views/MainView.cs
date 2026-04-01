using FlightBookingSystem.Domain;
using FlightBookingSystem.Service;

namespace FlightBookingSystem.Resources.Views;

public partial class MainView : Form
{
    private Employee _loggedEmployee;
    private FlightService _flightService;
    private BookingService _bookingService;

    public MainView()
    {
        InitializeComponent();
    }

    public void Setup(Employee emp, FlightService fs, BookingService bs)
    {
        _loggedEmployee = emp;
        _flightService = fs;
        _bookingService = bs;

        this.SearchButton.Click += SearchButton_Click;
        this.ClearButton.Click += ClearButton_Click;
        this.LogoutButton.Click += LogoutButton_Click;
        this.BookFlightButton.Click += BookFlightButton_Click;
        this.ModifyToursitNamesButton.Click += ModifyToursitNamesButton_Click;

        LoadData();
    }

    private void LoadData()
    {
        try
        {
            var flights = _flightService.FindAll().Where(f => f.AvailableSeats > 0).ToList();
            FlightsTable.DataSource = flights;

            var bookings = _bookingService.FindAll().ToList();
            BookingsTable.DataSource = bookings;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void SearchButton_Click(object sender, EventArgs e)
    {
        string dest = SearchDestionationField.Text.Trim();
        DateTime date = dateTimePicker1.Value.Date;

        if (string.IsNullOrWhiteSpace(dest) || dest == "Destination...") return;

        var results = _flightService.FindByDestinationAndDepartureDate(dest, date).ToList();
        SearchTable.DataSource = results;
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        SearchDestionationField.Text = "Destination...";
        dateTimePicker1.Value = DateTime.Now;
        SearchTable.DataSource = null;
    }

    private void BookFlightButton_Click(object sender, EventArgs e)
    {
        Flight selectedFlight = null;
        if (SearchTable.SelectedRows.Count > 0)
            selectedFlight = (Flight)SearchTable.SelectedRows[0].DataBoundItem;
        else if (FlightsTable.SelectedRows.Count > 0)
            selectedFlight = (Flight)FlightsTable.SelectedRows[0].DataBoundItem;

        if (selectedFlight == null)
        {
            MessageBox.Show("Please select a flight!");
            return;
        }

        string input = ShowInputDialog($"Dest: {selectedFlight.ArrivalAirport}", "Book Flight", "");
        if (string.IsNullOrWhiteSpace(input)) return;

        List<string> Names = input.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList();
        if (Names.Count == 0 || Names.Count > selectedFlight.AvailableSeats)
        {
            MessageBox.Show("Invalid number of tourists!");
            return;
        }

        try
        {
            _bookingService.Save(new Booking(selectedFlight, Names.Count, Names));
            selectedFlight.AvailableSeats -= Names.Count;
            _flightService.Update(selectedFlight);
            LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void ModifyToursitNamesButton_Click(object sender, EventArgs e)
    {
        if (BookingsTable.SelectedRows.Count == 0) return;
        Booking sel = (Booking)BookingsTable.SelectedRows[0].DataBoundItem;
        
        string input = ShowInputDialog("New names:", "Modify", string.Join(", ", sel.TouristNames));
        if (string.IsNullOrWhiteSpace(input)) return;

        List<string> Names = input.Split(',').Select(s => s.Trim()).ToList();
        if (Names.Count != sel.NumberOfSeats) return;

        try
        {
            sel.TouristNames = Names;
            _bookingService.Update(sel);
            LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void LogoutButton_Click(object sender, EventArgs e)
    {
        Application.OpenForms["LoginView"]?.Show();
        this.Close();
    }
    
    

    private string ShowInputDialog(string head, string cap, string def)
    {
        Form p = new Form() { Width = 400, Height = 200, Text = cap, StartPosition = FormStartPosition.CenterScreen };
        TextBox tb = new TextBox() { Left = 20, Top = 50, Width = 340, Multiline = true, Height = 60, Text = def };
        Button b = new Button() { Text = "OK", Left = 260, Top = 120, DialogResult = DialogResult.OK };
        p.Controls.Add(tb); p.Controls.Add(b);
        return p.ShowDialog() == DialogResult.OK ? tb.Text : null;
    }
}
