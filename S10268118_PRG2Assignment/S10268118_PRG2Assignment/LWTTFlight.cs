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
    public class LWTTFlight : Flight
    {
        private const double requestFee = 500; // LWTT Code Request Fee

        public LWTTFlight(
            string flightNumber,
            string origin,
            string destination,
            DateTime expectedTime,
            string status = "Scheduled"
        )
            : base(flightNumber, origin, destination, expectedTime, status) { }

        public override double CalculateFees()
        {
            double fees = 300; //Boarding Gate Base Fee

            if (Destination == "Singapore (SIN)")
            {
                fees += 500; // Arriving Flight Fee
            }
            else if (Origin == "Singapore (SIN)")
            {
                fees += 800; // Departing Flight Fee
            }

            fees += requestFee; //Request Fee

            return fees;
        }
    }
}
