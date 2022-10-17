using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<OperationClaim> Add(OperationClaim operationClaim);
        IDataResult<OperationClaim> GetByName(string name);
    }
}