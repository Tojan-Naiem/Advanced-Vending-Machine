using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.BLL.Service.Enums
{
    public enum MachineStateType
    {
        Idle,
        WaitingForQR,
        WaitingForPayment,
        ProcessingPayment,
        DispensingItem,
        ReturningChange,
        Error
    }
}
