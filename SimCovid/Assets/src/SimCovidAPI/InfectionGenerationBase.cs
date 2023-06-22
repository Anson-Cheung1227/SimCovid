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

                ISpreadable infectionParam =
                    SimCovidHelper.CreateISpreadableWithAmount(targetISpreadableHandler, targetISpreadableHandler.GetActualISpreadablesCount());
                eligibleLocation.InfectionManager.UpdateLimit();
                AddInfection(targetISpreadableHandler, infectionParam);
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
                ISpreadable infectionParam = SimCovidHelper.CreateISpreadableWithAmount(targetISpreadableHandler,
                    eligibleLocation.InfectionManager.GetActive().GetActualISpreadablesCount());
                targetLocation.InfectionManager.UpdateLimit(); 
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
            ISpreadableDataHandler targetISpreadableHandler = targetLocation.InfectionManager.GetActive();
            ISpreadable infectionParam = SimCovidHelper.CreateISpreadableWithAmount(targetISpreadableHandler, 1);
            targetLocation.InfectionManager.UpdateLimit(); 
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