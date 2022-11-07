using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
        
    //Category Managerdaki methodaların soyutlarını tutar
    public interface ICategoryService
    {
        IResult Add(Category category);
        IResult Update(Category category);
        IResult Delete(Category category);
        IDataResult<List<Category>> GetByCompanyCode(string code);
        IDataResult<Category> GetById(int id);
        IDataResult<Category> GetByCategoryNameAndCompanyCode(string categoryName,string companyCode);
        IResult CategoryNameNotBeDuplicated(string name,string companyCode);
        IResult CompanyCodeWillBeExistsWhenRequested(string code);


    }
}