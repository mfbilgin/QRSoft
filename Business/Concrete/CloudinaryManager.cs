using System;
using Business.Abstract;
using Business.Constants;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class CloudinaryManager : ICloudinaryService
    {
        public IResult Upload(string imagePath)
        {
            Cloudinary cloudinary = CreateAccount();
            
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath),
                    PublicId = Guid.NewGuid().ToString()
                };
                cloudinary.Upload(uploadParams);
                return new SuccessResult("https://res.cloudinary.com/dbvephcae/image/upload/v1665870027/"+uploadParams.PublicId);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
           
        }

        public IResult Destroy(string publicId)
        {
            try
            {
                Cloudinary cloudinary = CreateAccount();
                DeletionParams deletionParams = new DeletionParams(publicId);
                cloudinary.Destroy(deletionParams);
                return new SuccessResult(Messages.ImageDestroyed);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        private Cloudinary CreateAccount()
        {
            Account account = new Account(
                cloud:CloudinaryConnection.Cloud,
                apiKey:CloudinaryConnection.ApiKey,
                apiSecret:CloudinaryConnection.ApiSecret);

            Cloudinary cloudinary = new Cloudinary(account);
            return cloudinary;
        }
    }
}