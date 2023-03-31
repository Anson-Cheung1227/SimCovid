using System.Collections.Generic;
using InfectionModule;
using ISimCovid;

namespace Core
{
    public class InfectionManager : ISpreadableManager<Infection>
    {
        private ISpreadableDataHandler<Infection> _all;
        private ActiveInfectionDataHandler<Infection> _active;
        private DeceasedInfectionDataHandler<Infection> _deceased;
        private InHospitalInfectionDataHandler<Infection> _inHospital;
        private RecoveredInfectionDataHandler<Infection> _recovered;

        public IEnumerable<ISpreadableDataHandler<Infection>> GetAll()
        {
            List<ISpreadableDataHandler<Infection>> returnList = new List<ISpreadableDataHandler<Infection>>();
            returnList.Add(_active);
            returnList.Add(_deceased);
            returnList.Add(_inHospital);
            returnList.Add(_recovered);
            return returnList;
        }
        public ISpreadableDataHandler<Infection> GetActive() => _active;

        public ISpreadableDataHandler<Infection> GetDeceased() => _deceased;

        public ISpreadableDataHandler<Infection> GetInHospital() => _inHospital;

        public ISpreadableDataHandler<Infection> GetRecovered() => _recovered;

        public long GetTotalInfections()
        {
            long total = _active.GetActualInfectionsCount() + _deceased.GetActualInfectionsCount() +
                         _inHospital.GetActualInfectionsCount() + _recovered.GetActualInfectionsCount();
            return total;
        }
        public InfectionManager()
        {
            _active = new ActiveInfectionDataHandler<Infection>();
            _deceased = new DeceasedInfectionDataHandler<Infection>();
            _inHospital = new InHospitalInfectionDataHandler<Infection>();
            _recovered = new RecoveredInfectionDataHandler<Infection>();
        }
    }
}
