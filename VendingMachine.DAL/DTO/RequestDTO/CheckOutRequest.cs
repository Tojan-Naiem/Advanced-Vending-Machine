using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DAL.DTO.RequestDTO
{
    public class CheckOutRequest
    {
        public long transactionId { get; set; }
    }
}
