using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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
            List<ISpreadable> disposableISpreadable = new List<ISpreadable>();
            while (iEnumeratorSpreadable.MoveNext())
            {
                ISpreadable spreadable = iEnumeratorSpreadable.Current;
                if ((TargetDate - spreadable.InHospitalDate).Value.TotalDays < DaysUntilEligible)
                {
                    continue;
                }

                long amount = (long)(spreadable.Amount * Rate);
                if (amount < 1 && spreadable.Amount != 1)
                {
                    continue;
                }
                if (spreadable.Amount == 1)
                {
                    if (Random.Range(0f, 100f) <= Rate * 100)
                    {
                        amount = 1;
                        disposableISpreadable.Add(spreadable);
                    }
                    else
                    {
                        continue;
                    }
                }
                spreadable.AddToInfection(amount * -1);
                death.SetLimit(death.Limit + amount);
                ISpreadable infectionParam = death.CreateISpreadable();
                infectionParam.SetActive(spreadable.Date);
                infectionParam.SetInHospital(spreadable.InHospitalDate);
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
            foreach (ISpreadable spreadable in disposableISpreadable)
            {
                inHospital.RemoveISpreadable(spreadable);
            }
        }
        public virtual void OnGenerate()
        {
            foreach (ILocation location in Locations)
            {
                location.InfectionManager.UpdateLimit();
                GenerateDeath(location.InfectionManager.GetInHospital(), location.InfectionManager.GetDeceased());
            }
        }
    }
}