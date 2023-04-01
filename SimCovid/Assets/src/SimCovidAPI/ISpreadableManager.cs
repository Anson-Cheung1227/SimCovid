using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCovidAPI
{
    public interface ISpreadableManager<ISpreadableTarget> where ISpreadableTarget : class, ISpreadable, new()
    {
        public long Limit { get; }
        public IEnumerable<ISpreadableDataHandler<ISpreadableTarget>> GetAll();
        public ISpreadableDataHandler<ISpreadableTarget> GetActive();
        public ISpreadableDataHandler<ISpreadableTarget> GetInHospital();
        public ISpreadableDataHandler<ISpreadableTarget> GetRecovered();
        public ISpreadableDataHandler<ISpreadableTarget> GetDeceased();
        public long GetTotalInfections();
    }
}
