using System.Collections.Generic;

namespace SimCovidAPI.Infection
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
        public long GetActualISpreadablesCount();
        public ISpreadable CreateISpreadable();
    }
}
