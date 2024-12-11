using Enums.Users;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.CoffeShop;
using WebPortalEverthing.Models.CoffeShop.Profile;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class CoffeShopController : Controller
    {
        private IKeyCoffeShopRepository _coffeShopRepository;
        private IBrandRepositoryReal _brandRepositoryReal;
        private IUserRepositryReal _userRepositryReal;
        private AuthService _authService;
        private IWebHostEnvironment _webHostEnvironment;

        public CoffeShopController(IKeyCoffeShopRepository coffeShopRepository, IBrandRepositoryReal brandRepositoryReal, IUserRepositryReal userRepositryReal, AuthService authService, IWebHostEnvironment webHostEnvironment)
        {
            _coffeShopRepository = coffeShopRepository;
            _brandRepositoryReal = brandRepositoryReal;
            _userRepositryReal = userRepositryReal;
            _authService = authService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var isAdmin = _authService.IsAdmin();

            if (!isAdmin)
            {
                return RedirectToAction("Coffe");
            }

            var viewModels = CoffeView();

            return View(viewModels);
        }

        public List<CoffeViewModel> CoffeView()
        {
            var valuesCoffeFromDb = _coffeShopRepository.GetAllWithCreatorsAndBrand();

            var userId = _authService.GetUserId();

            var viewModels = valuesCoffeFromDb
                .Select(coffeFromDb =>
                    new CoffeViewModel
                    {
                        Id = coffeFromDb.Id,
                        Coffe = coffeFromDb.Coffe,
                        Url = coffeFromDb.Url,
                        Cost = coffeFromDb.Cost,
                        CreatorName = coffeFromDb.Creator?.Login ?? "Неизвестный",
                        Brand = coffeFromDb.Brand?.Name ?? "MaxWell",
                        CanDeleteOrUpdate = coffeFromDb.Creator is null
                            || coffeFromDb.Creator?.Id == userId
                    }
                ).ToList();

            return viewModels;
        }

        public IActionResult Coffe()
        {
            var viewModels = CoffeView();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CoffeCreateViewModel();

            viewModel.Brands = _brandRepositoryReal
                .GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CoffeCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Brands = _brandRepositoryReal
                .GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
                return View(viewModel);
            }

            var currentUserId = _authService.GetUserId();

            var coffe = new CoffeData
            {
                Coffe = viewModel.Coffe,
                Url = viewModel.Url,
                Cost = viewModel.Cost
            };

            _coffeShopRepository.Create(coffe, currentUserId!.Value, viewModel.BrandId);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateCost(int id, decimal cost)
        {
            _coffeShopRepository.UpdateCost(id, cost);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateUrl(int id, string url)
        {
            _coffeShopRepository.UpdateImage(id, url);
            return RedirectToAction("Index");
        }

        [IsAuthenticated]
        public IActionResult UserProfile()
        {
            var viewModel = new ProfileViewModel();

            viewModel.UserName = _authService.GetName()!;

            var userId = _authService.GetUserId()!.Value;

            viewModel.ProfileAvatar = _userRepositryReal.GetAvatarUrl(userId);

            viewModel.Coffe = _coffeShopRepository
                .GetAllByCreatorId(userId)
                .Select(x => new CoffeShortViewModel
                {
                    Name = x.Coffe,
                    Url = x.Url,
                })
                .ToList();

            return View(viewModel);
        }

        [IsAuthenticated]
        [HttpPost]
        public IActionResult UpdateProfileAvatar(IFormFile avatar)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;

            var userId = _authService.GetUserId()!.Value;
            var avatarFileName = $"avatar-{userId}.jpg";

            var path = Path.Combine(webRootPath, "images", "avatars", avatarFileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                avatar
                    .CopyToAsync(fileStream)
                    .Wait();
            }

            var avatarUrl = $"/images/avatars/{avatarFileName}";
            _userRepositryReal.UpdateAvatarUrl(userId, avatarUrl);

            return RedirectToAction("UserProfile");
        }

        [IsAuthenticated]
        public IActionResult SetDefaultProfileAvatar()
        {
            var userId = _authService.GetUserId()!.Value;
            var avatarUrl = "/images/CoffeShop/default_user_profile.jpg";

            _userRepositryReal.UpdateAvatarUrl(userId, avatarUrl);

            return RedirectToAction("UserProfile");
        }

        public IActionResult UpdateLocale(Language language)
        {
            var userId = _authService.GetUserId()!.Value;
            _userRepositryReal.UpdateLocal(userId, language);

            return RedirectToAction("Coffe");
        }
    }
}