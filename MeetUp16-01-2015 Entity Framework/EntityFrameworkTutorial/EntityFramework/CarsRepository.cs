using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EntityFrameworkTutorial.EntityFramework
{
    class CarsRepository : ICarsRepository
    {
        public IEnumerable<Car> GetAllCars()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Cars.ToList();
            }
        }

        public Car GetCarById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Cars.Find(id);
            }
        }

        public int InsertCar(Car car)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Cars.Add(car);
                context.SaveChanges();
                return car.Id;
            }
        }

        public void UpdateCar(Car car)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Cars.Count(cc => cc.Id == car.Id) > 0)
                {
                    (context.Entry(car)).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteCar(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var carToDelete = context.Cars.Find(id);
                if (carToDelete != null)
                {
                    context.Cars.Remove(carToDelete);
                    context.SaveChanges();
                }
            }
        }
    }
}