using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISimCovid
{
    public interface ISpreadableDataHandler<ISpreadableTarget> where ISpreadableTarget : class , ISpreadable, new()
    {
        public long Count { get; }
        public IEnumerable<ISpreadableTarget> GetAll();
        public ISpreadableTarget FindExistingInstance(ISpreadableTarget instance);
        public void AddISpreadable(ISpreadableTarget spreadable);
        public void RemoveISpreadable(ISpreadableTarget spreadable);

        public ISpreadableTarget CreateISpreadable();
        public long GetActualInfectionsCount();
    }
}
