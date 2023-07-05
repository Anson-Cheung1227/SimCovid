using System.Collections.Generic;

namespace SimCovidAPI.Infection
{
    public abstract class InfectionManagerBase : ISpreadableManager
    {
        protected Dictionary<string, ISpreadableDataHandler> SpreadableDataHandlerDictionary { get; set; }

        protected InfectionManagerBase(long limit, ISpreadableDataHandler active,
            ISpreadableDataHandler deceased, ISpreadableDataHandler inHospital,
            ISpreadableDataHandler recovered)
        {
            Limit = limit;
            SpreadableDataHandlerDictionary = new Dictionary<string, ISpreadableDataHandler>();
            SpreadableDataHandlerDictionary.Add(InfectionStatus.Active.StatusTag, active);
            SpreadableDataHandlerDictionary.Add(InfectionStatus.InHospital.StatusTag, inHospital);
            SpreadableDataHandlerDictionary.Add(InfectionStatus.Deceased.StatusTag, deceased);
            SpreadableDataHandlerDictionary.Add(InfectionStatus.Recovered.StatusTag, recovered);
        }

        public long Limit { get; set; }

        public virtual IEnumerable<ISpreadableDataHandler> GetAll()
        {
            return SpreadableDataHandlerDictionary.Values;
        }

        public ISpreadableDataHandler GetISpreadableDataHandler(string tag)
        {
            return SpreadableDataHandlerDictionary[tag];
        }

        public virtual void UpdateLimit()
        {
            long limit =
                Limit - GetISpreadableDataHandler(InfectionStatus.Active.StatusTag).GetActualISpreadablesCount() -
                GetISpreadableDataHandler(InfectionStatus.Deceased.StatusTag).GetActualISpreadablesCount() -
                GetISpreadableDataHandler(InfectionStatus.InHospital.StatusTag).GetActualISpreadablesCount() -
                GetISpreadableDataHandler(InfectionStatus.Recovered.StatusTag).GetActualISpreadablesCount();
            GetISpreadableDataHandler(InfectionStatus.Active.StatusTag)
                .SetLimit(limit + GetISpreadableDataHandler(InfectionStatus.Active.StatusTag)
                    .GetActualISpreadablesCount());
            GetISpreadableDataHandler(InfectionStatus.Deceased.StatusTag).SetLimit(limit +
                                                                         GetISpreadableDataHandler(InfectionStatus
                                                                             .Deceased.StatusTag).GetActualISpreadablesCount());
            GetISpreadableDataHandler(InfectionStatus.InHospital.StatusTag).SetLimit(limit +
                GetISpreadableDataHandler(InfectionStatus
                        .InHospital.StatusTag)
                    .GetActualISpreadablesCount());
            GetISpreadableDataHandler(InfectionStatus.Recovered.StatusTag).SetLimit(limit +
                GetISpreadableDataHandler(InfectionStatus
                    .Recovered.StatusTag).GetActualISpreadablesCount());
        }

        public virtual long GetTotalISpreadableCount()
        {
            long total = GetISpreadableDataHandler(InfectionStatus.Active.StatusTag).GetActualISpreadablesCount() +
                         GetISpreadableDataHandler(InfectionStatus.Deceased.StatusTag).GetActualISpreadablesCount() +
                         GetISpreadableDataHandler(InfectionStatus.InHospital.StatusTag).GetActualISpreadablesCount() +
                         GetISpreadableDataHandler(InfectionStatus.Recovered.StatusTag).GetActualISpreadablesCount();
            return total;
        }
    }
}