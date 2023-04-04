using System.Collections.Generic;
using InfectionModule;
using SimCovidAPI;

namespace Core
{
    public class InfectionManager : InfectionManagerBase<Infection>
    {
        public InfectionManager(long limit) : base(limit, new ActiveSpreadableDataHandler<Infection>(limit),
            new DeceasedSpreadableDataHandler<Infection>(limit), new InHospitalSpreadableDataHandler<Infection>(limit),
            new RecoveredSpreadableDataHandler<Infection>(limit))
        {
        }
    }
}