using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.CoffeShop
{
    public class CoffeCreateViewModel
    {
        [IsUrl(ErrorMessage = "Это не ссылка")]
        public string Url { get; set; }

        [MinStringLength(3)]
        public string Coffe { get; set; }

        [IsDecimalNumberPositive]
        public decimal Cost { get; set; }

        public int BrandId { get; set; }
        public List<SelectListItem>? Brands { get; set; }
    }
}