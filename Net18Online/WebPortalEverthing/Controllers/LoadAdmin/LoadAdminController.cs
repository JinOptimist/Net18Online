using Enums.Users;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Controllers.LoadAdmin.LoadAdminAttrobute;
using WebPortalEverthing.Models.Admin;
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

        public IActionResult Users()
        {
            var users = _loadUserRepositryReal
                .GetAll()
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Name = x.Login,
                    Roles = _enumHelper.GetNames(x.Role)
                })
                .ToList();

            var viewModel = new AdminUserViewModel();
            viewModel.Users = users;

            viewModel.Roles = _enumHelper.GetSelectListItems<Role>();

            return View(viewModel);
        }

        public IActionResult UpdateRole(Role role, int userId)
        {
            _loadUserRepositryReal.UpdateRole(userId, role);
            return RedirectToAction("LoadUsers");
        }
    }
}
