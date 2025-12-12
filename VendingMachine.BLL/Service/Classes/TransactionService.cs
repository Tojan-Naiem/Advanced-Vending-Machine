using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.Repository.Interfaces;
using VendingMachine.DAL.Model;
using VendingMachine.DAL.DTO.ResponseDTO;
namespace VendingMachine.BLL.Service.Classes
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<TransactionResponse> CreateTransactionAsync(TransactionRequest request)
        {
            var product = await _transactionRepository.GetProductAsync(request.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var newItem = new Transaction()
            {
               
                PaymentStatus=PaymentStatus.Pending,
                ProductId=request.ProductId,
                CreatedTime=DateTime.Now
            };
             await  _transactionRepository.SaveAsync(newItem);
            return new TransactionResponse()
            {
                Id=newItem.Id,
                Name= product.Name,
                Price= product.Price,
                CreatedTime=newItem.CreatedTime,
            };

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
        public async Task<TransactionResponse> GetTransactionAsync(long transactionId)
        {
            var item = await _transactionRepository.GetTransactionAsync(transactionId);
            if(item is null)
            {
             
            }
            return new TransactionResponse()
            {
                Id=item!.Id,
                CreatedTime=item!.CreatedTime,
                PaymentStatus=item.PaymentStatus,
                Name=item.Product.Name,
                Price=item.Product.Price
            };
        }
    }
}
