using System;
using System.Collections.Generic;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// Class for cars
    /// </summary>
    public class Car
    {
        public Car()
        {
            PartBrands = new HashSet<PartBrand>();
        }
        public int Id { get; set; }
        public int NumberOfWheels { get; set; }
        public Fuel Fuel { get; set; }
        public int NumberOdSeats { get; set; }
        public int CategoryId { get; set; }
        public virtual CarCategory Category { get; set; }
        public virtual EngineDetails EngineDetails { get; set; }
        public virtual ICollection<PartBrand> PartBrands { get; set; }
    }
}