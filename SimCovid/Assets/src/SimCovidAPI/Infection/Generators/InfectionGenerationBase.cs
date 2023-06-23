using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace SimCovidAPI.Infection.Generators
{
    public abstract class InfectionGenerationBase : ISpreadableGenerationManager
    {
        protected List<ILocation> Locations;
        protected DateTime TargetDate;

        public virtual void GenerateLocal(List<ILocation> locationList)
        {
            List<ILocation> eligibleLocations = new List<ILocation>();
            foreach (ILocation location in locationList)
            {
                eligibleLocations.Add(location);
            }
            foreach (ILocation eligibleLocation in eligibleLocations)
            {
                ISpreadableDataHandler targetISpreadableHandler =
                    eligibleLocation.InfectionManager.GetISpreadableDataHandler(InfectionStatus.Active);
                if (targetISpreadableHandler.GetActualISpreadablesCount() == 0)
                {
                    continue;
                }

                ISpreadable infectionParam =
                    SimCovidHelper.CreateISpreadableWithAmount(targetISpreadableHandler, targetISpreadableHandler.GetActualISpreadablesCount());
                eligibleLocation.InfectionManager.UpdateLimit();
                infectionParam.SetActive(TargetDate);
                AddInfection(targetISpreadableHandler, infectionParam);
            }
        }

        public virtual void GenerateInterstate(List<ILocation> locationList)
        {
            List<ILocation> eligibleLocations = new List<ILocation>();
            foreach (ILocation location in locationList)
            {
                ISpreadableDataHandler spreadableDataHandler =
                    location.InfectionManager.GetISpreadableDataHandler(InfectionStatus.Active);
                if (spreadableDataHandler.GetActualISpreadablesCount() == 0)
                {
                    continue;
                }
                eligibleLocations.Add(location);
            }

            foreach (ILocation eligibleLocation in eligibleLocations)
            {
                ILocation targetLocation = locationList[Random.Range(0, locationList.Count)];
                ISpreadableDataHandler targetISpreadableHandler =
                    targetLocation.InfectionManager.GetISpreadableDataHandler(InfectionStatus.Active);
                ISpreadable infectionParam = SimCovidHelper.CreateISpreadableWithAmount(targetISpreadableHandler,
                    eligibleLocation.InfectionManager.GetISpreadableDataHandler(InfectionStatus.Active).GetActualISpreadablesCount());
                targetLocation.InfectionManager.UpdateLimit();
                infectionParam.SetActive(TargetDate);
                AddInfection(targetISpreadableHandler, infectionParam);
            }
        }
        public virtual void GenerateGlobal(List<ILocation> locationList)
        {
            List<ILocation> eligibleLocations = new List<ILocation>();
            foreach (ILocation location in locationList)
            {
                eligibleLocations.Add(location);
            }

            ILocation targetLocation = eligibleLocations[Random.Range(0, eligibleLocations.Count)];
            ISpreadableDataHandler targetISpreadableHandler =
                targetLocation.InfectionManager.GetISpreadableDataHandler(InfectionStatus.Active);
            ISpreadable infectionParam = SimCovidHelper.CreateISpreadableWithAmount(targetISpreadableHandler, 1);
            targetLocation.InfectionManager.UpdateLimit();
            infectionParam.SetActive(TargetDate);
            AddInfection(targetISpreadableHandler, infectionParam);
        }
        public virtual void OnGenerate()
        {
            GenerateLocal(Locations);
            GenerateInterstate(Locations);
            GenerateGlobal(Locations);
        }

        protected virtual void AddInfection(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            if (SimCovidHelper.CheckIfOverload(spreadableDataHandler, param))
            {
                AddInfectionOverload(spreadableDataHandler, param);
                return;
            }
            SimCovidHelper.AddISpreadable(spreadableDataHandler, param);
        }

        protected virtual void AddInfectionOverload(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            param.AddToInfection(param.Amount * -1); //Reset to zero
            param.AddToInfection(spreadableDataHandler.Limit - spreadableDataHandler.GetActualISpreadablesCount());
            SimCovidHelper.AddISpreadable(spreadableDataHandler, param);
        }
    }
}