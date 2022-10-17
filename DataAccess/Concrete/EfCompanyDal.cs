using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete
{
    public class EfCompanyDal : EfEntityRepositoryBase<Company,BaseDbContext>,ICompanyDal
    {
        public List<CompanyWithCodeDto> GetCompanyByCode(string code)
        {
            using (var context = new BaseDbContext())
            {
                var result = from company in context.Companies
                    join companyCode in context.CompanyCodes on company.Id equals companyCode.CompanyId
                    where companyCode.Code == code
                    select new CompanyWithCodeDto()
                    {
                        Id = company.Id,
                        CompanyCode = companyCode.Code,
                        CompanyName = company.CompanyName, 
                        MailAddress = company.MailAddress,
                        PhoneNumber = company.PhoneNumber
                    };
                return result.ToList();
            }
        }
        public List<OperationClaim> GetClaimById(int companyId)
        {
            using (var context = new BaseDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join companyOperationClaim in context.CompanyOperationClaims
                        on operationClaim.OperationClaimId equals companyOperationClaim.OperationClaimId
                    where companyOperationClaim.CompanyId == companyId
                    select new OperationClaim { OperationClaimId = operationClaim.OperationClaimId, OperationClaimName = operationClaim.OperationClaimName };
                return result.ToList();
            }
            

            
        }
    }
    
}