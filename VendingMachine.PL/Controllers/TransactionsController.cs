using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.Enums;

namespace VendingMachine.PL.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IVendingMachineService _vmService;

        public TransactionsController(ITransactionService transactionService, IVendingMachineService vmService)
        {
            _transactionService = transactionService;
            _vmService = vmService;

        }
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] TransactionRequest request)
        {
            var result = await _transactionService.CreateTransactionAsync(request);
            if(result is not null)
            {
                await _vmService.TriggerAsync(MachineEvent.Error_Occurred);

            }
            else await _vmService.TriggerAsync(MachineEvent.Item_Selected);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _transactionService.GetTransactionAsync(id);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var result = await _transactionService.DeleteTransactionAsync(id);
            return Ok(result);
        }

    }
}
