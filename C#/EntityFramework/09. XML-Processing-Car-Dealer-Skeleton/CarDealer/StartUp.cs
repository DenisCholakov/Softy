using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DataTransferObjects.Input;
using CarDealer.DataTransferObjects.Output;
using CarDealer.Models;
using CarDealer.XMLHelper;

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

            //var suppliersXml = File.ReadAllText("./Datasets/suppliers.xml");
            //var partsXml = File.ReadAllText("./Datasets/parts.xml");
            //var carsXml = File.ReadAllText("./Datasets/cars.xml");
            //var customersXml = File.ReadAllText("./Datasets/customers.xml");
            //var salesXml = File.ReadAllText("./Datasets/sales.xml");

            //ImportSuppliers(context, suppliersXml);
            //ImportParts(context, partsXml);
            //ImportCars(context, carsXml);
            //ImportCustomers(context, customersXml);
            //var result = ImportSales(context, salesXml);

            var result = GetSalesWithAppliedDiscount(context);

            Console.WriteLine(result);
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new SaleOutputModel
                {
                    Car = new SaleCarOutputModel
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(cp => cp.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(cp => cp.Part.Price) * ((100m - s.Discount )/ 100m)
                }).ToArray();

            var result = XmlConverter.Serialize(sales, "sales");

            return result;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers.Where(c => c.Sales.Any())
                .Select(x => new CustomerOutputModel
                {
                    FullName = x.Name,
                    BoughtCars = x.Sales.Count,
                    SpentMoney = x.Sales.Select(s => s.Car).SelectMany(x => x.PartCars)
                        .Sum(x => x.Part.Price)
                }).OrderByDescending(x => x.SpentMoney)
                .ToList();

            var result = XmlConverter.Serialize(customers, "customers");

            return result;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new CarPartOutputModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(cp => new CarPartsInfoOutputModel
                    {
                        Name = cp.Part.Name,
                        Price = cp.Part.Price
                    }).OrderByDescending(p => p.Price).ToArray()
                }).OrderByDescending(x => x.TraveledDistance)
                .ThenBy(x => x.Model)
                .Take(5)
                .ToArray();

            var result = XmlConverter.Serialize(cars, "cars");

            return result;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers.Where(s => !s.IsImporter)
                .Select(x => new SupplierOutputModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count()
                }).ToList();

            var result = XmlConverter.Serialize(suppliers, "suppliers");

            return result;
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars.Where(c => c.Make == "BMW")
                .Select(c => new BMWOutputModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    TraveledDistance = c.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TraveledDistance)
                .ToList();

            var result = XmlConverter.Serialize(cars, "cars");

            return result;
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2_000_000)
                .Select(c => new CarOutputModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TravelledDistance
                }).OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CarOutputModel[]), new XmlRootAttribute("cars"));

            var textWriter = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("","");
            xmlSerializer.Serialize(textWriter, cars, ns);

            var result = textWriter.ToString();

            return result;
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SupplierInputModel[]), new XmlRootAttribute("Suppliers"));
            var textRead = new StringReader(inputXml);
            var suppliersDto = xmlSerializer.Deserialize(textRead) as SupplierInputModel[];

            var suppliers = (suppliersDto ?? Array.Empty<SupplierInputModel>()).Select(x => new Supplier
            {
                Name = x.Name,
                IsImporter = x.IsImporter
            }).ToList();


            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(PartInputModel[]), new XmlRootAttribute("Parts"));
            var textReader = new StringReader(inputXml);

            var partsDto = xmlSerializer.Deserialize(textReader) as PartInputModel[];

            var suppliersIds = context.Suppliers.Select(s => s.Id).ToList();

            var parts = (partsDto ?? Array.Empty<PartInputModel>())
                .Where(s => suppliersIds.Contains(s.SupplierId))
                .Select(x => new Part
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    SupplierId = x.SupplierId
                }).ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsDto = XmlConverter.Deserializer<CarInputModel>(inputXml, "Cars");

            var allPartsIds = context.Parts.Select(p => p.Id).ToList();

            var cars = carsDto
                .Select(c => new Car
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TraveledDistance,
                    PartCars = c.Parts.Select(p => p.Id)
                        .Where(p => allPartsIds.Contains(p))
                        .Distinct()
                        .Select(p => new PartCar
                        {
                            PartId = p
                        })
                        .ToList()
                });

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";

        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            InitializeAutoMapper();

            var customersDto = XmlConverter.Deserializer<CustomerInputModel>(inputXml, "Customers");
            var customers = _mapper.Map<Customer[]>(customersDto);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var salesDto = XmlConverter.Deserializer<SaleInputModel>(inputXml, "Sales");
            var carsIds = context.Cars.Select(x => x.Id).ToList();
            var sales = salesDto
                .Where(s => carsIds.Contains(s.CarId))
                .Select(s => new Sale
                {
                    CarId = s.CarId,
                    CustomerId = s.CustomerId,
                    Discount = s.Discount
                }).ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}";
        }

        private static void InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());

            _mapper = config.CreateMapper();
        }
    }
}