namespace SimCovidAPI.Policies
{
    /// <summary>
    /// Represents a implementable policy of a state
    /// </summary>
    public interface IPolicy
    {
        /// <summary>
        /// The type of policy
        /// </summary>
        public IPolicyType PolicyType { get; }
        /// <summary>
        /// Whether the policy is active
        /// </summary>
        public bool Active { get; }
        public void SetActive(bool active);
    }
}