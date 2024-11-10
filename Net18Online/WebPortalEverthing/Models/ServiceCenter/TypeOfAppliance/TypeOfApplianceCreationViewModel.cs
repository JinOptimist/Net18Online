using System.ComponentModel.DataAnnotations;

namespace WebPortalEverything.Models.ServiceCenter
{
    public class TypeOfApplianceCreationViewModel
    {
        [Required(ErrorMessage = "Имя обязательно.")]
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; } // Property to hold the uploaded image file
    }
}
