using System;
using System.Collections.Generic;
using SimCovidAPI.Locations;

namespace SimCovidAPI.Infection.Generators
{
    public abstract class InHospitalGenerationBase : ISpreadableGenerationManager
    {
        protected float Rate = 1.0f;
        protected int DaysUntilEligible = 4;
        protected DateTime TargetDate;
        protected List<ILocation> Locations;
        public virtual void GenerateInHospital(ILocation location)
        {
            ISpreadableDataHandler active = location.InfectionManager.GetISpreadableDataHandler(InfectionStatus.Active);
            ISpreadableDataHandler inHospital = location.InfectionManager.GetISpreadableDataHandler(InfectionStatus.InHospital);
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
                    if (SimCovidHelper.BoolFromChance((int)Rate * 100))
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
                location.InfectionManager.UpdateLimit();
                ISpreadable infectionParam = SimCovidHelper.CreateISpreadableWithAmount(inHospital, amount);
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
                GenerateInHospital(location);
            }
        }

        public virtual bool AddInfection(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            bool success = SimCovidHelper.AddISpreadable(spreadableDataHandler, param);
            return success;
        }
    }
}