using System.Collections.Generic;
using UnityEngine;
using InfectionModule;
using SimCovidAPI;
using Random = UnityEngine.Random;

/// <summary>
/// Generates Infections
/// </summary>
public class InfectionGeneration : InfectionGenerationBase
{
    private DataManager _dataManager;
    public InfectionGeneration(List<ILocation> locationList, DataManager dataManager) : base(locationList)
    {
        _dataManager = dataManager;
    }

    private void UpdateInfectionList(List<State> list, List<ILocation> refState)
    {
        list.Clear();
        foreach (State state in refState)
        {
            if (list.Count == 0)
            {
                list.Add(state);
                continue;
            }

            int iter = list.Count;
            while (state.InfectionManager.GetTotalInfections() > list[iter - 1].InfectionManager.GetTotalInfections())
            {
                iter--;
                if (iter == 0) break;
            }

            list.Insert(iter, state);
        }
    }

    public override void OnGenerate()
    {
        base.OnGenerate();
        UpdateInfectionList(_dataManager.StateInfectionsTable, _locations);
    }
}