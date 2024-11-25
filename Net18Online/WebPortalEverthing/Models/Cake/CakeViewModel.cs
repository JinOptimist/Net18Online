using WebPortalEverthing.Models.Magazin;

namespace WebPortalEverthing.Models.Cake
{
    public class CakeViewModel
    {
        public string ImageSrc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MagazinViewModel>? Magazins { get; set; }
        public decimal Price { get; set; }
    }
}
