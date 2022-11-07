using Core.Entities.Abstract;

namespace Entities.Concrete
{

    //Ürünler
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
    }
}