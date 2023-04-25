using System;
using System.Collections.Generic;

namespace SimCovidAPI
{
    public abstract class RecoveryGenerationBase : ISpreadableGenerationManager
    {
        protected float Rate = 0.9f;
        protected int DaysUntilEligible = 4;
        protected DateTime TargetDate;
        protected List<ILocation> Locations;
        public virtual void GenerateRecovery(ISpreadableDataHandler inHospital, ISpreadableDataHandler recovered)
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
                spreadable.AddToInfection(amount * - 1);
                ISpreadable infectionParam = recovered.CreateISpreadable();
                infectionParam.SetActive(spreadable.Date);
                infectionParam.SetInHospital(spreadable.InHospitalDate);
                infectionParam.SetRecovery(TargetDate);
                infectionParam.AddToInfection(amount);
                ISpreadable findResult = recovered.FindExistingInstance(infectionParam);
                if (findResult == null)
                {
                    recovered.AddISpreadable(infectionParam);
                }
                else
                {
                    recovered.AddAmountToISpreadable(findResult, infectionParam.Amount);
                }
            }
            iEnumeratorSpreadable.Dispose();
        }
        public virtual void OnGenerate()
        {
            foreach (ILocation location in Locations)
            {
                location.InfectionManager.UpdateLimit();
                GenerateRecovery(location.InfectionManager.GetInHospital(), location.InfectionManager.GetRecovered());
            }
        }
    }
}