using UnityEngine;
using System.Collections.Generic;
using SimCovidAPI;
using Unity.Profiling;

/// <summary>
/// Handle the transfer of patients to hospital
/// </summary>
public class InHospitalGeneration : InHospitalGenerationBase 
{
    public InHospitalGeneration(List<ILocation> locationList)
    {
        Locations = locationList;
    }
}