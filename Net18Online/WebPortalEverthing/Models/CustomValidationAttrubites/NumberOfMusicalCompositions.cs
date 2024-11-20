using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.MoviePoster;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class NumberOfMusicalCompositions : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            var viewModel = validationContext.ObjectInstance as MovieCreationViewModel;
            if (viewModel is null)
            {
                return new ValidationResult("Not a view model");
            }

            if (viewModel.NumberOfMusicalCompositions < 3 && viewModel.FilmDuration >= 100)
            {
                return new ValidationResult("Too few music compositions");
            }

            return ValidationResult.Success;
        }
    }
}
