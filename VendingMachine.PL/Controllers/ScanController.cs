using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.Enums;

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
            if(request is null)
            {
                await _vmService.TriggerAsync(MachineEvent.Error_Occurred);

            }
            else await _vmService.TriggerAsync(DAL.Enums.MachineEvent.QR_Scanned);
            
            return Ok();
        }
    }
}
