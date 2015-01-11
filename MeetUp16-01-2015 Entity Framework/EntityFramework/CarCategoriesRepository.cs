﻿using System;
using System.Data.Entity;
using System.Linq;

namespace EntityFrameworkTutorial.EntityFramework
{
    public class CarCategoriesRepository : ICarCategoriesRepository
    {
        public IQueryable<CarCategory> GetAllCategories()
        {
            using (var context = new TablePerConcreteTypeContext())
            {
                return context.CarCategories;
            }
        }

        public CarCategory GetCategoryById(int id)
        {
            using (var context = new TablePerConcreteTypeContext())
            {
                return context.CarCategories.Find(id);
            }
        }

        public int InsertCarCategory(CarCategory category)
        {
            using (var context = new TablePerConcreteTypeContext())
            {
                context.CarCategories.Add(category);
                context.SaveChanges();
                return category.Id;
            }
        }

        public void UpdateCarCategory(CarCategory category)
        {
            using (var context = new TablePerConcreteTypeContext())
            {
                if (context.CarCategories.Count(cc => cc.Id == category.Id) > 0)
                {
                    (context.Entry(category)).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteCarCategory(int id)
        {
            using (var context = new TablePerConcreteTypeContext())
            {
                var categoryToDelete = context.CarCategories.Find(id);
                if (categoryToDelete!= null)
                {
                    context.CarCategories.Remove(categoryToDelete);
                    context.SaveChanges();
                }
            }
        }

    }
}