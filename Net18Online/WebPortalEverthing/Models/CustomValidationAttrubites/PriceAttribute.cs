using Everything.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.Cake;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class PriceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var viewModel = validationContext.ObjectInstance as CakeCreationViewModel;
            if (viewModel is null)
            {
                return new ValidationResult("not a view model");
            }

            var repository = validationContext.GetRequiredService<ICakeRepositoryReal>();
            if (viewModel.Price < repository.QuantityWords(viewModel.Description) * 1.5m)
            {
                return new ValidationResult("Low price");
            }

            return ValidationResult.Success;
        }
    }
}
