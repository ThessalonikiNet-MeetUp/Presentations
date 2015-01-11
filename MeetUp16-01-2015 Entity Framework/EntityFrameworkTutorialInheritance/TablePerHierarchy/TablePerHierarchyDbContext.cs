using System;
using System.Data.Entity;

namespace EntityFrameworkTutorialInheritance.TablePerHierarchy
{
    /// <summary>
    /// Context class responsible for data access based on table per hierarchy inheritance mapping.
    /// http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-1-table-per-hierarchy-tph
    /// </summary>
    public class TablePerHierarchyDbContext : DbContext
    {
        public TablePerHierarchyDbContext()
            : base("TablePerHierarchy") {}

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}