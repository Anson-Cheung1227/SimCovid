using UnityEngine;
using System.Collections.Generic;
using SimCovid.Core;
using SimCovidAPI;
using Unity.Profiling;

/// <summary>
/// Handle the transfer of patients to hospital
/// </summary>
public class InHospitalGeneration : InHospitalGenerationBase
{
    private DataManager _dataManager;
    public InHospitalGeneration(List<ILocation> locationList, DataManager dataManager)
    {
        _dataManager = dataManager;
        Locations = locationList;
    }
    public override void OnGenerate()
    {
        TargetDate = _dataManager.GameDateTime;
        base.OnGenerate();
    }
}