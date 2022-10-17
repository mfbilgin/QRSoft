using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    //BUG FIX
    {
        AccessToken CreateToken(Company company, List<OperationClaim> operationClaims);
    }
}
