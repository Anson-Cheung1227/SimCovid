using SimCovid.Core.Infection.InfectionDataHandlers;
using SimCovidAPI;
using SimCovidAPI.Infection;

namespace SimCovid.Core.Infection
{
    public class InfectionManager : InfectionManagerBase
    {
        public InfectionManager(long limit) : base(limit, new ActiveSpreadableDataHandler<Infection>(limit),
            new DeceasedSpreadableDataHandler<Infection>(limit), new InHospitalSpreadableDataHandler<Infection>(limit),
            new RecoveredSpreadableDataHandler<Infection>(limit))
        {
        }
    }
}