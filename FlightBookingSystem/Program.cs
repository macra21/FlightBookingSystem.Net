using System;
using System.Collections.Generic;
using FlightBookingSystem.Domain;
using FlightBookingSystem.Repository;
using FlightBookingSystem.Repository.AdoNet;
using log4net;

namespace FlightBookingSystem;

class Program
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

    static void Main(string[] args)
    {
        log4net.Config.XmlConfigurator.Configure();

        logger.Info("Starting application...");

        try
        {
            IEmployeeRepository employeeRepository = new EmployeeAdoNetRepository();

            Employee employee = new Employee(0, "Rata", "ParolaSecreta");
            employeeRepository.Save(employee);

            Employee foundEmployee = employeeRepository.FindOne(employee.Id);

            IEnumerable<Employee> allEmployees = employeeRepository.FindAll();
        }
        catch (Exception ex)
        {
            logger.Fatal("Aplicația s-a oprit din cauza unei erori critice.", ex);
        }

        Console.ReadKey();
    }
}