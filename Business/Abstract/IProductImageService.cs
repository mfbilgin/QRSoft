using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IProductImageService
    {
        IResult Upload(IFormFile file,int productId);
        IResult Add(ProductImage productImage);
        IResult Delete(ProductImage productImage);
        IResult Update(IFormFile file,int imageId);
        IResult Destroy(int imageId);
        
        IDataResult<ProductImage> GetByProductId(int productId);
        IDataResult<ProductImage> GetById(int id);
    }
}