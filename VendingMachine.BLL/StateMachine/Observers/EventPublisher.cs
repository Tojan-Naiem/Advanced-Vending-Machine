using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;

namespace VendingMachine.BLL.StateMachine.Observers
{
    public class EventPublisher : IEventPublisher 
    { 
        private event Action<MachineEvent, object?>? _onEvent; 
        public void Publish(MachineEvent evt, object? data = null) { 
            _onEvent?.Invoke(evt, data); 
        } 
        public void Subscribe(Action<MachineEvent, object?> handler) { 
            _onEvent += handler; 
        } 
        public void Unsubscribe(Action<MachineEvent, object?> handler) { 
            _onEvent -= handler; 
        } 
    }
}


