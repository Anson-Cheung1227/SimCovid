using System.Collections.Generic;
using NUnit.Framework;

namespace SimCovidAPI.Locations
{
    public abstract class EligibilityManagerBase : IEligibilityManager
    {
        protected readonly Dictionary<int,IEligibilityCalculator> EligibilityDictionary;

        public EligibilityManagerBase(IEligibilityCalculator local)
        {
            EligibilityDictionary = new Dictionary<int, IEligibilityCalculator>();
            EligibilityDictionary.Add(local.MediumType.MediumValue, local);
        }
        public virtual bool GetEligibility(int key)
        {
            return EligibilityDictionary[key].Calculate();
        }
    }
}