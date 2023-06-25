namespace SimCovidAPI.Locations
{
    public interface IEligibilityManager
    {
        bool GetEligibility(int key);
    }
}