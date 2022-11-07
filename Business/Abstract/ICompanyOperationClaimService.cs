using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    //CompanyOperationClaim Managerdaki methodaların soyutlarını tutar
    public interface ICompanyOperationClaimService
    {
        IResult Add(CompanyOperationClaim companyOperationClaim);
    }
}