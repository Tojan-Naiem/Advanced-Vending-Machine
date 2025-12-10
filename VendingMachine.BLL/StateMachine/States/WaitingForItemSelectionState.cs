using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine.States
{
    public class WaitingForItemSelectionState : IVendingState
    {
        public MachineStateType StateType => throw new NotImplementedException();

        public void HandleEvent(VendingMachineContext context, MachineEvent evt)
        {
            throw new NotImplementedException();
        }
    }
}
