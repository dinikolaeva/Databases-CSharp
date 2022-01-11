using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DataTransferObjects;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        static IMapper mapper;

        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            ////01. Import Users
            //var inputJson = File.ReadAllText("../../../Datasets/users.json");
            //Console.WriteLine(ImportUsers(context, inputJson));

            //02. Import Products
            //var usersJson = File.ReadAllText("../../../Datasets/users.json");
            //ImportUsers(context, usersJson);
            //var productJson = File.ReadAllText("../../../Datasets/products.json");
            //Console.WriteLine(ImportProducts(context, productJson));

            //03. Import Categories
            //var categoriesJson = File.ReadAllText("../../../Datasets/categories.json");
            //Console.WriteLine(ImportCategories(context, categoriesJson));

            //04. Import Categories and Products
            var usersJson = File.ReadAllText("../../../Datasets/users.json");
            ImportUsers(context, usersJson);
            var productJson = File.ReadAllText("../../../Datasets/products.json");
            ImportProducts(context, productJson);
            var categoriesJson = File.ReadAllText("../../../Datasets/categories.json");
            ImportCategories(context, categoriesJson);
            var cpJson = File.ReadAllText("../../../Datasets/categories-products.json");
            Console.WriteLine(ImportCategoryProducts(context, cpJson));

            //05. Export Products In Range
            //Console.WriteLine(GetProductsInRange(context));

            //06. Export Sold Products
            //Console.WriteLine(GetSoldProducts(context));

            //07. Export Categories By Products Count
            //Console.WriteLine(GetCategoriesByProductsCount(context));

            //08. Export Users and Products
            //Console.WriteLine(GetUsersWithProducts(context));

        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            InitializeAutoMapper();

            var users = JsonConvert.DeserializeObject<IEnumerable<UserInputModel>>(inputJson);

            var mappedUsers = mapper.Map<IEnumerable<User>>(users);

            context.Users.AddRange(mappedUsers);
            context.SaveChanges();

            return $"Successfully imported {mappedUsers.Count()}";
        }

        //02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            InitializeAutoMapper();

            var products = JsonConvert.DeserializeObject<IEnumerable<ProductInputModel>>(inputJson);

            var mappedProducts = mapper.Map<IEnumerable<Product>>(products);

            context.Products.AddRange(mappedProducts);
            context.SaveChanges();

            return $"Successfully imported {mappedProducts.Count()}";
        }

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            InitializeAutoMapper();

            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoriesInputModel>>(inputJson)
                                        .Where(c => c.Name != null)
                                        .ToList();

            var mappedCategories = mapper.Map<IEnumerable<Category>>(categories);

            context.Categories.AddRange(mappedCategories);
            context.SaveChanges();

            return $"Successfully imported {mappedCategories.Count()}";
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            InitializeAutoMapper();

            var catProd = JsonConvert.DeserializeObject<IEnumerable<CatProdInputModel>>(inputJson);

            var mappedCatProd = mapper.Map<IEnumerable<CategoryProduct>>(catProd);

            context.CategoryProducts.AddRange(mappedCatProd);
            context.SaveChanges();

            return $"Successfully imported {mappedCatProd.Count()}";
        }


        //05. Export Products in Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                                  .Where(p => p.Price >= 500 && p.Price <= 1000)
                                  .Select(i => new
                                  {
                                      name = i.Name,
                                      price = i.Price,
                                      seller = i.Seller.FirstName + " " + i.Seller.LastName
                                  })
                                  .OrderBy(p => p.price)
                                  .ToList();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                               .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                               .Select(u => new
                               {
                                   firstName = u.FirstName,
                                   lastName = u.LastName,
                                   soldProducts = u.ProductsSold
                                                   .Select(ps => new
                                                   {
                                                       name = ps.Name,
                                                       price = ps.Price,
                                                       buyerFirstName = ps.Buyer.FirstName,
                                                       buyerLastName = ps.Buyer.LastName
                                                   })
                               })
                               .OrderBy(u => u.lastName)
                               .ThenBy(u => u.firstName)
                               .ToList();

            return JsonConvert.SerializeObject(users, Formatting.Indented);
        }

        //07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                                    .Select(c => new
                                    {
                                        category = c.Name,
                                        productsCount = c.CategoryProducts.Count,
                                        averagePrice = c.CategoryProducts
                                                        .Average(p => p.Product.Price)
                                                        .ToString("F2"),
                                        totalRevenue = c.CategoryProducts
                                                        .Sum(p => p.Product.Price)
                                                        .ToString("F2")
                                    })
                                    .OrderByDescending(cat => cat.productsCount)
                                    .ToList();

            return JsonConvert.SerializeObject(categories, Formatting.Indented);
        }

        //08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var orderedUsers = context.Users
                                      .Include(u=>u.ProductsSold)
                                      .ToList()
                                      .Where(u => u.ProductsSold
                                                   .Any(b => b.BuyerId != null))
                                      .Select(us => new
                                      {
                                          firstName = us.FirstName,
                                          lastName = us.LastName,
                                          age = us.Age,
                                          soldProducts = new
                                          {
                                              count = us.ProductsSold
                                                        .Where(b => b.BuyerId != null)
                                                        .Count(),
                                              products = us.ProductsSold
                                                           .Where(b => b.BuyerId != null)
                                                           .Select(p => new
                                                           {
                                                               name = p.Name,
                                                               price = p.Price
                                                           })
                                          }
                                      })
                                      .OrderByDescending(x => x.soldProducts.count)
                                      .ToList();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var result = new
            {
                usersCount = context.Users
                                    .Where(u => u.ProductsSold
                                                 .Any(b => b.BuyerId != null))
                                    .Count(),
                users = orderedUsers
            };

            return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSerializerSettings);
        }

        private static void InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            mapper = new Mapper(config);
        }
    }
}