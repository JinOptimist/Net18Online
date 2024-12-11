using WebPortalEverthing.Models.CoffeShop;

namespace WebPortalEverthing.Models.Brand
{
    public class BrandShortInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Company { get; set; }

        public List<CoffeNameAndIdViewModel> CoffeList { get; set; } = new();
    }
}
