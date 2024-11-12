using System.ComponentModel.DataAnnotations;
using Everything.Data.Repositories;

namespace WebPortalEverthing.Models.CustomValidationAttrubites;

public class EcologyTextAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value, 
        ValidationContext validationContext)
    {
        var text = value as string;
        if (text == null)
        {
            return new ValidationResult("Not a string");
        }

        var repository = validationContext.GetRequiredService<IEcologyRepositoryReal>();
        if (!repository.IsEclogyTextHas(text))
        {
            return new ValidationResult("Not uniq text");
        }

        return ValidationResult.Success;
    }
}