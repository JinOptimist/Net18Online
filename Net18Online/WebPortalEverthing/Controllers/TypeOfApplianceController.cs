using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using WebPortalEverything.Models.ServiceCenter;

namespace WebPortalEverthing.Controllers
{
    public class TypeOfApplianceController : Controller
    {
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/ServiceCenter/typeOfAppliance.json");

        /// <summary>
        /// Method to read appliances from the JSON file
        /// </summary>
        private List<TypeOfApplianceViewModel> ReadTypeOfAppliancesFromJson()
        {
            if (!System.IO.File.Exists(_jsonFilePath))
            {
                return new List<TypeOfApplianceViewModel>(); // Return an empty list if file doesn't exist
            }

            var jsonData = System.IO.File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<List<TypeOfApplianceViewModel>>(jsonData) ?? new List<TypeOfApplianceViewModel>();
        }

        /// <summary>
        /// Method to save appliances to the JSON file
        /// </summary>
        private void SaveTypeOfAppliancesToJson(List<TypeOfApplianceViewModel> appliances)
        {
            var jsonData = JsonConvert.SerializeObject(appliances, Formatting.Indented);
            System.IO.File.WriteAllText(_jsonFilePath, jsonData);

            Console.WriteLine($"Saved appliances to JSON: {jsonData}");
        }


        public IActionResult AllTypeOfAppliances()
        {
            var appliances = ReadTypeOfAppliancesFromJson();
            return View(appliances);
        }

        [HttpGet]
        public IActionResult CreateTypeOfAppliance()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTypeOfAppliance(TypeOfApplianceCreationViewModel viewModel, IFormFile imageFile)
        {
            if (ModelState.IsValid) 
            {
                if (imageFile != null && imageFile.Length > 0) 
                {
                    var filePath = Path.Combine("wwwroot/images/ServiceCenter/TypeOfAppliances", imageFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    var appliances = ReadTypeOfAppliancesFromJson();
                    var newAppliance = new TypeOfApplianceViewModel
                    {
                        Id = appliances.Any() ? appliances.Max(a => a.Id) + 1 : 1, 
                        Name = viewModel.Name,
                        ImageSrc = $"/images/ServiceCenter/TypeOfAppliances/{imageFile.FileName}" 
                    };

                    appliances.Add(newAppliance);
                    SaveTypeOfAppliancesToJson(appliances); 

                    return RedirectToAction("AllTypeOfAppliances"); 
                }

                ModelState.AddModelError("", "Пожалуйста, выберите файл изображения.");
            }
            return View(viewModel); 
        }
    }
}
