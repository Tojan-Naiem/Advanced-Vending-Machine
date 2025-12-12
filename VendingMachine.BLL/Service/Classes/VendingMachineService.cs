using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.BLL.StateMachine;
using VendingMachine.BLL.StateMachine.Observers;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.Service.Classes
{
    public class VendingMachineService: IVendingMachineService
    {
        private readonly IServiceProvider _serviceProvider; 
        private readonly IEventPublisher _publisher;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1); 

        public VendingMachineService(IServiceProvider serviceProvider, IEventPublisher publisher)
        {
            _serviceProvider = serviceProvider;
            _publisher = publisher;

            _publisher.Subscribe(HandleEventAsync);


        }
        public async void HandleEventAsync(MachineEvent evt, object? data)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var context = new VendingMachineContext(dbContext);

                await context.TriggerEvent(evt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error handling event {evt}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                _semaphore.Release();
            }
        }
        public async Task TriggerAsync(MachineEvent evt, object? data = null)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var context = new VendingMachineContext(dbContext);

                await context.TriggerEvent(evt);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public  MachineStateType GetCurrentState()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var context = new VendingMachineContext(dbContext);
            return context.GetCurrentStateType();
        }
        public async Task<List<MachineStateLog>> GetMachineLogHistoryAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var logs = await dbContext.MachineStateLogs
                                      .OrderByDescending(l => l.Timestamp)
                                      .ToListAsync();

            return logs;
        }

    }
}
