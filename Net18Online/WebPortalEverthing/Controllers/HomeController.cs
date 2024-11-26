using Enums.Users;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.Home;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class HomeController : Controller
    {
        private AuthService _authService;
        private IUserRepositryReal _userRepositryReal;

        public HomeController(AuthService authService, IUserRepositryReal userRepositryReal)
        {
            _authService = authService;
            _userRepositryReal = userRepositryReal;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var userName = _authService.GetName();

            viewModel.UserName = userName;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [IsAuthenticated]
        public IActionResult UpdateLocale(Language language)
        {
            var userId = _authService.GetUserId()!.Value;
            _userRepositryReal.UpdateLocal(userId, language);

            return RedirectToAction("Index");
        }
    }
}
