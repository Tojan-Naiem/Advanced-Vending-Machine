using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;

namespace VendingMachine.BLL.StateMachine.Observers
{
    public interface IEventSubscriber { 
        void OnEvent(MachineEvent evt, object? data); 
    }
}

