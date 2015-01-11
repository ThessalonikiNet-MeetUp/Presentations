using System;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// Class for cars
    /// </summary>
    public class Car : Vehicle
    {
        public int NumberOdSeats { get; set; }

        public int CategoryId { get; set; }

        public virtual CarCategory Category { get; set; }
    }
}