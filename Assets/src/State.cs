using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class State
{
    [field: SerializeField] public string Name {get;set;}
    [field: SerializeField] public long Population {get; set;}
    [field: SerializeField] public float localSpreadRate {get; set;}
    [field: SerializeField] public long DailyIncomingPeople {get; set;}
    [field: SerializeField] public List<Infection> Infections {get; set;} = new List<Infection>();
    [field: SerializeField] public long InHospital {get; set;}
    [field: SerializeField] public long Recovered {get; set;}
    [field: SerializeField] public List<Airport> AirportList {get; set;} = new List<Airport>();
}