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
        public IActionResult Trigger([FromQuery] MachineEvent evt)
        {
            _service.Trigger(evt); 
            return Ok(new { state = _service.GetCurrentState() });
        }

        [HttpGet("state")]
        public IActionResult GetState()
        {
            return Ok(new { state = _service.GetCurrentState() });
        }
    }
}
