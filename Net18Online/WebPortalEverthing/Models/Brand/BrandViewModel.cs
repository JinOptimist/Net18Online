using WebPortalEverthing.Models.CoffeShop;

namespace WebPortalEverthing.Models.Brand
{
    public class BrandViewModel
    {
        public List<BrandShortInfoViewModel> Brands { get; set; }

        public List<CoffeNameAndIdViewModel> Coffe { get; set; }
    }
}
