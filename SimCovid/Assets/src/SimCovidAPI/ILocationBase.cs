using System;
using System.Collections.Generic;
using SimCovidAPI;
using Core;

namespace SimCovidAPI
{
    public abstract class ILocationBase : ILocation
    {
        public string Name { get; }
        public long Population { get; }
        public float LocalSpreadRate { get; }
        public long DailyIncomingPeople { get; }
        public ISpreadableManager InfectionManager { get; }
        protected ILocationBase()
        {
            
        }
    }
}