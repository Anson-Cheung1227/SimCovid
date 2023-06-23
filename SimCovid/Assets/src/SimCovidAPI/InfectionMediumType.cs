namespace SimCovidAPI
{
    public class InfectionMediumType
    {
        //Zero and Negative Numbers are reserved for Error uses.
        public static readonly LocalMediumType Local = new LocalMediumType();
        public static readonly InterstateMediumType Interstate = new InterstateMediumType();
        public static readonly GlobalMediumType Global = new GlobalMediumType();
        public class LocalMediumType : ISpreadableMediumType
        {
            public string MediumName { get { return "Local"; } }
            public int MediumValue { get { return 1; } }
            public static implicit operator string(LocalMediumType localMediumType) => localMediumType.MediumName;
            public static implicit operator int(LocalMediumType localMediumType) => localMediumType.MediumValue;
        }

        public class InterstateMediumType : ISpreadableMediumType
        {
            public string MediumName { get { return "Interstate"; } }
            public int MediumValue { get { return 2; } }
            public static implicit operator string(InterstateMediumType interstateMediumType) => interstateMediumType.MediumName;
            public static implicit operator int(InterstateMediumType interstateMediumType) => interstateMediumType.MediumValue;
        }

        public class GlobalMediumType : ISpreadableMediumType
        {
            public string MediumName { get { return "Global"; } }
            public int MediumValue { get { return 3; } }
            public static implicit operator string(GlobalMediumType globalMediumType) => globalMediumType.MediumName;
            public static implicit operator int(GlobalMediumType globalMediumType) => globalMediumType.MediumValue;
        }
    }
}