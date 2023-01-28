using UnityEngine;

namespace InfectionModule
{

    [System.Serializable]
    public class Infection
    {
        [field: SerializeField] public TimeModule.Date Date { get; set; }
        [field: SerializeField] public InfectionStatus InfectionStatus { get; set; }
        [field: SerializeField] public long Amount { get; set; }
        [field: SerializeField] public bool HasSpread {get; set;}
        /*
            This function finds if there is an existing infection with the same parameters,
            if there is, return the instance, 
            else, return null
        */
        public static Infection FindExistingInfection(State state, TimeModule.Date date, InfectionStatus infectionStatus, bool hasSpread)
        {
            Infection findResult = null;
            if (infectionStatus == InfectionStatus.Active)
            {
                foreach (InfectionModule.Infection infection in state.ActiveInfections)
                {
                    if (date == infection.Date && hasSpread == infection.HasSpread)
                    {
                        findResult = infection;
                        break;
                    }
                }
            }
            else if (infectionStatus == InfectionStatus.InHospital)
            {
                foreach (Infection infection in state.InHospital)
                {
                    if (date == infection.Date && hasSpread == infection.HasSpread)
                    {
                        findResult = infection;
                        break;
                    }
                }
            }
            else if (infectionStatus == InfectionStatus.Recovered)
            {
                foreach (Infection infection in state.InHospital)
                {
                    if (date == infection.Date && hasSpread == infection.HasSpread)
                    {
                        findResult = infection;
                        break;
                    }
                }
            }
            return findResult;
        }
    }
    [System.Serializable]
    public struct InfectionStruct
    {
        [field: SerializeField] public TimeModule.Date Date { get; set; }
        [field: SerializeField] public InfectionStatus InfectionStatus { get; set; }
        [field: SerializeField] public long Amount { get; set; }
        [field: SerializeField] public bool HasSpread {get; set;}
        public static explicit operator Infection(InfectionStruct infectionStruct)
        {
            return new Infection
            {
                Date = infectionStruct.Date, 
                InfectionStatus = infectionStruct.InfectionStatus, 
                Amount = infectionStruct.Amount,
                HasSpread = infectionStruct.HasSpread
            };
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
}