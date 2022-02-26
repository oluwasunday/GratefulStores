using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductsBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrands>>(brandsData);

                    foreach (var brand in brands)
                    {
                        context.ProductsBrands.Add(brand);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.ProductsTypes.Any())
                {
                    var productsTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var productsTypes = JsonSerializer.Deserialize<List<ProductTypes>>(productsTypesData);

                    foreach (var prodType in productsTypes)
                    {
                        context.ProductsTypes.Add(prodType);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var prod in products)
                    {
                        context.Products.Add(prod);
                    }
                    await context.SaveChangesAsync();
                }
                
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
