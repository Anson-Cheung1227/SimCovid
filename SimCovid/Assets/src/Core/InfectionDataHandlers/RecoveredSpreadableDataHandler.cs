using InfectionModule;
using SimCovidAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RecoveredSpreadableDataHandler<TISpreadableTarget> : ISpreadableDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public RecoveredSpreadableDataHandler(long limit) : base(limit)
        {
        }
    }
}
