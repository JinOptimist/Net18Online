using Everything.Data;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.Cake;
using WebPortalEverthing.Models.Cake.MyCreation;
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
                    Id = dbCake.Id,
                    ImageSrc = dbCake.ImageSrc,
                    Name = dbCake.Name,
                    Description = dbCake.Description,
                    Price = dbCake.Price,
                    CreatorName = dbCake.Creator.Login ?? "None",
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
        [IsAuthenticated]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [IsAuthenticated]
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
        [IsAuthenticated]
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
        [IsAuthenticated]
        public IActionResult Link(CakeAndMagazinLinkViewModel viewModel)
        {
            _cakeRepository.Link(viewModel.CakeId, viewModel.MagazinId);

            return RedirectToAction("Link");
        }
        [IsAdmin]
        public IActionResult Admin()
        {
            var cakesFromDb = _webDbContext.Cakes;

            var cakesViewModel = cakesFromDb.Select(dbCake => new CakeViewModel
            {
                Id = dbCake.Id,
                ImageSrc = dbCake.ImageSrc,
                Name = dbCake.Name,
                Description = dbCake.Description,
                Price = dbCake.Price,
                CreatorName = dbCake.Creator.Login ?? "None",
                Magazins = dbCake.Magazins
                                .Select(dbMagazin => new MagazinViewModel
                                {
                                    Name = dbMagazin.Name ?? "Not in stores",
                                })
                                .ToList(),
            }).ToList();

            return View(cakesViewModel);
        }
        [HttpGet]
        [IsAuthenticated]
        public IActionResult MyCreation()
        {
            var myCreationViewModel = new MyCreationViewModel();

            var userId = _authService.GetUserId();

            myCreationViewModel.Cakes = _cakeRepository.GetMyCakesICreated(userId)
                                                        .Select(cake => new CakeShortInfoViewModel()
                                                        {
                                                            Id = cake.Id,
                                                            ImageSrc = cake.ImageSrc,
                                                            Name = cake.Name,
                                                            Description = cake.Description,
                                                            Price = cake.Price,
                                                        }).ToList();

            myCreationViewModel.Magazins = _magazinRepository.GetMyMagazinsICreated(userId)
                                                                .Select(magazin => new MagazinShortInfoViewModel()
                                                                {
                                                                    Id = magazin.Id,
                                                                    Name = magazin.Name,
                                                                }).ToList();

            return View(myCreationViewModel);
        }
        [HttpPost]
        [IsAuthenticated]
        public IActionResult UpdateName(int id, string newName)
        {
            _cakeRepository.UpdateName(id, newName);

            if(_authService.IsAdmin())
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("MyCreation");
        }
        [HttpPost]
        [IsAuthenticated]
        public IActionResult UpdateImage(int id, string newImage)
        {
            _cakeRepository.UpdateImage(id, newImage);
            
            if (_authService.IsAdmin())
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("MyCreation");
        }
        [HttpPost]
        [IsAuthenticated]
        public IActionResult UpdateDescription(int id, string newDescription)
        {
            _cakeRepository.UpdateDescription(id, newDescription);
            
            if (_authService.IsAdmin())
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("MyCreation");
        }
        [HttpPost]
        [IsAuthenticated]
        public IActionResult UpdatePrice(int id, decimal newPrice)
        {
            _cakeRepository.UpdatePrice(id, newPrice);
            
            if (_authService.IsAdmin())
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("MyCreation");
        }
        [IsAuthenticated]
        public IActionResult Remove(int id)
        {
            _cakeRepository.Delete(id);

            if (_authService.IsAdmin())
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("MyCreation");
        }
    }
}
