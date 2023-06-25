using SimCovidAPI.Locations;

namespace SimCovid.Core.Policies
{
    public class EligibilityManager : EligibilityManagerBase
    {
        public EligibilityManager(ILocation location) : base(new LocalEligibilityCalculator(location))
        {
        }
    }
}