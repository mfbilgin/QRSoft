using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfProductImageDal : EfEntityRepositoryBase<ProductImage,BaseDbContext>,IProductImageDal
    {
        public List<ProductImage> GetProductImage(Expression<Func<ProductImage, bool>> filter = null)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                var result = from image in filter == null ? context.ProductImages : context.ProductImages.Where(filter)
                    join product in context.Products on image.ProductId equals product.Id
                    select new ProductImage()
                    {
                        Id = image.Id,
                        ImagePath = image.ImagePath,
                        ProductId = product.Id,
                        Date = image.Date
                    };
                return result.ToList();
            }
        }
    }
}