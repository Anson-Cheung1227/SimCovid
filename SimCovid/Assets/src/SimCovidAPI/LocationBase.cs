﻿using System;
using System.Collections.Generic;
using SimCovidAPI;
using SimCovidAPI.Infection;

namespace SimCovidAPI
{
    public abstract class LocationBase : ILocation
    {
        public string Name { get; }
        public long Population { get; }
        public float LocalSpreadRate { get; }
        public long DailyIncomingPeople { get; }
        public ISpreadableManager InfectionManager { get; }
        protected LocationBase()
        {

        }
    }
}