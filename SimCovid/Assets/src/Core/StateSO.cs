using System.Collections.Generic;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Scriptable Object for State (Template)
    /// This file is read-only and should be loaded on Start
    /// </summary>
    [CreateAssetMenu(fileName = "New State", menuName = "Scriptable Objects/State")]
    public class StateSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public long Population { get; private set; }
        [field: SerializeField] public float LocalSpreadRate { get; set; }
        [field: SerializeField] public long DailyIncomingPeople { get; private set; }
        [field: SerializeField] public List<AirportSO> AirportList { get; private set; } = new List<AirportSO>();
        public static explicit operator State(StateSO stateSO)
        {
            List<Airport> airports = new List<Airport>();
            foreach (AirportSO airportSO in stateSO.AirportList)
            {
                airports.Add((Airport)airportSO);
            }

            State target = new State(stateSO.Name);
            target.Population = stateSO.Population;
            target.LocalSpreadRate = stateSO.LocalSpreadRate;
            target.DailyIncomingPeople = stateSO.DailyIncomingPeople;
            target.AirportList = airports;
            return target;
        }
    }
}