using System;
using System.Collections.Generic;
using SimCovidAPI;
using Random = UnityEngine.Random;

namespace SimCovidAPI
{
    public abstract class InfectionGenerationBase : ISpreadableGenerationManager
    {
        protected List<ILocation> _locations;
        public InfectionGenerationBase(List<ILocation> locationList)
        {
            _locations = locationList;
        }

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
                if (targetISpreadableHandler.GetActualInfectionsCount() == 0)
                {
                    continue;
                }
                ISpreadable infectionParam = targetISpreadableHandler.CreateISpreadable();
                infectionParam.AddToInfection(targetISpreadableHandler.GetActualInfectionsCount());
                AddInfection(targetISpreadableHandler, infectionParam);
            }
        }

        public virtual void GenerateInterstate(List<ILocation> locationList)
        {
            List<ILocation> eligibleLocations = new List<ILocation>();
            foreach (ILocation location in locationList)
            {
                ISpreadableDataHandler spreadableDataHandler = location.InfectionManager.GetActive();
                if (spreadableDataHandler.GetActualInfectionsCount() == 0)
                {
                    continue;
                }
                eligibleLocations.Add(location);
            }

            foreach (ILocation eligibleLocation in eligibleLocations)
            {
                ILocation targetLocation = locationList[Random.Range(0, locationList.Count)];
                ISpreadableDataHandler targetSpreadableHandler = targetLocation.InfectionManager.GetActive();
                ISpreadable infectionParam = targetSpreadableHandler.CreateISpreadable();
                infectionParam.AddToInfection(eligibleLocation.InfectionManager.GetActive().GetActualInfectionsCount());
                AddInfection(targetSpreadableHandler, infectionParam);
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
            ISpreadableDataHandler targetSpreadableHandler = targetLocation.InfectionManager.GetActive();
            ISpreadable infectionParam = targetSpreadableHandler.CreateISpreadable();
            infectionParam.AddToInfection(1);
            AddInfection(targetSpreadableHandler, infectionParam);
        }

        public virtual bool AddInfection(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            if (spreadableDataHandler.GetActualInfectionsCount() >= spreadableDataHandler.Limit) return false;
            ISpreadable findResult = spreadableDataHandler.FindExistingInstance(param);
            if (findResult == null)
            {
                spreadableDataHandler.AddISpreadable(param);
            }
            else
            {
                spreadableDataHandler.AddAmountToISpreadable(findResult, param.Amount);
            }

            return true;
        }

        public virtual void OnGenerate()
        {
            GenerateLocal(_locations);
            GenerateInterstate(_locations);
            GenerateGlobal(_locations);
        }
    }
}