using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.Controllers
{
      public class DriverController : Controller
    {

        private IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<DriverViewModel> drivers = _driverService.GetAll()
                .Select(driver => new DriverViewModel
                {
                    Id = driver.Id,
                    FullName = driver.FullName,
                    Adress = driver.Adress,
                    PhoneNumber = driver.PhoneNumber,
                }).ToList();
            return View(drivers);
        }

        [HttpGet]
        public IActionResult AddDriver()
        {
            var vm = new DriverViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddDriver(DriverViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var driverDTO = vm.Map();
            try
            {
                _driverService.Add(driverDTO);
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

        [HttpGet]
        public IActionResult DeleteDriver()
        {
            var vm = new DriverDeleteViewModel();

            return View(vm);
        }
        [HttpPost]
        public IActionResult DeleteDriver(DriverDeleteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var driver = _driverService.FindByFullName(vm.FullNumber);

            if (driver != null)
            {

                _driverService.DeleteById(driver.Id);
                return RedirectToAction("Index");

            }

            return NotFound("Водитель не найден");


        }



        [HttpGet]
        public IActionResult SeachDriver()
        {
            var vm = new SearchDriverViewModel();

            return View(vm);
        }

        [HttpPost]
        public IActionResult SeachDriver(SearchDriverViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var driver = _driverService.FindByFullName(vm.FullName);

            if (driver != null)
            {
                var driverInfo = new DriverViewModel()
                {
                    Id = driver.Id,
                    FullName = driver.FullName,
                    Adress = driver.Adress,
                    PhoneNumber = driver.PhoneNumber
                };
                return View("ShowDriverForEdit", driverInfo);

            }

            return NotFound("Водитель не найден");


        }

        [HttpGet]
        public IActionResult EditDriverFullName(int id)
        {
            var vm = new EditDriverFullNameViewModel
            {
                Id = id,
                FullName = _driverService.FindById(id).FullName
            };
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult EditDriverFullName(EditDriverFullNameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                DriverDTO driverDTO = _driverService.FindById(vm.Id);
                driverDTO.FullName = vm.FullName;
                try
                {
                    _driverService.Update(driverDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityArgumentException ex)
                {
                    ModelState.AddModelError(ex.ParamName, ex.Message);
                }
            }
            return View(vm);
        }


        [HttpGet]
        public IActionResult EditDriverAdress(int id)
        {
            var vm = new EditDriverAdressViewModel
            {
                Id = id,
                Adress = _driverService.FindById(id).Adress
            };
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult EditDriverAdress(EditDriverAdressViewModel vm)
        {
            if (ModelState.IsValid)
            {
                DriverDTO driverDTO = _driverService.FindById(vm.Id);
                driverDTO.Adress = vm.Adress;
                try
                {
                    _driverService.Update(driverDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityArgumentException ex)
                {
                    ModelState.AddModelError(ex.ParamName, ex.Message);
                }
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditDriverPhoneNumber(int id)
        {
            var vm = new EditDriverPhoneNumberViewModel
            {
                Id = id,
               PhoneNumber = _driverService.FindById(id).PhoneNumber
            };
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult EditDriverPhoneNumber(EditDriverPhoneNumberViewModel vm)
        {
            if (ModelState.IsValid)
            {
                DriverDTO driverDTO = _driverService.FindById(vm.Id);
                driverDTO.PhoneNumber = vm.PhoneNumber;
                try
                {
                    _driverService.Update(driverDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityArgumentException ex)
                {
                    ModelState.AddModelError(ex.ParamName, ex.Message);
                }
            }
            return View(vm);
        }
    }
}
