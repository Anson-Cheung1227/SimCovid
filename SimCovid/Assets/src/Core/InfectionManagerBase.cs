using System.Collections.Generic;
using InfectionModule;
using SimCovidAPI;

namespace Core
{
    public abstract class InfectionManagerBase<TISpreadableTarget> : ISpreadableManager<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        protected virtual ISpreadableDataHandler<TISpreadableTarget> _all { get; set; }
        protected virtual ISpreadableDataHandler<TISpreadableTarget> _active { get; set; }
        protected virtual ISpreadableDataHandler<TISpreadableTarget> _deceased { get; set; }
        protected virtual ISpreadableDataHandler<TISpreadableTarget> _inHospital { get; set; }
        protected virtual ISpreadableDataHandler<TISpreadableTarget> _recovered { get; set; }

        protected InfectionManagerBase(long limit, ISpreadableDataHandler<TISpreadableTarget>active, DeceasedSpreadableDataHandler<TISpreadableTarget> deceased, InHospitalSpreadableDataHandler<TISpreadableTarget> inHospital, RecoveredSpreadableDataHandler<TISpreadableTarget> recovered)
        {
            Limit = limit;
            _active = active;
            _deceased = deceased;
            _inHospital = inHospital;
            _recovered = recovered;
        }

        public long Limit { get; set; }

        public virtual IEnumerable<ISpreadableDataHandler<TISpreadableTarget>> GetAll()
        {
            List<ISpreadableDataHandler<TISpreadableTarget>> returnList = new List<ISpreadableDataHandler<TISpreadableTarget>>();
            returnList.Add(_active);
            returnList.Add(_deceased);
            returnList.Add(_inHospital);
            returnList.Add(_recovered);
            return returnList;
        }

        public virtual ISpreadableDataHandler<TISpreadableTarget> GetActive() => _active;
        public virtual ISpreadableDataHandler<TISpreadableTarget> GetDeceased() => _deceased;
        public virtual ISpreadableDataHandler<TISpreadableTarget> GetInHospital() => _inHospital;
        public virtual ISpreadableDataHandler<TISpreadableTarget> GetRecovered() => _recovered;

        public virtual long GetTotalInfections()
        {
            long total = _active.GetActualInfectionsCount() + _deceased.GetActualInfectionsCount() +
                         _inHospital.GetActualInfectionsCount() + _recovered.GetActualInfectionsCount();
            return total;
        }
    }
}