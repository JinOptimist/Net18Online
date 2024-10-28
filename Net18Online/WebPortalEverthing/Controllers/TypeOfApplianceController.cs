using Everything.Data.Fake.Models;
using Everything.Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverything.Models.ServiceCenter;

namespace WebPortalEverything.Controllers
{
    public class TypeOfApplianceController : Controller
    {
        private readonly ITypeOfApplianceRepository _typeOfApplianceRepository;

        public TypeOfApplianceController(ITypeOfApplianceRepository typeOfApplianceRepository)
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

                    // Ensure the directory exists
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

                    // Save to JSON file
                    string jsonFilePath = Path.Combine("Data", "ServiceCenter", "typeOfAppliance.json");
                    _typeOfApplianceRepository.SaveDataToJson(jsonFilePath);
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Please upload an image.");
                }
            }
            return View(viewModel);
        }
    }
}
