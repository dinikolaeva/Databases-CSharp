using CarDealer.Data;
using CarDealer.DTO.Export;
using CarDealer.DTO.Input;
using CarDealer.Models;
using CarDealer.XMLHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //09.Import Suppliers
            //var supplierXml = File.ReadAllText("./Datasets/suppliers.xml");
            //Console.WriteLine(ImportSuppliers(context, supplierXml));

            //10. Import Parts
            //var supplierXml = File.ReadAllText("./Datasets/suppliers.xml");
            //ImportSuppliers(context, supplierXml);
            //var partsXml = File.ReadAllText("./Datasets/parts.xml");
            //Console.WriteLine(ImportParts(context, partsXml));

            //11. Import Cars
            //var supplierXml = File.ReadAllText("./Datasets/suppliers.xml");
            //ImportSuppliers(context, supplierXml);
            //var partsXml = File.ReadAllText("./Datasets/parts.xml");
            //ImportParts(context, partsXml);
            //var carsJson = File.ReadAllText("./Datasets/cars.xml");
            //Console.WriteLine(ImportCars(context, carsJson));

            //12. Import Customers
            //var customersXml = File.ReadAllText("./Datasets/customers.xml");
            //Console.WriteLine(ImportCustomers(context, customersXml));

            //13. Import Sales
            //var supplierXml = File.ReadAllText("./Datasets/suppliers.xml");
            //ImportSuppliers(context, supplierXml);
            //var partsXml = File.ReadAllText("./Datasets/parts.xml");
            //ImportParts(context, partsXml);
            //var carsJson = File.ReadAllText("./Datasets/cars.xml");
            //ImportCars(context, carsJson);
            //var customersXml = File.ReadAllText("./Datasets/customers.xml");
            //ImportCustomers(context, customersXml);
            //var salesXml = File.ReadAllText("./Datasets/sales.xml");
            //Console.WriteLine(ImportSales(context, salesXml));

            //14. Export Cars With Distance
            //Console.WriteLine(GetCarsWithDistance(context));

            //15. Export Cars From Make BMW
            //Console.WriteLine(GetCarsFromMakeBmw(context));

            //16. Export Local Suppliers
            //Console.WriteLine(GetLocalSuppliers(context));

            //17. Export Cars With Their List Of Parts
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));

            //18. Export Total Sales By Customer
            //Console.WriteLine(GetTotalSalesByCustomer(context));

            //19. Export Sales With Applied Discount
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            //var xmlSerializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("Suppliers"));
            //var textRead = new StringReader(inputXml);
            //var suppliersDto = xmlSerializer.Deserialize(textRead) as SupplierDto[];

            var suppliersDto = XmlConverter.Deserializer<SupplierDto>(inputXml, "Suppliers");

            var suppliers = suppliersDto.Select(s => new Supplier
            {
                Name = s.Name,
                IsImporter = s.IsImporter
            });

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            //Without XMLHelper:
            //var xmlSerializer = new XmlSerializer(typeof(PartsDto[]), new XmlRootAttribute("Parts"));
            //var textRead = new StringReader(inputXml);
            //var partsDto = xmlSerializer.Deserialize(textRead) as PartsDto[];

            //With XMLHelper:
            var partsDto = XmlConverter.Deserializer<PartsDto>(inputXml, "Parts");

            var suppliersId = context.Suppliers
                                     .Select(s => s.Id)
                                     .ToList();

            var parts = partsDto.Where(p => suppliersId.Contains(p.SupplierId))
                                .Select(p => new Part
                                {
                                    Name = p.Name,
                                    Price = p.Price,
                                    Quantity = p.Quantity,
                                    SupplierId = p.SupplierId
                                })
                                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        //11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsDto = XmlConverter.Deserializer<CarsDto>(inputXml, "Cars");

            var cars = new List<Car>();

            var partsIds = context.Parts
                                  .Select(i => i.Id)
                                  .ToList();

            foreach (var car in carsDto)
            {
                var parts = car.CarParts
                               .Select(p => p.Id)
                               .Distinct();

                var currCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };

                foreach (var part in parts.Intersect(partsIds))
                {
                    var currPart = new PartCar()
                    {
                        PartId = part
                    };

                    currCar.PartCars.Add(currPart);
                }

                cars.Add(currCar);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var costomersDto = XmlConverter.Deserializer<CustomersDto>(inputXml, "Customers");

            var customers = costomersDto.Select(c => new Customer
            {
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver
            })
                                         .ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var salesDto = XmlConverter.Deserializer<SalesDto>(inputXml, "Sales");

            var carIds = context.Cars
                                .Select(c => c.Id)
                                .ToList();

            var sales = salesDto.Where(s => carIds.Contains(s.CarId))
                                .Select(s => new Sale
                                {
                                    CarId = s.CarId,
                                    CustomerId = s.CustomerId,
                                    Discount = s.Discount
                                })
                                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        //14. Export Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                              .Where(c => c.TravelledDistance > 2_000_000)
                              .Select(c => new ExportCarsDto
                              {
                                  Make = c.Make,
                                  Model = c.Model,
                                  TravelledDistance = c.TravelledDistance
                              })
                              .OrderBy(c => c.Make)
                              .ThenBy(c => c.Model)
                              .Take(10)
                              .ToList();

            return XmlConverter.Serialize(cars, "cars");
        }

        //15. Export Cars From Make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars.Where(c => c.Make == "BMW")
                                   .Select(c => new ExportsCarsBWVDto
                                   {
                                       Id = c.Id,
                                       Model = c.Model,
                                       TravelledDistance = c.TravelledDistance
                                   })
                                   .OrderBy(c => c.Model)
                                   .ThenByDescending(c => c.TravelledDistance)
                                   .ToList();

            return XmlConverter.Serialize(cars, "cars");
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                                   .Where(s => s.IsImporter == false)
                                   .Select(s => new ExportsSuppliersDto
                                   {
                                       Id = s.Id,
                                       Name = s.Name,
                                       PartsCount = s.Parts.Count
                                   })
                                   .ToList();

            return XmlConverter.Serialize(suppliers, "suppliers");
        }

        //17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                              .Select(c => new ExportCarsParts
                              {
                                  Make = c.Make,
                                  Model = c.Model,
                                  TravelledDistance = c.TravelledDistance,
                                  CarPartsArray = c.PartCars
                                                   .Select(cp => new ExportParts
                                                   {
                                                       Name = cp.Part.Name,
                                                       Price = cp.Part.Price
                                                   })
                                                   .OrderByDescending(p => p.Price)
                                                   .ToArray()
                              })
                              .OrderByDescending(c => c.TravelledDistance)
                              .ThenBy(c => c.Model)
                              .Take(5)
                              .ToList();

            return XmlConverter.Serialize(cars, "cars");
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                                   .Where(c => c.Sales.Count >= 1)
                                   .Select(c => new ExportCustomersDto
                                   {
                                       Name = c.Name,
                                       BoughtCars = c.Sales.Count,
                                       SpentMoney = c.Sales.Select(c => c.Car)
                                                           .SelectMany(p => p.PartCars)
                                                           .Sum(pc => pc.Part.Price)
                                   })
                                   .OrderByDescending(m => m.SpentMoney)
                                   .ToList();

            return XmlConverter.Serialize(customers, "customers");
        }

        //19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                               .Select(s => new ExportSaleDto
                               {
                                   Car = new CarDto
                                   {
                                       Make = s.Car.Make,
                                       Model = s.Car.Model,
                                       TravelledDistance = s.Car.TravelledDistance
                                   },
                                   Discount = s.Discount,
                                   CustomerName = s.Customer.Name,
                                   Price = s.Car.PartCars.Sum(p => p.Part.Price),
                                   PriceWithDiscount = s.Car.PartCars.Sum(p => p.Part.Price) - s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100
                               })
                               .ToList();

            return XmlConverter.Serialize(sales, "sales");
        }
    }
}