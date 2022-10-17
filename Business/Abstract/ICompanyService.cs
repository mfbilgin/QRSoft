using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
public interface ICompanyService
{
    IDataResult<List<Company>> GetAll();
    IDataResult<Company> GetByEmail(string email);
    IDataResult<CompanyWithCodeDto> GetByCode(string code);
    IDataResult<Company> GetByCompanyId(int companyId);
    IDataResult<Company> GetByPhoneNumber(string phoneNumber);
    IDataResult<Company> GetByCompanyName(string companyName);
    IResult Add(Company company);
    IResult Update(Company company);
    IResult Delete(Company company);
    IDataResult<List<OperationClaim>> GetClaimById(int companyId);
}

}