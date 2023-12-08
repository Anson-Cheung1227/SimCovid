namespace SimCovidAPI
{
    /// <summary>
    /// Represents the Medium of infection, how the infection occured, through what means (for example, Local, interstate, global)
    /// </summary>
    public interface ISpreadableMediumType
    {
        /// <summary>
        /// Unique identifier name for the Medium of Infection. It MUST NOT conflict with other types.
        /// </summary>
        public string MediumName { get; }
        /// <summary>
        /// Unique identifier integer for the Medium of Infection. It MUST NOT conflict with other types. 
        /// </summary>
        public string MediumTag { get; }
    }
}