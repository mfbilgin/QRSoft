using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class CompanyOperationClaimManager : ICompanyOperationClaimService
    {
        private readonly ICompanyOperationClaimDal _companyOperationClaimDal;

        public CompanyOperationClaimManager(ICompanyOperationClaimDal companyOperationClaimDal)
        {
            _companyOperationClaimDal = companyOperationClaimDal;
        }

        public IResult Add(CompanyOperationClaim companyOperationClaim)
        {
            _companyOperationClaimDal.Add(companyOperationClaim);
            return new SuccessResult(Messages.ClaimAdded);
        }
    }
}