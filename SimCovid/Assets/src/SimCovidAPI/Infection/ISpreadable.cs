using System;

namespace SimCovidAPI.Infection
{
    public interface ISpreadable
    {
        public DateTime Date { get; }
        public Nullable<DateTime> InHospitalDate { get; }
        public Nullable<DateTime> RecoveryDate { get; }
        public Nullable<DateTime> DeceasedDate { get; }
        public ISpreadableStatus Status { get; }
        public long Amount { get; }
        public bool HasSpread { get; }
        public void AddToInfection(long amount);
        public void SetActive(DateTime date);
        public void SetInHospital(Nullable<DateTime> date);
        public void SetRecovery(Nullable<DateTime> date);
        public void SetDeceased(Nullable<DateTime> date);
        public void SetHasSpread(bool spread);
        public bool IsSameValue(ISpreadable a)
        {
            return Date == a.Date &&
                   InHospitalDate == a.InHospitalDate &&
                   RecoveryDate == a.RecoveryDate &&
                   DeceasedDate == a.DeceasedDate &&
                   HasSpread == a.HasSpread &&
                   Status == a.Status;
        }
        public bool ValidateISpreadable();
    }
}
