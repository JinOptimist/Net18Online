using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.Ecology
{
    public class PostCreationViewModel
    {
        [IsUrl(ErrorMessage = "This URL is invalid")]
        public string Url { get; set; }
        
        [EcologyText]
        [IsCorrectLength(15)]
        public string Text { get; set;}
    }
}