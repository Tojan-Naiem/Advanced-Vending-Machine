using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.Service.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class DispensingItemState : IVendingState
    {
        public MachineStateType StateType => MachineStateType.DispensingItem;

        public void HandleEvent(VendingMachineContext context, MachineEvent evt, object? data = null)
        {
            if (evt == MachineEvent.DispenseFinished)
                context.SetState(new ReturningChangeState());
        }
    }

}
