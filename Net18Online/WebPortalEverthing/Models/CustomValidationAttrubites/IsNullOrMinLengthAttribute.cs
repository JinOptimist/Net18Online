using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class IsNullOrMinLengthAttribute : ValidationAttribute
    {
        private int _minLength;

        public IsNullOrMinLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Поле может быть пустым или содержать {_minLength} или более символов"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is not string)
            {
                return false;
            }

            var valueString = (string)value;

            if (valueString.Length < _minLength)
            {
                return false;
            }

            return true;
        }
    }
}
