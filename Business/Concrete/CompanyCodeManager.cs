using System;
using Autofac;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CompanyCodeManager : ICompanyCodeService
    {
        private readonly ICompanyCodeDal _companyCodeDal;

        public CompanyCodeManager(ICompanyCodeDal companyCodeDal)
        {
            _companyCodeDal = companyCodeDal;
        }

        [ValidationAspect(typeof(CompanyCodeValidator))]
        [CacheRemoveAspect("ICompanyCodeService.Get")]
        public IDataResult<CompanyCode> Add(int companyId)
        {
            CompanyCode companyCode = new CompanyCode()
            {
                Code = Guid.NewGuid().ToString().ToUpper(),
                CompanyId = companyId
            };
            _companyCodeDal.Add(companyCode);
            return new SuccessDataResult<CompanyCode>(companyCode,Messages.CodeAdded);
        }
        
        [ValidationAspect(typeof(CompanyCodeValidator))]
        [CacheRemoveAspect("ICompanyCodeService.Get")]
        public IResult Update(CompanyCode companyCode)
        {
            _companyCodeDal.Update(companyCode);
            return new SuccessResult(Messages.CodeUpdated);
        }

        [CacheRemoveAspect("ICompanyCodeService.Get")]
        public IResult Delete(CompanyCode companyCode)
        {
            _companyCodeDal.Delete(companyCode);
            return new SuccessResult(Messages.CodeDeleted);
        }

        [CacheAspect]
        public IDataResult<CompanyCode> GetById(int companyCodeId)
        {
            return new SuccessDataResult<CompanyCode>(_companyCodeDal.Get(code => code.Id == companyCodeId));
        }

        [CacheAspect]
        public IDataResult<CompanyCode> GetByCompanyId(int companyId)
        {
            return new SuccessDataResult<CompanyCode>(_companyCodeDal.Get(code => code.CompanyId == companyId));
        }
        [CacheAspect]
        public IDataResult<CompanyCode> GetByCode(string code)
        {
            return new SuccessDataResult<CompanyCode>(_companyCodeDal.Get(companyCode => companyCode.Code == code));
        }
    }
}