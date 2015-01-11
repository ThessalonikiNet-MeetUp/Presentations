using System;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// Class for trucks
    /// </summary>
    public class Truck : Vehicle
    {
        public float Cargo { get; set; }

        public int EngineSize { get; set; }
    }
}