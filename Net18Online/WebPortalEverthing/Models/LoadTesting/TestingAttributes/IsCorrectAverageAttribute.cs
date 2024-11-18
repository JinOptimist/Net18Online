using System;
using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.LoadTesting.TestingAttributes
{
    public class IsCorrectAverageAttribute : ValidationAttribute
    {
        private double _min;
        private double _max;
        private UnitLoad _option;
        private LoadLevel _level;

        public IsCorrectAverageAttribute()
        {
            _min = 0.01;
            _max = 20000;
            _option = UnitLoad.Seconds;
        }

        public IsCorrectAverageAttribute(double min, double max)
        {
            _min = min;
            _max = max;
            _option = UnitLoad.Seconds;
        }

        public IsCorrectAverageAttribute(double min, double max, UnitLoad option, LoadLevel level)
        {
            _min = min;
            _max = max;
            _option = option;
            _level = level;
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
            if (value is not double)
            {
                return false;
            }

            var average = (double)value;

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
                    return "Секунды";
                case UnitLoad.Milliseconds:
                    return "Миллисекунды";
                case UnitLoad.Minutes:
                    return "Минуты";
                case UnitLoad.Hours:
                    return "Часы";
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
