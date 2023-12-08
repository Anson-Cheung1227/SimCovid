namespace SimCovidAPI.Policies
{
    /// <summary>
    /// Base class for a Policy
    /// </summary>
    public abstract class PolicyBase : IPolicy
    {
        protected readonly IPolicyType p_PolicyType;
        /// <summary>
        /// The type of Policy. Read-only.
        /// </summary>
        public virtual IPolicyType PolicyType { get { return p_PolicyType; }}
        /// <inheritdoc />
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