namespace SimCovidAPI
{
    public class InfectionStatus
    {
        public static readonly ActiveInfection Active = new ActiveInfection();
        public static readonly InHospitalInfection InHospital = new InHospitalInfection();
        public static readonly RecoveredInfection Recovered = new RecoveredInfection();
        public static readonly DeceasedInfection Deceased = new DeceasedInfection();
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
            public string StatusName { get { return "Recovered"; } }
            public int StatusValue { get { return 2; } }
        }
        public class DeceasedInfection : ISpreadableStatus
        {
            public string StatusName { get { return "Deceased"; } }
            public int StatusValue { get { return 3; } }
        }
    }
}