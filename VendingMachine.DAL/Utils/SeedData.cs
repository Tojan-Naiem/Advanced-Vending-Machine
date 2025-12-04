using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace VendingMachine.DAL.Utils
{
    public class SeedData:ISeedData
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
           
        }
}
