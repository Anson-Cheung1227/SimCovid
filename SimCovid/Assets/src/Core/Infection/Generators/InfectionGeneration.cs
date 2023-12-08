using System.Collections.Generic;
using SimCovidAPI.Infection.Generators;
using SimCovidAPI.Locations;

namespace SimCovid.Core.Infection.Generators
{
    /// <summary>
    /// Generates Infections
    /// </summary>
    public class InfectionGeneration : InfectionGenerationBase
    {
        private DataManager _dataManager;
        public InfectionGeneration(List<ILocation> locationList, DataManager dataManager)
        {
            Locations = locationList;
            _dataManager = dataManager;
            UpdateInfectionList(_dataManager.StateInfectionsTable, Locations);
        }

        private void UpdateInfectionList(List<State> list, List<ILocation> refState)
        {
            list.Clear();
            foreach (ILocation state in refState)
            {
                if (list.Count == 0)
                {
                    list.Add(state as State);
                    continue;
                }

                int iter = list.Count;
                while (state.InfectionManager.GetTotalISpreadableCount() > list[iter - 1].InfectionManager.GetTotalISpreadableCount())
                {
                    iter--;
                    if (iter == 0) break;
                }

                list.Insert(iter, state as State);
            }
        }

        public override void OnGenerate()
        {
            TargetDate = _dataManager.GameDateTime;
            base.OnGenerate();
            UpdateInfectionList(_dataManager.StateInfectionsTable, Locations);
        }
    }
}