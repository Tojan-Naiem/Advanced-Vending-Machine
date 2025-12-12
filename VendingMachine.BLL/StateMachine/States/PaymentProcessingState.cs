using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class PaymentProcessingState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.PaymentProcessing;

        public async Task HandleEvent(VendingMachineContext context, MachineEvent evt)
        {
            if (evt == MachineEvent.Payment_Confirmed)
                await context.SetState(new ProductDispensingState());
            else if (evt == MachineEvent.Payment_Failed)
                await context.SetState(new ItemSelectionState());
            else if (evt == MachineEvent.Error_Occurred)
                await context.SetState(new ErrorState());
            // if there's no event and an error occurred
            else
                Console.WriteLine($" Event {evt} ignored in ProcessingPaymentState state");
        }
    }
}
