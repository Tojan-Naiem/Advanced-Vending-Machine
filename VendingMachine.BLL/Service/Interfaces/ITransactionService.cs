using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.DTO.ResponseDTO;

namespace VendingMachine.BLL.Service.Interfaces
{
    public interface ITransactionService
    {
        public Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request);
        public Task<bool> DeleteTransactionAsync(long transactionId);
        public Task<TransactionResponse> GetTransactionAsync(long transactionId);
    }
}
