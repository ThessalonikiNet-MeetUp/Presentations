using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkTutorial.Ado;

namespace EntityFrameworkTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            ICarCategoriesRepository carCategoriesRepository = new AdoCarCategoriesRepository();


            var id =
                carCategoriesRepository.InsertCarCategory(new CarCategory {Code = "code", Description = "Description"});
        }
    }
}
