using Everything.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class UniqBrandAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value as string;
            if (name is null)
            {
                return new ValidationResult("Not a string");
            }

            var repository = validationContext.GetRequiredService<IBrandRepositoryReal>();
            if (!repository.IsBrandUniq(name))
            {
                return new ValidationResult("Not uniq name");
            }
            return ValidationResult.Success;
        }
    }
}
