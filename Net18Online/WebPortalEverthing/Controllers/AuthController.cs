﻿using Enums.Users;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using WebPortalEverthing.Hubs;
using WebPortalEverthing.Models.Auth;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class AuthController : Controller
    {
        public IUserRepositryReal _userRepositryReal;
        public IHubContext<ChatHub, IChatHub> _chatHub;

        public AuthController(IUserRepositryReal userRepositryReal, 
            IHubContext<ChatHub, IChatHub> chatHub)
        {
            _userRepositryReal = userRepositryReal;
            _chatHub = chatHub;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel viewModel)
        {
            var user = _userRepositryReal.Login(viewModel.UserName, viewModel.Password);
            
            if (user is null)
            {
                ModelState.AddModelError(
                    nameof(viewModel.UserName), 
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
                new Claim(AuthService.CLAIM_TYPE_ROLE, ((int)user.Role).ToString()),
                new Claim (ClaimTypes.AuthenticationMethod, AuthService.AUTH_TYPE_KEY),
            };

            var identity = new ClaimsIdentity(claims, AuthService.AUTH_TYPE_KEY);

            var principal = new ClaimsPrincipal(identity);

            HttpContext
                .SignInAsync(principal)
                .Wait();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel viewModel)
        {
            if (!_userRepositryReal.CheckIsNameAvailable(viewModel.UserName))
            {
                return View(viewModel);
            }

            _userRepositryReal.Register(
                viewModel.UserName,
                viewModel.Password,
                viewModel.Age);

            // TODO Notify chat about new user
            _chatHub.Clients.All.NewMessageAdded($"Новый пользователь зарегестировался у нас на сайте. Его зовут {viewModel.UserName}");

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext
                .SignOutAsync()
                .Wait();

            return RedirectToAction("Index", "Home");
        }
    }
}
