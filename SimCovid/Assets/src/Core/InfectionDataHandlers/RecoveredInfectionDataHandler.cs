using InfectionModule;
using SimCovidAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RecoveredInfectionDataHandler<TISpreadableTarget> : InfectionDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public RecoveredInfectionDataHandler(long limit) : base(limit)
        {
        }
    }
}
