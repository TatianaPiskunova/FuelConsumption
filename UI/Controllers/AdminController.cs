using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;

namespace UI.Controllers
{
    
    public class AdminController : Controller
    {

        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<UserViewModel> users = _userService.GetAll()
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    PositionOffice = user.PositionOffice,
                    UserName = user.UserName,
                    Password = user.Password,
                    NameRole = user.NameRole,

                }).ToList();
            return View(users);


        }
        [HttpGet]
        public IActionResult AddUser()
        {
            var vm = new UserViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddUser(UserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var userDTO = vm.Map();
            try
            {
                _userService.Add(userDTO);
                return RedirectToAction("Index");
            }
            catch (InvalidIdException e)
            {
                ModelState.AddModelError("Id", e.Message);
                return View(vm);
            }
            catch (EntityArgumentException e)
            {
                ModelState.AddModelError(e.ParamName, e.Message);
                return View(vm);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(vm);
            }

        }
    }
}
