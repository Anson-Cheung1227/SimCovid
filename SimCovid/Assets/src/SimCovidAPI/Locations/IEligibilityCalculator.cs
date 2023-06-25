namespace SimCovidAPI.Locations
{
    /// <summary>
    /// Calculates the eligibility for spread of infection at a location
    /// </summary>
    public interface IEligibilityCalculator
    {
        public ISpreadableMediumType MediumType { get; }
        /// <summary>
        /// Returns the eligibility of a location
        /// </summary>
        /// <returns>Eligibility of a location</returns>
        public bool Calculate();
    }
}