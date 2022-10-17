using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]CompanyForRegisterDto companyForRegisterDto)
        {
            var companyNameExists = _authService.CompanyNameExists(companyForRegisterDto.CompanyName);
            if (!companyNameExists.Success) return BadRequest(companyNameExists);
            
            var mailAddressExists = _authService.MailAddressExists(companyForRegisterDto.CompanyName);
            if (!mailAddressExists.Success) return BadRequest(mailAddressExists);
            var phoneNumberExists = _authService.PhoneNumberExists(companyForRegisterDto.PhoneNumber);
           
            if (!phoneNumberExists.Success) return BadRequest(phoneNumberExists);
            
            var registerResult = _authService.Register(companyForRegisterDto);
            
            var result = _authService.CreateAccessToken(registerResult.Data);
            result.Data.CompanyCode = registerResult.Message;
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody]CompanyForLoginDto companyForLoginDto)
        {
            var loginResult = _authService.Login(companyForLoginDto);
            if (!loginResult.Success) return BadRequest(loginResult.Message);
            var result = _authService.CreateAccessToken(loginResult.Data);
            result.Data.CompanyCode = loginResult.Message;
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("passwordChange")]
        public ActionResult PasswordChange([FromBody]ChangePasswordDto changePasswordDto)
        {
            var result = _authService.ChangePassword(changePasswordDto);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


    }
}