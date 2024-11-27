using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.Ecology
{
    public class PostCreationViewModel
    {
        [IsUrl(
            ErrorMessageResourceType = typeof(Localizations.Ecology), 
            ErrorMessageResourceName = "ValidationMessage_IsInvalidUrl")]
        //[IsUrl(ErrorMessage = "This URL is invalid")]
        public string Url { get; set; }
        
        [Required(ErrorMessage = "Text is required.")]
        [EcologyText, IsCorrectLength(15)] 
        public string Text { get; set;}
        public int PostId { get; set; }
        public List<SelectListItem>? Posts { get; set; }
        
        [MaxFileSize(52428800)]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Please upload a valid image file (jpg, jpeg, png).")]
        public IFormFile ImageFile { get; set; }
        
    }
}