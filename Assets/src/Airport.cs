using UnityEngine;

[System.Serializable]
public class Airport 
{
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public string IATACode {get; private set; }  
    [field: SerializeField] public long DailyPassengers {get; private set;}
    [field: SerializeField] public string CityServed {get; private set; }      
}