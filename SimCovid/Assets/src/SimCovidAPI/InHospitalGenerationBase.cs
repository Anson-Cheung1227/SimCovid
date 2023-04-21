using System.Collections.Generic;
using UnityEngine;

namespace SimCovidAPI
{
    public abstract class InHospitalGenerationBase : ISpreadableGenerationManager
    {
        protected float Rate = 0.9f;
        protected List<ILocation> Locations;
        public virtual void GenerateInHospital(ISpreadableDataHandler active, ISpreadableDataHandler inHospital)
        {
            IEnumerable<ISpreadable> iEnumerableSpreadable = active.GetAll();
            IEnumerator<ISpreadable> iEnumeratorSpreadable = iEnumerableSpreadable.GetEnumerator();
            while (iEnumeratorSpreadable.MoveNext())
            {
                ISpreadable spreadable = iEnumeratorSpreadable.Current;
                long amount = (long)(spreadable.Amount * Rate);
                if (amount < 1) continue;
                spreadable.AddToInfection(amount * -1);
                ISpreadable infectionParam = inHospital.CreateISpreadable();
                infectionParam.AddToInfection(amount);
                ISpreadable findResult = inHospital.FindExistingInstance(infectionParam);
                if (findResult == null)
                {
                    inHospital.AddISpreadable(infectionParam);
                }
                else
                {
                    inHospital.AddAmountToISpreadable(findResult, infectionParam.Amount);
                }
            }
            iEnumeratorSpreadable.Dispose();
        }
        public void OnGenerate()
        {
            foreach (ILocation location in Locations)
            {
                GenerateInHospital(location.InfectionManager.GetActive(), location.InfectionManager.GetInHospital());
            }
        }
    }
}