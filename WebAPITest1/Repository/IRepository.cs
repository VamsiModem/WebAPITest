using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPITest1.Models;

namespace WebAPITest1.Repository
{
    interface IRepository
    {
        IEnumerable<Car> GetCars();
        int UpdateCar(int id, Car car);
        int InsertCar(Car car);
    }
}
