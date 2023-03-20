using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ISimCovid
{
    public interface ISpreadableStatus
    {
        public string StatusName { get; }
        public int StatusValue { get; }
    }
    public class InfectionStatus
    {
        public static readonly ActiveInfection ActiveInfectionStatus = new ActiveInfection();
        public static readonly InHospitalInfection InHospitalInfectionStatus = new InHospitalInfection();
        public static readonly RecoveredInfection RecoveredInfectionStatus = new RecoveredInfection();
        public static readonly DeceasedInfection DeceasedInfectionStatus = new DeceasedInfection();
        public class ActiveInfection : ISpreadableStatus
        {
            public string StatusName { get { return "Active"; } }
            public int StatusValue { get { return 0; } }
        }
        public class InHospitalInfection : ISpreadableStatus
        {
            public string StatusName { get { return "InHospital"; } }

            public int StatusValue { get { return 1; } }
        }
        public class RecoveredInfection : ISpreadableStatus
        {
            public string StatusName { get { return "Recovereed"; } }
            public int StatusValue { get { return 2; } }
        }
        public class DeceasedInfection : ISpreadableStatus
        { 
            public string StatusName { get { return "Deceased"; } } 
            public int StatusValue { get { return 3; } }
        }
    }
}
