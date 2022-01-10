using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        static IMapper mapper;

        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //09. Import Suppliers
            //var suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //Console.WriteLine(ImportSuppliers(context, suppliersJson));

            //10. Import Parts
            //var suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //ImportSuppliers(context, suppliersJson);
            //var partsJson = File.ReadAllText("../../../Datasets/parts.json");
            //Console.WriteLine(ImportParts(context, partsJson));

            //11. Import Cars
            //var suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //ImportSuppliers(context, suppliersJson);
            //var partsJson = File.ReadAllText("../../../Datasets/parts.json");
            //ImportParts(context, partsJson);
            //var carsJson = File.ReadAllText("../../../Datasets/cars.json");
            //Console.WriteLine(ImportCars(context, carsJson));           

            //12.Import Customers
            //var customersJson = File.ReadAllText("../../../Datasets/customers.json");
            //Console.WriteLine(ImportCustomers(context, customersJson));

            //13. Import Sales
            //var suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //ImportSuppliers(context, suppliersJson);
            //var partsJson = File.ReadAllText("../../../Datasets/parts.json");
            //ImportParts(context, partsJson);
            //var carsJson = File.ReadAllText("../../../Datasets/cars.json");
            //ImportCars(context, carsJson);
            //var customersJson = File.ReadAllText("../../../Datasets/customers.json");
            //ImportCustomers(context, customersJson);
            //var salesJson = File.ReadAllText("../../../Datasets/sales.json");
            //Console.WriteLine(ImportSales(context, salesJson));

            //14. Export Ordered Customers
            //Console.WriteLine(GetOrderedCustomers(context));

            //15.Export Cars From Make Toyota
            //Console.WriteLine(GetCarsFromMakeToyota(context));

            //16. Export Local Suppliers
            //Console.WriteLine(GetLocalSuppliers(context));

            //17. Export Cars With Their List Of Parts
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));

            //18.Export Total Sales By Customer
            //Console.WriteLine(GetTotalSalesByCustomer(context));

            //19.Export Sales With Applied Discount
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            InitialiseAutoMapper();

            var suppliers = JsonConvert.DeserializeObject<IEnumerable<SuppliersDTO>>(inputJson);

            var mappedSuppliers = mapper.Map<IEnumerable<Supplier>>(suppliers);

            context.Suppliers.AddRange(mappedSuppliers);
            context.SaveChanges();

            return $"Successfully imported {mappedSuppliers.Count()}.";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            InitialiseAutoMapper();

            var suppliersId = context.Suppliers
                                     .Select(s => s.Id)
                                     .ToList();

            var parts = JsonConvert.DeserializeObject<IEnumerable<PartsDTO>>(inputJson)
                                   .Where(p => suppliersId.Any(s => s == p.SupplierId))
                                   .ToList();

            var mappedParts = mapper.Map<IEnumerable<Part>>(parts);

            context.Parts.AddRange(mappedParts);
            context.SaveChanges();

            return $"Successfully imported {mappedParts.Count()}.";
        }

        //11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            InitialiseAutoMapper();

            var carsDto = JsonConvert.DeserializeObject<IEnumerable<CarsDTO>>(inputJson);

            var cars = new List<Car>();
            var parts = new List<PartCar>();

            foreach (var car in carsDto)
            {
                var currCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };

                foreach (var part in car.PartsId.Distinct())
                {
                    var currPart = new PartCar()
                    {
                        PartId = part,
                        Car = currCar
                    };

                    parts.Add(currPart);
                }

                cars.Add(currCar);
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}.";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            InitialiseAutoMapper();

            var customersDto = JsonConvert.DeserializeObject<IEnumerable<CustomersDTO>>(inputJson);

            var mappedCustomers = mapper.Map<IEnumerable<Customer>>(customersDto);

            context.Customers.AddRange(mappedCustomers);
            context.SaveChanges();

            return $"Successfully imported {mappedCustomers.Count()}.";
        }

        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var salesDto = JsonConvert.DeserializeObject<IEnumerable<Sale>>(inputJson);

            //var mappedSales = mapper.Map<IEnumerable<Sale>>(salesDto); Judge don`t want it!

            context.Sales.AddRange(salesDto);
            context.SaveChanges();

            return $"Successfully imported {salesDto.Count()}.";
        }

        //14. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var cusomers = context.Customers
                                  .OrderBy(d => d.BirthDate)
                                  .ThenBy(x => x.IsYoungDriver)
                                  .Select(c => new
                                  {
                                      Name = c.Name,
                                      BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                                      IsYoungDriver = c.IsYoungDriver
                                  })
                                  .ToList();

            return JsonConvert.SerializeObject(cusomers, Formatting.Indented);
        }

        //15. Export Cars From Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context.Cars
                                    .Where(c => c.Make == "Toyota")
                                    .Select(t => new
                                    {
                                        Id = t.Id,
                                        Make = t.Make,
                                        Model = t.Model,
                                        TravelledDistance = t.TravelledDistance,

                                    })
                                    .OrderBy(x => x.Model)
                                    .ThenByDescending(t => t.TravelledDistance)
                                    .ToList();

            return JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                                  .Where(s => s.IsImporter == false)
                                  .Select(i => new
                                  {
                                      Id = i.Id,
                                      Name = i.Name,
                                      PartsCount = i.Parts.Count
                                  })
                                  .ToList();

            return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
        }

        //17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                               .Select(c => new
                               {
                                   car = new
                                   {
                                       Make = c.Make,
                                       Model = c.Model,
                                       TravelledDistance = c.TravelledDistance
                                   },
                                   parts = c.PartCars
                                                  .Select(p => new
                                                  {
                                                      Name = p.Part.Name,
                                                      Price = $"{p.Part.Price:F2}"
                                                  })
                                                  .ToArray()
                               })
                               .ToList();

            return JsonConvert.SerializeObject(cars, Formatting.Indented);
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                                   .Where(c => c.Sales.Count >= 1)
                                   .Select(i => new
                                   {
                                       fullName = i.Name,
                                       boughtCars = i.Sales.Count,
                                       spentMoney = i.Sales
                                                     .Sum(c => c.Car.PartCars
                                                                  .Sum(p => p.Part.Price))
                                   })
                                   .OrderByDescending(x => x.spentMoney)
                                   .ThenByDescending(x => x.boughtCars)
                                   .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        //19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                               .Select(s => new
                               {
                                   car = new
                                   {
                                       Make = s.Car.Make,
                                       Model = s.Car.Model,
                                       TravelledDistance = s.Car.TravelledDistance
                                   },
                                   customerName = s.Customer.Name,
                                   Discount = s.Discount.ToString("F2"),
                                   price = s.Car.PartCars
                                                .Sum(p => p.Part.Price).ToString("F2"),
                                   priceWithDiscount = $"{ s.Car.PartCars.Sum(p => p.Part.Price) - (s.Discount / 100) * (s.Car.PartCars.Sum(p => p.Part.Price)):F2}"
                               })
                               .Take(10)
                               .ToList();

            return JsonConvert.SerializeObject(sales, Formatting.Indented);
        }

        private static void InitialiseAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            mapper = new Mapper(config);
        }
    }
}