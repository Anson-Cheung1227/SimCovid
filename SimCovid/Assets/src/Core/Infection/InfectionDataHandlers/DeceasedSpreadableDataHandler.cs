using SimCovidAPI;
using SimCovidAPI.Infection;

namespace SimCovid.Core.Infection.InfectionDataHandlers
{
    public class DeceasedSpreadableDataHandler<TISpreadableTarget> : SpreadableDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public DeceasedSpreadableDataHandler(long limit) : base(limit)
        {
        }
    }
}
