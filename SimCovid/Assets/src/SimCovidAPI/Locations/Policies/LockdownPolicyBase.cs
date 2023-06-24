namespace SimCovidAPI.Locations.Policies
{
    public abstract class LockdownPolicyBase : PolicyBase
    {
        public LockdownPolicyBase(bool active = false) : base(new PolicyDefaultTypes.LockdownPolicyType(), active)
        {
        }
    }
}