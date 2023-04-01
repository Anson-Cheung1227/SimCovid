using System.Collections.Generic;
using SimCovidAPI;

namespace Core
{
    public abstract class InfectionDataHandlerBase<TISpreadableTarget> : ISpreadableDataHandler<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        protected List<TISpreadableTarget> _spreadables = new List<TISpreadableTarget>();

        protected InfectionDataHandlerBase(long limit)
        {
            Limit = limit;
        }

        public virtual long Count { get { return _spreadables.Count; } }
        public virtual long Limit { get; }

        public virtual bool AddISpreadable(TISpreadableTarget spreadable)
        {
            if (spreadable.Amount + GetActualInfectionsCount() > Limit) return false;
            _spreadables.Add(spreadable);
            return true;
        }

        public virtual void RemoveISpreadable(TISpreadableTarget spreadable)
        {
            _spreadables.Remove(spreadable);
        }

        public virtual TISpreadableTarget CreateISpreadable()
        {
            return new TISpreadableTarget();
        }

        public virtual IEnumerable<TISpreadableTarget> GetAll()
        {
            return _spreadables;
        }

        public virtual long GetActualInfectionsCount()
        {
            long count = 0;
            foreach (TISpreadableTarget spreadable in _spreadables)
            {
                count += spreadable.Amount;
            }
            return count;
        }

        public virtual TISpreadableTarget FindExistingInstance(TISpreadableTarget instance)
        {
            foreach (TISpreadableTarget spreadable in _spreadables)
            {
                if (spreadable.IsSameValue(instance)) return spreadable;
            }
            return null;
        }
    }
}