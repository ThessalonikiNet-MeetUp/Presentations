using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTutorial
{
    public interface ICarsRepository
    {
        IQueryable<Car> GetAllCars();

        Car GetCarById(int id);

        int InsertCar(Car car);

        void UpdateCar(Car car);

        void DeleteCar(int id);
    }
}