using UnityEngine;
[System.Serializable]
public class Infection
{
    [field: SerializeField] public TimeModule.Date Date {get; set;}
    [field: SerializeField] public InfectionStatus InfectionStatus {get; set;} 
}
public enum InfectionType
{
    Local,
    Interstate, 
    Global, 
}
[System.Serializable]
public enum InfectionStatus
{
    Active,
    InHospital, 
    Recovered, 
}