using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.CoffeShop.Profile
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }

        [AllowedExtensions([".jpg", ".png"])]
        public string ProfileAvatar { get; set; }

        public List<CoffeShortViewModel> Coffe { get; set; }
    }
}