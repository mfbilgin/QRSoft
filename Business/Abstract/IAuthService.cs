using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{

    //Auth Managerdaki methodaların soyutlarını tutar
    public interface IAuthService
    {
        IDataResult<Company> Register(CompanyForRegisterDto companyForRegisterDto);

        IDataResult<Company> Login(CompanyForLoginDto companyForLoginDto);
        IResult MailAddressExists(string mailAddress);
        IResult PhoneNumberExists(string phoneNumber);
        IResult CompanyNameExists(string companyName);
        IDataResult<AuthorizedCompanyDto> CreateAccessToken(Company company);
        IResult ChangePassword(ChangePasswordDto changePasswordDto);
    }
}