using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Models.CakeLink;
using WebPortalEverthing.Models.Magazin;

namespace WebPortalEverthing.Controllers
{
    public class MagazinController : Controller
    {
        public IMagazinRepositoryReal _magazinRepository;
        public MagazinController(IMagazinRepositoryReal magazinRepository) 
        {
            _magazinRepository = magazinRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MagazinViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dataMagazin = new MagazinData()
            {
                Name = viewModel.Name,
            };

            _magazinRepository.Add(dataMagazin);

            return RedirectToAction("Index", "Cake");
        }
    }
}
