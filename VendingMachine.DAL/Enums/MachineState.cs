using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DAL.Enums
{
    public enum MachineStateType
    {
        Idle, // the first state
        ItemSelection,
        PaymentPending,
        PaymentProcessing,
        ProductDispensing, // final state
        TransactionError,
        Error
    }
}
