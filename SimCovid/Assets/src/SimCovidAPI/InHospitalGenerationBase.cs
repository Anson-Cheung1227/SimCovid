using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimCovidAPI
{
    public abstract class InHospitalGenerationBase : ISpreadableGenerationManager
    {
        protected float Rate = 0.9f;
        protected int DaysUntilEligible = 4;
        protected DateTime TargetDate;
        protected List<ILocation> Locations;
        public virtual void GenerateInHospital(ISpreadableDataHandler active, ISpreadableDataHandler inHospital)
        {
            IEnumerable<ISpreadable> iEnumerableSpreadable = active.GetAll();
            IEnumerator<ISpreadable> iEnumeratorSpreadable = iEnumerableSpreadable.GetEnumerator();
            List<ISpreadable> disposableISpreadable = new List<ISpreadable>();
            while (iEnumeratorSpreadable.MoveNext())
            {
                ISpreadable spreadable = iEnumeratorSpreadable.Current;
                if ((TargetDate - spreadable.Date).TotalDays < DaysUntilEligible)
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
                inHospital.SetLimit(inHospital.Limit + amount);
                ISpreadable infectionParam = inHospital.CreateISpreadable();
                infectionParam.AddToInfection(amount);
                infectionParam.SetActive(spreadable.Date);
                infectionParam.SetInHospital(TargetDate);
                AddInfection(inHospital, infectionParam);
            }
            iEnumeratorSpreadable.Dispose();
            foreach (ISpreadable spreadable in disposableISpreadable)
            {
                active.RemoveISpreadable(spreadable);
            }
        }
        public virtual void OnGenerate()
        {
            foreach (ILocation location in Locations)
            {
                location.InfectionManager.UpdateLimit();
                GenerateInHospital(location.InfectionManager.GetActive(), location.InfectionManager.GetInHospital());
            }
        }

        public virtual bool AddInfection(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            ISpreadable findResult = spreadableDataHandler.FindExistingInstance(param);
            bool success; 
            if (findResult == null)
            {
                success = spreadableDataHandler.AddISpreadable(param);
            }
            else
            {
                success = spreadableDataHandler.AddAmountToISpreadable(findResult, param.Amount);
            }

            return success;
        }
    }
}