using System.Collections.Generic;

namespace SimCovidAPI.Infection
{
    public abstract class InfectionManagerBase : ISpreadableManager
    {
        protected Dictionary<int, ISpreadableDataHandler> SpreadableDataHandlerDictionary { get; set; }

        protected InfectionManagerBase(long limit, ISpreadableDataHandler active,
            ISpreadableDataHandler deceased, ISpreadableDataHandler inHospital,
            ISpreadableDataHandler recovered)
        {
            Limit = limit;
            SpreadableDataHandlerDictionary = new Dictionary<int, ISpreadableDataHandler>();
            SpreadableDataHandlerDictionary.Add(InfectionStatus.Active.StatusValue, active);
            SpreadableDataHandlerDictionary.Add(InfectionStatus.InHospital.StatusValue, inHospital);
            SpreadableDataHandlerDictionary.Add(InfectionStatus.Deceased.StatusValue, deceased);
            SpreadableDataHandlerDictionary.Add(InfectionStatus.Recovered.StatusValue, recovered);
        }

        public long Limit { get; set; }

        public virtual IEnumerable<ISpreadableDataHandler> GetAll()
        {
            return SpreadableDataHandlerDictionary.Values;
        }

        public ISpreadableDataHandler GetISpreadableDataHandler(int key)
        {
            return SpreadableDataHandlerDictionary[key];
        }

        public virtual void UpdateLimit()
        {
            long limit = Limit - GetISpreadableDataHandler(InfectionStatus.Active).GetActualISpreadablesCount() -
                         GetISpreadableDataHandler(InfectionStatus.Deceased).GetActualISpreadablesCount() -
                         GetISpreadableDataHandler(InfectionStatus.InHospital).GetActualISpreadablesCount() -
                         GetISpreadableDataHandler(InfectionStatus.Recovered).GetActualISpreadablesCount();
            GetISpreadableDataHandler(InfectionStatus.Active)
                .SetLimit(limit + GetISpreadableDataHandler(InfectionStatus.Active).GetActualISpreadablesCount());
            GetISpreadableDataHandler(InfectionStatus.Deceased).SetLimit(limit +
                                                                         GetISpreadableDataHandler(InfectionStatus
                                                                             .Deceased).GetActualISpreadablesCount());
            GetISpreadableDataHandler(InfectionStatus.InHospital).SetLimit(limit +
                                                                           GetISpreadableDataHandler(InfectionStatus
                                                                                   .InHospital)
                                                                               .GetActualISpreadablesCount());
            GetISpreadableDataHandler(InfectionStatus.Recovered).SetLimit(limit +
                                                                          GetISpreadableDataHandler(InfectionStatus
                                                                              .Recovered).GetActualISpreadablesCount());
        }

        public virtual long GetTotalISpreadableCount()
        {
            long total = GetISpreadableDataHandler(InfectionStatus.Active).GetActualISpreadablesCount() +
                         GetISpreadableDataHandler(InfectionStatus.Deceased).GetActualISpreadablesCount() +
                         GetISpreadableDataHandler(InfectionStatus.InHospital).GetActualISpreadablesCount() +
                         GetISpreadableDataHandler(InfectionStatus.Recovered).GetActualISpreadablesCount();
            return total;
        }
    }
}