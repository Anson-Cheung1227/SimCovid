using ISimCovid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class InfectionManager : ISpreadableManager
    {
        private ISpreadableDataHandler _all;
        private ISpreadableDataHandler _active;
        private ISpreadableDataHandler _deceased;
        private ISpreadableDataHandler _inHospital;
        private ISpreadableDataHandler _recovered;
        public ISpreadableDataHandler GetAll() => _all;
        public ISpreadableDataHandler GetActive() => _active;

        public ISpreadableDataHandler GetDeceased() => _deceased;

        public ISpreadableDataHandler GetInHospital() => _inHospital;

        public ISpreadableDataHandler GetRecovered() => _recovered;
    }
}
