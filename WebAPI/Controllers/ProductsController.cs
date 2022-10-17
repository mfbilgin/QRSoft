using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Product product)
        {
            var result = _productService.Add(product);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update([FromBody] Product product)
        {
            var result = _productService.Update(product);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Product product)
        {
            var result = _productService.Delete(product);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getProducts")]
        public IActionResult GetProducts([FromQuery] int categoryId,[FromQuery] string companyCode)
        {
            var result = _productService.GetByCompanyCodeAndCategoryId(categoryId, companyCode);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var result = _productService.GetDtoById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}