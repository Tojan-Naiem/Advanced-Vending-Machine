using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.DTO.RequestDTO;

namespace VendingMachine.BLL.Service.Interfaces
{
    public interface ITransactionService
    {
        public Task<bool> CreateTransaction(TransactionRequest request);
    }
}
