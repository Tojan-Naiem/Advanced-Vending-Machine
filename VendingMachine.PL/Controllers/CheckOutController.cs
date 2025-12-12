using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendingMachine.BLL.Service.Classes;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;
namespace VendingMachine.PL.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutService _checkOutService;
        private readonly VendingMachineService _service
        public CheckOutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] CheckOutRequest request)
        {
            var response = await _checkOutService.ProcessPaymentAsync(request, Request);
            return Ok(response);
        }
        [HttpGet("success/{session_id}/{transactionId}")]
        [AllowAnonymous]
        public async Task<ActionResult> Success(string session_id, [FromRoute] int transactionId)
        {
            var result = _checkOutService.HandlePaymentSuccessAsync(session_id, transactionId);

            return Ok("Success");
        }
        [HttpGet("cancel")]
        [AllowAnonymous]
        public ActionResult Cancel()
        {
            return Ok("Cancel");
        }
    }
}
