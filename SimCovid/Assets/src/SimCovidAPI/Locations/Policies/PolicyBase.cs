using UnityEngine;

namespace SimCovidAPI.Locations.Policies
{
    public abstract class PolicyBase : IPolicy
    {
        protected readonly IPolicyType p_PolicyType;
        public virtual IPolicyType PolicyType { get { return p_PolicyType; }}
        public virtual bool Active { get; protected set; }
        public void SetActive(bool active)
        {
            Active = active;
        }

        public PolicyBase(IPolicyType policyType, bool active = false)
        {
            p_PolicyType = policyType;
            Active = active;
        }
    }
}