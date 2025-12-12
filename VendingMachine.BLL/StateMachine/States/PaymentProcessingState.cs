using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class PaymentPendingState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.PaymentPending;

        public async Task HandleEvent(VendingMachineContext context, MachineEvent evt)
        {
            if (evt == MachineEvent.Payment_Initiated)
                await context.SetState(new ProcessingPaymentState());
            else if (evt == MachineEvent.Payment_Failed)
                await context.SetState(new IdleState());
            else if (evt == MachineEvent.Error_Occurred)
                await context.SetState(new ErrorState());
            // if there's no event and an error occurred
            else
                Console.WriteLine($" Event {evt} ignored in WaitingForPayment state");
        }
    }

}
