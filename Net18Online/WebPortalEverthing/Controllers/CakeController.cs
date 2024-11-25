using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Models.Cake;
using WebPortalEverthing.Models.CakeLink;
using WebPortalEverthing.Models.Magazin;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class CakeController : Controller
    {
        private ICakeRepositoryReal _cakeRepository;
        private IMagazinRepositoryReal _magazinRepository;
        private AuthService _authService;
        private WebDbContext _webDbContext;

        public CakeController(ICakeRepositoryReal cakeRepository, IMagazinRepositoryReal magazinRepository, WebDbContext webDbContext, AuthService authService)
        {
            _cakeRepository = cakeRepository;
            _webDbContext = webDbContext;
            _magazinRepository = magazinRepository;
            _authService = authService;
        }
        public IActionResult Index()
        {
            var cakesFromDb = _webDbContext
                .Cakes;

            var cakesViewModel = cakesFromDb
                .Select(dbCake => new CakeViewModel
                {
                    ImageSrc = dbCake.ImageSrc,
                    Name = dbCake.Name,
                    Description = dbCake.Description,
                    Price = dbCake.Price,
                    Magazins = dbCake.Magazins
                                .Select(dbMagazin => new MagazinViewModel
                                {
                                    Name = dbMagazin.Name,
                                })
                                .ToList(),
                }).ToList();

            return View(cakesViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CakeCreationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var currentUserId = _authService.GetUserId();

            var cakeData = new CakeData()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ImageSrc = viewModel.Url,
            };

            _cakeRepository.Create(cakeData, currentUserId!.Value);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Link()
        {
            var model = new CakeAndMagazinLinkViewModel()
            {
                Cakes = _cakeRepository.GetAll()
                .Select(cake => new SelectListItem()
                { Value = cake.Id.ToString(), Text = cake.Name })
                .ToList(),
                Magazins = _magazinRepository.GetAll()
                .Select(magazin => new SelectListItem()
                { Value = magazin.Id.ToString(), Text = magazin.Name })
                .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Link(CakeAndMagazinLinkViewModel viewModel)
        {
            _cakeRepository.Link(viewModel.CakeId, viewModel.MagazinId);

            return RedirectToAction("Link");
        }
    }
}
