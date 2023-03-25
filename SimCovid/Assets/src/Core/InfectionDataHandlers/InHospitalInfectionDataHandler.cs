using InfectionModule;
using ISimCovid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class InHospitalInfectionDataHandler<ISpreadableTarget> : ISpreadableDataHandler<ISpreadableTarget> where ISpreadableTarget : class, ISpreadable, new()
    {
        private List<ISpreadable> _spreadables = new List<ISpreadable>();
        public long Count { get { return _spreadables.Count; } }

        public void AddISpreadable(ISpreadable spreadable)
        {
            _spreadables.Add(spreadable);
        }
        public void RemoveISpreadable(ISpreadable spreadable)
        {
            _spreadables.Remove(spreadable);
        }

        public IEnumerable<ISpreadable> GetAll()
        {
            return _spreadables;
        }
        public long GetActualInfectionsCount()
        {
            long count = 0;
            foreach (ISpreadable spreadable in _spreadables)
            {
                count += spreadable.Amount;
            }
            return count;
        }
        public ISpreadable FindExistingInstance(ISpreadable instance)
        {
            foreach (ISpreadable spreadable in _spreadables)
            {
                if (spreadable.IsSameValue(instance)) return spreadable;
            }
            return null;
        }

        IEnumerable<ISpreadableTarget> ISpreadableDataHandler<ISpreadableTarget>.GetAll()
        {
            throw new NotImplementedException();
        }

        public ISpreadableTarget FindExistingInstance(ISpreadableTarget instance)
        {
            throw new NotImplementedException();
        }

        public void AddISpreadable(ISpreadableTarget spreadable)
        {
            throw new NotImplementedException();
        }

        public void RemoveISpreadable(ISpreadableTarget spreadable)
        {
            throw new NotImplementedException();
        }
    }
}
