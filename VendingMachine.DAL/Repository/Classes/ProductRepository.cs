using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.Model;
using VendingMachine.DAL.Repository.Interfaces;

namespace VendingMachine.DAL.Repository.Classes
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(
            ApplicationDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }
        public async Task SaveAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
         
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<Product?> GetProductAsync(long id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
        public async Task RemoveAsync(Product product)
        {
             _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangesInDatabase()
        {
            await _dbContext.SaveChangesAsync();

        }
    }
}
