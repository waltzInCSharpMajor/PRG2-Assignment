using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public class Terminal
    {
        private string _terminalName;

        public string TerminalName
        {
            get { return _terminalName; }
            set { _terminalName = value; }
        }

        //private Dictionary<string, Airline> _airlines;

        //public Dictionary<string, Airline> Airlines
        //{
        //    get { return _airlines; }
        //    set { _airlines = value; }
        //}

        private Dictionary<string, Flight> _flights;

        public Dictionary<string, Flight> Flights
        {
            get { return _flights; }
            set { _flights = value; }
        }

        //private Dictionary<string, BoardingGate> _boardingGates;

        //public Dictionary<string, BoardingGate> BoardingGates
        //{
        //    get { return _boardingGates; }
        //    set { _boardingGates = value; }
        //}

        private Dictionary<string, double> _gateFees;

        public Dictionary<string, double> GateFees
        {
            get { return _gateFees; }
            set { _gateFees = value; }
        }


    }
}
