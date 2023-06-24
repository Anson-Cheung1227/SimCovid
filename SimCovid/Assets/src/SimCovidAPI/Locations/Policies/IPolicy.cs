namespace SimCovidAPI.Locations.Policies
{
    public interface IPolicy
    {
        public IPolicyType PolicyType { get; }
        public bool Active { get; }
        public void SetActive(bool active);
    }
}