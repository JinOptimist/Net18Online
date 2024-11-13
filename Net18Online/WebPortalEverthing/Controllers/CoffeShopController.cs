using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.CoffeShop;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
	public class CoffeShopController : Controller
	{
		private IKeyCoffeShopRepository _coffeShopRepository;
		private IUserRepositryReal _userRepositryReal;
		private AuthService _authService;

		public CoffeShopController(IKeyCoffeShopRepository coffeShopRepository, IUserRepositryReal userRepositryReal, AuthService authService)
		{
			_coffeShopRepository = coffeShopRepository;
			_userRepositryReal = userRepositryReal;
			_authService = authService;

		}

		public IActionResult Index()
		{
			var id = _authService.GetUserId();

			if (id is null)
			{
				return RedirectToAction("Index", "Home");
			}

			var viewModels = CoffeView();

			return View(viewModels);
		}

		public List<CoffeViewModel> CoffeView()
		{
			var id = _authService.GetUserId();

			var user = _userRepositryReal.Get(id.Value);

			var valuesCoffeFromDb = user.Age > 16
				? _coffeShopRepository.GetAll()
				: _coffeShopRepository.GetDefaultCoffe();

			var viewModels = valuesCoffeFromDb
				.Select(coffeFromDb =>
					new CoffeViewModel
					{
						Id = coffeFromDb.Id,
						Coffe = coffeFromDb.Coffe,
						Url = coffeFromDb.Url,
						Cost = coffeFromDb.Cost,
					}
				).ToList();

			return viewModels;
		}

		public IActionResult Coffe()
		{
			var id = _authService.GetUserId();

			if (id is null)
			{
				return RedirectToAction("Index", "Home");
			}

			var viewModels = CoffeView();

			return View(viewModels);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CoffeCreateViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			var coffe = new CoffeData
			{
				Coffe = viewModel.Coffe,
				Url = viewModel.Url,
				Cost = viewModel.Cost
			};

			_coffeShopRepository.Add(coffe);

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