using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCovidAPI
{
    public interface ISpreadableDataHandler<ISpreadableTarget> where ISpreadableTarget : class , ISpreadable, new()
    {
        public long Count { get; }
        public long Limit { get; }
        public IEnumerable<ISpreadableTarget> GetAll();
        public ISpreadableTarget FindExistingInstance(ISpreadableTarget instance);
        public bool AddISpreadable(ISpreadableTarget spreadable);
        public void RemoveISpreadable(ISpreadableTarget spreadable);

        public ISpreadableTarget CreateISpreadable();
        public long GetActualInfectionsCount();
    }
}
