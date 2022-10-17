using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyCodeService _companyCodeService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IOperationClaimService _operationClaimService;
        private readonly ICompanyOperationClaimService _companyOperationClaimService;
        public AuthManager(ICompanyService companyService, ICompanyCodeService companyCodeService, ITokenHelper tokenHelper, IOperationClaimService operationClaimService, ICompanyOperationClaimService companyOperationClaimService)
        {
            _companyService = companyService;
            _companyCodeService = companyCodeService;
            _tokenHelper = tokenHelper;
            _operationClaimService = operationClaimService;
            _companyOperationClaimService = companyOperationClaimService;
        }

        public IDataResult<Company> Register(CompanyForRegisterDto companyForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(companyForRegisterDto.Password, out passwordHash, out passwordSalt);
            var company = new Company()
            {
                CompanyName = companyForRegisterDto.CompanyName,
                MailAddress = companyForRegisterDto.MailAddress,
                PhoneNumber = companyForRegisterDto.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            }; 
            
            _companyService.Add(company);
            
            var code = _companyCodeService.Add(company.Id).Data.Code;
            
            OperationClaim operationClaim = _operationClaimService.GetByName("Company").Data;
            
            if (operationClaim == null)
                operationClaim = _operationClaimService.Add(new OperationClaim() { OperationClaimName = "Company" }).Data;
            CompanyOperationClaim companyOperationClaim = new CompanyOperationClaim()
            {
                CompanyId = company.Id,
                OperationClaimId = operationClaim.OperationClaimId
            };
            _companyOperationClaimService.Add(companyOperationClaim);
            return new SuccessDataResult<Company>(company,code);
        }

        public IDataResult<Company> Login(CompanyForLoginDto companyForLoginDto)
        {
            Company company = _companyService.GetByEmail(companyForLoginDto.MailAddress).Data;
            if (company == null) return new ErrorDataResult<Company>(Messages.CompanyNotExists);
            
            bool verifiedPassword =  HashingHelper.VerifyPasswordHash(companyForLoginDto.Password,company.PasswordHash,company.PasswordSalt);
            
            if (!verifiedPassword) return new ErrorDataResult<Company>(Messages.PasswordError);
            if (!company.Status) return new ErrorDataResult<Company>(Messages.CompanyWasBlocked);
            string code = _companyCodeService.GetByCompanyId(company.Id).Data.Code;
            return new SuccessDataResult<Company>(company,code);
        }


        public IResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            byte[] passwordHash, passwordSalt;
            Company company = _companyService.GetByCompanyId(changePasswordDto.CompanyId).Data;
            if (company == null) return new ErrorResult(Messages.CompanyNotExists);
                var verifiedPasswordHash = HashingHelper.VerifyPasswordHash(changePasswordDto.OldPassword,company.PasswordHash,company.PasswordSalt);
            if (!verifiedPasswordHash)
            {
                return new ErrorResult(Messages.PasswordError);
            }
            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword,out passwordHash,out passwordSalt);
            company.PasswordHash = passwordHash;
            company.PasswordSalt = passwordSalt;
            _companyService.Update(company);
            return new SuccessResult(Messages.PasswordChanged);
        }
        public IResult PhoneNumberExists(string phoneNumber)
        {
            if (_companyService.GetByPhoneNumber(phoneNumber).Data != null)
            {
                return new ErrorResult(Messages.PhoneNumberAlreadyExists);
            }
            return new SuccessResult();
        }
        public IResult MailAddressExists(string mailAddress)
        {
            if (_companyService.GetByEmail(mailAddress).Data != null)
            {
                return new ErrorResult(Messages.MailAddressAlreadyExists);
            }
            return new SuccessResult();
        }
        public IResult CompanyNameExists(string companyName)
        {
            if (_companyService.GetByCompanyName(companyName).Data != null)
            {
                
                return new ErrorResult(Messages.CompanyNameAlreadyExists);
            }
            return new SuccessResult();
        }
        public IDataResult<AuthorizedCompanyDto> CreateAccessToken(Company company)
        {

            var claims = _companyService.GetClaimById(company.Id).Data;
            
            var accessToken = _tokenHelper.CreateToken(company, claims);
            AuthorizedCompanyDto authorizedCompanyDto = new AuthorizedCompanyDto()
            {
                Id = company.Id,
                AccessToken = accessToken,
                CompanyName = company.CompanyName
            };
            return new SuccessDataResult<AuthorizedCompanyDto>(authorizedCompanyDto, Messages.AccessTokenCreated);
        }
    }
}