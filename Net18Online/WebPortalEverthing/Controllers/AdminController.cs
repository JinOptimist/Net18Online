﻿using Enums.Users;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.Admin;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    [IsAdmin]
    public class AdminController : Controller
    {
        private IUserRepositryReal _userRepositryReal;
        private EnumHelper _enumHelper;

        public AdminController(
            IUserRepositryReal userRepositryReal, 
            EnumHelper enumHelper)
        {
            _userRepositryReal = userRepositryReal;
            _enumHelper = enumHelper;
        }

        public IActionResult Users()
        {
            var users = _userRepositryReal
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
            _userRepositryReal.UpdateRole(userId, role);
            return RedirectToAction("Users");
        }
    }
}
