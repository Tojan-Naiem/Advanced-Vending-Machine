using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VendingMachine.BLL.Service.Classes;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.DTO.ResponseDTO;

namespace VendingMachine.PL.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(
         IProductService productService
         )
        {
            _productService = productService;


        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductResponse>>> GetAll()
        {
            var productsDto = await _productService.GetAllAsync();
            return Ok(new { productsDto });

        }
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> Get([FromRoute] long id)
        {
            ProductResponse? productResponse = await _productService.GetByIdAsync(id);
            if (productResponse is null) return NotFound();
            return Ok(productResponse);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductRequest productRequest)
        {
            await _productService.CreateFile(productRequest);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] ProductRequest productRequest)
        {
            bool isExist = await _productService.UpdateProductAsync(id, productRequest);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Updated" });
        }
        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus([FromRoute] long id)
        {
            bool isExist = await _productService.ToggleStatusAsync(id);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Updated" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            bool isExist = await _productService.DeleteFile(id);
            if (isExist is false) return NotFound(new { message = "Not found" });
            return Ok(new { message = "Deleted" });
        }
    
}
}
