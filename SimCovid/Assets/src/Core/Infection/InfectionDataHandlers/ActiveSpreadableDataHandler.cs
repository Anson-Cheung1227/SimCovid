using SimCovidAPI;

namespace SimCovid.Core.Infection.InfectionDataHandlers
{
    public class ActiveSpreadableDataHandler<TISpreadableTarget> : SpreadableDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public ActiveSpreadableDataHandler(long limit) : base (limit)
        {
        }
    }
}
