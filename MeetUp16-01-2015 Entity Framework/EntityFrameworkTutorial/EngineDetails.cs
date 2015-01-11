using System;

namespace EntityFrameworkTutorial
{
    /// <summary>
    /// Class for car's engine details
    /// </summary>
    public class EngineDetails
    {
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public int EngineSize { get; set; }
        public int NumberOfGears { get; set; }
    }
}
