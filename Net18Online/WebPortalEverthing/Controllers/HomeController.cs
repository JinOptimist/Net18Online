using Enums.Users;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.Home;
using WebPortalEverthing.Services;
using WebPortalEverthing.Services.Apis;

namespace WebPortalEverthing.Controllers
{
    public class HomeController : Controller
    {
        private AuthService _authService;
        private IUserRepositryReal _userRepositryReal;
        private HttpNumberApi _httpNumberApi;
        private HttpWoofApi _httpWoofApi;

        public HomeController(AuthService authService,
            IUserRepositryReal userRepositryReal,
            HttpNumberApi httpNumberApi,
            HttpWoofApi httpWoofApi)
        {
            _authService = authService;
            _userRepositryReal = userRepositryReal;
            _httpNumberApi = httpNumberApi;
            _httpWoofApi = httpWoofApi;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            var userName = _authService.GetName();
            var userId = _authService.GetUserId();

            viewModel.UserName = userName;
            viewModel.UserId = userId ?? -1;

            viewModel.TheNumber = DateTime.Now.Second;
            
            var taskforNumber = _httpNumberApi.GetFactAsync(viewModel.TheNumber);
            var taskforDog = _httpWoofApi.GetRandomDogImage();

            await Task.WhenAll(taskforNumber, taskforDog);

            viewModel.FactAboutNumber = taskforDog.Result;
            viewModel.DogImageSrc = taskforDog.Result;

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
