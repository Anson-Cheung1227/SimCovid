using SimCovidAPI;

namespace SimCovid.Core.Infection.InfectionDataHandlers
{
    public class InHospitalSpreadableDataHandler<TISpreadableTarget> : SpreadableDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public InHospitalSpreadableDataHandler(long limit) : base(limit)
        {
        }
    }
}
