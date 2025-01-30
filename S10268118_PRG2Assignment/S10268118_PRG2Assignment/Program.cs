using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Runtime.InteropServices;
using code;
using S10268118_PRG2Assignment;

void DisplayMenu()
{
    Console.WriteLine(
        @"=============================================
Welcome to Changi Airport Terminal 5
=============================================
1. List All Flights
2. List Boarding Gates
3. Assign a Boarding Gate to a Flight
4. Create Flight
5. Display Airline Flights
6. Modify Flight Details
7. Display Flight Schedule
0. Exit

Please select your option:"
    );
}

void LoadAirlines(Dictionary<string, Airline> airlines)
{
    try
    {
        string[] lines = File.ReadAllLines("airlines.csv").Skip(1).ToArray();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            airlines[parts[1]] = new Airline(parts[1], parts[0]);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading airlines: {ex.Message}");
    }
}

void LoadBoardingGates(Dictionary<string, BoardingGate> boardingGates)
{
    try
    {
        string[] lines = File.ReadAllLines("boardinggates.csv").Skip(1).ToArray();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            boardingGates[parts[0]] = new BoardingGate(
                parts[0],
                bool.Parse(parts[1]),
                bool.Parse(parts[2]),
                bool.Parse(parts[3])
            );
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading boarding gates: {ex.Message}");
    }
}

void LoadFlights(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlines)
{
    try
    {
        string[] lines = File.ReadAllLines("flights.csv").Skip(1).ToArray();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            string[] flightParts = parts[0].Split(' ');

            if (airlines.TryGetValue(flightParts[0], out Airline? airline))
            {
                string specialRequestCode = parts.Length > 4 ? parts[4].Trim() : "";
                DateTime timeOnly = DateTime.ParseExact(
                    parts[3],
                    "h:mm tt",
                    CultureInfo.InvariantCulture
                );
                DateTime expectedTime = DateTime.Today.Add(timeOnly.TimeOfDay);

                // Create appropriate flight type based on special request code
                Flight flight = specialRequestCode switch
                {
                    "CFFT" => new CFFTFlight(
                        parts[0],
                        parts[1],
                        parts[2],
                        expectedTime,
                        "Scheduled"
                    ),
                    "LWTT" => new LWTTFlight(
                        parts[0],
                        parts[1],
                        parts[2],
                        expectedTime,
                        "Scheduled"
                    ),
                    "DDJB" => new DDJBFlight(
                        parts[0],
                        parts[1],
                        parts[2],
                        expectedTime,
                        "Scheduled"
                    ),
                    _ => new NORMFlight(parts[0], parts[1], parts[2], expectedTime, "Scheduled"),
                };
                flights[parts[0]] = flight;
                airline.AddFlight(flight);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading flights: {ex.Message}");
    }
}

void ListAllFlights(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlines)
{
    string row_format = "{0, -16}{1, -23}{2, -23}{3, -23}{4}";
    Console.WriteLine(
        row_format,
        "Flight Number",
        "Airline Name",
        "Origin",
        "Destination",
        "Expected Departure/Arrival Time"
    );

    foreach (Flight flight in flights.Values)
    {
        Console.WriteLine(
            row_format,
            flight.FlightNumber,
            airlines[flight.FlightNumber[..2]].Name,
            flight.Origin,
            flight.Destination,
            flight.ExpectedTime
        );
    }
}

void CreateFlight(ref Dictionary<string, Flight> flights)
{
    while (true)
    {
        Console.Write("Enter Flight Number: ");
        string newFlightNumber = Console.ReadLine();

        Console.Write("Enter Origin: ");
        string newFlightOrigin = Console.ReadLine();

        Console.Write("Enter Destination: ");
        string newFlightDestination = Console.ReadLine();

        Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
        string newFlightExpectedTimeString = Console.ReadLine();

        Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string newFlightSpecialRequestCode = Console.ReadLine();

        try
        {
            DateTime newFlightExpectedTime = DateTime.Parse(newFlightExpectedTimeString);
            switch (newFlightSpecialRequestCode)
            {
                case "CFFT":
                    flights.Add(
                        newFlightNumber,
                        new CFFTFlight(
                            newFlightNumber,
                            newFlightOrigin,
                            newFlightDestination,
                            newFlightExpectedTime
                        )
                    );
                    break;
                case "DDJB":
                    flights.Add(
                        newFlightNumber,
                        new DDJBFlight(
                            newFlightNumber,
                            newFlightOrigin,
                            newFlightDestination,
                            newFlightExpectedTime
                        )
                    );
                    break;
                case "LWTT":
                    flights.Add(
                        newFlightNumber,
                        new LWTTFlight(
                            newFlightNumber,
                            newFlightOrigin,
                            newFlightDestination,
                            newFlightExpectedTime
                        )
                    );
                    break;
                case "None":
                    flights.Add(
                        newFlightNumber,
                        new NORMFlight(
                            newFlightNumber,
                            newFlightOrigin,
                            newFlightDestination,
                            newFlightExpectedTime
                        )
                    );
                    break;
                default:
                    throw new FormatException("Special Request Code is invalid.");
            }

            try
            {
                using (StreamWriter sw = new("flights.csv", true))
                {
                    sw.WriteLine(
                        String.Join(
                            ",",
                            newFlightNumber,
                            newFlightOrigin,
                            newFlightDestination,
                            newFlightExpectedTimeString,
                            newFlightSpecialRequestCode == "None" ? "" : newFlightSpecialRequestCode
                        )
                    );
                }
            }
            catch
            {
                flights.Remove(newFlightNumber);
                throw;
            }

            Console.WriteLine($"Flight {newFlightNumber} has been added!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}\nNo flight has been added.");
        }

        Console.WriteLine(
            "Would you like to add another flight? (\"N\" to stop, else will continue)"
        );

        if (Console.ReadLine() == "N")
        {
            break;
        }
    }
}

Dictionary<string, Airline> airlines = [];
Dictionary<string, BoardingGate> boardingGates = [];
Dictionary<string, Flight> flights = [];

Console.WriteLine("Loading Airlines...");
LoadAirlines(airlines);
Console.WriteLine($"{airlines.Count} Airlines Loaded!\n");

Console.WriteLine("Loading Boarding Gates...");
LoadBoardingGates(boardingGates);
Console.WriteLine($"{boardingGates.Count} Boarding Gates Loaded!\n");

Console.WriteLine("Loading Flights...");
LoadFlights(flights, airlines);
Console.WriteLine($"{flights.Count} Flights Loaded!\n");

Console.WriteLine("\n\n\n");

while (true)
{
    DisplayMenu();
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine(
                @"=============================================
List of Flights for Changi Airport Terminal 5
============================================="
            );
            ListAllFlights(flights, airlines);
            break;
        case "2":
            //ListBoardingGates();
            break;
        case "3":
            Console.WriteLine(
                @"=============================================
Assign a Boarding Gate to a Flight
============================================="
            );
            //AssignBoardingGate();
            break;
        case "4":
            CreateFlight(ref flights);
            break;
        case "5":
            //DisplayAirlineFlights();
            break;
        case "6":
            //ModifyFlightDetails();
            break;
        case "7":
            //DisplayFlightSchedule();
            break;
        case "0":
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid option! Please try again.");
            break;
    }

    Console.WriteLine("\n\n\n\n");
}
