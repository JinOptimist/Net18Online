using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.GameStore
{
    public class AddGameViewModel
    {
        [Required]
        public string Name { get; set; }
        [IsUrl]
        public string Url { get; set; }
        [CorrectCost(1, 100, CurrencyOption.Dollar)]
        public int Cost { get; set; }
    }
}
