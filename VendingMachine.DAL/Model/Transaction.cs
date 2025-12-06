using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DAL.Model
{
    public enum PaymentStatus
    {
        Success,
        Pending,
        Failed
    }
    public class Transaction
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
