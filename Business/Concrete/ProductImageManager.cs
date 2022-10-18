using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        private readonly IProductImageDal _productImageDal;
        private readonly IProductDal _productDal;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductImageManager(ICloudinaryService cloudinaryService, IProductImageDal productImageDal, IProductDal productDal)
        {
            _cloudinaryService = cloudinaryService;
            _productImageDal = productImageDal;
            _productDal = productDal;
        }

        [SecuredOperation("Company")]
        public IResult Upload(IFormFile file, int productId)
        {
            string filePath = FileHelper.Add(file).Message;
            var uploadResult = _cloudinaryService.Upload(filePath);
            string uploadedImagePath = uploadResult.Message;
            if (!uploadResult.Success) return uploadResult;

            var deleteResult = FileHelper.Delete(filePath);
            if (!deleteResult.Success) return deleteResult;

            ProductImage productImage = new ProductImage
                { ProductId = Int32.Parse(productId.ToString()), ImagePath = uploadedImagePath, Date = DateTime.Now };
            _productImageDal.Add(productImage);
            return new SuccessResult(uploadedImagePath);
        }

        public IResult Add(ProductImage productImage)
        {
            _productImageDal.Add(productImage);
            return new SuccessResult();
        }

        public IResult Delete(ProductImage productImage)
        {
            _productImageDal.Delete(productImage);
            return new SuccessResult();
        }

        [SecuredOperation("Company")]
        public IResult Update(IFormFile file, int imageId)
        {
            ProductImage productImage = _productImageDal.Get(pi => pi.Id == imageId);
            if (productImage == null) return new ErrorResult(Messages.ImageNotFound);
            var destroyResult = Destroy(productImage.Id);
            if (!destroyResult.Success) return destroyResult;
            var uploadResult = Upload(file, productImage.ProductId);
            if (!uploadResult.Success) return uploadResult;
            return new SuccessResult(Messages.ImageUpdated);
        }

        [SecuredOperation("Company")]
        public IResult Destroy(int imageId)
        {
            ProductImage productImage = _productImageDal.Get(pi => pi.Id == imageId);
            if (productImage == null) return new ErrorResult(Messages.ProductNotHaveImage);
            string publicId = productImage.ImagePath.Split('/')[7].Split('.')[0];
            IResult result = new SuccessResult();
            if (publicId != "default")
            {
                result = _cloudinaryService.Destroy(publicId);
                if (!result.Success) return result;
            }
            _productImageDal.Delete(productImage);
            return result;
        }

        public IDataResult<ProductImage> GetByProductId(int productId)
        {
            
            var result = _productImageDal.Get(pi => pi.ProductId == productId);
            if (result == null) return new SuccessDataResult<ProductImage>(Messages.ProductNotHaveImage);
            return new SuccessDataResult<ProductImage>(result);
        }

        [SecuredOperation("Company")]
        public IDataResult<ProductImage> GetById(int id)
        {
            return new SuccessDataResult<ProductImage>(_productImageDal.Get(pi => pi.Id == id));
        }

        private IResult CheckIfProductHaveImage(int productId)
        {
            var result = _productImageDal.Get(pi => pi.ProductId == productId);
            if (result != null) return new ErrorResult("Product have image");
            return new SuccessResult();
        }
    }
}