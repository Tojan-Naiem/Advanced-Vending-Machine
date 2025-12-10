using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.BLL.StateMachine
{

    public enum MachineEvent
    {

        QR_Scanned,
        Item_Selected,
        Cancel,
        Payment_Received,
        Payment_Failed,
        Dispense_Complete,
        ERROR_OCCURRED

    }
}

