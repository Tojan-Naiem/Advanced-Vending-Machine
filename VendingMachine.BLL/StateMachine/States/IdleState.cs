using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class IdleState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.Idle;

        public async Task HandleEvent(VendingMachineContext context, MachineEvent evt)
        {
           
            if (evt == MachineEvent.QR_Scanned)
                await context.SetState(new ItemSelectionState());
            else if (evt == MachineEvent.Error_Occurred)
                await context.SetState(new ErrorState());
            // if there's no event and an error occurred
            else
                Console.WriteLine($" Event {evt} ignored in Idle state");

        }

       
    }

}

