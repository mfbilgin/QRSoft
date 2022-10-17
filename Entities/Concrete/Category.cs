using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CompanyCode { get; set; }
    }
}