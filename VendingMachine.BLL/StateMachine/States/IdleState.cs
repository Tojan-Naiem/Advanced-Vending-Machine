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

        public void HandleEvent(VendingMachineContext context, MachineEvent evt, object? data = null)
        {
            if (evt == MachineEvent.InsertQR)
                context.SetState(new WaitingForQRState());
        }
    }

}

