using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BangazonWeb.Models;

namespace BangazonWeb.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BangazonWebContext(serviceProvider.GetRequiredService<DbContextOptions<BangazonWebContext>>()))
            {
              // Look for any products.
              if (context.Product.Any())
              {
                  return;   // DB has been seeded
              }

              var customers = new Customer[]
              {
                  new Customer { 
                      FirstName = "James",
                      LastName = "Regnier"
                  },
                  new Customer { 
                      FirstName = "Scott",
                      LastName = "Promises"
                  },
                  new Customer { 
                      FirstName = "Zaq",
                      LastName = "Repasse"
                  }
              };

              foreach (Customer c in customers)
              {
                  context.Customer.Add(c);
              }
              context.SaveChanges();

              var productTypes = new ProductType[]
              {
                  new ProductType { 
                      Name = "Electronics",
                      Description = "Many electronics here"
                  },
                  new ProductType { 
                      Name = "La Croix",
                      Description = "Cool La Croix taste really good"
                  },
                  new ProductType { 
                      Name = "Appliances",
                      Description = "Many appliances, much productivity"
                  },
              };

              foreach (ProductType i in productTypes)
              {
                  context.ProductType.Add(i);
              }
              context.SaveChanges();


              var products = new Product[]
              {
                  new Product { 
                      Description = "Pampelmouse La Croix which taste real good",
                      ProductTypeId = productTypes.Single(s => s.Name == "La Croix").ProductTypeId,
                      Name = "Pampelmouse La Croix",
                      Price = 3.55
                  },
                  new Product { 
                      Description = "A 2012 iPod Shuffle. Headphones are included. 16G capacity.",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics").ProductTypeId,
                      Name = "iPod Shuffle",
                      Price = 18.00
                  },
                  new Product { 
                      Description = "Stainless steel refrigerator. Three years old. Minor scratches.",
                      ProductTypeId = productTypes.Single(s => s.Name == "Appliances").ProductTypeId,
                      Name = "Samsung refrigerator",
                      Price = 500.00
                  }
              };

              foreach (Product i in products)
              {
                  context.Product.Add(i);
              }
              context.SaveChanges();
          }
       }
    }
}