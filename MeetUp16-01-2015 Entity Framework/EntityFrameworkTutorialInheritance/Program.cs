using System;
using System.Linq;
using EntityFrameworkTutorialInheritance.TablePerConcreteType;
using EntityFrameworkTutorialInheritance.TablePerHierarchy;
using EntityFrameworkTutorialInheritance.TablePerType;

namespace EntityFrameworkTutorialInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            DemonstrateTablePerHierarchy();
            DemonstrateTablePerType();
            DemonstrateTablePerConcreteType();
        }

        private static void DemonstrateTablePerHierarchy()
        {
            using (var context = new TablePerHierarchyDbContext())
            {
                AddVehiclesTablePerHierarchy(context);
                var allVehicles = context.Vehicles.ToList();
                var allCars = context.Vehicles.OfType<Car>().ToList();
                var firstCar = context.Vehicles.Find(allCars.First().Id);
            }
        }

        private static void AddVehiclesTablePerHierarchy(TablePerHierarchyDbContext context)
        {
            context.Vehicles.Add(new Car
            {
                Fuel = Fuel.Petrol,
                NumberOfWheels = 4,
                NumberOdSeats = 5,
                DrivingPlates = "EEP3587"
            });
            context.Vehicles.Add(new Car
            {
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 4,
                NumberOdSeats = 2,
                DrivingPlates = "EEH4685"
            });

            context.Vehicles.Add(new Truck
            {
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 6,
                EngineSize = 4500,
                Cargo = 7500
            });

            context.Vehicles.Add(new Truck
            {
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 10,
                EngineSize = 6500,
                Cargo = 12500
            });

            context.Vehicles.Add(new Motorbike
            {
                Fuel = Fuel.Petrol,
                NumberOfWheels = 2,
                NumberOfPistons = 1
            });
            context.SaveChanges();
        }

        private static void DemonstrateTablePerType()
        {
            using (var context = new TablePerTypeDbContext())
            {
                AddVehiclesTablePerType(context);
                var allVehicles = context.Vehicles.ToList();
                var allCars = context.Vehicles.OfType<Car>().ToList();
                var firstCar = context.Vehicles.Find(allCars.First().Id);
            }
        }

        private static void AddVehiclesTablePerType(TablePerTypeDbContext context)
        {
            context.Vehicles.Add(new Car
            {
                Fuel = Fuel.Petrol,
                NumberOfWheels = 4,
                NumberOdSeats = 5,
                DrivingPlates = "EEP3587"
            });
            context.Vehicles.Add(new Car
            {
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 4,
                NumberOdSeats = 2,
                DrivingPlates = "EEH4685"
            });

            context.Vehicles.Add(new Truck
            {
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 6,
                EngineSize = 4500,
                Cargo = 7500
            });

            context.Vehicles.Add(new Truck
            {
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 10,
                EngineSize = 6500,
                Cargo = 12500
            });

            context.Vehicles.Add(new Motorbike
            {
                Fuel = Fuel.Petrol,
                NumberOfWheels = 2,
                NumberOfPistons = 1
            });
            context.SaveChanges();
        }

        private static void DemonstrateTablePerConcreteType()
        {
            using (var context = new TablePerConcreteTypeDbContext())
            {
                AddVehiclesTablePerConcreteType(context);
                var allVehicles = context.Vehicles.ToList();
                var allCars = context.Vehicles.OfType<Car>().ToList();
                var firstCar = context.Vehicles.Find(allCars.First().Id);
            }
        }

        private static void AddVehiclesTablePerConcreteType(TablePerConcreteTypeDbContext context)
        {
            context.Vehicles.Add(new Car
            {
                Id = 1,
                Fuel = Fuel.Petrol,
                NumberOfWheels = 4,
                NumberOdSeats = 5,
                DrivingPlates = "EEP3587"
            });
            context.Vehicles.Add(new Car
            {
                Id = 2,
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 4,
                NumberOdSeats = 2,
                DrivingPlates = "EEH4685"
            });

            context.Vehicles.Add(new Truck
            {
                Id = 3,
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 6,
                EngineSize = 4500,
                Cargo = 7500
            });

            context.Vehicles.Add(new Truck
            {
                Id = 4,
                Fuel = Fuel.Petroleum,
                NumberOfWheels = 10,
                EngineSize = 6500,
                Cargo = 12500
            });

            context.Vehicles.Add(new Motorbike
            {
                Id = 5,
                Fuel = Fuel.Petrol,
                NumberOfWheels = 2,
                NumberOfPistons = 1
            });
            context.SaveChanges();
        }
    }
}