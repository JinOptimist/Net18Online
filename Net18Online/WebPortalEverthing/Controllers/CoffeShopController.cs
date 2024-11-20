using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Models.CoffeShop;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
	public class CoffeShopController : Controller
	{
		private const int MINIMAL_AGE = 16;
		private IKeyCoffeShopRepository _coffeShopRepository;
		private IBrandRepositoryReal _brandRepositoryReal;
		private IUserRepositryReal _userRepositryReal;
		private AuthService _authService;

		public CoffeShopController(IKeyCoffeShopRepository coffeShopRepository, IBrandRepositoryReal brandRepositoryReal, IUserRepositryReal userRepositryReal, AuthService authService)
		{
			_coffeShopRepository = coffeShopRepository;
			_brandRepositoryReal = brandRepositoryReal;
			_userRepositryReal = userRepositryReal;
			_authService = authService;

		}

		public IActionResult Index()
		{
			var userId = _authService.IsAdmin();

			if (!userId)
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

		public IActionResult Delete(int id)
		{
			_coffeShopRepository.Delete(id);
			return RedirectToAction("Index");
		}

		public IActionResult UpdateCoffe(int id, string name)
		{
			_coffeShopRepository.UpdateCoffeName(id, name);
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
	}
}