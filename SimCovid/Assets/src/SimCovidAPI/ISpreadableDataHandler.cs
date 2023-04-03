using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCovidAPI
{
    public interface ISpreadableDataHandler<TISpreadableTarget> where TISpreadableTarget : class , ISpreadable
    {
        public long Count { get; }
        public long Limit { get; }
        public bool SetLimit(long limit);
        public IEnumerable<TISpreadableTarget> GetAll();
        public TISpreadableTarget FindExistingInstance(TISpreadableTarget instance);
        public bool AddISpreadable(TISpreadableTarget spreadable);
        public void RemoveISpreadable(TISpreadableTarget spreadable);
        public bool AddAmountToISpreadable(TISpreadableTarget spreadableTarget, long amount);
        public long GetActualInfectionsCount();
    }
}
