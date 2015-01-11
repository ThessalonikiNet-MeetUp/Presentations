using System;

namespace EntityFrameworkTutorialInheritance
{
    /// <summary>
    /// A base class for all vehicles
    /// </summary>
    public abstract class Vehicle 
    {
        public int Id { get; set; }

        public int NumberOfWheels { get; set; }

        public Fuel Fuel { get; set; }    
    }
}