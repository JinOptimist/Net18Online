namespace WebPortalEverthing.Models.CoffeShop.Profile
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }

        public string ProfileAvatar { get; set; }

        public List<CoffeShortViewModel> Coffe { get; set; }

        public List<CoffeObjectViewModel> CoffeInCart { get; set; }
    }
}