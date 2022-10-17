using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IProductImageDal : IEntityRepository<ProductImage>
    {
        List<ProductImage> GetProductImage(Expression<Func<ProductImage,bool>> filter=null );
    }
}