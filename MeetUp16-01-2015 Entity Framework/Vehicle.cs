using System;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// A base class for all vehicles
    /// </summary>
    public class Vehicle 
    {
        public int Id { get; set; }

        public int NumberOfWheels { get; set; }

        public Fuel Fuel { get; set; }    
    }
}