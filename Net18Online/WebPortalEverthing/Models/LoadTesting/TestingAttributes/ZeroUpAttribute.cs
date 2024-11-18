using Everything.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebPortalEverthing.Models.LoadTesting.TestingAttributes
{
    public class ZeroUpAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Not a number");
            }

            if (value is decimal number|| value is double|| value is float|| value is int) // Проверка на соответствие типу double/decimal
            {
                return new ValidationResult("Invalid data type");
            }
            if ((decimal)value <= 0)
            {
                return new ValidationResult("Number must be positive");
            }
            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? "Number must be positive"
                : base.ErrorMessage;
        }
    }
}
