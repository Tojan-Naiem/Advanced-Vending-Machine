using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class IdleState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.Idle;

        public void HandleEvent(VendingMachineContext context, MachineEvent evt)
        {
           
            if (evt == MachineEvent.QR_Scanned)
                context.SetState(new WaitingForItemSelectionState());
            else if (evt == MachineEvent.Error_Occurred)
                context.SetState(new ErrorState());
            // if there's no event and an error occurred
            else
                Console.WriteLine($" Event {evt} ignored in Idle state");

        }
    }

}

