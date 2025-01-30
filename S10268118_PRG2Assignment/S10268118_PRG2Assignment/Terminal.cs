using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268118_PRG2Assignment
{
    //==========================================================
    // Student Number : S10268118
    // Student Name : Ryan Wee Wei Yan
    // Partner Name : Ong Yong Sheng
    //==========================================================
    public class Terminal
    {
        private string _terminalName;

        public string TerminalName
        {
            get { return _terminalName; }
            set { _terminalName = value; }
        }

        private Dictionary<string, Airline> _airlines;

        public Dictionary<string, Airline> Airlines
        {
            get { return _airlines; }
            set { _airlines = value; }
        }

        private Dictionary<string, Flight> _flights;

        public Dictionary<string, Flight> Flights
        {
            get { return _flights; }
            set { _flights = value; }
        }

        private Dictionary<string, BoardingGate> _boardingGates;

        public Dictionary<string, BoardingGate> BoardingGates
        {
            get { return _boardingGates; }
            set { _boardingGates = value; }
        }

        private Dictionary<string, double> _gateFees;

        public Dictionary<string, double> GateFees
        {
            get { return _gateFees; }
            set { _gateFees = value; }
        }

        public bool AddAirline(Airline airline)
        {
            return Airlines.TryAdd(airline.Code, airline);
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            return BoardingGates.TryAdd(boardingGate.GateName, boardingGate);
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            return Airlines[flight.FlightNumber[..2]];
        }

        ////TODO
        //public void PrintAirlineFees
        //{

        //}

        public override string ToString()
        {
            return TerminalName;
        }
    }
}
