using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;

namespace VendingMachine.BLL.StateMachine.Observers
{
    public interface IEventPublisher { 
        void Publish(MachineEvent evt, object? data = null); 
        void Subscribe(Action<MachineEvent, object?> handler); 
        void Unsubscribe(Action<MachineEvent, object?> handler); 
    }
}
