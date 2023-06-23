using System;
using SimCovidAPI;
using UnityEngine;

namespace SimCovid.Core.Infection
{
    /// <summary>
    /// Represents an Infection with amount
    /// </summary>
    [System.Serializable]
    public class Infection : ISpreadable
    {
        [field: SerializeField] public DateTime Date { get; private set; }
        [field: SerializeField] public Nullable<DateTime> InHospitalDate { get; private set; }
        [field: SerializeField] public Nullable<DateTime> RecoveryDate { get; private set; }
        [field: SerializeField] public Nullable<DateTime> DeceasedDate { get; private set; }
        [field: SerializeField] public ISpreadableStatus Status { get; private set; }
        [field: SerializeField] public long Amount { get; private set; }
        [field: SerializeField] public bool HasSpread { get; private set; }

        public void AddToInfection(long amount)
        {
            Amount += amount;
        }

        public void SetDeceased(Nullable<DateTime> date)
        {
            Status = InfectionStatus.Deceased;
            DeceasedDate = date;
        }

        public void SetHasSpread(bool spread)
        {
            HasSpread = spread;
        }

        public void SetInHospital(Nullable<DateTime> date)
        {
            Status = InfectionStatus.InHospital;
            InHospitalDate = date;
        }

        public void SetRecovery(Nullable<DateTime> date)
        {
            Status = InfectionStatus.Recovered;
            RecoveryDate = date;
        }

        public void SetActive(DateTime date)
        {
            Status = InfectionStatus.Active;
            Date = date;
        }

        public bool ValidateISpreadable()
        {
            bool validISpreadable = Amount > 0 && Status != null;
            return validISpreadable;
        }
    }
}