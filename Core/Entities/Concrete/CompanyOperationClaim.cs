using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class CompanyOperationClaim : IEntity
    {
        public int CompanyOperationClaimId { get; set; }
        public int CompanyId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
