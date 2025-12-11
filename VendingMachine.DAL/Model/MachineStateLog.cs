using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DAL.Model
{
    public class MachineStateLog
    {
        public long Id { get; set; }
        public string? StateBefore { get; set; }

        public string StateAfter { get; set; } = string.Empty;

        public string? EventTriggered { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
