﻿using SimCovidAPI.Locations.Policies;

namespace SimCovid.Core.Policies
{
    public class PolicyManager : PolicyManagerBase
    {
        public PolicyManager(IPolicy lockdown) : base(lockdown)
        {
        }
    }
}