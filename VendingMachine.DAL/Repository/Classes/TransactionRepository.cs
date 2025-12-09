using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Model;
using VendingMachine.DAL.Repository.Interfaces;

namespace VendingMachine.DAL.Repository.Classes
{
    public class TransactionRepository:ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TransactionRepository(
                  ApplicationDbContext dbContext
                  )
        {
            _dbContext = dbContext;
        }
        public async Task SaveAsync(Transaction request)
        {
            await _dbContext.Transactions.AddAsync(request);
            await _dbContext.SaveChangesAsync();

        }
        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }
        public async Task<Transaction?> GetTransactionAsync(long id)
        {
            return await _dbContext.Transactions
                 .Include(t => t.Product)
                 .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task RemoveAsync(Transaction request)
        {
            _dbContext.Transactions.Remove(request);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangesInDatabase()
        {
            await _dbContext.SaveChangesAsync();

        }
    }
}
