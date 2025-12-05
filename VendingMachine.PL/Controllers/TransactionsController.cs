using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;

namespace VendingMachine.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] TransactionRequest request)
        {
            var result = await _transactionService.CreateTransactionAsync(request);
            return result ? Ok("Done") : BadRequest();
        }
    }
}
