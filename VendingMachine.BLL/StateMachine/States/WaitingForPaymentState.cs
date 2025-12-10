using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.Service.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class WaitingForPaymentState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.WaitingForPayment;

        public void HandleEvent(VendingMachineContext context, MachineEvent evt, object? data = null)
        {
            if (evt == MachineEvent.PaymentReceived)
                context.SetState(new ProcessingPaymentState());
            else if (evt == MachineEvent.PaymentFailed)
                context.SetState(new ReturningChangeState());
        }
    }

}
