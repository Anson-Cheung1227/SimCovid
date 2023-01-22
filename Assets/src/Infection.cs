using UnityEngine;
[System.Serializable]
public class Infection
{
    [field: SerializeField] public TimeModule.Date Date {get; set;}
    [field: SerializeField] public InfectionStatus InfectionStatus {get; set;} 
    [field: SerializeField] public int hasSpread {get; set;}
}
[System.Serializable]
public struct InfectionStruct
{
    [field: SerializeField] public TimeModule.Date Date {get; set;}
    [field: SerializeField] public InfectionStatus InfectionStatus {get; set;} 
    public static explicit operator Infection(InfectionStruct infectionStruct)
    {
        return new Infection{Date = infectionStruct.Date, InfectionStatus = infectionStruct.InfectionStatus};
    }
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