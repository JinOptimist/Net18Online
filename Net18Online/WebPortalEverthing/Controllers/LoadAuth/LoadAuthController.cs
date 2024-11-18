using Everything.Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebPortalEverthing.Models.Auth;
using WebPortalEverthing.Models.LoadTesting.Auth;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers.LoadAuth
{
    public class LoadAuthController : Controller
    {
        public ILoadUserRepositryReal _loadUserRepositryReal;

        public LoadAuthController(ILoadUserRepositryReal loadUserRepositryReal)
        {
            _loadUserRepositryReal = loadUserRepositryReal;
        }

        [HttpGet]
        public IActionResult LoginLoadUserView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginLoadUserView(LoginLoadUserViewModel viewModel)
        {
            var user = _loadUserRepositryReal.Login(viewModel.Login, viewModel.Password);

            if (user is null)
            {
                ModelState.AddModelError(
                    nameof(viewModel.Login),
                    "Не правильный логин или пароль");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //Good user

            var claims = new List<Claim>()
            {
                new Claim(AuthService.CLAIM_TYPE_ID, user.Id.ToString()),
                new Claim(AuthService.CLAIM_TYPE_NAME, user.Login),
                new Claim (ClaimTypes.AuthenticationMethod, AuthService.AUTH_TYPE_KEY),
            };

            var identity = new ClaimsIdentity(claims, AuthService.AUTH_TYPE_KEY);

            var principal = new ClaimsPrincipal(identity);

            HttpContext
                .SignInAsync(principal)
                .Wait();

            return RedirectToAction("IndexLoadVolumeView", "LoadVolumeTesting");
        }

        [HttpGet]
        public IActionResult RegistrationLoadUserView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrationLoadUserView(RegLoadUserViewModel viewModel)
        {
            _loadUserRepositryReal.Register(
                viewModel.Login,
                viewModel.Password,
                viewModel.Email);

            return RedirectToAction("LoginLoadUserView");
        }

        public IActionResult Logout()
        {
            HttpContext
                .SignOutAsync()
                .Wait();

            return RedirectToAction("IndexLoadVolumeView", "LoadVolumeTesting");
        }
    }
}