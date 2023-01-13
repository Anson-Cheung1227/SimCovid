using UnityEngine;
[System.Serializable]
public class Infection
{
    [field: SerializeField] public TimeModule.Date Date {get; set;}
}
public enum InfectionType
{
    Local,
    Interstate, 
    Global, 
}