using SimCovidAPI;
using SimCovidAPI.Infection;

namespace SimCovid.Core.Infection.InfectionDataHandlers
{
    public class RecoveredSpreadableDataHandler<TISpreadableTarget> : SpreadableDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public RecoveredSpreadableDataHandler(long limit) : base(limit)
        {
        }
    }
}
