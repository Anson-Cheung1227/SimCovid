using System.Collections.Generic;
using InfectionModule;
using SimCovidAPI;

namespace Core
{
    public class InfectionManager : ISpreadableManager<Infection>
    {
        private ISpreadableDataHandler<Infection> _all;
        private ActiveInfectionDataHandler<Infection> _active;
        private DeceasedInfectionDataHandler<Infection> _deceased;
        private InHospitalInfectionDataHandler<Infection> _inHospital;
        private RecoveredInfectionDataHandler<Infection> _recovered;
        public long Limit { get; private set; }
        public InfectionManager(long limit)
        {
            Limit = limit;
            _active = new ActiveInfectionDataHandler<Infection>(limit);
            _deceased = new DeceasedInfectionDataHandler<Infection>(limit);
            _inHospital = new InHospitalInfectionDataHandler<Infection>(limit);
            _recovered = new RecoveredInfectionDataHandler<Infection>(limit);
        }

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
    }
}
