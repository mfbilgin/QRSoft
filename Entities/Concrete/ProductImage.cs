using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    //Ürün resimleri sınıfı
    public class ProductImage : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}