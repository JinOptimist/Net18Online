using Enums.Users;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Controllers.LoadAdmin.LoadAdminAttrobute;
using WebPortalEverthing.Models.Admin;
using WebPortalEverthing.Models.LoadTesting.Admin;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers.LoadAdmin
{
    [IsLoadAdmin]
    public class LoadAdminController : Controller
    {
        private ILoadUserRepositryReal _loadUserRepositryReal;
        private EnumHelper _enumHelper;

        public LoadAdminController(
            ILoadUserRepositryReal loadUserRepositryReal,
            EnumHelper enumHelper)
        {
            _loadUserRepositryReal = loadUserRepositryReal;
            _enumHelper = enumHelper;
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
            _loadUserRepositryReal.UpdateRole(userId, role);
            return RedirectToAction("LoadUsersView", "LoadAdmin");
        }
    }
}
