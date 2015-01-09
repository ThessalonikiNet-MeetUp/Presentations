using System;
using System.Collections.Generic;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// A Category that a car belongs to (e.g. sports car, SUV, etc)
    /// </summary>
    public class CarCategory
    {
        public CarCategory()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}