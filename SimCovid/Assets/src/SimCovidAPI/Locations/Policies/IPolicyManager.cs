using System.Collections;

namespace SimCovidAPI.Locations.Policies
{
    public interface IPolicyManager
    {
        public IEnumerable GetAll();
        public IPolicy GetPolicy(string name);
    }
}