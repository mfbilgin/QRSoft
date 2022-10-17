using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : Controller
    {
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;
        public ProductImagesController(IProductImageService productImageService, IProductService productService)
        {
            _productImageService = productImageService;
            _productService = productService;
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file,[FromQuery] int productId)
        {
            var productResult = _productService.GetProductById(productId);
            if (productResult.Data == null) return BadRequest(Messages.ProductNotFound);
            var result = _productImageService.Upload(file,productId);
            if (result.Success) return Ok(result);
            return BadRequest(result);

        }
        [HttpPost("destroy")]
        public IActionResult Destroy([FromQuery] int imageId)
        {
            var result = _productImageService.Destroy(imageId);
            if (result.Success) return Ok(result);
            return BadRequest(result);

        }
        [HttpPost("update")]
        public IActionResult Update(IFormFile file,[FromQuery] int imageId)
        {
            var result = _productImageService.Update(file,imageId);
            if (result.Success) return Ok(result);
            return BadRequest(result);

        }

        [HttpGet("getByProductId")]
        public IActionResult GetByProductId([FromQuery] int productId)
        {
            var result = _productImageService.GetByProductId(productId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var result = _productImageService.GetById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

    }
}