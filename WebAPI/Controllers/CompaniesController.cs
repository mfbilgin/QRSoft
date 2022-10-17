using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IAuthService _authService;

        public CompaniesController(ICompanyService companyService, IAuthService authService)
        {
            _companyService = companyService;
            _authService = authService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _companyService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getByCode")]
        public IActionResult GetByCode([FromQuery] string code)
        {
            var result = _companyService.GetByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}