using code;

namespace S10268118_PRG2Assignment
{
    //==========================================================
    // Student Number	: S10270550
    // Student Name	: Ong Yong Sheng
    // Partner Name	: Ryan Wee Wei Yan
    //==========================================================
    public class BoardingGate
    {
        private string _gateName;
        public string GateName
        {
            get { return _gateName; }
            set { _gateName = value; }
        }

        private bool _supportsDDJB;
        public bool SupportsDDJB
        {
            get { return _supportsDDJB; }
            set { _supportsDDJB = value; }
        }

        private bool _supportsCFFT;
        public bool SupportsCFFT
        {
            get { return _supportsCFFT; }
            set { _supportsCFFT = value; }
        }

        private bool _supportsLWTT;
        public bool SupportsLWTT
        {
            get { return _supportsLWTT; }
            set { _supportsLWTT = value; }
        }

        private Flight? _flight;
        public Flight? Flight
        {
            get { return _flight; }
            set { _flight = value; }
        }

        public BoardingGate(
            string gateName,
            bool supportsDDJB,
            bool supportsCFFT,
            bool supportsLWTT
        )
        {
            GateName = gateName;
            SupportsDDJB = supportsDDJB;
            SupportsCFFT = supportsCFFT;
            SupportsLWTT = supportsLWTT;
            Flight = null;
        }

        public double CalculateFees()
        {
            if (Flight == null)
            {
                return 0;
            }
            else
            {
                return Flight.CalculateFees();
            }
        }

        public override string ToString()
        {
            return $"Gate {GateName} [DDJB:{SupportsDDJB} CFFT:{SupportsCFFT} LWTT:{SupportsLWTT}]"
                + $"\nAssigned Flight: {(Flight != null ? Flight.FlightNumber : "None")}";
        }
    }
}
