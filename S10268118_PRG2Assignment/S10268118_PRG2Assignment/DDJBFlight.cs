using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268118_PRG2Assignment
{
    public class DDJBFlight : NORMFlight
    {
        private const double requestFee = 300; // DDJB Code Request Fee

        public DDJBFlight(string flightNumber, string origin, string destination,
                         DateTime expectedTime, string status = "Scheduled")
            : base(flightNumber, origin, destination, expectedTime, status)
        {
        }

        public override double CalculateFees()
        {
            return base.CalculateFees() + requestFee;
        }
    }
}
