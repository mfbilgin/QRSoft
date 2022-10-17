using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        [SecuredOperation("Admin")]
        [CacheAspect]
        public IDataResult<List<Company>> GetAll()
        {
            return new SuccessDataResult<List<Company>>(_companyDal.GetAll());
        }
        
        [CacheAspect]
        public IDataResult<Company> GetByEmail(string email)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(company => company.MailAddress == email));
        }
        
        [SecuredOperation("Company")]
        [CacheAspect]
        public IDataResult<CompanyWithCodeDto> GetByCode(string code)
        {
            var result = _companyDal.GetCompanyByCode(code);
            if (result.Count == 0) return new ErrorDataResult<CompanyWithCodeDto>(Messages.CompanyNotExists);
            
            return new SuccessDataResult<CompanyWithCodeDto>(result[0]);
        }
        
        [CacheAspect]
        public IDataResult<Company> GetByCompanyId(int companyId)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(company => company.Id == companyId));
        }
        
        [CacheAspect]
        public IDataResult<Company> GetByPhoneNumber(string phoneNumber)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(company => company.PhoneNumber == phoneNumber));
        }
        
        
        [CacheAspect]
        public IDataResult<Company> GetByCompanyName(string companyName) {
            return new SuccessDataResult<Company>(_companyDal.Get(company => company.CompanyName == companyName));
        }

        [ValidationAspect(typeof(CompanyValidator))]
        [CacheRemoveAspect("ICompanyService.Get")]
        [SecuredOperation("Company")]
        public IResult Add(Company company)
        {
            _companyDal.Add(company);
            return new SuccessResult(Messages.CompanyAdded);
        }
        
        [ValidationAspect(typeof(CompanyValidator))]
        [SecuredOperation("Company")]
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult Update(Company company)
        {
            _companyDal.Update(company);
            return new SuccessResult(Messages.CompanyUpdated);
        }
        [SecuredOperation("Company")]
        public IResult Delete(Company company)
        {
            company.Status = false;
            _companyDal.Update(company);
            return new SuccessResult(Messages.CompanyBlocked);
        }
        
        public IDataResult<List<OperationClaim>> GetClaimById(int companyId)
        {
            return new SuccessDataResult<List<OperationClaim>>(_companyDal.GetClaimById(companyId));
        }
    }
}