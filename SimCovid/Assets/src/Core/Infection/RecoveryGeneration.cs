using System.Collections.Generic;
using SimCovid.Core;
using SimCovidAPI;

/// <summary>
/// Handles the recovery of patients
/// </summary>
public class RecoveryGeneration : RecoveryGenerationBase
{
    private DataManager _dataManager;
    public RecoveryGeneration(List<ILocation> locationList, DataManager dataManager)
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