using UnityEngine;
using System.Collections.Generic;
using InfectionModule;

[System.Serializable]
public class State
{
    [field: SerializeField] public string Name {get;set;}
    [field: SerializeField] public long Population {get; set;}
    [field: SerializeField] public float localSpreadRate {get; set;}
    [field: SerializeField] public long DailyIncomingPeople {get; set;}
    [field: SerializeField] public List<Infection> Infections {get; set;} = new List<Infection>();
    [field: SerializeField] public List<Infection> ActiveInfections {get; set;} = new List<Infection>();
    [field: SerializeField] public List<Infection> InHospital {get; set;} = new List<Infection>();
    [field: SerializeField] public List<Infection> Recovered {get; set;} = new List<Infection>();
    [field: SerializeField] public long InfectionsLong {get; set;}
    [field: SerializeField] public long ActiveInfectionsLong {get; set;}
    [field: SerializeField] public long InHospitalLong {get; set;}
    [field: SerializeField] public long RecoveredLong {get; set;}
    [field: SerializeField] public List<Airport> AirportList {get; set;} = new List<Airport>();
}