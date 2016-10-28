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
                      Name = "Electronics"
                  },
                  new ProductType { 
                      Name = "Food and Drink"
                  },
                  new ProductType { 
                      Name = "Appliances"
                  },
              };

              foreach (ProductType i in productTypes)
              {
                  context.ProductType.Add(i);
              }
              context.SaveChanges();

              var subProductTypes = new SubProductType[]
              {
                  new SubProductType { 
                      Name = "On the Go",
                      Description = "Home electronics",
                      ProductTypeId = productTypes.Single(s => s.Name == "Electronics").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "La Croix",
                      Description = "Cool La Croix taste really good",
                      ProductTypeId = productTypes.Single(s => s.Name == "Food and Drink").ProductTypeId
                  },
                  new SubProductType { 
                      Name = "Kitchen",
                      Description = "Appliances for your kitchen",
                      ProductTypeId = productTypes.Single(s => s.Name == "Appliances").ProductTypeId
                  },
              };

              foreach (SubProductType i in subProductTypes)
              {
                  context.SubProductType.Add(i);
              }
              context.SaveChanges();

              var products = new Product[]
              {
                  new Product { 
                      Description = "Pampelmouse La Croix which taste real good",
                      SubProductTypeId = subProductTypes.Single(s => s.Name == "La Croix").SubProductTypeId,
                      Name = "Pampelmouse La Croix",
                      Price = 3.55
                  },
                  new Product { 
                      Description = "A 2012 iPod Shuffle. Headphones are included. 16G capacity.",
                      SubProductTypeId = subProductTypes.Single(s => s.Name == "On the Go").SubProductTypeId,
                      Name = "iPod Shuffle",
                      Price = 18.00
                  },
                  new Product { 
                      Description = "Stainless steel refrigerator. Three years old. Minor scratches.",
                      SubProductTypeId = subProductTypes.Single(s => s.Name == "Kitchen").SubProductTypeId,
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