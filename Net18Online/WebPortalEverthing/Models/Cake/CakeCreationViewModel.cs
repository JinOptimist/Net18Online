using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.Cake
{
    public class CakeCreationViewModel
    {
        public const long FILE_SIZE_IS_ONE_MB = 1024 * 1024;
        [Required]
        public string Name { get; set; }
        [IsImageForCake(FILE_SIZE_IS_ONE_MB)]
        public IFormFile Url { get; set; }
        [Required]
        [QuantityWords(10)]
        public string Description { get; set; }
        [Price]
        public decimal Price { get; set; }
    }
}
