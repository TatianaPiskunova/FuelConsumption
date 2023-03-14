using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICarService : IService<CarDTO>
    {
        CarDTO FindByNumber(string number);
        List<DriverDTO> GetDrivers(int id);
        void AddCarDriver(int idCar, int idDriver, int idNumberWatch);
        List<WatchDTO> GetNumberWatchForDriver(int driverId);
        List<DriverDTO> GetDriverForNumberWatch(int watchId, int carId);
        WatchDTO FindByNumberWatch(string numberWatch);
        WatchDTO FindByNumberWatchId(int numberWatchId);



    }
}
