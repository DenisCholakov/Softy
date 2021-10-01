using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        private static IMapper _mapper;

        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //var suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //var partsJson = File.ReadAllText("../../../Datasets/parts.json");
            //var carsJson = File.ReadAllText("../../../Datasets/cars.json");
            //var customersJson = File.ReadAllText("../../../Datasets/customers.json");
            //var salesJson = File.ReadAllText("../../../Datasets/sales.json");

            //ImportSuppliers(context, suppliersJson);
            //ImportParts(context, partsJson);
            //ImportCars(context, carsJson);
            //ImportCustomers(context, customersJson);
            //ImportSales(context, salesJson);

            var result = GetSalesWithAppliedDiscount(context);

            Console.WriteLine(result);

        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var salesDb = context.Sales
                .Include(s => s.Car)
                .ThenInclude(c => c.PartCars)
                .ThenInclude(cp => cp.Part)
                .Include(s => s.Customer).ToList();

            var sales = new List<SaleOutputModel>();

            foreach (var sale in salesDb)
            {
                var price = sale.Car.PartCars.Sum(cp => cp.Part.Price);

                var currentSale = new SaleOutputModel
                {
                    car = new CarOutputModel
                    {
                        Make = sale.Car.Make,
                        Model = sale.Car.Model,
                        TravelledDistance = sale.Car.TravelledDistance
                    },
                    customerName = sale.Customer.Name,
                    Discount = sale.Discount.ToString("f2"),
                    price = price.ToString("f2"),
                    priceWithDiscount = (price - (price * (sale.Discount / 100))).ToString("f2")
                };

                sales.Add(currentSale);
            };

            var result = JsonConvert.SerializeObject(sales.Take(10), Formatting.Indented);

            return result;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers.Where(c => c.Sales.Any())
                .Select(customer => new
                {
                    fullName = customer.Name,
                    boughtCars = customer.Sales.Count(),
                    spentMoney = customer.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                }).OrderByDescending(x => x.spentMoney)
                .ThenByDescending(x => x.boughtCars)
                .ToList();

            var result = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return result;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars.Select(c => new
            {
                car = new
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                },
                parts = c.PartCars.Select(p => new
                {
                    Name = p.Part.Name,
                    Price = p.Part.Price.ToString("f2"),
                })
            }).ToList();

            var result = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return result;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers.Where(s => !s.IsImporter)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                }).ToList();

            var result = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return result;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars.Where(c => c.Make == "Toyota")
                .Select(c => new
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                }).OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();

            var result = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return result;
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = c.IsYoungDriver
                }).ToList();

            var result = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return result;
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            InitializeMapper();

            var salesJson = JsonConvert.DeserializeObject<IEnumerable<SaleInputModel>>(inputJson);
            var sales = _mapper.Map<IEnumerable<Sale>>(salesJson);

            context.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            InitializeMapper();

            var customersJson = JsonConvert.DeserializeObject<IEnumerable<CustomerInputModel>>(inputJson);
            var customers = _mapper.Map<IEnumerable<Customer>>(customersJson);

            context.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}.";

        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsJson = JsonConvert.DeserializeObject<IEnumerable<CarInputModel>>(inputJson);
            var listOfCars = new List<Car>();

            foreach (var car in carsJson)
            {
                var currentCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };

                // can use .Distinct()
                //var ids = new HashSet<int>(car.PartsId);

                foreach (var partId in car.PartsId.Distinct())
                {
                    currentCar.PartCars.Add(new PartCar
                    {
                        PartId = partId
                    });
                }

                listOfCars.Add(currentCar);
                // Judge compile time error
                //context.Cars.Add(currentCar);
                //context.SaveChanges();
            }

            context.Cars.AddRange(listOfCars);
            context.SaveChanges();

            return $"Successfully imported { listOfCars.Count() }.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            InitializeMapper();

            var suppliersIds = context.Suppliers.Select(s => s.Id).ToHashSet();

            var partsJson = JsonConvert.DeserializeObject<IEnumerable<PartsInutModel>>(inputJson)
                .Where(p => suppliersIds.Contains(p.supplierId));
            var parts = _mapper.Map<IEnumerable<Part>>(partsJson);

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported { parts.Count() }.";
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliersJson = JsonConvert.DeserializeObject<ICollection<SupplierInputModel>>(inputJson);

            var suppliers = suppliersJson.Select(x => new Supplier
            {
                Name = x.Name,
                IsImporter = x.IsImporter
            }).ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}.";
        }

        private static void InitializeMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
            _mapper = new Mapper(config);
        }
    }
}