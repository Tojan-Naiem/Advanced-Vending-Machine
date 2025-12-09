using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class WaitingForQRState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.WaitingForQR;

        public void HandleEvent(VendingMachineContext context, MachineEvent evt, object? data = null)
        {
            if (evt == MachineEvent.PaymentReceived)
                context.SetState(new WaitingForPaymentState());
            else if (evt == MachineEvent.Reset)
                context.SetState(new IdleState());
        }
    }

}
