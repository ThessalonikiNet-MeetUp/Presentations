using System;

namespace EntityFrameworkTutorialInheritance
{
    /// <summary>
    /// Class for cars
    /// </summary>
    public class Car : Vehicle
    {
        public int NumberOdSeats { get; set; }

        public int CategoryId { get; set; }

    }
}