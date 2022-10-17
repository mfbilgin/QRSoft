using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;

namespace DataAccess.Concrete
{
    public class EfCompanyOperationClaimDal : EfEntityRepositoryBase<CompanyOperationClaim,BaseDbContext>,ICompanyOperationClaimDal
    {
        
    }
}