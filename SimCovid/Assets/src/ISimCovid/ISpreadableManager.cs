using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISimCovid
{
    public interface ISpreadableManager
    {
        public ISpreadableDataHandler GetAll();
        public ISpreadableDataHandler GetActive();
        public ISpreadableDataHandler GetInHospital();
        public ISpreadableDataHandler GetRecovered();
        public ISpreadableDataHandler GetDeceased();
    }
}
