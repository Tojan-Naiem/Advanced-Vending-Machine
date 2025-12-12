using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.DAL.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        public Task SaveAsync(Transaction request);
        public Task<List<Transaction>> GetTransactionsAsync();
        public Task<Transaction?> GetTransactionAsync(long id);
        public Task RemoveAsync(Transaction request);
        public Task SaveChangesInDatabase();
        public Task<Product?> GetProductAsync(long id);

    }
}
