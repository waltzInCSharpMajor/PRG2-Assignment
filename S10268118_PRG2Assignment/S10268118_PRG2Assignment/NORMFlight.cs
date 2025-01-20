using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code;

namespace S10268118_PRG2Assignment
{
    public class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination,
                         DateTime expectedTime, string status = "Scheduled")
            : base(flightNumber, origin, destination, expectedTime, status)
        {
        }

        public override double CalculateFees()
        {
            double fees = 0;

            // Base boarding gate fee
            fees += 300; // Boarding Gate Base Fee

            // Flight fees based on direction
            if (Destination.Contains("SIN"))
            {
                fees += 500; // Arriving Flight Fee
            }
            else if (Origin.Contains("SIN"))
            {
                fees += 800; // Departing Flight Fee
            }

            return fees;
        }
    }
}
