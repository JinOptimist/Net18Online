﻿using System;
using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.CustomValidationAttrubites;
using WebPortalEverthing.Localizations;

namespace WebPortalEverthing.Models.LoadTesting.TestingAttributes
{
    public class IsCorrectAverageAttribute : ValidationAttribute
    {
        private decimal _min;
        private decimal _max;
        private UnitLoad _option;
        private LoadLevel _level;

        public IsCorrectAverageAttribute(decimal min, decimal max, UnitLoad option, LoadLevel level)
        {
            _min = 0.01m;
            _max = 20000m;
            _option = UnitLoad.Seconds;
        }

        public IsCorrectAverageAttribute(double min, double max)
        {
            _min = (decimal)min;
            _max = (decimal)max;
            _option = UnitLoad.Seconds;
        }

        public IsCorrectAverageAttribute(double min, double max, UnitLoad option, LoadLevel level)
        {
            _min = (decimal)min;
            _max = (decimal)max;
            _option = option;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Average must have diapazon [{_min}, {_max}] {GetUnitLoadOptionDisplayName()}"
                + $" and level {GetLevelLoadOptionDisplayName()}"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is not decimal)
            {
                return false;
            }

            var average = (decimal)value;

            if (average < _min)
            {
                return false;
            }

            if (average > _max)
            {
                return false;
            }

            return true;
        }

        private string GetUnitLoadOptionDisplayName()
        {
            switch (_option)
            {
                case UnitLoad.Seconds:
                    return LoadStuffs.Seconds;
                //  return "Секунды";
                case UnitLoad.Milliseconds:
                    return LoadStuffs.MilliSeconds;
                //   return "Миллисекунды";
                case UnitLoad.Minutes:
                    return LoadStuffs.Minutes;
                //   return "Минуты";
                case UnitLoad.Hours:
                    return LoadStuffs.Hours;
                    //   return "Часы";
            }

            throw new NotImplementedException();
        }

        private string GetLevelLoadOptionDisplayName()
        {
            switch (_level)
            {
                case LoadLevel.Lowest:
                    return "Lowest";
                case LoadLevel.Low:
                    return "Low";
                case LoadLevel.Medium:
                    return "Medium";
                case LoadLevel.High:
                    return "High";
                case LoadLevel.Critical:
                    return "Critical";
                case LoadLevel.Blocker:
                    return "Blocker";
            }

            throw new NotImplementedException();
        }
    }

    public enum LoadLevel
    {
        Lowest,
        Low,
        Medium,
        High,
        Critical,
        Blocker,
    }

    public enum UnitLoad
    {
        Milliseconds,
        Seconds,
        Minutes,
        Hours,
    }
}
