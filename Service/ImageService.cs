using mPole.Data.DTOs;
using mPole.Data.Models;
using mPole.Interfaces;

namespace mPole.Services
{
    public class ImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IWebHostEnvironment _environment;

        public ImageService(IImageRepository imageRepository, IWebHostEnvironment environment)
        {
            _imageRepository = imageRepository;
            _environment = environment;
        }

        private string ImageBase64(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return string.Empty;

            return $"data:image/png;base64,{Convert.ToBase64String(imageData)}";
        }
    }
}