using UnityEngine;
using System.Collections.Generic;
using InfectionModule;

[System.Serializable]
public class State
{
    #region Infections
    [field: SerializeField] public string Name {get;set;}
    [field: SerializeField] public long Population {get; set;}
    [field: SerializeField] public float LocalSpreadRate {get; set;}
    [field: SerializeField] public long DailyIncomingPeople {get; set;}
    [field: SerializeField] public List<Infection> Infections {get; set;} = new List<Infection>();
    [field: SerializeField] public List<Infection> ActiveInfections {get; set;} = new List<Infection>();
    [field: SerializeField] public List<Infection> InHospital {get; set;} = new List<Infection>();
    [field: SerializeField] public List<Infection> Recovered {get; set;} = new List<Infection>();
    [field: SerializeField] public List<Infection> Deceased {get; set;} = new List<Infection>();
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