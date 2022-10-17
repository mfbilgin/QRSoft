using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface ICompanyOperationClaimService
    {
        IResult Add(CompanyOperationClaim companyOperationClaim);
    }
}