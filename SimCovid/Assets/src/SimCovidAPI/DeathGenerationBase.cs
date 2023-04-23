using System;
using System.Collections.Generic;

namespace SimCovidAPI
{
    public abstract class DeathGenerationBase : ISpreadableGenerationManager
    {
        protected float Rate = 0.1f;
        protected int DaysUntilEligible = 4;
        protected DateTime TargetDate;
        protected List<ILocation> Locations;
        public virtual void GenerateDeath(ISpreadableDataHandler inHospital, ISpreadableDataHandler death)
        {
            IEnumerable<ISpreadable> iEnumerableSpreadable = inHospital.GetAll();
            IEnumerator<ISpreadable> iEnumeratorSpreadable = iEnumerableSpreadable.GetEnumerator();
            while (iEnumeratorSpreadable.MoveNext())
            {
                ISpreadable spreadable = iEnumeratorSpreadable.Current;
                if ((TargetDate - spreadable.InHospitalDate).Value.TotalDays < DaysUntilEligible)
                {
                    continue;
                }

                long amount = (long)(spreadable.Amount * Rate);
                if (amount < 1) continue;
                spreadable.AddToInfection(amount * -1);
                ISpreadable infectionParam = death.CreateISpreadable();
                infectionParam.SetActive(spreadable.Date);
                infectionParam.SetInHospital(spreadable.Date);
                infectionParam.SetDeceased(TargetDate);
                infectionParam.AddToInfection(amount);
                ISpreadable findResult = death.FindExistingInstance(infectionParam);
                if (findResult == null)
                {
                    death.AddISpreadable(infectionParam);
                }
                else
                {
                    death.AddAmountToISpreadable(findResult, amount);
                }
            }
            iEnumeratorSpreadable.Dispose();
        }
        public virtual void OnGenerate()
        {
            foreach (ILocation location in Locations)
            {
                GenerateDeath(location.InfectionManager.GetInHospital(), location.InfectionManager.GetDeceased());
            }
        }
    }
}