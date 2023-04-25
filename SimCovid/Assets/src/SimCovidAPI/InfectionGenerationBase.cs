using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace SimCovidAPI
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
                ISpreadableDataHandler targetISpreadableHandler = eligibleLocation.InfectionManager.GetActive();
                if (targetISpreadableHandler.GetActualISpreadablesCount() == 0)
                {
                    continue;
                }
                ISpreadable infectionParam = targetISpreadableHandler.CreateISpreadable();
                infectionParam.AddToInfection(targetISpreadableHandler.GetActualISpreadablesCount());
                eligibleLocation.InfectionManager.UpdateLimit();
                bool success = AddInfection(targetISpreadableHandler, infectionParam);
                if (!success)
                {
                    AddInfectionOverload(targetISpreadableHandler, infectionParam);
                }
            }
        }

        public virtual void GenerateInterstate(List<ILocation> locationList)
        {
            List<ILocation> eligibleLocations = new List<ILocation>();
            foreach (ILocation location in locationList)
            {
                ISpreadableDataHandler spreadableDataHandler = location.InfectionManager.GetActive();
                if (spreadableDataHandler.GetActualISpreadablesCount() == 0)
                {
                    continue;
                }
                eligibleLocations.Add(location);
            }

            foreach (ILocation eligibleLocation in eligibleLocations)
            {
                ILocation targetLocation = locationList[Random.Range(0, locationList.Count)];
                ISpreadableDataHandler targetISpreadableHandler = targetLocation.InfectionManager.GetActive();
                ISpreadable infectionParam = targetISpreadableHandler.CreateISpreadable();
                infectionParam.AddToInfection(eligibleLocation.InfectionManager.GetActive().GetActualISpreadablesCount());
                targetLocation.InfectionManager.UpdateLimit();
                bool success = AddInfection(targetISpreadableHandler, infectionParam);
                if (!success)
                {
                    AddInfectionOverload(targetISpreadableHandler, infectionParam);
                }
            }
        }
        public virtual void GenerateGlobal(List<ILocation> locationList)
        {
            List<ILocation> eligibleLocations = new List<ILocation>();
            foreach (ILocation location in locationList)
            {
                ISpreadableDataHandler spreadableDataHandler = location.InfectionManager.GetActive();
                eligibleLocations.Add(location);
            }

            ILocation targetLocation = eligibleLocations[Random.Range(0, eligibleLocations.Count)];
            ISpreadableDataHandler targetISpreadableHandler = targetLocation.InfectionManager.GetActive();
            ISpreadable infectionParam = targetISpreadableHandler.CreateISpreadable();
            infectionParam.AddToInfection(1);
            targetLocation.InfectionManager.UpdateLimit();
            bool success = AddInfection(targetISpreadableHandler, infectionParam);
            if (!success)
            {
                AddInfectionOverload(targetISpreadableHandler, infectionParam);
            }
        }
        public virtual void OnGenerate()
        {
            GenerateLocal(Locations);
            GenerateInterstate(Locations);
            GenerateGlobal(Locations);
        }

        public virtual bool AddInfection(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            if (spreadableDataHandler.GetActualISpreadablesCount() >= spreadableDataHandler.Limit) return false;
            param.SetActive(TargetDate);
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

        protected virtual void AddInfectionOverload(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            param.AddToInfection(param.Amount * -1); //Reset
            param.AddToInfection(spreadableDataHandler.Limit - spreadableDataHandler.GetActualISpreadablesCount());
            ISpreadable findResult = spreadableDataHandler.FindExistingInstance(param);
            if (findResult == null)
            {
                spreadableDataHandler.AddISpreadable(param);
            }
            else
            {
                spreadableDataHandler.AddAmountToISpreadable(findResult, param.Amount);
            }
        }
    }
}