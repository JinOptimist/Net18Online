using Enums.Users;
using Everything.Data;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Controllers.LoadAdmin.LoadAdminAttrobute;
using WebPortalEverthing.Controllers.LoadTesting.Attribute;
using WebPortalEverthing.Models.Admin;
using WebPortalEverthing.Models.LoadTesting.Admin;
using WebPortalEverthing.Services;
using WebPortalEverthing.Services.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadAdmin
{
    [IsLoadAdmin]
    public class LoadAdminController : Controller
    {
        private ILoadUserRepositryReal _loadUserRepositryReal;
        private LoadAuthService _authService;
        private WebDbContext _webDbContext;
        private EnumHelper _enumHelper;
        private IWebHostEnvironment _webHostEnvironment;

        public LoadAdminController(
ILoadUserRepositryReal loadUserRepositryReal, LoadAuthService authService, WebDbContext webDbContext, EnumHelper enumHelper, IWebHostEnvironment webHostEnvironment)
        {
            _loadUserRepositryReal = loadUserRepositryReal;
            _authService = authService;
            _webDbContext = webDbContext;
            _enumHelper = enumHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult LoadUsersView()
        {
            var loadUsers = _loadUserRepositryReal
                .GetAll()
                .Select(x => new LoadUserVewModel
                {
                    Id = x.Id,
                    Login = x.Login,
                    Roles = _enumHelper.GetNames(x.Role)
                })
                .ToList();

            var viewModel = new LoadAdminViewModel();
            viewModel.LoadUsers = loadUsers;

            viewModel.Roles = _enumHelper.GetSelectListItems<Role>();

            return View(viewModel);
        }

        public IActionResult UpdateRole(Role role, int userId)
        {
            //авторизация проверяется через атрибут isLoadAdmin
            _loadUserRepositryReal.UpdateRole(userId, role);
            return RedirectToAction("LoadUsersView", "LoadAdmin");
        }

        [IsLoadAuthenticated]
        public IActionResult UpdateLocale(Language language)
        {
            var userId = _authService.GetUserId()!.Value;
            _loadUserRepositryReal.UpdateLocal(userId, language);

            return RedirectToAction("LoadUsersView");
        }
    }
}
