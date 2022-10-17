using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        List<CompanyWithCodeDto> GetCompanyByCode(string code);
        List<OperationClaim> GetClaimById(int companyId);
    }
}