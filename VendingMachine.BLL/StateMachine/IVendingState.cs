using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine
{
    public interface IVendingState
    {
        MachineStateType StateType { get; }
        public Task HandleEvent(VendingMachineContext context, MachineEvent evt);
    }

}
