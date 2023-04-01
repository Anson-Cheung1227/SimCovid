using InfectionModule;
using SimCovidAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DeceasedInfectionDataHandler<TISpreadableTarget> : ActiveInfectionDataHandler<TISpreadableTarget>
        where TISpreadableTarget : class, ISpreadable, new()
    {
        public DeceasedInfectionDataHandler(long limit) : base(limit)
        {
        }
    }
}
