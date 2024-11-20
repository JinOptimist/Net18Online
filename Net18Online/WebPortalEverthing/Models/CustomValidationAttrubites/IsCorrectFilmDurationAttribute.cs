using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class IsCorrectFilmDurationAttribute : ValidationAttribute
    {
        private int _min;
        private int _max;
        private DurationOption _option;

        public IsCorrectFilmDurationAttribute()
        {
            _min = 60;
            _max = 180;
            _option = DurationOption.Minutes;
        }

        public IsCorrectFilmDurationAttribute(int min, int max)
        {
            _min = min;
            _max = max;
            _option = DurationOption.Minutes;
        }

        public IsCorrectFilmDurationAttribute(int min, int max, DurationOption option)
        {
            _min = min;
            _max = max;
            _option = option;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Продолжительность фильма должна быть от {_min} до {_max} {GetFilmDurationOptionDisplayName()}"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is not int)
            {
                return false;
            }

            var filmDuration = (int)value;

            if (filmDuration < _min)
            {
                return false;
            }

            if (filmDuration > _max)
            {
                return false;
            }

            return true;
        }

        private string GetFilmDurationOptionDisplayName()
        {
            switch (_option)
            {
                case DurationOption.Minutes:
                    return "Минут";
                case DurationOption.Hours:
                    return "Часов";
            }

            throw new NotImplementedException();
        }
    }
    public enum DurationOption
    {
        Minutes,
        Hours
    }
}
