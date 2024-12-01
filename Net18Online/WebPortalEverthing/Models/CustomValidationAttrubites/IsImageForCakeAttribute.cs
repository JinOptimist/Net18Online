using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class IsImageForCakeAttribute : ValidationAttribute
    {
        public const string FILE_FORMAT = ".jpg";
        private long _maxFileSize;

        public IsImageForCakeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return new ValidationResult("No file specified");
            }

            var extension = Path.GetExtension(file.FileName);
            if (extension != FILE_FORMAT)
            {
                return new ValidationResult($"The file format is not correct, the format should be: {FILE_FORMAT}");
            }

            if (file.Length >= _maxFileSize)
            {
                return new ValidationResult($"The file size must not exceed {_maxFileSize} B");
            }

            return ValidationResult.Success;
        }
    }
}
