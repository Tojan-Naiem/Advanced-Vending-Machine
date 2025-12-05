using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.Repository.Interfaces;
using VendingMachine.DAL.Model;
namespace VendingMachine.BLL.Service.Classes
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<bool> CreateTransactionAsync(TransactionRequest request)
        {
            var newItem = new Transaction()
            {
                PaymentMethod=PaymentMethod.Pending,
                ProductId=request.ProductId,
                CreatedTime=DateTime.Now
            };
             await  _transactionRepository.SaveAsync(newItem);
            return true;

        }

        public async Task<bool> DeleteTransactionAsync(long transactionId)
        {
            var item = await _transactionRepository.GetTransactionAsync(transactionId);
            if(item is null)
            {
                return false;
            }
             await _transactionRepository.RemoveAsync(item);
            return true;
        }
    }
}
