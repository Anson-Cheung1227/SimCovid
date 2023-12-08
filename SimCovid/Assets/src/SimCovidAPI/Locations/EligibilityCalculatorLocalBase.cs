using SimCovidAPI.Policies;

namespace SimCovidAPI.Locations
{
    /// <summary>
    /// Base class for calculating whether a location is eligible for Local spread
    /// </summary>
    public abstract class EligibilityCalculatorLocalBase : IEligibilityCalculator
    {
        protected readonly ILocation Location;
        public ISpreadableMediumType MediumType { get; }
        public EligibilityCalculatorLocalBase(ISpreadableMediumType type,ILocation location)
        {
            MediumType = type;
            Location = location;
        }

        /// <inheritdoc />
        public bool Calculate()
        {
            return !Location.PolicyManager.GetPolicy(PolicyDefaultTypes.Lockdown.PolicyTag).Active;
        }
    }
}