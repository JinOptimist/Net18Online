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
    public class LoadAdminController(
        ILoadUserRepositryReal loadUserRepositryReal,
        EnumHelper enumHelper) : Controller
    {
        private ILoadUserRepositryReal _loadUserRepositryReal = loadUserRepositryReal;
        private LoadAuthService _authService;
        private WebDbContext _webDbContext;
        private EnumHelper _enumHelper = enumHelper;
        private IWebHostEnvironment _webHostEnvironment;

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

        
    }
}
