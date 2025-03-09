using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.Brand
{
    public class CreateBrandViewModel
    {
        [UniqBrand]
        public string Name { get; set; }
        [Required]
        public string Company { get; set; }
    }
}
