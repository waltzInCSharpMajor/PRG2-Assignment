using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code;

namespace S10268118_PRG2Assignment
{
    //==========================================================
    // Student Number	: S10270550
    // Student Name	: Ong Yong Sheng
    // Partner Name	: Ryan Wee Wei Yan
    //==========================================================
    public class Airline
    {
        private string _airlineCode;
        public string AirlineCode
        {
            get { return _airlineCode; }
            set { _airlineCode = value; }
        }

        private string _airlineName;
        public string AirlineName
        {
            get { return _airlineName; }
            set { _airlineName = value; }
        }

        private List<Flight> _flights;
        public List<Flight> Flights
        {
            get { return _flights; }
            set { _flights = value; }
        }

        public Airline(string airlineCode, string airlineName)
        {
            AirlineCode = airlineCode;
            AirlineName = airlineName;
            Flights = new List<Flight>();
        }

        public void AddFlight(Flight flight)
        {
            Flights.Add(flight);
        }

        public void RemoveFlight(Flight flight)
        {
            Flights.Remove(flight);
        }

        public override string ToString()
        {
            return $"{AirlineCode} - {AirlineName}";
        }
    }
}
