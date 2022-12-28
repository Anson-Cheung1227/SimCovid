using UnityEngine;

[CreateAssetMenu(fileName = "New Airport", menuName = "ScriptableObjects/Airport")]
public class Airport : ScriptableObject
{
    public string Name {get; private set;}
    public string IATACode {get; private set; }  
    public long DailyPassengers {get; private set;}
    public string CityServed {get; private set; }      
}