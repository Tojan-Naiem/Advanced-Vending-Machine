using Microsoft.AspNetCore.Mvc;
using VendingMachine.BLL.Service.Classes;
using VendingMachine.DAL.Enums;

namespace VendingMachine.PL.Controllers
{
    [ApiController]
    [Route("api/v1/vending")]
    public class VendingController : ControllerBase
    {
        private readonly VendingMachineService _service;

        public VendingController(VendingMachineService service)
        {
            _service = service;
        }

        [HttpPost("trigger")]
        public async Task<IActionResult> Trigger([FromQuery] MachineEvent evt)
        {
            try
            {
                await _service.Trigger(evt); 
                var state = _service.GetCurrentState();
                return Ok(new { state = state.ToString(), message = "Event triggered successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpGet("state")]
        public  IActionResult GetState()
        {
            try
            {
                var state = _service.GetCurrentState();
                return Ok(new { state = state.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet()]
    }
}
