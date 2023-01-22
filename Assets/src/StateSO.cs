using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New State", menuName = "Scriptable Objects/State")]
public class StateSO : ScriptableObject 
{
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public long Population {get; private set;}
    [field: SerializeField] public float localSpreadRate {get; set;}
    [field: SerializeField] public long DailyIncomingPeople {get; private set;}
    [field: SerializeField] public List<InfectionStruct> Infections {get; private set;} = new List<InfectionStruct>(); 
    [field: SerializeField] public List<AirportSO> AirportList {get; private set;} = new List<AirportSO>(); 
    public static explicit operator State(StateSO stateSO)
    {
        List<Infection> infections = new List<Infection>();
        List<Infection> activeInfections = new List<Infection>();
        List<Infection> inHospital = new List<Infection>();
        List<Airport> airports = new List<Airport>();
        //Copy the ScriptableObject into a new List to avoid changing the lists in ScriptableObjects.
        foreach (InfectionStruct infection in stateSO.Infections)
        {
            Infection refInfection = (Infection)infection;
            infections.Add(refInfection); 
            switch (infection.InfectionStatus)
            {
                case InfectionStatus.Active:
                    activeInfections.Add(refInfection);
                    break;
                case InfectionStatus.InHospital:
                    inHospital.Add(refInfection);
                    break;
            }
        }
        foreach (AirportSO airportSO in stateSO.AirportList)
        {
            airports.Add((Airport)airportSO);
        }
        return new State{Name = stateSO.Name, Population = stateSO.Population, localSpreadRate = stateSO.localSpreadRate ,DailyIncomingPeople = stateSO.DailyIncomingPeople, Infections = infections,ActiveInfections = activeInfections ,InHospital = inHospital, AirportList = airports};
    }
}