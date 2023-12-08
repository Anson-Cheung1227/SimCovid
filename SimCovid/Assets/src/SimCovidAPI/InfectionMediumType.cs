namespace SimCovidAPI
{
    /// <summary>
    /// Class for built-in ISpreadableMediumTypes.
    /// </summary>
    public class InfectionMediumType
    {
        //Zero and Negative Numbers are reserved for Error uses.
        public static readonly LocalMediumType Local = new LocalMediumType();
        public static readonly InterstateMediumType Interstate = new InterstateMediumType();
        public static readonly GlobalMediumType Global = new GlobalMediumType();
        /// <summary>
        /// Local infection. The infection occured locally.
        /// </summary>
        public class LocalMediumType : ISpreadableMediumType
        {
            public string MediumName { get { return "Local"; } }
            public string MediumTag { get { return "Core_Local"; } }
        }
        /// <summary>
        /// Interstate infection. The infection occured through traveling across states.
        /// </summary>
        public class InterstateMediumType : ISpreadableMediumType
        {
            public string MediumName { get { return "Interstate"; } }
            public string MediumTag { get { return "Core_Interstate"; } }
        }
        /// <summary>
        /// Global infection. The infection occured through traveling across countries. 
        /// </summary>
        public class GlobalMediumType : ISpreadableMediumType
        {
            public string MediumName { get { return "Global"; } }
            public string MediumTag { get { return "Core_Global"; } }
        }
    }
}