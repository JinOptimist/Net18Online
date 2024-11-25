using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Models.Cake;
using WebPortalEverthing.Models.Magazin;

namespace WebPortalEverthing.Models.CakeLink
{
    public class CakeAndMagazinLinkViewModel
    {
        public int CakeId { get; set; }
        public int MagazinId { get; set; }
        public List<SelectListItem> Cakes { get; set; }
        public List<SelectListItem> Magazins { get; set; }
    }
}
