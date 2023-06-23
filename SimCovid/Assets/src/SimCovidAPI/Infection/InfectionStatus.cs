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
            public static implicit operator string(ActiveInfection activeInfection) => activeInfection.StatusName;
            public static implicit operator int(ActiveInfection activeInfection) => activeInfection.StatusValue;
        }
        public class InHospitalInfection : ISpreadableStatus
        {
            public string StatusName { get { return "InHospital"; } }

            public int StatusValue { get { return 1; } }
            public static implicit operator string(InHospitalInfection activeInfection) => activeInfection.StatusName;
            public static implicit operator int(InHospitalInfection activeInfection) => activeInfection.StatusValue;
        }
        public class RecoveredInfection : ISpreadableStatus
        {
            public string StatusName { get { return "Recovered"; } }
            public int StatusValue { get { return 2; } }
            public static implicit operator string(RecoveredInfection activeInfection) => activeInfection.StatusName;
            public static implicit operator int(RecoveredInfection activeInfection) => activeInfection.StatusValue;
        }
        public class DeceasedInfection : ISpreadableStatus
        {
            public string StatusName { get { return "Deceased"; } }
            public int StatusValue { get { return 3; } }
            public static implicit operator string(DeceasedInfection activeInfection) => activeInfection.StatusName;
            public static implicit operator int(DeceasedInfection activeInfection) => activeInfection.StatusValue;
        }
    }
}