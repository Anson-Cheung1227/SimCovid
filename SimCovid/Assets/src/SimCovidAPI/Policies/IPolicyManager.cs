using System.Collections;

namespace SimCovidAPI.Policies
{
    /// <summary>
    /// Manager for a group of IPolicy
    /// </summary>
    public interface IPolicyManager
    {
        /// <summary>
        /// Returns all policies managed by IPolicyManager
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAll();
        /// <summary>
        /// Get a policy via its name
        /// </summary>
        /// <param name="tag">Tag of policy</param>
        /// <returns></returns>
        public IPolicy GetPolicy(string tag);
    }
}