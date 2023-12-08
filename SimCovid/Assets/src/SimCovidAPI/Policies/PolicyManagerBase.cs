using System.Collections;
using System.Collections.Generic;

namespace SimCovidAPI.Policies
{
    /// <summary>
    /// Default implementation for IPolicyManager
    /// </summary>
    public abstract class PolicyManagerBase : IPolicyManager
    {
        private Dictionary<string, IPolicy> _policies;

        public PolicyManagerBase(IPolicy lockdown)
        {
            _policies = new Dictionary<string, IPolicy>();
            _policies.Add(PolicyDefaultTypes.Lockdown.PolicyTag, lockdown);
        }
        /// <summary>
        /// Returns all the policies managed
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAll()
        {
            return _policies.Values;
        }
        /// <summary>
        /// Get a specific policy via name
        /// </summary>
        /// <param name="name">Name of policy</param>
        /// <returns></returns>
        public IPolicy GetPolicy(string name)
        {
            return _policies[name];
        }
    }
}