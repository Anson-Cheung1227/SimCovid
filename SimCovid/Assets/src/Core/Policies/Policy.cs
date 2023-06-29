using SimCovidAPI.Policies;

namespace SimCovid.Core.Policies
{
    public class Policy : PolicyBase
    {
        public Policy(IPolicyType policyType, bool active = false) : base(policyType, active)
        {
        }
    }
}