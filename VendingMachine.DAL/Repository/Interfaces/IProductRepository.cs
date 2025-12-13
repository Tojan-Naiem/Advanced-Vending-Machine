using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.DAL.Repository.Interfaces
{
    public interface IProductRepository
    {
        public Task SaveAsync(Product product);
        public Task<List<Product>> GetProductsAsync();
        public Task<Product?> GetProductAsync(long id);
        public Task RemoveAsync(Product product);
        public Task SaveChangesInDatabase();
        public Task DecreaseProductQuantityAsync(Product product);

    }
}
