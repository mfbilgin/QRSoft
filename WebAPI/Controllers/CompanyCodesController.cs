using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyCodesController : Controller
    {
        private readonly ICompanyCodeService _companyCodeService;
        public CompanyCodesController(ICompanyCodeService companyCodeService)
        {
            _companyCodeService = companyCodeService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromQuery] int companyId)
        {
            var result = _companyCodeService.Add(companyId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update([FromBody] CompanyCode companyCode)
        {
            var result = _companyCodeService.Update(companyCode);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] CompanyCode companyCode)
        {
            var result = _companyCodeService.Delete(companyCode);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getById")]
        public IActionResult GetById([FromQuery]int id)
        {
            var result = _companyCodeService.GetById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getByCompanyId")]
        public IActionResult GetByCompanyId([FromQuery]int companyId)
        {
            var result = _companyCodeService.GetByCompanyId(companyId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getByCode")]
        public IActionResult GetByCode([FromQuery]string code)
        {
            var result = _companyCodeService.GetByCode(code);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}