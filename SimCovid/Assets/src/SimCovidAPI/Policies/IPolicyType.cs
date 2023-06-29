namespace SimCovidAPI.Locations.Policies
{
    /// <summary>
    /// Represents a policy
    /// </summary>
    public interface IPolicyType
    {
        /// <summary>
        /// Name of the policy, it MUST NOT conflict with any other policy
        /// </summary>
        public string PolicyName { get; }
        /// <summary>
        /// Description of the policy
        /// </summary>
        public string PolicyDescription { get; }
    }
}