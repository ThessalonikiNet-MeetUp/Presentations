using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkTutorial.Ado;
using EntityFrameworkTutorial.EntityFramework;

namespace EntityFrameworkTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            CarCatetoriesManagement();
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

            var allCategories = carCategoriesRepository.GetAllCategories();
            allCategories.ToList();

            var firstExistingCategory = carCategoriesRepository.GetCategoryById(firstCategoryId);
            firstExistingCategory.Code = "suv changed";
            carCategoriesRepository.UpdateCarCategory(firstExistingCategory);

            carCategoriesRepository.DeleteCarCategory(firstCategoryId);
        }
    }
}