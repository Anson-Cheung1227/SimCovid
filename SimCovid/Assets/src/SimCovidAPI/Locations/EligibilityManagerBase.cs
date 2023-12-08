using System.Collections.Generic;

namespace SimCovidAPI.Locations
{
    public abstract class EligibilityManagerBase : IEligibilityManager
    {
        protected readonly Dictionary<string,IEligibilityCalculator> EligibilityDictionary;

        public EligibilityManagerBase(IEligibilityCalculator local)
        {
            EligibilityDictionary = new Dictionary<string, IEligibilityCalculator>();
            EligibilityDictionary.Add(local.MediumType.MediumTag, local);
        }
        public virtual bool GetEligibility(string tag)
        {
            return EligibilityDictionary[tag].Calculate();
        }
    }
}