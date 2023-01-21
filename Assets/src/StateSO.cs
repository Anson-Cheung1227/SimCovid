using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New State", menuName = "Scriptable Objects/State")]
public class StateSO : ScriptableObject 
{
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public long Population {get; private set;}
    [field: SerializeField] public long DailyIncomingPeople {get; private set;}
    [field: SerializeField] public List<Infection> Infections {get; private set;} = new List<Infection>(); 
    [field: SerializeField] public List<Infection> InHospital {get; private set;} = new List<Infection>();
    [field: SerializeField] public List<AirportSO> AirportList {get; private set;} = new List<AirportSO>(); 
    public static explicit operator State(StateSO stateSO)
    {
        List<Infection> infections = new List<Infection>();
        List<Infection> inHospital = new List<Infection>();
        List<Airport> airports = new List<Airport>();
        //Copy the ScriptableObject into a new List to avoid changing the lists in ScriptableObjects.
        foreach (Infection infection in stateSO.Infections)
        {
            infections.Add(infection);
        }
        foreach (Infection infection in stateSO.InHospital)
        {
            inHospital.Add(infection);
        }
        foreach (AirportSO airportSO in stateSO.AirportList)
        {
            airports.Add((Airport)airportSO);
        }
        return new State{Name = stateSO.Name, Population = stateSO.Population, DailyIncomingPeople = stateSO.DailyIncomingPeople, Infections = infections, InHospital = inHospital, AirportList = airports};
    }
}