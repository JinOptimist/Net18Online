using Everything.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class QuantityWordsAttribute : ValidationAttribute
    {
        private int _minQuantityWords;

        public QuantityWordsAttribute()
        {
            _minQuantityWords = 50;
        }

        public QuantityWordsAttribute(int minQuantityWords)
        {
            _minQuantityWords = minQuantityWords;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var description = value as string;
            if (description == null)
            {
                return new ValidationResult("Not a string");
            }

            var repository = validationContext.GetRequiredService<HelperForValidatingCake>();
            if (repository.QuantityWords(description) < _minQuantityWords)
            {
                return new ValidationResult("The number of words is not allowed");
            }

            return ValidationResult.Success;
        }
    }
}
