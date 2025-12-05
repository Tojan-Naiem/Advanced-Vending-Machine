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
                    new Product { Name = "Clothes" },
                    new Product { Name = "Phones" }
                    );
            }
            await _dbContext.SaveChangesAsync();

        }
    }
}
