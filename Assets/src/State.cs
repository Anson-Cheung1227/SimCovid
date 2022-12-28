using UnityEngine;
using System.Collections.Generic;

public class State : MonoBehaviour
{
    [field: SerializeField] public int Infections {get; set;} 
    [field: SerializeField] public int InHospital {get; private set;}
    [field: SerializeField] public List<Airport> AirportList {get; private set;} = new List<Airport>();
}