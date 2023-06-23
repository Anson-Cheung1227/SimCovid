namespace SimCovidAPI
{
    /// <summary>
    /// Represents the Medium of infection, how the infection occured, through what means (for example, Local, interstate, global)
    /// </summary>
    public interface ISpreadableMediumType
    {
        public string MediumName { get; }
        public int MediumValue { get; }
    }
}