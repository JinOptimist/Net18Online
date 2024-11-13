using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class IsDecimalNumberPositiveAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? "Значение не может быть отрицательнным"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }

            if (value is not decimal decimalValue)
            {
                return false;
            }

            if (decimalValue <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
