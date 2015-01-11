using System;
using System.Collections.Generic;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// Repository for managing data access regarding <see cref="Car"/>s
    /// </summary>
    public interface ICarsRepository
    {
        IEnumerable<Car> GetAllCars();

        Car GetCarById(int id);

        int InsertCar(Car car);

        void UpdateCar(Car car);

        void DeleteCar(int id);
    }
}