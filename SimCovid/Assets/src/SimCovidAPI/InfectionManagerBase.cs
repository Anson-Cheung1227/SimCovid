using System.Collections.Generic;

namespace SimCovidAPI
{
    public abstract class InfectionManagerBase<TISpreadableTarget> : ISpreadableManager where TISpreadableTarget : class, ISpreadable, new()
    {
        protected virtual ISpreadableDataHandler _all { get; set; }
        protected virtual ISpreadableDataHandler _active { get; set; }
        protected virtual ISpreadableDataHandler _deceased { get; set; }
        protected virtual ISpreadableDataHandler _inHospital { get; set; }
        protected virtual ISpreadableDataHandler _recovered { get; set; }

        protected InfectionManagerBase(long limit, ISpreadableDataHandler active,
            ISpreadableDataHandler deceased, ISpreadableDataHandler inHospital,
            ISpreadableDataHandler recovered)
        {
            Limit = limit;
            _active = active;
            _deceased = deceased;
            _inHospital = inHospital;
            _recovered = recovered;
        }

        public long Limit { get; set; }

        public virtual IEnumerable<ISpreadableDataHandler> GetAll()
        {
            List<ISpreadableDataHandler> returnList = new List<ISpreadableDataHandler>();
            returnList.Add(_active);
            returnList.Add(_deceased);
            returnList.Add(_inHospital);
            returnList.Add(_recovered);
            return returnList;
        }

        public virtual ISpreadableDataHandler GetActive() => _active;
        public virtual ISpreadableDataHandler GetDeceased() => _deceased;
        public virtual ISpreadableDataHandler GetInHospital() => _inHospital;
        public virtual ISpreadableDataHandler GetRecovered() => _recovered;

        public virtual long GetTotalInfections()
        {
            long total = _active.GetActualInfectionsCount() + _deceased.GetActualInfectionsCount() +
                         _inHospital.GetActualInfectionsCount() + _recovered.GetActualInfectionsCount();
            return total;
        }
    }
}