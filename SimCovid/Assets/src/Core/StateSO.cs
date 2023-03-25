using UnityEngine;
using System.Collections.Generic;
using InfectionModule;

/// <summary>
/// Scriptable Object for State (Template)
/// This file is read-only and should be loaded on Start
/// </summary>
[CreateAssetMenu(fileName = "New State", menuName = "Scriptable Objects/State")]
public class StateSO : ScriptableObject 
{
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public long Population {get; private set;}
    [field: SerializeField] public float LocalSpreadRate {get; set;}
    [field: SerializeField] public long DailyIncomingPeople {get; private set;}
    [field: SerializeField] public List<InfectionStruct> Infections {get; private set;} = new List<InfectionStruct>(); 
    [field: SerializeField] public List<AirportSO> AirportList {get; private set;} = new List<AirportSO>(); 
    public static explicit operator State(StateSO stateSO)
    {
        List<Airport> airports = new List<Airport>();
        long infectionsLong = 0;
        long activeInfectionsLong = 0;
        long inHospitalLong = 0;
        long recoveredLong = 0;
        long deceasedLong = 0;
        foreach (AirportSO airportSO in stateSO.AirportList)
        {
            airports.Add((Airport)airportSO);
        }
        return new State
        {
            Name = stateSO.Name, 
            Population = stateSO.Population, 
            LocalSpreadRate = stateSO.LocalSpreadRate ,
            DailyIncomingPeople = stateSO.DailyIncomingPeople, 
            InfectionsLong = infectionsLong,
            ActiveInfectionsLong = activeInfectionsLong,
            InHospitalLong = inHospitalLong,
            RecoveredLong = recoveredLong,
            DeceasedLong = deceasedLong,
            AirportList = airports,
        };
    }
}