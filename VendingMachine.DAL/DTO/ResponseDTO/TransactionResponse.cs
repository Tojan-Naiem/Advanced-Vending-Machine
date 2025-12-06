using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.DAL.DTO.ResponseDTO
{
    public class TransactionResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
