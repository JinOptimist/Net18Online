using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.AnimeGirl;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class WeightAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value, 
            ValidationContext validationContext)
        {
            var viewModel = validationContext.ObjectInstance as GirlCreationViewModel;
            if (viewModel is null)
            {
                return new ValidationResult("not a view model");
            }

            if (viewModel.Weight > viewModel.Height - 100)
            {
                return new ValidationResult("To big");
            }

            return ValidationResult.Success;
        }
    }
}
