using CarDealer.XMLHelper;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.Dto.Export;
using ProductShop.Dto.Import;
using ProductShop.Models;
using System;
using System.IO;
using System.Linq;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //01. Import Users
            //var usersXml = File.ReadAllText("../../../Datasets/users.xml");
            //ImportUsers(context, usersXml);

            //02. Import Products
            //var productsXml = File.ReadAllText("../../../Datasets/products.xml");
            //ImportProducts(context, productsXml);

            //03. Import Categories
            //var categoriesXml = File.ReadAllText("../../../Datasets/categories.xml");
            //ImportCategories(context, categoriesXml);

            //04. Import Categories and Products
            //var catProductsXml = File.ReadAllText("../../../Datasets/categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(context, catProductsXml));

            //05.Export Products In Range
            //Console.WriteLine(GetProductsInRange(context));

            //06. Export Sold Products
            //Console.WriteLine(GetSoldProducts(context));

            //07. Export Categories By Products Count
            //Console.WriteLine(GetCategoriesByProductsCount(context));

            //08. Export Users and Products
            Console.WriteLine(GetUsersWithProducts(context));
        }

        //08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users.Include(u => u.ProductsSold)
                                     .ToList()
                                     .OrderByDescending(u => u.ProductsSold.Count)
                                     .Where(u => u.ProductsSold.Any())
                                     .Select(u => new UsersWithSoldProducts
                                     {
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         Age = u.Age,
                                         CountOfUserProductsDto = new CountOfUserProductsDto
                                         {
                                             Count = u.ProductsSold.Count(),
                                             SoldProducts = u.ProductsSold.Select(p => new SoldProductsDto
                                             {
                                                 Name = p.Name,
                                                 Price = p.Price
                                             })
                                             .OrderByDescending(p => p.Price)
                                             .ToArray()
                                         }
                                     })
                                     .Take(10)
                                     .ToArray();

            var usersAndProsucts = new UsersSoldProductsExportDto
            {
                CountOfProducts = context.Users
                                    .Where(u => u.ProductsSold
                                                 .Any(b => b.BuyerId != null))
                                    .Count(),
                UsersWithSoldProducts = users
            };

            return XmlConverter.Serialize(usersAndProsucts, "Users");
        }

        //07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                                    .Select(c => new CategoriesProductCountExportDto
                                    {
                                        Name = c.Name,
                                        Count = c.CategoryProducts.Count,
                                        AveragePrice = c.CategoryProducts
                                                        .Average(cp => cp.Product.Price),
                                        TotalRevenue = c.CategoryProducts
                                                        .Sum(cp => cp.Product.Price)
                                    })
                                    .OrderByDescending(c => c.Count)
                                    .ThenBy(c => c.TotalRevenue)
                                    .ToList();

            return XmlConverter.Serialize(categories, "Categories");
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                                .Where(u => u.ProductsSold.Count >= 1)
                                .Select(p => new UsersExportDto
                                {
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    SoldProducts = p.ProductsSold
                                                    .Select(ps => new SoldProductsDto
                                                    {
                                                        Name = ps.Name,
                                                        Price = ps.Price
                                                    })
                                                    .ToArray()
                                })
                                .OrderBy(u => u.LastName)
                                .ThenBy(u => u.FirstName)
                                .Take(5)
                                .ToList();

            return XmlConverter.Serialize(users, "Users");
        }

        //05.Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                                  .Where(p => p.Price >= 500 && p.Price <= 1000)
                                  .Select(p => new ProductsExportDto
                                  {
                                      Name = p.Name,
                                      Price = p.Price,
                                      Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                                  })
                                  .OrderBy(x => x.Price)
                                  .Take(10)
                                  .ToList();

            return XmlConverter.Serialize(products, "Products");
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var catProdDto = XmlConverter.Deserializer<CatProdImportDto>(inputXml, "CategoryProducts");

            var catProds = catProdDto
                .Select(cp => new CategoryProduct
                {
                    CategoryId = cp.CategoryId,
                    ProductId = cp.ProductId
                })
                .ToList();

            context.CategoryProducts.AddRange(catProds);
            context.SaveChanges();

            return $"Successfully imported {catProds.Count}";
        }

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var categoriesDto = XmlConverter.Deserializer<CategoriesImportDto>(inputXml, "Categories");

            var categories = categoriesDto
                .Select(c => new Category
                {
                    Name = c.Name
                })
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        //02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var productsDto = XmlConverter.Deserializer<ProductsImportDto>(inputXml, "Products");

            var products = productsDto
                .Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    SellerId = p.SellerId,
                    BuyerId = p.BuyerId
                });

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var usersDto = XmlConverter.Deserializer<UsersImportDto>(inputXml, "Users");

            var users = usersDto
                .Select(u => new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age
                })
                .ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }
    }
}