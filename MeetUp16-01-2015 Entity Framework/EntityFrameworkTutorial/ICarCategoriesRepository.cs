﻿using System;
using System.Collections.Generic;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// Repository for managing data access regarding <see cref="CarCategory"/>
    /// </summary>
    public interface ICarCategoriesRepository
    {
        IEnumerable<CarCategory> GetAllCategories();

        CarCategory GetCategoryById(int id);

        int InsertCarCategory(CarCategory category);

        void UpdateCarCategory(CarCategory category);

        void DeleteCarCategory(int id);
    }
}