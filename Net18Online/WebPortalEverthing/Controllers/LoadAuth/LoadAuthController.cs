using Enums.Users;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using WebPortalEverthing.Models.Auth;
using WebPortalEverthing.Models.LoadTesting.Auth;
using WebPortalEverthing.Services;
using WebPortalEverthing.Services.LoadTesting;
using WebPortalEverthing.Hubs;


namespace WebPortalEverthing.Controllers.LoadAuth
{
    public class LoadAuthController : Controller
    {
        public ILoadUserRepositryReal _loadUserRepositryReal;
        //общий сервис, у меня отдельный
        public IUserRepositryReal _userRepositryReal;
        public IHubContext<LoadChatHub, ILoadChatHub> _chatHub;

        public LoadAuthController(ILoadUserRepositryReal loadUserRepositryReal,
            IUserRepositryReal userRepositryReal,
            IHubContext<LoadChatHub, ILoadChatHub> chatHub)
        {
            _loadUserRepositryReal = loadUserRepositryReal;
            _userRepositryReal = userRepositryReal;
            _chatHub = chatHub;
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
                new Claim(LoadAuthService.CLAIM_TYPE_ID, user.Id.ToString()),
                new Claim(LoadAuthService.CLAIM_TYPE_NAME, user.Login),
                new Claim(LoadAuthService.CLAIM_TYPE_COINS, user.Coins.ToString()),
                new Claim (ClaimTypes.AuthenticationMethod, LoadAuthService.AUTH_TYPE_KEY),
                new Claim(LoadAuthService.CLAIM_TYPE_ROLE, ((int)user.Role).ToString()),
            };

            var identity = new ClaimsIdentity(claims, LoadAuthService.AUTH_TYPE_KEY);

            var principal = new ClaimsPrincipal(identity);

            HttpContext
                .SignInAsync(principal)
                .Wait();

            _chatHub.Clients.All.NewMessageAdded($"пользователь login: {viewModel.Login} зашел на сайт.");

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


            //тут в общий сервис и БД  зарегистрировать пользователя
            if (!_userRepositryReal.CheckIsNameAvailable(viewModel.Login))
            {
                return View(viewModel);
            }

            _userRepositryReal.Register(
                viewModel.Login,
                viewModel.Password,
                18);

            _chatHub.Clients.All.NewMessageAdded($"Новый пользователь зарегестировался у нас на сайте. Его зовут {viewModel.Login}");


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