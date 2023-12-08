using System;
using System.Collections.Generic;
using SimCovidAPI.Locations;
using Random = UnityEngine.Random;

namespace SimCovidAPI.Infection.Generators
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
                spreadable.AddToInfection(amount * - 1);
                recovered.SetLimit(recovered.Limit + amount);
                ISpreadable infectionParam = recovered.CreateISpreadable();
                infectionParam.SetActive(spreadable.Date);
                infectionParam.SetInHospital(spreadable.InHospitalDate);
                infectionParam.SetRecovery(TargetDate);
                infectionParam.AddToInfection(amount);
                ISpreadable findResult = recovered.FindExistingInstance(infectionParam);
                AddInfection(recovered, infectionParam);
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
                GenerateRecovery(location.InfectionManager.GetISpreadableDataHandler(InfectionStatus.InHospital.StatusTag),
                    location.InfectionManager.GetISpreadableDataHandler(InfectionStatus.Recovered.StatusTag));
                    location.InfectionManager.GetISpreadableDataHandler(InfectionStatus.Recovered.StatusTag);
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