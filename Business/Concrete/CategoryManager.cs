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
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly ICompanyCodeDal _companyCodeDal;
        public CategoryManager(ICategoryDal categoryDal, ICompanyCodeDal companyCodeDal)
        {
            _categoryDal = categoryDal;
            _companyCodeDal = companyCodeDal;
        }
        [SecuredOperation("Company")]
        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        [SecuredOperation("Company")]
        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }

        [SecuredOperation("Company")]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Category>> GetByCompanyCode(string code)
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(c => c.CompanyCode == code));
        }

        [SecuredOperation("Company")]
        [CacheAspect]
        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.Id == id));
        }
        [CacheAspect]
        public IDataResult<Category> GetByCategoryName(string categoryName)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryName == categoryName));
        }
        
        public IResult CategoryNameNotBeDuplicated(string name)
        {
            Category category = _categoryDal.Get(c => c.CategoryName == name);
            if (category != null) return new ErrorResult(Messages.CategoryAlreadyExists);
            return new SuccessResult();
        }

        public IResult CompanyCodeWillBeExistsWhenRequested(string code)
        {
            CompanyCode companyCode = _companyCodeDal.Get(c => c.Code == code);
            if (companyCode == null) return new ErrorResult(Messages.CodeNotExists);
            return new SuccessResult();
        }
    }
}