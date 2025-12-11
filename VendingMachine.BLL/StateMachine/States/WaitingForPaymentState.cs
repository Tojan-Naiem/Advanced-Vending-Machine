using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class WaitingForPaymentState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.WaitingForPayment;

        public async void HandleEvent(VendingMachineContext context, MachineEvent evt)
        {
            if (evt == MachineEvent.Payment_Received)
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
