namespace SimCovidAPI.Policies
{
    /// <summary>
    /// Represents a policy
    /// </summary>
    public interface IPolicyType
    {
        /// <summary>
        /// Name of the policy
        /// </summary>
        public string PolicyName { get; }
        /// <summary>
        /// Description of the policy
        /// </summary>
        public string PolicyDescription { get; }
        public string PolicyTag { get; }
    }
}