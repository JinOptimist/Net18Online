using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class IsUrlAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? "Не правильный урл"
                : ErrorMessage;
        }

        public override bool IsValid(object? value)
        {
            var url = value as string;
            if (url == null)
            {
                return false;
            }

            if (!url.ToLower().StartsWith("http"))
            {
                return false;
            }

            return true;
        }
    }
}
