using System;
using System.Collections.ObjectModel;
using System.Linq;
using EntityFrameworkTutorial.Ado;
using EntityFrameworkTutorial.EntityFramework;

namespace EntityFrameworkTutorial
{
    class Program
    {
        static void Main()
        {
            CarCatetoriesManagement();
            CarsManagement();
        }

        private static void CarCatetoriesManagement()
        {
            ICarCategoriesRepository carCategoriesRepository;

            //carCategoriesRepository = new AdoCarCategoriesRepository();
            carCategoriesRepository = new CarCategoriesRepository();

            var firstCategoryId = carCategoriesRepository.InsertCarCategory(new CarCategory
            {
                Code = "suv",
                Description = "suburban utility vehicle"
            });

            carCategoriesRepository.InsertCarCategory(new CarCategory
            {
                Code = "sportsCar",
                Description = "sports car"
            });

            var allCategories = carCategoriesRepository.GetAllCategories().ToList();

            var firstExistingCategory = carCategoriesRepository.GetCategoryById(firstCategoryId);
            firstExistingCategory.Code = "suv changed";
            carCategoriesRepository.UpdateCarCategory(firstExistingCategory);

            carCategoriesRepository.DeleteCarCategory(firstCategoryId);
        }

        private static void CarsManagement()
        {
            ICarCategoriesRepository carCategoriesRepository = new CarCategoriesRepository();
            var firstCarCategory = carCategoriesRepository.GetAllCategories().First();

            ICarsRepository carsRepository = new CarsRepository();

            var firstCarId = carsRepository.InsertCar(new Car
            {
                CategoryId = firstCarCategory.Id,
                Fuel = Fuel.Petrol,
                NumberOdSeats = 5,
                NumberOfWheels = 4
            });

            carsRepository.InsertCar(new Car
            {
                CategoryId = firstCarCategory.Id,
                Fuel = Fuel.Petroleum,
                NumberOdSeats = 2,
                NumberOfWheels = 4,
                EngineDetails = new EngineDetails
                {
                    EngineSize = 1800,
                    NumberOfGears = 6
                },
                PartBrands = new Collection<PartBrand>
                {
                    new PartBrand
                    {
                        Name = "Mercedes"
                    },
                    new PartBrand
                    {
                        Name = "BMW"
                    }
                }
            });

            var firstCategory = carCategoriesRepository.GetCategoryById(firstCarCategory.Id);

        }

    }
}