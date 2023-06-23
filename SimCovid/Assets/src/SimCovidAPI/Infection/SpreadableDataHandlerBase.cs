using System.Collections.Generic;

namespace SimCovidAPI.Infection
{
    public abstract class SpreadableDataHandlerBase<TISpreadableTarget> : ISpreadableDataHandler
        where TISpreadableTarget : class, ISpreadable, new()
    {
        protected List<TISpreadableTarget> Spreadables = new List<TISpreadableTarget>();

        protected SpreadableDataHandlerBase(long limit)
        {
            Limit = limit;
        }

        public virtual long Count { get { return Spreadables.Count; } }
        public virtual long Limit { get; private set; }

        public virtual bool AddISpreadable(ISpreadable spreadable)
        {
            TISpreadableTarget target = spreadable as TISpreadableTarget;
            if (spreadable.Amount + GetActualISpreadablesCount() > Limit || target == null) return false;
            Spreadables.Add(target);
            return true;
        }

        public virtual bool RemoveISpreadable(ISpreadable spreadable)
        {
            TISpreadableTarget target = spreadable as TISpreadableTarget;
            if (target == null) return false;
            Spreadables.Remove(target);
            return true;
        }

        public virtual bool AddAmountToISpreadable(ISpreadable spreadableTarget, long amount)
        {
            bool overloaded = amount + GetActualISpreadablesCount() > Limit;
            if (!overloaded) spreadableTarget.AddToInfection(amount);
            bool success = !overloaded;
            return success;
        }
        public virtual bool SetLimit(long limit)
        {
            bool overloaded = limit < GetActualISpreadablesCount();
            if (!overloaded) Limit = limit;
            bool success = !overloaded;
            return success;
        }

        public virtual IEnumerable<ISpreadable> GetAll()
        {
            return Spreadables;
        }

        public virtual long GetActualISpreadablesCount()
        {
            long count = 0;
            foreach (TISpreadableTarget spreadable in Spreadables)
            {
                count += spreadable.Amount;
            }
            return count;
        }

        public virtual ISpreadable FindExistingInstance(ISpreadable instance)
        {
            TISpreadableTarget target = instance as TISpreadableTarget;
            if (target == null) return null;
            foreach (TISpreadableTarget spreadable in Spreadables)
            {
                if (spreadable.IsSameValue(target)) return spreadable;
            }
            return null;
        }

        public ISpreadable CreateISpreadable()
        {
            TISpreadableTarget instance = new TISpreadableTarget();
            return instance;
        }
    }
}