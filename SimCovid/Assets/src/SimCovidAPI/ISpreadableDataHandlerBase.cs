using System.Collections.Generic;
using SimCovidAPI;

namespace Core
{
    public abstract class ISpreadableDataHandlerBase<TISpreadableTarget> : ISpreadableDataHandler<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable
    {
        protected List<TISpreadableTarget> Spreadables = new List<TISpreadableTarget>();

        protected ISpreadableDataHandlerBase(long limit)
        {
            Limit = limit;
        }

        public virtual long Count { get { return Spreadables.Count; } }
        public virtual long Limit { get; private set; }

        public virtual bool AddISpreadable(TISpreadableTarget spreadable)
        {
            if (spreadable.Amount + GetActualInfectionsCount() > Limit) return false;
            Spreadables.Add(spreadable);
            return true;
        }

        public virtual void RemoveISpreadable(TISpreadableTarget spreadable)
        {
            Spreadables.Remove(spreadable);
        }

        public virtual bool AddAmountToISpreadable(TISpreadableTarget spreadableTarget, long amount)
        {
            bool overloaded = amount + GetActualInfectionsCount() < Limit;
            if (!overloaded) spreadableTarget.AddToInfection(amount);
            bool success = !overloaded;
            return success;
        }
        public virtual bool SetLimit(long limit)
        {
            bool overloaded = limit < GetActualInfectionsCount();
            if (!overloaded) Limit = limit;
            bool success = !overloaded;
            return success;
        }

        public virtual IEnumerable<TISpreadableTarget> GetAll()
        {
            return Spreadables;
        }

        public virtual long GetActualInfectionsCount()
        {
            long count = 0;
            foreach (TISpreadableTarget spreadable in Spreadables)
            {
                count += spreadable.Amount;
            }
            return count;
        }

        public virtual TISpreadableTarget FindExistingInstance(TISpreadableTarget instance)
        {
            foreach (TISpreadableTarget spreadable in Spreadables)
            {
                if (spreadable.IsSameValue(instance)) return spreadable;
            }
            return null;
        }
    }
}