using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICompanyCodeService
    {
        IDataResult<CompanyCode> Add(int companyId);
        IResult Update(CompanyCode companyCode);
        IResult Delete(CompanyCode companyCode);
        IDataResult<CompanyCode> GetById(int companyCodeId);
        IDataResult<CompanyCode> GetByCompanyId(int companyId);
        IDataResult<CompanyCode> GetByCode(string code);
        
    }
}