using UnityEngine;
using System.Collections.Generic;
using InfectionModule;

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
        List<Infection> infections = new List<Infection>();
        List<Infection> activeInfections = new List<Infection>();
        List<Infection> inHospital = new List<Infection>();
        List<Infection> recovered = new List<Infection>();
        List<Infection> deceased = new List<Infection>();
        List<Airport> airports = new List<Airport>();
        long infectionsLong = 0;
        long activeInfectionsLong = 0;
        long inHospitalLong = 0;
        long recoveredLong = 0;
        long deceasedLong = 0;
        //Copy the ScriptableObject into a new List to avoid changing the lists in ScriptableObjects.
        foreach (InfectionStruct infection in stateSO.Infections)
        {
            Infection refInfection = (Infection)infection;
            infections.Add(refInfection); 
            infectionsLong += infection.Amount;
            switch (infection.InfectionStatus)
            {
                case InfectionStatus.Active:
                    activeInfections.Add(refInfection);
                    activeInfectionsLong += infection.Amount;
                    break;
                case InfectionStatus.InHospital:
                    inHospital.Add(refInfection);
                    inHospitalLong += infection.Amount;
                    break;
                case InfectionStatus.Recovered:
                    recovered.Add(refInfection);
                    recoveredLong += infection.Amount;
                    break;
                case InfectionStatus.Deceased:
                    deceased.Add(refInfection);
                    deceasedLong += infection.Amount;
                    break;
            }
        }
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
            Infections = infections,
            ActiveInfections = activeInfections,
            InHospital = inHospital, 
            Recovered = recovered,
            Deceased = deceased,
            InfectionsLong = infectionsLong,
            ActiveInfectionsLong = activeInfectionsLong,
            InHospitalLong = inHospitalLong,
            RecoveredLong = recoveredLong,
            DeceasedLong = deceasedLong,
            AirportList = airports,
        };
    }
}