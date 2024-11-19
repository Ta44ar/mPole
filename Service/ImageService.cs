using mPole.Interface;

namespace mPole.Services
{
    public class ImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository, IWebHostEnvironment environment)
        {
            _imageRepository = imageRepository;
        }
    }
}