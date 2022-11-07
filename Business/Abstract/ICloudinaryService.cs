using Core.Utilities.Results;

namespace Business.Abstract
{
    //Cloudinary Managerdaki methodaların soyutlarını tutar
    public interface ICloudinaryService
    {
        IResult Upload(string imagePath);
        IResult Destroy(string publicId);
    }
}