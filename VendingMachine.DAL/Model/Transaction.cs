using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DAL.Model
{
    public enum PaymentMethod
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
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
