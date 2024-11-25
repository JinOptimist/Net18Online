using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.CustomValidationAttrubites
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        public override bool IsValid(object? value)
        {
            if (value is not IFormFile file)
            {
                return false;
            }

            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
               ? $"Этот формат фото не поддерживается. {_extensions} - примеры поддерживаемых"
               : ErrorMessage;
        }
    }

}