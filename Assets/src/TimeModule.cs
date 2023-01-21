using System;
using System.Globalization;
using UnityEngine;
public class TimeModule
{
    /// <summary>
    /// Class that is for Hour, Minute, Second
    /// </summary>
    public class Time
    {
        public float Second { get; set; }
        public float Minute { get; set; }
        public float Hour { get; set; }

        public Time(float hour, float minute, float second)
        {
            Second = second;
            Minute = minute;
            Hour = hour;
        }
        public static Time operator +(Time a, Time b)
        {
            var resultTime = new Time(0, 0, 0);
            resultTime.Second += a.Second + b.Second;
            int multi = (int)Math.Floor(resultTime.Second / 60);
            resultTime.Second += multi * 60 * -1;
            resultTime.Minute += multi;
            resultTime.Minute += a.Minute + b.Minute;
            multi = (int)Math.Floor(resultTime.Minute / 60);
            resultTime.Minute += multi * 60 * -1;
            resultTime.Hour += multi;
            resultTime.Hour += a.Hour + b.Hour;
            return resultTime;
        }
        public static Time operator -(Time a, Time b)
        {
            var resultTime = new Time(0, 0, 0);
            resultTime.Second -= (a.Second + b.Second);
            int multi = (int)Math.Floor(resultTime.Second / 60);
            resultTime.Second += multi * 60 * -1;
            resultTime.Minute += multi;
            resultTime.Minute -= (a.Minute + b.Minute);
            multi = (int)Math.Floor(resultTime.Minute / 60);
            resultTime.Minute += multi * 60 * -1;
            resultTime.Hour += multi;
            resultTime.Hour -= (a.Hour + b.Hour);
            return resultTime;
        }
        public static Time operator *(Time a, float multiplier)
        {
            var resultTime = new Time(0, 0, 0);
            resultTime.Second += a.Second * multiplier;
            int multi = (int)Math.Floor(resultTime.Second / 60);
            resultTime.Second += multi * 60 * -1;
            resultTime.Minute += multi;
            resultTime.Minute += a.Minute * multiplier;
            multi = (int)Math.Floor(resultTime.Minute / 60);
            resultTime.Minute += multi * 60 * -1;
            resultTime.Hour += multi;
            resultTime.Hour += a.Hour * multiplier;
            return resultTime;
        }
        public static Time operator *(Time a, Time multiplier)
        {
            var resultTime = new Time(0, 0, 0);
            resultTime.Second += (a.Second * multiplier.Second);
            int multi = (int)Math.Floor(resultTime.Second / 60);
            resultTime.Second += multi * 60 * -1;
            resultTime.Minute += multi;
            resultTime.Minute += a.Minute * multiplier.Minute;
            multi = (int)Math.Floor(resultTime.Minute / 60);
            resultTime.Minute += multi * 60 * -1;
            resultTime.Hour += multi;
            resultTime.Hour += a.Hour * multiplier.Hour;
            return resultTime;
        }
        public static Time operator /(Time a, float divisor)
        {
            var resultTime = new Time(0, 0, 0);
            if (!(Double.IsInfinity(a.Second / divisor) || Double.IsNaN(a.Second / divisor)))
                resultTime.Second += a.Second / divisor;
            else
                resultTime.Second = a.Second;
            float multi = (float)Math.Truncate(resultTime.Second / 60);
            if (multi != 0)
            {
                resultTime.Second += (multi * 60 * -1);
                resultTime.Minute += multi;
            }
            if (!(Double.IsInfinity(a.Minute / divisor) || Double.IsNaN(a.Minute / divisor)))
                resultTime.Minute += a.Minute / divisor;
            else
                resultTime.Minute = a.Minute;
            multi = (float)Math.Truncate((resultTime.Minute) / 60);
            if (multi != 0)
            {
                resultTime.Minute += multi * 60 * -1;
                resultTime.Hour += multi;
            }
            if (!(Double.IsInfinity(a.Hour / divisor) || Double.IsNaN(a.Hour / divisor)))
                resultTime.Hour += (a.Hour / divisor);
            else
                resultTime.Hour = a.Hour;
            return resultTime;
        }
        public static Time operator /(Time a, Time divisor)
        {
            var resultTime = new Time(0, 0, 0);
            if (!(Double.IsInfinity(a.Second / divisor.Second) || Double.IsNaN(a.Second / divisor.Second)))
                resultTime.Second += a.Second / divisor.Second;
            else
                resultTime.Second = a.Second;
            float multi = (float)Math.Truncate(resultTime.Second / 60);
            if (multi != 0)
            {
                resultTime.Second += (multi * 60 * -1);
                resultTime.Minute += multi;
            }
            if (!(Double.IsInfinity(a.Minute / divisor.Minute) || Double.IsNaN(a.Minute / divisor.Minute)))
                resultTime.Minute += a.Minute / divisor.Minute;
            else
                resultTime.Minute = a.Minute;
            multi = (float)Math.Truncate((resultTime.Minute) / 60);
            if (multi != 0)
            {
                resultTime.Minute += multi * 60 * -1;
                resultTime.Hour += multi;
            }
            if (!(Double.IsInfinity(a.Hour / divisor.Hour) || Double.IsNaN(a.Hour / divisor.Hour)))
                resultTime.Hour += (a.Hour / divisor.Hour);
            else
                resultTime.Hour = a.Hour;
            return resultTime;
        }
        public static explicit operator string(Time origin)
        {
            return (origin.Hour.ToString(CultureInfo.InvariantCulture) + ":" +
                    origin.Minute.ToString(CultureInfo.InvariantCulture) + ":" +
                    origin.Second.ToString(CultureInfo.InvariantCulture));
        }
    }
    /// <summary>
    /// Class that is for Year, Month, Day
    /// </summary>
    public class Date
    {
        private static int[] _dayList = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public float Day { get; set; }
        public float Month { get; set; }
        public float Year { get; set; }
        public Date(float year, float month, float day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
        public static Date operator +(Date a, Date b)
        {
            var resultDate = new Date(0,0,0);
            resultDate.Day = a.Day + b.Day;
            resultDate.Month = a.Month;
            resultDate.Year = a.Year;
            while (resultDate.Day > _dayList[((int)resultDate.Month - 1) % 12])
            {
                resultDate.Day -= _dayList[((int)resultDate.Month - 1) % 12];
                ++resultDate.Month;
            }
            while (resultDate.Day < 1)
            {
                --resultDate.Month;
                if (resultDate.Month < 1)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                }
                resultDate.Day += _dayList[((int)resultDate.Month - 1) % 12];
            }
            resultDate.Month += b.Month; 
            if (resultDate.Month > 12 || resultDate.Month < 1)
            {
                int multi = (int)Math.Truncate(resultDate.Month / 12);
                resultDate.Month -= 12 * multi;
                resultDate.Year += multi;
                if (resultDate.Month <= 0)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                } 
            }
            resultDate.Year += b.Year;
            return resultDate; 
        }
        public static Date operator -(Date a, Date b)
        {
            var resultDate = new Date(0,0,0);
            resultDate.Day = a.Day - b.Day;
            resultDate.Month = a.Month;
            resultDate.Year = a.Year;
            while (resultDate.Day > _dayList[((int)resultDate.Month - 1) % 12])
            {
                resultDate.Day -= _dayList[((int)resultDate.Month - 1) % 12];
                ++resultDate.Month;
            }
            while (resultDate.Day < 1)
            {
                --resultDate.Month;
                if (resultDate.Month < 1)
                {
                    resultDate.Month += 12;
                    --resultDate.Year;
                }
                resultDate.Day += _dayList[((int)resultDate.Month - 1) % 12];
            }
            resultDate.Month -= b.Month; 
            if (resultDate.Month > 12 || resultDate.Month < 1)
            {
                int multi = (int)Math.Truncate(resultDate.Month / 12);
                resultDate.Month -= 12 * multi;
                resultDate.Year += multi;
                if (resultDate.Month <= 0)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                } 
            }
            resultDate.Year -= b.Year;
            return resultDate;
        }
        public static Date operator *(Date a, float multiplier)
        {
            var resultDate = new Date(0,0,0);
            resultDate.Day = a.Day * multiplier; 
            resultDate.Month = a.Month;
            resultDate.Year = a.Year;
            while (resultDate.Day > _dayList[((int)resultDate.Month - 1) % 12])
            {
                resultDate.Day -= _dayList[((int)resultDate.Month - 1) % 12];
                ++resultDate.Month;
            }
            while (resultDate.Day < 1)
            {
                --resultDate.Month;
                if (resultDate.Month < 1)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                }
                resultDate.Day += _dayList[((int)resultDate.Month - 1) % 12];
            }
            resultDate.Month *= multiplier; 
            if (resultDate.Month > 12 || resultDate.Month < 1)
            {
                int multi = (int)Math.Truncate(resultDate.Month / 12);
                resultDate.Month -= 12 * multi;
                resultDate.Year += multi;
                if (resultDate.Month <= 0)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                } 
            }
            resultDate.Year *= multiplier;
            return resultDate;
        }
        public static Date operator *(Date a, Date multiplier)
        {
            var resultDate = new Date(0,0,0);
            resultDate.Day = a.Day * multiplier.Day;
            resultDate.Month = a.Month;
            resultDate.Year = a.Year;
            while (resultDate.Day > _dayList[((int)resultDate.Month - 1) % 12])
            {
                resultDate.Day -= _dayList[((int)resultDate.Month - 1) % 12];
                ++resultDate.Month;
            }
            while (resultDate.Day < 1)
            {
                --resultDate.Month;
                if (resultDate.Month < 1)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                }
                resultDate.Day += _dayList[((int)resultDate.Month - 1) % 12];
            }
            resultDate.Month *= multiplier.Month; 
            if (resultDate.Month > 12 || resultDate.Month < 1)
            {
                int multi = (int)Math.Truncate(resultDate.Month / 12);
                resultDate.Month -= 12 * multi;
                resultDate.Year += multi;
                if (resultDate.Month <= 0)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                } 
            }
            resultDate.Year *= multiplier.Year;
            return resultDate;
        }

        public static Date operator /(Date a, Date divisor)
        {
            var resultDate = new Date(0,0,0);
            if (divisor.Day == 0) resultDate.Day = a.Day;
            else resultDate.Day = a.Day / divisor.Day;
            resultDate.Month = a.Month;
            resultDate.Year = a.Year;
            while (resultDate.Day > _dayList[((int)resultDate.Month - 1) % 12])
            {
                resultDate.Day -= _dayList[((int)resultDate.Month - 1) % 12];
                ++resultDate.Month;
            }
            while (resultDate.Day < 1)
            {
                --resultDate.Month;
                if (resultDate.Month < 1)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                }
                resultDate.Day += _dayList[((int)resultDate.Month - 1) % 12];
            }
            if (divisor.Month == 0) resultDate.Month = a.Month;
            else resultDate.Month = a.Month / divisor.Month; 
            if (resultDate.Month > 12 || resultDate.Month < 1)
            {
                int multi = (int)Math.Truncate(resultDate.Month / 12);
                resultDate.Month -= 12 * multi;
                resultDate.Year += multi;
                if (resultDate.Month <= 0)
                {
                    resultDate.Month += 12; 
                    --resultDate.Year;
                } 
            }
            if (divisor.Year == 0) resultDate.Year = a.Year;
            else resultDate.Year = a.Year / divisor.Year;
            return resultDate;
        }
        public static explicit operator string(Date origin)
        {
            return (origin.Year.ToString(CultureInfo.InvariantCulture) + "." +
                    origin.Month.ToString(CultureInfo.InvariantCulture) + "." +
                    origin.Day.ToString(CultureInfo.InvariantCulture));
        }
    }
}
