using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class WaitingForItemSelectionState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.Selection;

        public async Task HandleEvent(VendingMachineContext context, MachineEvent evt)
        {

            if (evt == MachineEvent.Item_Selected)
                await context.SetState(new WaitingForPaymentState());
            else if (evt == MachineEvent.Error_Occurred)
                await context.SetState(new ErrorState());
            // if there's no event and an error occurred
            else
                Console.WriteLine($" Event {evt} ignored in WaitingForItemSelection state");

        }
    }
}
