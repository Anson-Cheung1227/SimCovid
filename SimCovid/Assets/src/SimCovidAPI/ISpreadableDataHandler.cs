using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCovidAPI
{
    public interface ISpreadableDataHandler
    {
        public long Count { get; }
        public long Limit { get; }
        public bool SetLimit(long limit);
        public IEnumerable<ISpreadable> GetAll();
        public ISpreadable FindExistingInstance(ISpreadable instance);
        public bool AddISpreadable(ISpreadable spreadable);
        public bool RemoveISpreadable(ISpreadable spreadable);
        public bool AddAmountToISpreadable(ISpreadable spreadableTarget, long amount);
        public long GetActualInfectionsCount();
        public ISpreadable CreateISpreadable();
    }
}
