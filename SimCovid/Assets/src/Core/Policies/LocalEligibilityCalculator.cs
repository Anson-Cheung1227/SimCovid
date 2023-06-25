using SimCovidAPI;
using SimCovidAPI.Locations;

namespace SimCovid.Core.Policies
{
    public class LocalEligibilityCalculator : EligibilityCalculatorLocalBase
    {
        public LocalEligibilityCalculator(ILocation location) : base(new InfectionMediumType.LocalMediumType(), location)
        {
        }
    }
}