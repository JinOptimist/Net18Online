using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class MinStringLengthAttribute : ValidationAttribute
    {
        private int _minLength;

        public MinStringLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"Не соответствует минимальной длинне {_minLength}"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            var valueString = value as string;

            if (valueString == null)
            {
                return false;
            }

            if (valueString.Length < _minLength)
            {
                return false;
            }

            return true;
        }
    }
}
