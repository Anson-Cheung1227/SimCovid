using InfectionModule;
using ISimCovid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class InfectionManager : ISpreadableManager<Infection>
    {
        private ISpreadableDataHandler<Infection> _all;
        private ISpreadableDataHandler<Infection> _active;
        private ISpreadableDataHandler<Infection> _deceased;
        private ISpreadableDataHandler<Infection> _inHospital;
        private ISpreadableDataHandler<Infection> _recovered;
        public ISpreadableDataHandler<Infection> GetAll() => _all;
        public ISpreadableDataHandler<Infection> GetActive() => _active;

        public ISpreadableDataHandler<Infection> GetDeceased() => _deceased;

        public ISpreadableDataHandler<Infection> GetInHospital() => _inHospital;

        public ISpreadableDataHandler<Infection> GetRecovered() => _recovered;
        public InfectionManager()
        {
            _active = new ActiveInfectionDataHandler<Infection>();
        }
    }
}
