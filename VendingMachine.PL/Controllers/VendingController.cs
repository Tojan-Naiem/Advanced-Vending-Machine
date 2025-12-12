using Microsoft.AspNetCore.Mvc;
using VendingMachine.BLL.Service.Classes;
using VendingMachine.DAL.Enums;

namespace VendingMachine.PL.Controllers
{
    [ApiController]
    [Route("api/vending")]
    public class VendingController : ControllerBase
    {
        private readonly VendingMachineService _service;

        public VendingController(VendingMachineService service)
        {
            _service = service;
        }

        [HttpPost("trigger")]
        public async IActionResult Trigger([FromQuery] MachineEvent evt)
        {
           await _service.Trigger(evt); 
            return Ok(new { state = _service.GetCurrentState() });
        }

        [HttpGet("state")]
        public async IActionResult GetState()
        {
            return Ok(new { state = await _service.GetCurrentState() });
        }
    }
}
