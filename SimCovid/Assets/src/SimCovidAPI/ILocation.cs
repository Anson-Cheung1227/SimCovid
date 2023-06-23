using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimCovidAPI.Infection;

namespace SimCovidAPI
{
    public interface ILocation
    {
        public string Name { get; }
        public long Population { get; }
        public float LocalSpreadRate { get; }
        public long DailyIncomingPeople { get; }
        public ISpreadableManager InfectionManager { get; }
    }
}
