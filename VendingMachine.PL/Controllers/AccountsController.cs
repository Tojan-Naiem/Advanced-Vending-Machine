using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.DAL.DTO.ResponseDTO;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.BLL.Service.Interfaces;

namespace VendingMachine.PL.Controllers
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("auth")]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenticationService _authintecationService;

        public AccountsController(IAuthenticationService authintecationService)
        {
            _authintecationService = authintecationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
        {
            var result = await _authintecationService.LoginAsync(request);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
        {
            var result = await _authintecationService.ConfirmEmail(token, userId);
            return Ok(result);
        }
    }
}
