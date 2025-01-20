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

        private Flight? _assignedFlight;
        public Flight? AssignedFlight
        {
            get { return _assignedFlight; }
            set { _assignedFlight = value; }
        }

        public BoardingGate(string gateName, bool supportsDDJB, bool supportsCFFT, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsDDJB = supportsDDJB;
            SupportsCFFT = supportsCFFT;
            SupportsLWTT = supportsLWTT;
            AssignedFlight = null;
        }

        public bool CanSupportRequest(string? specialRequestCode)
        {
            if (string.IsNullOrEmpty(specialRequestCode)) return true;

            return specialRequestCode switch
            {
                "DDJB" => SupportsDDJB,
                "CFFT" => SupportsCFFT,
                "LWTT" => SupportsLWTT,
                _ => false
            };
        }

        public override string ToString()
        {
            return $"Gate {GateName} [DDJB:{SupportsDDJB} CFFT:{SupportsCFFT} LWTT:{SupportsLWTT}]" +
                   $"\nAssigned Flight: {(AssignedFlight != null ? AssignedFlight.FlightNumber : "None")}";
        }
    }
}
