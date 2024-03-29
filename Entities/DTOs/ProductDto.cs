﻿using Core.Entities.Abstract;
using Entities.Concrete;

namespace Entities.DTOs
{
    //Ürünleri resimleri ile birlikte getirmek için kullanılan DTO(data transfer object)dur.
    public class ProductDto : IDto
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}