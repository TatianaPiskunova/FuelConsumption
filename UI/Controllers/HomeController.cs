using AutoMapper;
using AutoMapper.Execution;
using BLL.DTO;
using BLL.Interfaces;

using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using UI.Models;
using UI.ViewModels;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
   
        public HomeController(IUserService userService)
        {
            _userService = userService;
          
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {  
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var vm = new LoginViewModel();
                return View(vm);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isOK = await _userService.Login(vm.UserName, vm.Password);
                if (isOK)
                {
                    var user = _userService.GetAll().FirstOrDefault(x => x.UserName == vm.UserName);
                    if (user != null)
                    {
                        
                        if (_userService.IsInRoleAsync(user, "admin").Result) return RedirectToAction("Index", "Admin");
                        if (_userService.IsInRoleAsync(user, "BossCar").Result) return RedirectToAction("Index", "Car");
                        if (_userService.IsInRoleAsync(user, "BossDriver").Result) return RedirectToAction("Index", "Driver");
                        if (_userService.IsInRoleAsync(user, "Engineer").Result) return RedirectToAction("Index", "Fuel");
                        if (_userService.IsInRoleAsync(user, "user").Result) return RedirectToAction("Info", "Home");
                    }
                }
              
               
            }

            ModelState.AddModelError(string.Empty, "Неправильный логин и (или) пароль. Для регистрации обратитесь к Admin");
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await _userService.Logout();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Info()
        {
           return View();
        }
    }
}