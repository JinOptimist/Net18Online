using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.Cake
{
    public class CakeCreationViewModel
    {
        [UniqCakeUrl]
        public string Url { get; set; }
        [Required]
        [QuantityWords(10)]
        public string Description { get; set; }
        [Price]
        public decimal Price { get; set; }
    }
}
