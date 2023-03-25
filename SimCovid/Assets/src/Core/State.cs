using UnityEngine;
using System.Collections.Generic;
using InfectionModule;
using Core;

/// <summary>
/// Represents a state
/// </summary>
[System.Serializable]
public class State
{
    #region Infections
    [field: SerializeField] public string Name {get;set;}
    [field: SerializeField] public long Population {get; set;}
    [field: SerializeField] public float LocalSpreadRate {get; set;}
    [field: SerializeField] public long DailyIncomingPeople {get; set;}
    [field: SerializeField] public InfectionManager InfectionManager { get; set;}
    [field: SerializeField] public long InfectionsLong {get; set;}
    [field: SerializeField] public long ActiveInfectionsLong {get; set;}
    [field: SerializeField] public long InHospitalLong {get; set;}
    [field: SerializeField] public long RecoveredLong {get; set;}
    [field: SerializeField] public long DeceasedLong{get; set;}
    [field: SerializeField] public List<Airport> AirportList {get; set;} = new List<Airport>();
    #endregion Infection
    #region Civilian
    [field: SerializeField] public float Morale {get; set;}
    #endregion Civilian
    #region Policies
    [field: SerializeField] public bool LocalLockdown {get; set;}
    [field: SerializeField] public bool InterstateLockdown {get; set;} 
    [field: SerializeField] public bool GlobalLockdown {get; set;}
    [field: SerializeField] public bool MandatoryMask {get; set;}
    #endregion Policies
}