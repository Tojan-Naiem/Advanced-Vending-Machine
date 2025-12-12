using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DAL.Enums
{

    public enum MachineEvent
    {

        QR_Scanned,
        Item_Selected,
        Cancel,
        Payment_Initiated,
        Payment_Confirmed,
        Payment_Failed,
        Dispense_Complete,
        Error_Reset,
        Error_Occurred

    }
}

