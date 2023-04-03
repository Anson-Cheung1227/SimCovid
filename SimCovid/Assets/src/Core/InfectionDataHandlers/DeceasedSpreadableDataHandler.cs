using InfectionModule;
using SimCovidAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DeceasedSpreadableDataHandler<TISpreadableTarget> : ISpreadableDataHandlerBase<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public DeceasedSpreadableDataHandler(long limit) : base(limit)
        {
        }
    }
}
