using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New State", menuName = "Scriptable Objects/State")]
public class StateSO : ScriptableObject 
{
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public long Population {get; private set;}
    [field: SerializeField] public long DailyIncomingPeople {get; private set;}
    [field: SerializeField] public long Infections {get; set;} 
    [field: SerializeField] public long InHospital {get; private set;}
    [field: SerializeField] public List<AirportSO> AirportList {get; private set;} = new List<AirportSO>(); 
    public static explicit operator State(StateSO stateSO)
    {
        List<Airport> airports = new List<Airport>();
        foreach (AirportSO airportSO in stateSO.AirportList)
        {
            airports.Add((Airport)airportSO);
        }
        return new State{Name = stateSO.Name, Population = stateSO.Population, DailyIncomingPeople = stateSO.DailyIncomingPeople, Infections = stateSO.Infections, InHospital = stateSO.InHospital, AirportList = airports};
    }
}