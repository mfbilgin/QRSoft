using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IProductService
    {
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
        IDataResult<List<ProductDto>> GetByCompanyCodeAndCategoryId(int categoryId,string companyCode);
        IDataResult<ProductDto> GetDtoById(int productId);
        IDataResult<Product> GetProductById(int productId);
        
    }
}