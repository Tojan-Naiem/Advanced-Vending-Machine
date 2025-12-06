using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.BLL.Service.Classes;
using VendingMachine.BLL.Service.Interfaces;
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
    }
}
