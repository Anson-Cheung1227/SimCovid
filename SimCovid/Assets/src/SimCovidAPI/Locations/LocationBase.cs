using SimCovidAPI.Infection;
using SimCovidAPI.Policies;

namespace SimCovidAPI.Locations
{
    public abstract class LocationBase : ILocation
    {
        public virtual string Name { get; protected set; }
        public virtual long Population { get; set; }
        public virtual float LocalSpreadRate { get; set; }
        public virtual long DailyIncomingPeople { get; set; }
        public ISpreadableManager InfectionManager { get; set; }
        public IPolicyManager PolicyManager { get; set; }
        public IEligibilityManager EligibilityManager { get; set; }
    }
}