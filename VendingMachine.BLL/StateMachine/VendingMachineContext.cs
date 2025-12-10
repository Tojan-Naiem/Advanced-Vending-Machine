using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Enums;
using VendingMachine.BLL.StateMachine.States;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.StateMachine
{
    public class VendingMachineContext
    {
        private readonly ApplicationDbContext _dbContext;
        private IVendingState _currentState;

        private readonly long id = 1;


        public VendingMachineContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            var entity = _dbContext.VendingMachineStates.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                entity = new VendingMachineState
                {
                    CurrentState = MachineStateType.Idle,
                    UpdatedAt = DateTime.Now
                };

                _dbContext.VendingMachineStates.Add(entity);
                _dbContext.SaveChanges();
            }

            _currentState = CreateState(entity.CurrentState);
        }

        private IVendingState CreateState(MachineStateType type) =>
            type switch
            {
                MachineStateType.Idle => new IdleState(),
                MachineStateType.Selection => new WaitingForItemSelectionState(),
                MachineStateType.WaitingForPayment => new WaitingForPaymentState(),
                MachineStateType.ProcessingPayment => new ProcessingPaymentState(),
                MachineStateType.DispensingItem => new DispensingItemState(),
                _ => new ErrorState()
            };

        // This is now called from the service
        public MachineStateType GetCurrentStateType() => _currentState.StateType;

        // Alias for your service
        public void TriggerEvent(MachineEvent evt) => Trigger(evt);

        public void Trigger(MachineEvent evt)
        {
            _currentState.HandleEvent(this, evt);
        }

        public void SetState(IVendingState newState)
        {
            _currentState = newState;

            var entity = _dbContext.VendingMachineStates.First(x => x.Id == id);
            entity.CurrentState = newState.StateType;
            entity.UpdatedAt = DateTime.Now;

            _dbContext.SaveChanges();
        }
    }
}
