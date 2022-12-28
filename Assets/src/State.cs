using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New State", menuName = "ScriptableObjects/State", order = 0)]
public class State : ScriptableObject 
{
    [field: SerializeField] public int Infections {get; private set;}
    [field: SerializeField] public int InHospital {get; private set;}
    [field: SerializeField] public List<Airport> AirportList {get; private set;} = new List<Airport>();
}