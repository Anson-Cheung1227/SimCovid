﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCovidAPI
{
    public interface ISpreadable
    {
        public DateTime Date { get; }
        public Nullable<DateTime> InHospitalDate { get; }
        public Nullable<DateTime> RecoveryDate { get; }
        public Nullable<DateTime> DeceasedDate { get; }
        public long Amount { get; }
        public bool HasSpread { get; }
        public void AddToInfection(long amount);
        public void SetSpreadDate(DateTime date);
        public void SetInHospitalDate(DateTime date);
        public void SetRecoveryDate(DateTime date);
        public void SetDeceasedDate(DateTime date);
        public void SetHasSpread(bool spread);
        public bool IsSameValue(ISpreadable a)
        {
            return Date == a.Date && InHospitalDate == a.InHospitalDate && RecoveryDate == a.RecoveryDate && DeceasedDate == a.DeceasedDate && HasSpread == a.HasSpread;
        }
    }
}