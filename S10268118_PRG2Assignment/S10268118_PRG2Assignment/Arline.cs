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

        private List<Flight> _flights = new();
        public IReadOnlyList<Flight> Flights => _flights.AsReadOnly();

        public Airline(string code, string name)
        {
            AirlineCode = code;
            AirlineName = name;
        }

        public void AddFlight(Flight flight) => _flights.Add(flight);

        public void RemoveFlight(Flight flight) => _flights.Remove(flight);

        public override string ToString() => $"{AirlineCode} - {AirlineName}";
    }
}
