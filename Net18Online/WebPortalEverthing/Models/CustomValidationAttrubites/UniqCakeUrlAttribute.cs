using Everything.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class UniqCakeUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var url = value as string;
            if (url == null)
            {
                return new ValidationResult("Not a string");
            }

            var repository = validationContext.GetRequiredService<ICakeRepositoryReal>();
            if (!repository.IsUrlUniq(url))
            {
                return new ValidationResult("Not uniq url");
            }

            return ValidationResult.Success;
        }
    }
}
