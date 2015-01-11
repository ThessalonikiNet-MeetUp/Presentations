using System;

namespace EntityFrameworkTutorialInheritance
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