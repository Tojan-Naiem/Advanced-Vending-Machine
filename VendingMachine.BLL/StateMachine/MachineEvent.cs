using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.BLL.StateMachine
{

    public enum MachineEvent
    {
        InsertQR,
        PaymentReceived,
        PaymentFailed,
        Dispense,
        DispenseFinished,
        ReturnChange,
        Reset
    }
}

