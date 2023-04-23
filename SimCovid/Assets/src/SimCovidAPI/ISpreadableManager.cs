using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCovidAPI
{
    public interface ISpreadableManager
    {
        public long Limit { get; }
        public IEnumerable<ISpreadableDataHandler> GetAll();
        public ISpreadableDataHandler GetActive();
        public ISpreadableDataHandler GetInHospital();
        public ISpreadableDataHandler GetRecovered();
        public ISpreadableDataHandler GetDeceased();
        public void UpdateLimit();
        public long GetTotalISpreadableCount();
    }
}
