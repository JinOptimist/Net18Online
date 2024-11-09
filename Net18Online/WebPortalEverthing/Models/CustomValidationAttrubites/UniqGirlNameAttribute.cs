using Everything.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class UniqGirlNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value, 
            ValidationContext validationContext)
        {
            var name = value as string;
            if (name == null)
            {
                return new ValidationResult("Not a string");
            }

            var repository = validationContext.GetRequiredService<IAnimeGirlRepositoryReal>();
            if (!repository.IsNameUniq(name))
            {
                return new ValidationResult("Not uniq name");
            }

            return ValidationResult.Success;
        }
    }
}
