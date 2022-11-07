using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService, IAuthService authService)
        {
            _companyService = companyService;
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

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Company company)
        {
            var result = _companyService.Delete(company);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update([FromBody] Company companyToUpdate)
        {
            Company company = _companyService.GetByCompanyId(companyToUpdate.Id).Data;
            if (company == null) return BadRequest(Messages.CompanyNotExists);
            companyToUpdate.PasswordHash = company.PasswordHash;
            companyToUpdate.PasswordSalt = company.PasswordSalt;
            companyToUpdate.Status = company.Status;
            var result = _companyService.Update(companyToUpdate);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}