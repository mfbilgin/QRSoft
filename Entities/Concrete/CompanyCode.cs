using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class CompanyCode : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
    }
}