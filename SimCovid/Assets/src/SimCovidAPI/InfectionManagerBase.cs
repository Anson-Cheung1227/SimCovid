using System.Collections.Generic;

namespace SimCovidAPI
{
    public abstract class InfectionManagerBase : ISpreadableManager
    {
        protected virtual ISpreadableDataHandler All { get; set; }
        protected virtual ISpreadableDataHandler Active { get; set; }
        protected virtual ISpreadableDataHandler Deceased { get; set; }
        protected virtual ISpreadableDataHandler InHospital { get; set; }
        protected virtual ISpreadableDataHandler Recovered { get; set; }

        protected InfectionManagerBase(long limit, ISpreadableDataHandler active,
            ISpreadableDataHandler deceased, ISpreadableDataHandler inHospital,
            ISpreadableDataHandler recovered)
        {
            Limit = limit;
            Active = active;
            Deceased = deceased;
            InHospital = inHospital;
            Recovered = recovered;
        }

        public long Limit { get; set; }

        public virtual IEnumerable<ISpreadableDataHandler> GetAll()
        {
            List<ISpreadableDataHandler> returnList = new List<ISpreadableDataHandler>();
            returnList.Add(Active);
            returnList.Add(Deceased);
            returnList.Add(InHospital);
            returnList.Add(Recovered);
            return returnList;
        }

        public virtual ISpreadableDataHandler GetActive() => Active;
        public virtual ISpreadableDataHandler GetDeceased() => Deceased;
        public virtual ISpreadableDataHandler GetInHospital() => InHospital;
        public virtual ISpreadableDataHandler GetRecovered() => Recovered;

        public virtual void UpdateLimit()
        {
            long limit = Limit - GetActive().GetActualISpreadablesCount() - GetDeceased().GetActualISpreadablesCount() -
                         GetInHospital().GetActualISpreadablesCount() - GetRecovered().GetActualISpreadablesCount();
            GetActive().SetLimit(limit + GetActive().GetActualISpreadablesCount());
            GetDeceased().SetLimit(limit + GetDeceased().GetActualISpreadablesCount());
            GetInHospital().SetLimit(limit + GetInHospital().GetActualISpreadablesCount());
            GetRecovered().SetLimit(limit + GetRecovered().GetActualISpreadablesCount());
        }

        public virtual long GetTotalISpreadableCount()
        {
            long total = Active.GetActualISpreadablesCount() + Deceased.GetActualISpreadablesCount() +
                         InHospital.GetActualISpreadablesCount() + Recovered.GetActualISpreadablesCount();
            return total;
        }
    }
}