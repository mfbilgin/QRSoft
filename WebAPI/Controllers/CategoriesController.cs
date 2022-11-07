using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Category category)
        {
            var codeResult = _categoryService.CompanyCodeWillBeExistsWhenRequested(category.CompanyCode);
            if (!codeResult.Success) return BadRequest(codeResult);
            
            var nameResult = _categoryService.CategoryNameNotBeDuplicated(category.CategoryName,category.CompanyCode);
            if (!nameResult.Success) return BadRequest(nameResult);
            
            var result = _categoryService.Add(category);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update([FromBody] Category category,[FromQuery] string companyName)
        {
            var codeResult = _categoryService.CompanyCodeWillBeExistsWhenRequested(category.CompanyCode);
            if (!codeResult.Success) return BadRequest(codeResult);
            
            var nameResult = _categoryService.CategoryNameNotBeDuplicated(category.CategoryName,companyName);
            if (!nameResult.Success) return BadRequest(nameResult);
            
            var result = _categoryService.Update(category);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Category category)
        {
            var result = _categoryService.Delete(category);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getByCompanyCode")]
        public IActionResult GetByCompanyCode([FromQuery] string code)
        {
            var result = _categoryService.GetByCompanyCode(code);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var result = _categoryService.GetById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getByCategoryName")]
        public IActionResult GetByCategoryName([FromQuery] string name,[FromQuery] string companyCode)
        {
            var result = _categoryService.GetByCategoryNameAndCompanyCode(name,companyCode);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}