using UnityEngine;
using System;

namespace InfectionModule
{
    /// <summary>
    /// Represents an Infection with amount
    /// </summary>
    [System.Serializable]
    public class Infection
    {
        [field: SerializeField] public DateTime Date { get; set; }
        [field: SerializeField] public Nullable<DateTime> InHospitalDate {get; set;}
        [field: SerializeField] public Nullable<DateTime> RecoveryDate {get; set;}
        [field: SerializeField] public Nullable<DateTime> DeceasedDate {get; set;}
        [field: SerializeField] public InfectionStatus InfectionStatus { get; set; }
        [field: SerializeField] public long Amount { get; set; }
        [field: SerializeField] public bool HasSpread {get; set; }
        /// <summary>
        /// This function finds if there is an existing infection with the same parameters,
        /// if there is, return the instance,
        /// else, return null
        /// </summary>
        /// <param name="state">target state</param>
        /// <param name="date">target date</param>
        /// <param name="inHospitalDate">target inHospitalDate</param>
        /// <param name="recovredDate">target recoveredDate</param>
        /// <param name="deceasedDate">target deceased date</param>
        /// <param name="infectionStatus">target infection status</param>
        /// <param name="hasSpread">target hasSpread</param>
        /// <returns>returns null if no copy is found, returns the Infection class if one is found</returns>
        public static Infection FindExistingInfection(State state, Nullable<DateTime> date,Nullable<DateTime> inHospitalDate, Nullable<DateTime> recovredDate, Nullable<DateTime> deceasedDate, InfectionStatus infectionStatus, bool hasSpread)
        {
            Infection findResult = null;
            if (infectionStatus == InfectionStatus.Active)
            {
                foreach (Infection infection in state.ActiveInfections)
                {
                    if (date == infection.Date && inHospitalDate == infection.InHospitalDate && recovredDate == infection.RecoveryDate && deceasedDate == infection.DeceasedDate && hasSpread == infection.HasSpread)
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
                    if (date == infection.Date && inHospitalDate == infection.InHospitalDate && recovredDate == infection.RecoveryDate && deceasedDate == infection.DeceasedDate && hasSpread == infection.HasSpread)
                    {
                        findResult = infection;
                        break;
                    }
                }
            }
            else if (infectionStatus == InfectionStatus.Recovered)
            {
                foreach (Infection infection in state.Recovered)
                {
                    if (date == infection.Date && inHospitalDate == infection.InHospitalDate && recovredDate == infection.RecoveryDate && deceasedDate == infection.DeceasedDate && hasSpread == infection.HasSpread)
                    {
                        findResult = infection;
                        break;
                    }
                }
            }
            return findResult;
        }
    }
    /// <summary>
    /// Infection struct
    /// </summary>
    [System.Serializable]
    public struct InfectionStruct
    {
        [field: SerializeField] public DateTime Date { get; set; }
        [field: SerializeField] public DateTime InHospitalDate {get; set;}
        [field: SerializeField] public DateTime RecoveryDate {get; set;}
        [field: SerializeField] public DateTime DeceasedDate {get; set;}
        [field: SerializeField] public InfectionStatus InfectionStatus { get; set; }
        [field: SerializeField] public long Amount { get; set; }
        [field: SerializeField] public bool HasSpread {get; set;}
        public static explicit operator Infection(InfectionStruct infectionStruct)
        {
            return new Infection
            {
                Date = infectionStruct.Date, 
                InHospitalDate = infectionStruct.InHospitalDate,
                RecoveryDate = infectionStruct.RecoveryDate,
                DeceasedDate = infectionStruct.DeceasedDate,
                InfectionStatus = infectionStruct.InfectionStatus, 
                Amount = infectionStruct.Amount,
                HasSpread = infectionStruct.HasSpread
            };
        }
    }
    /// <summary>
    /// Represents Infection type
    /// </summary>
    public enum InfectionType
    {
        Local,
        Interstate,
        Global,
    }
    /// <summary>
    /// Represents Infection Status
    /// </summary>
    [System.Serializable]
    public enum InfectionStatus
    {
        Active,
        InHospital,
        Recovered,
        Deceased, 
    }
}