# Flight Booking System - Design and Programming Environments (C#)

This repository contains the C# implementation of the "Flight Booking System" project, developed for the Design and Programming Environments (MPP) course.

## 🏗️ Solution Architecture
The project is structured following Layered Architecture principles to ensure separation of concerns:
- **Domain**: Core entities (`Flight`, `Booking`, `Employee`) and the generic `IEntity` interface.
- **Repository**: Persistence interfaces and concrete implementations using **ADO.NET**.
- **Service**: Business Logic layer mediating between Repositories and the UI.
- **Validators**: Dedicated classes for data validation and integrity rules.
- **Utils**: Utilities for database connection management and cryptography (SHA256).
- **GUI (WinForms)**: Desktop interface for employee interaction.

## 🛠️ Technologies Used
- **Language**: C# (.NET 8.0)
- **Framework**: Windows Forms (WinForms)
- **Database**: MySQL via ADO.NET (`MySql.Data`)
- **Logging**: log4net (configured for both Console and File output)
- **Configuration**: Standard XML `App.config` for connection strings and logging settings.

## 📈 Assignment Status & Implemented Features

### Week 3: Domain Model and Interfaces
- Implemented core domain classes: `Flight`, `Booking`, `Employee`.
- Defined Repository interfaces (`IFlightRepository`, `IBookingRepository`, `IEmployeeRepository`), establishing clear contracts for CRUD operations.

### Week 4: ADO.NET Persistence and Logging
- Implemented repository interfaces using relational databases (the `AdoNetRepository` classes).
- Configured MySQL database connectivity. Authentication credentials are dynamically extracted from `App.config`, ensuring no sensitive data is hardcoded.
- Integrated the **log4net** logging framework.
- Logging is configured to record events in `logs/app.log`.
- Comprehensive logging of application flow (method entries/exits) and error handling.

### Week 5: Service Layer, Validation, and WinForms GUI
- **Service Layer**: Implemented `FlightService`, `EmployeeService`, and `BookingService` to handle business logic, decoupling the UI from the persistence layer.
- **Validation & Security**:
    - Added validation logic for entities (`FlightValidator`, `EmployeeValidator`, `BookingValidator`).
    - Implemented **SHA-256 hashing** for secure employee authentication via the `Encryption` utility.
- **Desktop GUI**:
    - Designed the user interface using **Windows Forms**.
    - Implemented the **Login View** for employee authentication.
    - Developed the **Main View** featuring real-time flight search, filtering by destination/date, and the booking process for tourists.

## ⚙️ Setup and Run Instructions

1. **Project Configuration**:
    - Ensure you are running on **Windows** with the **.NET 8.0 SDK** installed.
    - The project is configured as a `WinExe` in the `.csproj`.

2. **Database Setup**:
    - Ensure you have an active MySQL server running.
    - Execute the provided SQL scripts to create the required tables (`flights`, `bookings`, `employees`).

3. **Configuration File**:
    - Update `FlightBookingSystem/App.config` with your local database credentials:
   ```xml
   <connectionStrings>
       <add name="FlightBookingDB" 
            connectionString="Database=your_db_name;Data Source=localhost;User id=your_user;Password=your_password;" />
   </connectionStrings>
