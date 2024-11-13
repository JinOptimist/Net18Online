using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.AnimeCatalog
{
    public class AnimeCatalogCreationViewModel
    {
        public string Name { get; set; }
        [IsUrl]
        public string ImageSrc { get; set; }
    }
}
