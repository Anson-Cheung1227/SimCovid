using InfectionModule;
using SimCovidAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ActiveSpreadableDataHandler<TISpreadableTarget> : SpreadableDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public ActiveSpreadableDataHandler(long limit) : base (limit)
        {
        }
    }
}
