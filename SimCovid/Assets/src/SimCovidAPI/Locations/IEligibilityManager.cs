namespace SimCovidAPI.Locations
{
    public interface IEligibilityManager
    {
        bool GetEligibility(string tag);
    }
}