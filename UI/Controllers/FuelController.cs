using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using UI.ViewModels;
namespace UI.Controllers
{

    public class FuelController : Controller
    {

        private ICarService _carService;
        private IDriverService _driverService;
        private ICarFuelConsumptionService _carFuelConsumptionService;
        private IWatchService _watchService;



        public FuelController(ICarService carService,
            IDriverService driverService,
            ICarFuelConsumptionService carFuelConsumptionService,
            IWatchService watchService)
        {
            _carService = carService;
            _driverService = driverService;
            _carFuelConsumptionService = carFuelConsumptionService;
            _watchService = watchService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<EqualFuelConsumptionViewModel> fueles = _carFuelConsumptionService.GetAll()
             .Select(fuel => new EqualFuelConsumptionViewModel
             {
                 Id = fuel.Id,
                 CarNumber = _carService.FindById(Convert.ToInt32(fuel.CarId)).Number,
                 //DriverFullName = _driverService.FindById(fuel.DriverId).FullName,
                 DriverFullName = _carService.GetDriverForNumberWatch(_carService.FindByNumberWatchId(fuel.WatchNumberId).Id, _carService.FindById(Convert.ToInt32(fuel.CarId)).Id).FirstOrDefault().FullName,
                 WatchNumber = _carService.FindByNumberWatchId(fuel.WatchNumberId).NumberWatch,
                 Mounth = fuel.Mounth,
                 Year = fuel.Year,
                 EqualFuelConsumptionFirst_1Watch = fuel.EqualFuelConsumptionFirst_1Watch,
                 FuelConsumptionPlus_1Watch = fuel.FuelConsumptionPlus_1Watch,
                 FuelConsumptionMinus_1Watch = fuel.FuelConsumptionMinus_1Watch,
                 EqualFuelConsumptionLast_1Watch = fuel.EqualFuelConsumptionLast_1Watch,
                 EqualFuelConsumptionFirst_2Watch = fuel.EqualFuelConsumptionFirst_2Watch,
                 FuelConsumptionPlus_2Watch = fuel.FuelConsumptionPlus_2Watch,
                 FuelConsumptionMinus_2Watch = fuel.FuelConsumptionMinus_2Watch,
                 EqualFuelConsumptionLast_2Watch = fuel.EqualFuelConsumptionLast_2Watch,
             }).ToList();

            var tmpArr = fueles.OrderBy(x => x.CarNumber).ThenBy(x => x.DriverFullName).ToList();

            return View(tmpArr);

        }
        [HttpGet]
        public IActionResult AddFuel()
        {
            var vm = new AddFuelConsumptionViewModel();

            return View(vm);
        }
        [HttpPost]
        public IActionResult AddFuel(AddFuelConsumptionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var car = _carService.FindByNumber(vm.Number);
            var watch = _carService.FindByNumberWatch(vm.WatchNumber);
            var tmpMounth = _carFuelConsumptionService.NameMounth(vm.Mounth);

            if (car != null)
            {

                if (tmpMounth != "error")
                {
                    if (tmpMounth != "january")
                    {
                        if (vm.WatchNumber == "1")
                        {

                            var fuelFor2Watch_Last = _carFuelConsumptionService.GetAll()
                                .FirstOrDefault(i => i.Mounth == tmpMounth && i.CarId == car.Id && i.WatchNumberId == 2)
                                .EqualFuelConsumptionLast_2Watch;

                            var rez = double.Parse(fuelFor2Watch_Last) + double.Parse(vm.FuelConsumptionPlus) - double.Parse(vm.FuelConsumptionMinus);

                            _carFuelConsumptionService.Add(new CarFuelConsumptionDTO
                            {
                                CarId = car.Id,
                                WatchNumberId = watch.Id,
                                DriverId = _carService.GetDriverForNumberWatch(watch.Id, car.Id).FirstOrDefault().Id,
                                Mounth = vm.Mounth,
                                Year = vm.Year,
                                EqualFuelConsumptionFirst_1Watch = fuelFor2Watch_Last,
                                FuelConsumptionPlus_1Watch = vm.FuelConsumptionPlus,
                                FuelConsumptionMinus_1Watch = vm.FuelConsumptionMinus,
                                EqualFuelConsumptionLast_1Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR")),
                                EqualFuelConsumptionFirst_2Watch = "0",
                                FuelConsumptionPlus_2Watch = "0",
                                FuelConsumptionMinus_2Watch = "0",
                                EqualFuelConsumptionLast_2Watch = "0",

                            });
                        }
                        if (vm.WatchNumber == "2")
                        {

                            var fuelFor1Watch = _carFuelConsumptionService.GetAll()
                                    .FirstOrDefault(i => i.Mounth == vm.Mounth && i.CarId == car.Id && i.WatchNumberId == 1)
                                    .EqualFuelConsumptionLast_1Watch;

                            var rez = double.Parse(fuelFor1Watch) + double.Parse(vm.FuelConsumptionPlus) - double.Parse(vm.FuelConsumptionMinus);

                            _carFuelConsumptionService.Add(new CarFuelConsumptionDTO
                            {
                                CarId = car.Id,
                                WatchNumberId = watch.Id,
                                DriverId = _carService.GetDriverForNumberWatch(watch.Id, car.Id).FirstOrDefault().Id,
                                Mounth = vm.Mounth,
                                Year = vm.Year,
                                EqualFuelConsumptionFirst_1Watch = "0",
                                FuelConsumptionPlus_1Watch = "0",
                                FuelConsumptionMinus_1Watch = "0",
                                EqualFuelConsumptionLast_1Watch = "0",
                                EqualFuelConsumptionFirst_2Watch = fuelFor1Watch,
                                FuelConsumptionPlus_2Watch = vm.FuelConsumptionPlus,
                                FuelConsumptionMinus_2Watch = vm.FuelConsumptionMinus,
                                EqualFuelConsumptionLast_2Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR")),

                            });

                        }

                        return RedirectToAction("Index");
                    }
                    return NotFound("Для записи данных на январь месяц - нажмите соответствующую кнопку на главной странице");
                }
                return NotFound("Месяц введен не верно");
            }

            return NotFound("Автомобиль не найден");


        }
        public IActionResult AddFuelJanuary()
        {
            var vm = new AddFuelConsumptionJanuaryViewModel();

            return View(vm);
        }
        [HttpPost]
        public IActionResult AddFuelJanuary(AddFuelConsumptionJanuaryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var car = _carService.FindByNumber(vm.Number);
            var watch = _carService.FindByNumberWatch(vm.WatchNumber);
            var tmpMounth = _carFuelConsumptionService.NameMounth(vm.Mounth);

            if (car != null)
            {

                if (tmpMounth != "error")
                {
                    if (tmpMounth == "january")
                    {
                        if (vm.WatchNumber == "1")
                        {
                            var rez = double.Parse(vm.FuelConsumptionDecember) + double.Parse(vm.FuelConsumptionPlus) - double.Parse(vm.FuelConsumptionMinus);
                            _carFuelConsumptionService.Add(new CarFuelConsumptionDTO
                            {
                                CarId = car.Id,
                                WatchNumberId = watch.Id,
                                DriverId = _carService.GetDriverForNumberWatch(watch.Id, car.Id).FirstOrDefault().Id,
                                Mounth = vm.Mounth,
                                Year = vm.Year,
                                EqualFuelConsumptionFirst_1Watch = vm.FuelConsumptionDecember,
                                FuelConsumptionPlus_1Watch = vm.FuelConsumptionPlus,
                                FuelConsumptionMinus_1Watch = vm.FuelConsumptionMinus,
                                EqualFuelConsumptionLast_1Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR")),
                                EqualFuelConsumptionFirst_2Watch = "0",
                                FuelConsumptionPlus_2Watch = "0",
                                FuelConsumptionMinus_2Watch = "0",
                                EqualFuelConsumptionLast_2Watch = "0",

                            });

                        }
                        if (vm.WatchNumber == "2")
                        {

                            var fuelFor1Watch = _carFuelConsumptionService.GetAll().FirstOrDefault(i => i.Mounth == vm.Mounth && i.CarId == car.Id && i.WatchNumberId == 1).
                                   EqualFuelConsumptionLast_1Watch;
                            var rez = double.Parse(fuelFor1Watch) + double.Parse(vm.FuelConsumptionPlus) - double.Parse(vm.FuelConsumptionMinus);

                            _carFuelConsumptionService.Add(new CarFuelConsumptionDTO
                            {
                                CarId = car.Id,
                                WatchNumberId = watch.Id,
                                DriverId = _carService.GetDriverForNumberWatch(watch.Id, car.Id).FirstOrDefault().Id,
                                Mounth = vm.Mounth,
                                Year = vm.Year,
                                EqualFuelConsumptionFirst_1Watch = "0",
                                FuelConsumptionPlus_1Watch = "0",
                                FuelConsumptionMinus_1Watch = "0",
                                EqualFuelConsumptionLast_1Watch = "0",
                                EqualFuelConsumptionFirst_2Watch = fuelFor1Watch,
                                FuelConsumptionPlus_2Watch = vm.FuelConsumptionPlus,
                                FuelConsumptionMinus_2Watch = vm.FuelConsumptionMinus,
                                EqualFuelConsumptionLast_2Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR")),

                            });

                        }

                        return RedirectToAction("Index");
                    }
                    return NotFound("Для записи данных НЕ на январь месяц - нажмите соответствующую кнопку на главной странице");
                }
                return NotFound("Месяц введен не верно");
            }

            return NotFound("Автомобиль не найден");


        }
        [HttpGet]
        public IActionResult DeleteFuel()
        {
            var vm = new DeleteFuelConsumptionViewModel();

            return View(vm);
        }
        [HttpPost]
        public IActionResult DeleteFuel(DeleteFuelConsumptionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var car = _carService.FindByNumber(vm.Number);
            var mounth = _carFuelConsumptionService.GetAll().FirstOrDefault(i => i.Mounth == vm.Mounth);
            var year = _carFuelConsumptionService.GetAll().FirstOrDefault(i => i.Year == vm.Year);
            var watch = _carService.FindByNumberWatch(vm.WatchNumber);

            if (car != null)
            {

                if (mounth != null && year != null)
                {
                    var tpmFuel = _carFuelConsumptionService.GetAll().FirstOrDefault(i => i.Mounth == vm.Mounth && i.WatchNumberId == watch.Id && i.CarId == car.Id).Id;
                    _carFuelConsumptionService.DeleteById(tpmFuel);
                    return RedirectToAction("Index");
                }

                return NotFound("Дата не найдена или введена неправильно");
            }

            return NotFound("Автомобиль не найден");

        }

        [HttpGet]
        public IActionResult SearchFuel()
        {
            var vm = new SearchFuelConsumptionViewModel();

            return View(vm);
        }

        [HttpPost]
        public IActionResult SearchFuel(SearchFuelConsumptionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var car = _carService.FindByNumber(vm.Number);
            var watch = _carService.FindByNumberWatch(vm.WatchNumber);

            var tmpFuel = _carFuelConsumptionService.GetAll().
                Where(i => i.CarId == car.Id).
                Where(i => i.WatchNumberId == watch.Id).
                Where(i => i.Mounth == vm.Mounth).
                Where(i => i.Year == vm.Year).FirstOrDefault();

            if (tmpFuel != null)
            {
                if (vm.WatchNumber == "1")
                {

                    var fuelFor1WatchInfo = new FuelConsumption_1WatchViewModel() {

                        Id = tmpFuel.Id,
                        CarNumber = _carService.FindById(tmpFuel.CarId).Number,
                        WatchNumber = _carService.FindByNumberWatchId(tmpFuel.WatchNumberId).NumberWatch,
                        DriverFullName = _carService.GetDriverForNumberWatch(tmpFuel.WatchNumberId, tmpFuel.CarId).FirstOrDefault().FullName,
                        Mounth = tmpFuel.Mounth,
                        Year = tmpFuel.Year,
                        EqualFuelConsumptionFirst_1Watch = tmpFuel.EqualFuelConsumptionFirst_1Watch,
                        FuelConsumptionPlus_1Watch = tmpFuel.FuelConsumptionPlus_1Watch,
                        FuelConsumptionMinus_1Watch = tmpFuel.FuelConsumptionMinus_1Watch,
                        EqualFuelConsumptionLast_1Watch = tmpFuel.EqualFuelConsumptionLast_1Watch

                    };
                    return View("ShowFuelConsumptionForEdit_1Watch", fuelFor1WatchInfo);

                }
                if (vm.WatchNumber == "2")
                {

                    var fuelFor2WatchInfo = new FuelConsumption_2WatchViewModel()
                    {
                        Id = tmpFuel.Id,
                        CarNumber = _carService.FindById(tmpFuel.CarId).Number,
                        WatchNumber = _carService.FindByNumberWatchId(tmpFuel.WatchNumberId).NumberWatch,
                        DriverFullName = _carService.GetDriverForNumberWatch(tmpFuel.WatchNumberId, tmpFuel.CarId).FirstOrDefault().FullName,
                        Mounth = tmpFuel.Mounth,
                        Year = tmpFuel.Year,
                        EqualFuelConsumptionFirst_2Watch = tmpFuel.EqualFuelConsumptionFirst_2Watch,
                        FuelConsumptionPlus_2Watch = tmpFuel.FuelConsumptionPlus_2Watch,
                        FuelConsumptionMinus_2Watch = tmpFuel.FuelConsumptionMinus_2Watch,
                        EqualFuelConsumptionLast_2Watch = tmpFuel.EqualFuelConsumptionLast_2Watch

                    };
                    return View("ShowFuelConsumptionForEdit_2Watch", fuelFor2WatchInfo);
                }


                return RedirectToAction("Index");


            }

            return NotFound("По запросу ничего не найдено. Уточните данные");


        }

        [HttpGet]
        public IActionResult EditPlusFuel_1Watch(int id)
        {
            var vm = new EditFuelPlus_1WatchViewModel
            {
                Id = id,
                FuelConsumptionPlus_1Watch = _carFuelConsumptionService.FindById(id).FuelConsumptionPlus_1Watch
            };
            return PartialView(vm);
        }

        [HttpPost]
        public IActionResult EditPlusFuel_1Watch(EditFuelPlus_1WatchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarFuelConsumptionDTO carFuelConsumptionDTO = _carFuelConsumptionService.FindById(vm.Id);
                carFuelConsumptionDTO.FuelConsumptionPlus_1Watch = vm.FuelConsumptionPlus_1Watch;
                try
                {
                    _carFuelConsumptionService.Update(carFuelConsumptionDTO);
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
        public IActionResult EditPlusFuel_2Watch(int id)
        {
            var vm = new EditFuelPlus_2WatchViewModel
            {
                Id = id,
                FuelConsumptionPlus_2Watch = _carFuelConsumptionService.FindById(id).FuelConsumptionPlus_2Watch
            };
            return PartialView(vm);
        }

        [HttpPost]
        public IActionResult EditPlusFuel_2Watch(EditFuelPlus_2WatchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarFuelConsumptionDTO carFuelConsumptionDTO = _carFuelConsumptionService.FindById(vm.Id);
                carFuelConsumptionDTO.FuelConsumptionPlus_2Watch = vm.FuelConsumptionPlus_2Watch;
                try
                {
                    _carFuelConsumptionService.Update(carFuelConsumptionDTO);
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
        public IActionResult EditMinusFuel_1Watch(int id)
        {
            var vm = new EditFuelMinus_1WatchViewModel
            {
                Id = id,
                FuelConsumptionMinus_1Watch = _carFuelConsumptionService.FindById(id).FuelConsumptionMinus_1Watch
            };
            return PartialView(vm);
        }

        [HttpPost]
        public IActionResult EditMinusFuel_1Watch(EditFuelMinus_1WatchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarFuelConsumptionDTO carFuelConsumptionDTO = _carFuelConsumptionService.FindById(vm.Id);
                carFuelConsumptionDTO.FuelConsumptionMinus_1Watch = vm.FuelConsumptionMinus_1Watch;
                try
                {
                    _carFuelConsumptionService.Update(carFuelConsumptionDTO);
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
        public IActionResult EditMinusFuel_2Watch(int id)
        {
            var vm = new EditFuelMinus_2WatchViewModel
            {
                Id = id,
                FuelConsumptionMinus_2Watch = _carFuelConsumptionService.FindById(id).FuelConsumptionMinus_2Watch
            };
            return PartialView(vm);
        }

        [HttpPost]
        public IActionResult EditMinusFuel_2Watch(EditFuelMinus_2WatchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarFuelConsumptionDTO carFuelConsumptionDTO = _carFuelConsumptionService.FindById(vm.Id);
                carFuelConsumptionDTO.FuelConsumptionMinus_2Watch = vm.FuelConsumptionMinus_2Watch;
                try
                {
                    _carFuelConsumptionService.Update(carFuelConsumptionDTO);
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
        public IActionResult UpdateFuel_1Watch(int id)
        {
            var vm = new UpdateFuel_1WatchViewModel
            {
                Id = id,
            };
            return PartialView(vm);
        }

        [HttpPost]
        public IActionResult UpdateFuel_1Watch(UpdateFuel_1WatchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarFuelConsumptionDTO carFuelConsumptionDTO = _carFuelConsumptionService.FindById(vm.Id);
                var tmpMounth = _carFuelConsumptionService.NameMounth(carFuelConsumptionDTO.Mounth);
                var tmpFuel = _carFuelConsumptionService.GetAll().
                    FirstOrDefault(i => i.Mounth == tmpMounth && i.CarId == carFuelConsumptionDTO.CarId && i.WatchNumberId == 2)
                                         .EqualFuelConsumptionLast_2Watch;

                carFuelConsumptionDTO.EqualFuelConsumptionFirst_1Watch = tmpFuel;

                var rez = double.Parse(tmpFuel)
                        + double.Parse(carFuelConsumptionDTO.FuelConsumptionPlus_1Watch)
                        - double.Parse(carFuelConsumptionDTO.FuelConsumptionMinus_1Watch);
                carFuelConsumptionDTO.EqualFuelConsumptionLast_1Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR"));

                try
                {
                    _carFuelConsumptionService.Update(carFuelConsumptionDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityArgumentException ex)
                {
                    ModelState.AddModelError(ex.ParamName, ex.Message);
                }
            }
            return NotFound("Ошибка");
        }

        [HttpGet]
        public IActionResult UpdateFuel_2Watch(int id)
        {
            var vm = new UpdateFuel_2WatchViewModel
            {
                Id = id,
            };
            return PartialView(vm);
        }

        [HttpPost]
        public IActionResult UpdateFuel_2Watch(UpdateFuel_2WatchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarFuelConsumptionDTO carFuelConsumptionDTO = _carFuelConsumptionService.FindById(vm.Id);
                var tmpFuel = _carFuelConsumptionService.GetAll().FirstOrDefault(i => i.Mounth == carFuelConsumptionDTO.Mounth && i.CarId == carFuelConsumptionDTO.CarId && i.WatchNumberId == 1).
                                   EqualFuelConsumptionLast_1Watch;
                carFuelConsumptionDTO.EqualFuelConsumptionFirst_2Watch = tmpFuel;
                var rez = double.Parse(tmpFuel)
                        + double.Parse(carFuelConsumptionDTO.FuelConsumptionPlus_2Watch)
                        - double.Parse(carFuelConsumptionDTO.FuelConsumptionMinus_2Watch);
                carFuelConsumptionDTO.EqualFuelConsumptionLast_2Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR"));

                try
                {
                    _carFuelConsumptionService.Update(carFuelConsumptionDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityArgumentException ex)
                {
                    ModelState.AddModelError(ex.ParamName, ex.Message);
                }
            }
            return NotFound("Ошибка");
        }

        [HttpGet]
        public IActionResult SearchFuelByCar()
        {
            var vm = new SearchCarViewModel();

            return View(vm);
        }
        [HttpPost]
        public IActionResult SearchFuelByCar(SearchCarViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var car = _carService.FindByNumber(vm.Number);

            if (car != null)
            {
                var ListFuel = _carFuelConsumptionService.GetAll().
                    Where(i => i.CarId == car.Id).ToList();

                var list = new List<EqualFuelConsumptionViewModel>();

                foreach (var fuel in ListFuel) {

                    var detailedFuel = new EqualFuelConsumptionViewModel()
                    {
                        Id = fuel.Id,
                        CarNumber=_carService.FindById(fuel.CarId).Number,
                        DriverFullName = _carService.GetDriverForNumberWatch(_carService.FindByNumberWatchId(fuel.WatchNumberId).Id, _carService.FindById(Convert.ToInt32(fuel.CarId)).Id).FirstOrDefault().FullName,
                        WatchNumber = _carService.FindByNumberWatchId(fuel.WatchNumberId).NumberWatch,
                        Mounth = fuel.Mounth,
                        Year = fuel.Year,
                        EqualFuelConsumptionFirst_1Watch = fuel.EqualFuelConsumptionFirst_1Watch,
                        FuelConsumptionPlus_1Watch = fuel.FuelConsumptionPlus_1Watch,
                        FuelConsumptionMinus_1Watch = fuel.FuelConsumptionMinus_1Watch,
                        EqualFuelConsumptionLast_1Watch = fuel.EqualFuelConsumptionLast_1Watch,
                        EqualFuelConsumptionFirst_2Watch = fuel.EqualFuelConsumptionFirst_2Watch,
                        FuelConsumptionPlus_2Watch = fuel.FuelConsumptionPlus_2Watch,
                        FuelConsumptionMinus_2Watch = fuel.FuelConsumptionMinus_2Watch,
                        EqualFuelConsumptionLast_2Watch = fuel.EqualFuelConsumptionLast_2Watch,


                    };
                    list.Add(detailedFuel);
                }

                var tmpArr = list.OrderBy(x => x.CarNumber).ThenBy(x => x.DriverFullName).ToList();
                return View("ShowFuelConsumptionByCar", tmpArr);
            }
            return NotFound("Автомобиль не найден");

        }

        [HttpGet]
        public IActionResult UpdateFuel()
        {
            var vm = new SearchCarViewModel();

            return View(vm);
        }
        [HttpPost]
        public IActionResult UpdateFuel(SearchCarViewModel model)
        {
            var car = _carService.FindByNumber(model.Number);
            var ListFuel = _carFuelConsumptionService.GetAll().
                    Where(i => i.CarId == car.Id).ToList();
            //CarFuelConsumptionDTO carFuelConsumptionDTO = _carFuelConsumptionService.GetAll().FirstOrDefault(i => i.CarId == car.Id);

            foreach (var fuel in ListFuel)   
            {
                if(fuel.WatchNumberId == 1)
                {
                    if (fuel.Mounth == "январь")
                    {
                        var rez = double.Parse(fuel.EqualFuelConsumptionFirst_1Watch)
                            + double.Parse(fuel.FuelConsumptionPlus_1Watch)
                            - double.Parse(fuel.FuelConsumptionMinus_1Watch);
                        fuel.EqualFuelConsumptionLast_1Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR"));

                        try
                        {
                            _carFuelConsumptionService.Update(fuel);
                           
                        }
                        catch (EntityArgumentException ex)
                        {
                            ModelState.AddModelError(ex.ParamName, ex.Message);
                        }
                    }
                    else
                    {
                        var tmpMounth = _carFuelConsumptionService.NameMounth(fuel.Mounth);


                        var tmpFuel = _carFuelConsumptionService.GetAll().
                            FirstOrDefault(i => i.Mounth == tmpMounth && i.CarId == fuel.CarId && i.WatchNumberId == 2)
                                                 .EqualFuelConsumptionLast_2Watch;

                        fuel.EqualFuelConsumptionFirst_1Watch = tmpFuel;

                        var rez = double.Parse(tmpFuel)
                                + double.Parse(fuel.FuelConsumptionPlus_1Watch)
                                - double.Parse(fuel.FuelConsumptionMinus_1Watch);
                        fuel.EqualFuelConsumptionLast_1Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR"));

                        try
                        {
                            _carFuelConsumptionService.Update(fuel);
                          
                        }
                        catch (EntityArgumentException ex)
                        {
                            ModelState.AddModelError(ex.ParamName, ex.Message);
                        }
                    }
                }

                if (fuel.WatchNumberId == 2)
                {
                    
                    var tmpFuel = _carFuelConsumptionService.GetAll().FirstOrDefault(i => i.Mounth == fuel.Mounth && i.CarId == fuel.CarId && i.WatchNumberId == 1).
                                       EqualFuelConsumptionLast_1Watch;
                    fuel.EqualFuelConsumptionFirst_2Watch = tmpFuel;
                    var rez = double.Parse(tmpFuel)
                            + double.Parse(fuel.FuelConsumptionPlus_2Watch)
                            - double.Parse(fuel.FuelConsumptionMinus_2Watch);
                    fuel.EqualFuelConsumptionLast_2Watch = rez.ToString("F", CultureInfo.CreateSpecificCulture("fr-FR"));

                    try
                    {
                        _carFuelConsumptionService.Update(fuel);
                      
                    }
                    catch (EntityArgumentException ex)
                    {
                        ModelState.AddModelError(ex.ParamName, ex.Message);
                    }
                }

            }

       

            return RedirectToAction("Index");


        }



    }










}
            