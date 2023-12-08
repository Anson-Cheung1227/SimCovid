using SimCovidAPI.Infection;
using SimCovidAPI.Policies;

namespace SimCovidAPI.Locations
{
    public interface ILocation
    {
        public string Name { get; }
        public long Population { get; }
        public float LocalSpreadRate { get; }
        public long DailyIncomingPeople { get; }
        public ISpreadableManager InfectionManager { get; }
        public IPolicyManager PolicyManager { get; }
        public IEligibilityManager EligibilityManager { get; }
    }
}
