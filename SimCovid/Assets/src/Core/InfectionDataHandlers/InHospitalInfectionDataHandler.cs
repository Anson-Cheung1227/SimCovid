using InfectionModule;
using SimCovidAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class InHospitalInfectionDataHandler<TISpreadableTarget> : InfectionDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public InHospitalInfectionDataHandler(long limit) : base(limit)
        {
        }
    }
}
