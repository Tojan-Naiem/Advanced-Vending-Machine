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
        [HttpGet("success/{transactionId}")]
        [AllowAnonymous]
        public async Task<ActionResult> Success([FromRoute] int transactionId,[FromQuery] string session_id)
        {
            var result = _checkOutService.HandlePaymentSuccessAsync(session_id, transactionId);



            return Ok("Success");
        }
    }
}
