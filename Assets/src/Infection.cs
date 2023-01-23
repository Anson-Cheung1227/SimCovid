using UnityEngine;

namespace InfectionModule
{

    [System.Serializable]
    public class Infection
    {
        [field: SerializeField] public TimeModule.Date Date { get; set; }
        [field: SerializeField] public InfectionStatus InfectionStatus { get; set; }
        [field: SerializeField] public long Amount { get; set; }
        public static Infection FindExistingInfection(State state, TimeModule.Date date, InfectionStatus infectionStatus)
        {
            Infection findResult = null;
            if (infectionStatus == InfectionStatus.Active)
            {
                foreach (InfectionModule.Infection infection in state.ActiveInfections)
                {
                    if (date == infection.Date)
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
                    if (date == infection.Date)
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
                    if (date == infection.Date)
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
        public static explicit operator Infection(InfectionStruct infectionStruct)
        {
            return new Infection { Date = infectionStruct.Date, InfectionStatus = infectionStruct.InfectionStatus, Amount = infectionStruct.Amount };
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