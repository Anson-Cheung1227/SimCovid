using InfectionModule;
using ISimCovid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ActiveInfectionDataHandler<ISpreadableTarget> : ISpreadableDataHandler<ISpreadableTarget> where ISpreadableTarget : class, ISpreadable ,new()
    {
        private List<ISpreadableTarget> _spreadables = new List<ISpreadableTarget>();
        public long Count { get { return _spreadables.Count; } }

        public void AddISpreadable(ISpreadableTarget spreadable)
        {
            _spreadables.Add(spreadable);
        }
        public void RemoveISpreadable(ISpreadableTarget spreadable)
        {
            _spreadables.Remove(spreadable);
        }

        public ISpreadableTarget CreateISpreadable()
        {
            return new ISpreadableTarget();
        }

        public IEnumerable<ISpreadableTarget> GetAll()
        {
            return _spreadables;
        }

        public long GetActualInfectionsCount()
        {
            long count = 0;
            foreach (ISpreadableTarget spreadable in _spreadables)
            {
                count += spreadable.Amount;
            }
            return count;
        }

        public ISpreadableTarget FindExistingInstance(ISpreadableTarget instance)
        {
            foreach (ISpreadableTarget spreadable in _spreadables)
            {
                if (spreadable.IsSameValue(instance)) return spreadable;
            }
            return null;
        }
    }
}
