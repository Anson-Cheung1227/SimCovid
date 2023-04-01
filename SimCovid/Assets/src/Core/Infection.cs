using UnityEngine;
using System;
using SimCovidAPI;

namespace InfectionModule
{
    /// <summary>
    /// Represents an Infection with amount
    /// </summary>
    [System.Serializable]
    public class Infection : ISpreadable
    {
        [field: SerializeField] public DateTime Date { get; private set; }
        [field: SerializeField] public Nullable<DateTime> InHospitalDate {get; private set;}
        [field: SerializeField] public Nullable<DateTime> RecoveryDate {get; private set;}
        [field: SerializeField] public Nullable<DateTime> DeceasedDate {get; private set;}
        [field: SerializeField] public InfectionStatus InfectionStatus { get; private set; }
        [field: SerializeField] public long Amount { get; private set; }
        [field: SerializeField] public bool HasSpread {get; private set; }

        public void AddToInfection(long amount)
        {
            Amount += amount;
        }

        public void SetDeceasedDate(DateTime date)
        {
            DeceasedDate = date;
        }

        public void SetHasSpread(bool spread)
        {
            HasSpread = spread;
        }

        public void SetInHospitalDate(DateTime date)
        {
            InHospitalDate = date;
        }

        public void SetRecoveryDate(DateTime date)
        {
            RecoveryDate = date;
        }

        public void SetSpreadDate(DateTime date)
        {
            Date = date;
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