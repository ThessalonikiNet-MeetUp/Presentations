using System;
using System.Collections.Generic;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// Class for various brands of a car part
    /// </summary>
    public class PartBrand
    {
        public PartBrand()
        {
            Cars = new HashSet<Car>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}