using InfectionModule;
using SimCovidAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ActiveInfectionDataHandler<TISpreadableTarget> : InfectionDataHandlerBase<TISpreadableTarget> where TISpreadableTarget : class, ISpreadable,new()
    {
        public ActiveInfectionDataHandler(long limit) : base (limit)
        {
        }
    }
}
