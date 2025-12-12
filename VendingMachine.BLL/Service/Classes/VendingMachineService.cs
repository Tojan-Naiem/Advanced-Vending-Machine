using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.StateMachine;
using VendingMachine.BLL.StateMachine.Observers;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.Service.Classes
{
    public class VendingMachineService
    {
        private readonly IServiceProvider _serviceProvider; 
        private readonly IEventPublisher _publisher;

        public VendingMachineService(IServiceProvider serviceProvider, IEventPublisher publisher)
        {
            _serviceProvider = serviceProvider;
            _publisher = publisher;

            _publisher.Subscribe(async (evt, data) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var context = new VendingMachineContext(dbContext);

                await context.TriggerEvent(evt); 
            });
        }
        public void Trigger(MachineEvent evt, object? data = null)
        {
            _publisher.Publish(evt,data);
        }

        public  MachineStateType GetCurrentState()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var context = new VendingMachineContext(dbContext);
            return context.GetCurrentStateType();
        }
    }
}
