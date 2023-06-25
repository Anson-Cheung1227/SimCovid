namespace SimCovidAPI.Locations.Policies
{
    /// <summary>
    /// Base class for Lockdown policy
    /// </summary>
    public abstract class LockdownPolicyBase : PolicyBase
    {
        public LockdownPolicyBase(bool active = false) : base(new PolicyDefaultTypes.LockdownPolicyType(), active)
        {
        }
    }
}