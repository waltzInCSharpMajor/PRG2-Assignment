using System;
using System.Collections.Generic;
using System.Globalization;
using S10268118_PRG2Assignment;

namespace code
{
    class Program
    {
        private static Dictionary<string, Airline> airlines = new();
        private static Dictionary<string, BoardingGate> boardingGates = new();
        private static Dictionary<string, Flight> flights = new();

        static void Main(string[] args)
        {
            Console.WriteLine("Loading Airlines...");
            LoadAirlines();
            Console.WriteLine($"{airlines.Count} Airlines Loaded!\n");

            Console.WriteLine("Loading Boarding Gates...");
            LoadBoardingGates();
            Console.WriteLine($"{boardingGates.Count} Boarding Gates Loaded!\n");

            Console.WriteLine("Loading Flights...");
            LoadFlights();
            Console.WriteLine($"{flights.Count} Flights Loaded!\n");

            while (true)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListAllFlights();
                        break;
                    case "2":
                        ListBoardingGates();
                        break;
                    case "3":
                        AssignBoardingGate();
                        break;
                    case "4":
                        CreateFlight();
                        break;
                    case "5":
                        DisplayAirlineFlights();
                        break;
                    case "6":
                        ModifyFlightDetails();
                        break;
                    case "7":
                        DisplayFlightSchedule();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option! Please try again.");
                        break;
                }
            }
        }

        static void LoadAirlines()
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

        static void LoadBoardingGates()
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

        static void LoadFlights()
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
                        // Create appropriate flight type based on special request code
                        Flight flight;
                        string specialRequestCode = parts.Length > 4 ? parts[4].Trim() : "";

                        switch (specialRequestCode)
                        {
                            case "CFFT":
                                flight = new CFFTFlight(
                                    parts[0],
                                    parts[1],
                                    parts[2],
                                    DateTime.ParseExact(
                                        parts[3],
                                        "dd/MM/yyyy HH:mm",
                                        CultureInfo.InvariantCulture
                                    ),
                                    "Scheduled"
                                );
                                break;
                            case "LWTT":
                                flight = new LWTTFlight(
                                    parts[0],
                                    parts[1],
                                    parts[2],
                                    DateTime.ParseExact(
                                        parts[3],
                                        "dd/MM/yyyy HH:mm",
                                        CultureInfo.InvariantCulture
                                    ),
                                    "Scheduled"
                                );
                                break;
                            case "DDJB":
                                flight = new DDJBFlight(
                                    parts[0],
                                    parts[1],
                                    parts[2],
                                    DateTime.ParseExact(
                                        parts[3],
                                        "dd/MM/yyyy HH:mm",
                                        CultureInfo.InvariantCulture
                                    ),
                                    "Scheduled"
                                );
                                break;
                            default:
                                flight = new NORMFlight(
                                    parts[0],
                                    parts[1],
                                    parts[2],
                                    DateTime.ParseExact(
                                        parts[3],
                                        "dd/MM/yyyy HH:mm",
                                        CultureInfo.InvariantCulture
                                    ),
                                    "Scheduled"
                                );
                                break;
                        }

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

        static void DisplayMenu()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Welcome to Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List Boarding Gates");
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Please select your option:");
        }

        // Menu option methods would be implemented here based on the assignment of features to team members
    }
}
