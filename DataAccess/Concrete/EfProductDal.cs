using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete
{
    public class EfProductDal : EfEntityRepositoryBase<Product,BaseDbContext>,IProductDal
    {
        public List<ProductDto> GetProductDto(Expression<Func<Product, bool>> filter = null)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                var result = from product in filter == null ? context.Products : context.Products.Where(filter)
                    join productImage in context.ProductImages on product.Id equals productImage.ProductId
                    join category in context.Categories on product.CategoryId equals category.Id 
                    select new ProductDto()
                    {
                        Id = product.Id,
                        Description = product.Description,
                        CategoryName = category.CategoryName,
                        CompanyCode = product.CompanyCode,
                        ImagePath = productImage.ImagePath,
                        ProductName = product.ProductName,
                        UnitPrice = product.UnitPrice
                    };
                return result.ToList();
            }
        }
    }
}