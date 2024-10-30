using Everything.Data;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverything.Models.ServiceCenter;

namespace WebPortalEverything.Controllers
{
    public class TypeOfApplianceController : Controller
    {
        private readonly ITypeOfApplianceRepository _typeOfApplianceRepository;
        private readonly WebDbContext _webDbContext;

        public TypeOfApplianceController(ITypeOfApplianceRepository typeOfApplianceRepository, WebDbContext webDbContext)
        {
            _typeOfApplianceRepository = typeOfApplianceRepository;
            _webDbContext = webDbContext;
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

                    await _webDbContext.TypeOfAppliances.AddAsync(appliance);
                    await _webDbContext.SaveChangesAsync();
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
