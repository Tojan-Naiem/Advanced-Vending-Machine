using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class ProcessingPaymentState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.ProcessingPayment;

        public void HandleEvent(VendingMachineContext context, MachineEvent evt, object? data = null)
        {
            if (evt == MachineEvent.Dispense)
                context.SetState(new DispensingItemState());
        }
    }
}
