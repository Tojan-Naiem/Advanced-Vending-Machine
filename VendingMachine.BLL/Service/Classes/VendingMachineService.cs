using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.StateMachine.Observers;
using VendingMachine.BLL.StateMachine;
using VendingMachine.DAL.Model;
using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Enums;

namespace VendingMachine.BLL.Service.Classes
{
    public class VendingMachineService
    {
        public VendingMachineContext Context { get; }
        private readonly IEventPublisher _publisher;

        public VendingMachineService(ApplicationDbContext dbContext, IEventPublisher publisher)
        {
            _publisher = publisher;
            Context = new VendingMachineContext(dbContext);

            _publisher.Subscribe((evt, data) =>
            {
                Context.TriggerEvent(evt);
                dbContext.SaveChanges(); 
            });
        }
        public void Trigger(MachineEvent evt, object? data = null)
        {
            _publisher.Publish(evt,data);
        }

        public MachineStateType GetCurrentState()
        {
            return Context.GetCurrentStateType(); 
        }
    }
}
