using AutoMapper.Execution;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UI.ViewModels;


namespace UI.Controllers
{

    public class CarController : Controller
    {
       
        private ICarService _carService;
        private IDriverService _driverService;
        private IWatchService _watchService;

       

        public CarController(ICarService carService, 
            IDriverService driverService,
            IWatchService watchService)
        {
            _carService = carService;
            _driverService = driverService;
            _watchService = watchService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            
        List<CarViewModel> cars = _carService.GetAll()
            .Select(car => new CarViewModel
            {
                Id = car.Id,
                Name = car.Name,
                Model = car.Model,
                Number = car.Number,
                YearProduct = car.YearProduct,
                FuelConsumption = car.FuelConsumption,

            }).ToList();
        return View(cars);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            var vm = new CarViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddCar(CarViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var carDTO = vm.Map();
            try
            {
                _carService.Add(carDTO);
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
        public IActionResult DeleteCar()
        {
            var vm = new CarDeleteViewModel();
          
            return View(vm);
        }
        [HttpPost]
        public IActionResult DeleteCar(CarDeleteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var car = _carService.FindByNumber(vm.Number);

            if (car != null)
            {

                
                    _carService.DeleteById(car.Id);
                    return RedirectToAction("Index"); 
               
            }

            return NotFound("Автомобиль не найден");

          
        }
        [HttpGet]
        public IActionResult ShowCarDriver()
        {
            var vm = new ShowCarDriverViewModel();

            return View(vm);
        }
        [HttpPost]
        public IActionResult ShowCarDriver(ShowCarDriverViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var car = _carService.FindByNumber(vm.Number);

            if (car != null)
            {

                var dr = _carService.GetDrivers(car.Id);

                var carDrivers = dr
                    .Select(i => new CarDriverViewModel
                    {
                        Id = i.Id,
                        FullName = i.FullName,
                        PhoneNumber = i.PhoneNumber,
                        DriverWatch=_carService.GetNumberWatchForDriver(i.Id).FirstOrDefault().NumberWatch

                    }).ToList();
               
                var detailedInfo = new DetailedCatalogCarInfo()
                {
                    Id = car.Id,
                    Number = car.Number,
                    Drivers = carDrivers,
               
                    
                };

                
                return View("ShowDrivers", detailedInfo);


            }

            return NotFound("Автомобиль не найден");


        }


        [HttpGet]
        public IActionResult AddDriverCar(int id)
        {
            var vm = new CarDriverAddViewModel
            {
                Id = id
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddDriverCar(CarDriverAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var car = _carService.FindById(Convert.ToInt32(vm.Id));
                                
            var driver = _driverService.FindByFullName(vm.FullName);

            var watch = _carService.FindByNumberWatch(vm.Watch);


            if (driver != null && watch != null)
            {
                var tmpDr = _carService.GetDrivers(car.Id);
                var tmpList = new List<WatchDTO>();

                foreach (var d in tmpDr)
                {
                    tmpList.AddRange(_carService.GetNumberWatchForDriver(d.Id));
                }

                var searchNumberWatch = tmpList.FirstOrDefault(i => i.NumberWatch == vm.Watch);
                try
                {                   

                    if (searchNumberWatch == null)
                    {
                        _carService.AddCarDriver(car.Id, driver.Id, watch.Id);
                    }
                    else return NotFound("За вахтой водитель уже закреплен");

                    var dr = _carService.GetDrivers(car.Id);
                    var carDrivers = dr
                            .Select(i => new CarDriverViewModel
                            {
                                Id = i.Id,
                                FullName = i.FullName,
                                PhoneNumber = i.PhoneNumber,
                                DriverWatch = _carService.GetNumberWatchForDriver(i.Id).FirstOrDefault().NumberWatch
                            }).ToList();
                        var detailedInfo = new DetailedCatalogCarInfo()
                        {
                            Id = car.Id,
                            Number = car.Number,
                            Drivers = carDrivers,
                        };

                        return View("ShowDrivers", detailedInfo);

                   
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View(vm);
                }
               
               

            }


            return NotFound("Водитель не найден или вахта введена неправильно (должна быть 1 или 2)");

        }

        [HttpGet]
        public IActionResult SeachCar()
        {
            var vm = new SearchCarViewModel();

            return View(vm);
        }

        [HttpPost]
        public IActionResult SeachCar(SearchCarViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var car = _carService.FindByNumber(vm.Number);

            if (car != null)
            {
                var carInfo = new CarViewModel()
                {
                    Id = car.Id,
                    Name = car.Name,
                    Model = car.Model,
                    Number = car.Number,
                    YearProduct = car.YearProduct,
                    FuelConsumption = car.FuelConsumption

                };
                return View("ShowCarForEdit",carInfo);

            }

            return NotFound("Автомобиль не найден");


        }

        [HttpGet]
        public IActionResult EditCarName(int id)
        {
            var vm = new EditCarNameViewModel
            {
                Id = id,
                Name = _carService.FindById(id).Name
            };
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult EditCarName(EditCarNameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarDTO carDTO = _carService.FindById(vm.Id);
                carDTO.Name = vm.Name;
                try
                {
                   _carService.Update(carDTO);
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
        public IActionResult EditCarModel(int id)
        {
            var vm = new EditCarModelViewModel
            {
                Id = id,
                Model = _carService.FindById(id).Model
            };
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult EditCarModel(EditCarModelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarDTO carDTO = _carService.FindById(vm.Id);
                carDTO.Model = vm.Model;
                try
                {
                    _carService.Update(carDTO);
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
        public IActionResult EditCarNumber(int id)
        {
            var vm = new EditCarNumberViewModel
            {
                Id = id,
                Number = _carService.FindById(id).Number
            };
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult EditCarNumber(EditCarNumberViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarDTO carDTO = _carService.FindById(vm.Id);
                carDTO.Number = vm.Number;
                try
                {
                    _carService.Update(carDTO);
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
        public IActionResult EditCarYearProduct(int id)
        {
            var vm = new EditCarYearProductViewModel
            {
                Id = id,
                YearProduct = _carService.FindById(id).YearProduct
            };
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult EditCarYearProduct(EditCarYearProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarDTO carDTO = _carService.FindById(vm.Id);
                carDTO.YearProduct = vm.YearProduct;
                try
                {
                    _carService.Update(carDTO);
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
        public IActionResult EditCarFuelConsumption(int id)
        {
            var vm = new EditCarFuelConsumptionViewModel
            {
                Id = id,
                FuelConsumption = _carService.FindById(id).FuelConsumption
            };
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult EditCarFuelConsumption(EditCarFuelConsumptionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CarDTO carDTO = _carService.FindById(vm.Id);
                carDTO.FuelConsumption = vm.FuelConsumption;
                try
                {
                    _carService.Update(carDTO);
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
