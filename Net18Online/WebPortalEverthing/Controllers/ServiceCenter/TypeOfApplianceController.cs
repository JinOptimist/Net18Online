using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverything.Models.ServiceCenter;

namespace WebPortalEverything.Controllers
{
    public class TypeOfApplianceController : Controller
    {
        private readonly ITypeOfApplianceRepositoryReal _typeOfApplianceRepository;

        public TypeOfApplianceController(ITypeOfApplianceRepositoryReal typeOfApplianceRepository)
        {
            _typeOfApplianceRepository = typeOfApplianceRepository;
        }

        public IActionResult AllTypeOfAppliances()
        {
            var appliances = _typeOfApplianceRepository.GetAll()
                .Select(appliance => new TypeOfApplianceViewModel
                {
                    Id = appliance.Id,
                    Name = appliance.Name,
                    ImageSrc = appliance.ImageSrc
                })
                .ToList();

            return View(appliances);
        }

        [HttpGet]
        public IActionResult CreateTypeOfAppliance()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTypeOfAppliance(TypeOfApplianceCreationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
                {
                    var directoryPath = Path.Combine("wwwroot/images/ServiceCenter/TypeOfAppliances");

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, viewModel.ImageFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ImageFile.CopyToAsync(stream);
                    }

                    var appliance = new TypeOfApplianceData
                    {
                        Name = viewModel.Name,
                        ImageSrc = $"/images/ServiceCenter/TypeOfAppliances/{viewModel.ImageFile.FileName}"
                    };

                    _typeOfApplianceRepository.Add(appliance);
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Please upload an image.");
                    return View(viewModel);
                }

                return RedirectToAction("AllTypeOfAppliances");
            }

            return View(viewModel);
        }

        public IActionResult UpdateName(int id, string newName)
        {
            _typeOfApplianceRepository.UpdateName(id, newName);
            return RedirectToAction("AllTypeOfAppliances");
        }

        [HttpPost]
        public IActionResult UpdateImage(int id, IFormFile newImage)
        {
            if (newImage == null || newImage.Length == 0)
            {
                ModelState.AddModelError("ImageSrc", "Image cannot be empty.");
                return RedirectToAction("AllTypeOfAppliances"); 
            }

            string imageUrl = SaveImage(newImage);
            if (string.IsNullOrEmpty(imageUrl))
            {
                ModelState.AddModelError("ImageSrc", "Error saving the image.");
                return RedirectToAction("AllTypeOfAppliances");
            }

            _typeOfApplianceRepository.UpdateImage(id, imageUrl);

            return RedirectToAction("AllTypeOfAppliances");
        }

        ///<summary>
        ///Example method to save the uploaded image
        ///</summary>
        private string SaveImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null; 
            }

            var uploadsFolder = Path.Combine("wwwroot/images/ServiceCenter/TypeOfAppliances");

            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return $"/images/ServiceCenter/TypeOfAppliances/{fileName}";
        }


        public IActionResult Remove(int id)
        {
            _typeOfApplianceRepository.Delete(id);
            return RedirectToAction("AllTypeOfAppliances");
        }
    }
}
