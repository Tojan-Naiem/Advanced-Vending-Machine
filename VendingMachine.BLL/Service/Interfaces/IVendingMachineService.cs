using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Enums;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.Service.Interfaces
{
    public interface IVendingMachineService
    {
        public void HandleEventAsync(MachineEvent evt, object? data);
        public Task TriggerAsync(MachineEvent evt, object? data = null);
        public MachineStateType GetCurrentState();
        public  Task<List<MachineStateLog>> GetMachineLogHistoryAsync();
    }
}
