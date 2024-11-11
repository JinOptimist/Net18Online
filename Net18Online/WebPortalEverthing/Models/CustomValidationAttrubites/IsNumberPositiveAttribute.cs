﻿using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class IsNumberPositiveAttribute : ValidationAttribute
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

            if (value is not int intValue)
            {
                return false;
            }

            if (value is not float floatValue)
            {
                return false;
            }

            if (value is not double doubleValue)
            {
                return false;
            }

            if (decimalValue <= 0 || intValue <= 0 || floatValue <= 0 || doubleValue <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
