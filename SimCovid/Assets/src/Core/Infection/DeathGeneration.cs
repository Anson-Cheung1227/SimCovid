using System.Collections.Generic;
using SimCovid.Core;
using UnityEngine;
using SimCovidAPI;

/// <summary>
/// Handles Death Generation
/// </summary>
public class DeathGeneration : DeathGenerationBase
{
    private DataManager _dataManager;
    public DeathGeneration(List<ILocation> locationList, DataManager dataManager)
    {
        Locations = locationList;
        _dataManager = dataManager;
    }

    public override void OnGenerate()
    {
        TargetDate = _dataManager.GameDateTime;
        base.OnGenerate();
    }
}
