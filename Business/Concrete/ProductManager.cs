using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IProductImageService _productImageService;

        public ProductManager(IProductDal productDal, IProductImageService productImageService)
        {
            _productDal = productDal;
            _productImageService = productImageService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("Company")]
        public IResult Add(Product product)
        {
            var result = BusinessRules.Run(CheckIfProductExists(product.ProductName));
            if (result != null) return new ErrorResult(result.Message);
            _productDal.Add(product);
            ProductImage productImage = new ProductImage
            {
                ImagePath = "https://res.cloudinary.com/dbvephcae/image/upload/v1665952284/default.png",
                ProductId = product.Id,
                Date = DateTime.Now
            };
            _productImageService.Add(productImage);
            return new SuccessResult(Messages.ProductAdded);
         }

        [ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("Company")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        [CacheRemoveAspect("IProductService.Get")]
        [SecuredOperation("Company")]
        public IResult Delete(Product product)
        {
            ProductImage productImage = _productImageService.GetByProductId(product.Id).Data;
            var destroyResult = _productImageService.Destroy(productImage.Id);
            if (!destroyResult.Success) return destroyResult;
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [CacheAspect]
        public IDataResult<List<ProductDto>> GetByCompanyCodeAndCategoryId(int categoryId, string companyCode)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetProductDto(p =>
                p.CategoryId == categoryId && p.CompanyCode == companyCode));
        }

        [CacheAspect]
        public IDataResult<ProductDto> GetDtoById(int productId)
        {
            return new SuccessDataResult<ProductDto>(_productDal.GetProductDto(p => p.Id == productId).First());
        }

        public IDataResult<Product> GetProductById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == productId));
        }

        private IResult CheckIfProductExists(string productName)
        {
            var result = _productDal.Get(p => p.ProductName == productName);
            if (result != null) return new ErrorResult(Messages.ProductExists);
            return new SuccessResult();
        }
    }
}