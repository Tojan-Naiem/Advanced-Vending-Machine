using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendingMachine.BLL.Service.Classes;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.Enums;
namespace VendingMachine.PL.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutService _checkOutService;
        private readonly IVendingMachineService _vmService;
        public CheckOutController(ICheckOutService checkOutService, IVendingMachineService vmService)
        {
            _checkOutService = checkOutService;
            _vmService = vmService;
        }
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] CheckOutRequest request)
        {
            await _vmService.TriggerAsync(MachineEvent.Payment_Received);

            var response = await _checkOutService.ProcessPaymentAsync(request, Request);
            if (!response.Success)
            {
                await _vmService.TriggerAsync(MachineEvent.Payment_Failed);
            }
            return Ok(response);
        }
        [HttpGet("success/{session_id}/{transactionId}")]
        [AllowAnonymous]
        public async Task<ActionResult> Success(string session_id, [FromRoute] int transactionId)
        {
            var result = _checkOutService.HandlePaymentSuccessAsync(session_id, transactionId);
            await _vmService.TriggerAsync(MachineEvent.Payment_Received);
            await _vmService.TriggerAsync(MachineEvent.Dispense_Complete);

            return Ok("Success");
        }
        [HttpGet("cancel")]
        [AllowAnonymous]
        public async Task<ActionResult> Cancel()
        {
            await _vmService.TriggerAsync(MachineEvent.Cancel);
            return Ok("Cancel");
        }
    }
}
