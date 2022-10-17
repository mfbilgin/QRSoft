using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface ICloudinaryService
    {
        IResult Upload(string imagePath);
        IResult Destroy(string publicId);
    }
}