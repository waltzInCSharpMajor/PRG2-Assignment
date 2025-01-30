using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268118_PRG2Assignment
{
    //==========================================================
    // Student Number	: S10270550
    // Student Name	: Ong Yong Sheng
    // Partner Name	: Ryan Wee Wei Yan
    //==========================================================
    public class Airline
    {
        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Dictionary<string, Flight> _flights;
        public Dictionary<string, Flight> Flights
        {
            get { return _flights; }
            set { _flights = value; }
        }

        public bool AddFlight(Flight flight)
        {
            return Flights.TryAdd(flight.FlightNumber, flight);
        }

        public double CalculateFees()
        {
            double fees = 0;

            foreach (Flight flight in Flights.Values)
            {
                fees += flight.CalculateFees();
            }

            //Discounts

            if (Flights.Count > 5)
            {
                fees *= 0.97; //For each airline with more than 5 flights arriving/departing, the airline will receive an additional discount
            }

            fees -= 350 * Math.Floor(Flights.Count / 3.0); //For every 3 flights arriving/departing, airlines will receive a discount

            foreach (Flight flight in Flights.Values)
            {
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour >= 21)
                {
                    fees -= 110; //For each flight arriving/departing before 11am or after 9pm
                }

                if (
                    flight.Origin == "Dubai (DXB)"
                    || flight.Origin == "Bangkok (BKK)"
                    || flight.Origin == "Tokyo (NRT)"
                )
                {
                    fees -= 25; //For each flight with the Origin of Dubai (DXB), Bangkok (BKK) or Tokyo (NRT)
                }

                if (flight is NORMFlight)
                {
                    fees -= 50; //For each flight not indicating any Special Request Codes
                }
            }

            return fees;
        }

        public bool RemoveFlight(Flight flight)
        {
            return Flights.Remove(flight.FlightNumber);
        }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }

        public Airline(string airlineCode, string airlineName)
        {
            Code = airlineCode;
            Name = airlineName;
            Flights = [];
        }
    }
}
