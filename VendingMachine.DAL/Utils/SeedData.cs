using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Model;

namespace VendingMachine.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _dbContext;
        public SeedData(
             ApplicationDbContext kdbContext
            )
        {
            _dbContext = kdbContext;

        }

        public async Task DataSeedingAsync()
        {
            if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
            if (!await _dbContext.Products.AnyAsync())
            {
                await _dbContext.Products.AddRangeAsync(
                   new Product
                   {
                       Name = "Vanilla Ice Cream",
                       Price = 5.0m,
                       Quantity = 50,
                       MainImage = string.Empty
                   },
                   new Product
                   {
                       Name = "Chocolate Ice Cream",
                       Price = 5.5m,
                       Quantity = 60,
                       MainImage = string.Empty
                   },
                   new Product
                   {
                       Name = "Strawberry Ice Cream",
                       Price = 6.0m,
                       Quantity = 40,
                       MainImage = string.Empty
                   },
                   new Product
                   {
                       Name = "Mint Ice Cream",
                       Price = 5.75m,
                       Quantity = 30,
                       MainImage = string.Empty
                   },
                   new Product
                   {
                       Name = "Cookie Dough Ice Cream",
                       Price = 6.5m,
                       Quantity = 20,
                       MainImage = string.Empty
                   }
                    );
            }
            await _dbContext.SaveChangesAsync();

        }
    }
}
