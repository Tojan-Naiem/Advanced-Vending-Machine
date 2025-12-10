using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine
{
    public interface IVendingState
    {
        MachineStateType StateType { get; }
        void HandleEvent(VendingMachineContext context, MachineEvent evt);
    }

}
