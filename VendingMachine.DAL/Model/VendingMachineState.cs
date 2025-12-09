using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.DAL.Model
{
    public enum MachineStateType
    {
        Idle,
        WaitingForQR,
        WaitingForPayment,
        ProcessingPayment,
        DispensingItem,
        ReturningChange,
        Error
    }
    public class VendingMachineState
    {
        public long Id { get; set; }
        public MachineStateType CurrentState { get; set; } = MachineStateType.Idle;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }



}
