namespace SimCovidAPI.Infection
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
            public string StatusTag { get { return "Core_Active"; } }
        }
        public class InHospitalInfection : ISpreadableStatus
        {
            public string StatusName { get { return "InHospital"; } }

            public string StatusTag { get { return "Core_InHospital"; } }
        }
        public class RecoveredInfection : ISpreadableStatus
        {
            public string StatusName { get { return "Recovered"; } }
            public string StatusTag { get { return "Core_Recovered"; } }
        }
        public class DeceasedInfection : ISpreadableStatus
        {
            public string StatusName { get { return "Deceased"; } }
            public string StatusTag { get { return "Core_Deceased"; } }
        }
    }
}