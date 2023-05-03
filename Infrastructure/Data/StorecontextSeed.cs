using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StorecontextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory )
        {
        try
        {
         if(!context.ProductBrands.Any())
         {
         var brancData =File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

         var brand =JsonSerializer.Deserialize<List<ProductBrand>> (brancData);

         foreach(var item in brand)
         {
        context.ProductBrands.Add(item);
         }
         }


      if(!context.Products.Any())
         {
         var productsData =File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

         var products =JsonSerializer.Deserialize<List<Product>> (productsData);

         foreach(var item in products)
         {
        context.Products.Add(item);
         }
         }


        if(!context.ProductTypes.Any())
         {
         var tyoesData =File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

         var productstype =JsonSerializer.Deserialize<List<ProductType>> (tyoesData);

         foreach(var item in productstype)
         {
        context.ProductTypes.Add(item);
         }


         }

        await context.SaveChangesAsync();

        }
        catch(Exception ex)
        {
          
           var logger =loggerFactory.CreateLogger<StorecontextSeed>();
           logger.LogError(ex.Message);
        }
    }
}}