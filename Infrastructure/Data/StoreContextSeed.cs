using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                if (!context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    context.ProductTypes.AddRange(Types);
                    await context.SaveChangesAsync();
                }

                if (!context.ProductBrands.Any())
                {
                    var BrandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    context.ProductBrands.AddRange(Brands);
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    context.Products.AddRange(Products);
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
