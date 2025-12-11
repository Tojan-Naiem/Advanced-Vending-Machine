using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class ErrorState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.Error;

        public void HandleEvent(VendingMachineContext context, MachineEvent evt)
        {
            if (evt == MachineEvent.Error_Reset)
                context.SetState(new IdleState());
            else
                Console.WriteLine($" Event {evt} ignored in ProcessingPaymentState state");
        }
    }

}
