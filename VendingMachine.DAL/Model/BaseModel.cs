using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DAL.Model
{
    public enum Status{
        Active=1,
        In_Active=2
    }
    public class BaseModel
    {
        public long Id { get; set; }
        public Status Status { get; set; } = Status.Active;
    }
}
