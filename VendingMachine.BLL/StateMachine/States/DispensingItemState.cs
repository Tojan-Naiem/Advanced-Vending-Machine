using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class DispensingItemState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.DispensingItem;

        public void HandleEvent(VendingMachineContext context, MachineEvent evt)
        {
            if (evt == MachineEvent.Dispense_Complete)
                context.SetState(new IdleState());
            else if (evt == MachineEvent.Error_Occurred)
                context.SetState(new ErrorState());
            // if there's no event and an error occurred
            else
                Console.WriteLine($" Event {evt} ignored in ProcessingPaymentState state");
        }
    }

}
