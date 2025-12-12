using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;

namespace VendingMachine.PL.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ScanController : ControllerBase
    {
        private readonly IVendingMachineService _vmService;
        public ScanController( IVendingMachineService vmService)
        {
            _vmService = vmService;

        }
        [HttpPost("scan-qr")]
        public async Task<IActionResult> ScanQr([FromBody] ScanRequest request)
        {
            await _vmService.TriggerAsync(DAL.Enums.MachineEvent.QR_Scanned);
            return Ok();
        }
    }
}
