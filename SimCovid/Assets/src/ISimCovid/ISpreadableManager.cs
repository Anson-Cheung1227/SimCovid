using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISimCovid
{
    public interface ISpreadableManager<ISpreadableTarget> where ISpreadableTarget : class, ISpreadable, new()
    {
        public ISpreadableDataHandler<ISpreadableTarget> GetAll();
        public ISpreadableDataHandler<ISpreadableTarget> GetActive();
        public ISpreadableDataHandler<ISpreadableTarget> GetInHospital();
        public ISpreadableDataHandler<ISpreadableTarget> GetRecovered();
        public ISpreadableDataHandler<ISpreadableTarget> GetDeceased();
    }
}
