using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using mPole.Data.Models;
using mPole.Interface;

namespace mPole.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly List<string> allowedFormats = new List<string> { "image/jpeg", "image/png", "image/gif" };

        public ImageService(IImageRepository imageRepository, IWebHostEnvironment environment)
        {
            _imageRepository = imageRepository;
        }

        public string ImageBase64(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return string.Empty;

            return $"data:image/png;base64,{Convert.ToBase64String(imageData)}";
        }

        public async Task<List<Image>> UploadImagesAsync(List<IBrowserFile> files, string moveName)
        {
            var uploadedImages = new List<Image>();

            foreach (var file in files)
            {
                if (!allowedFormats.Contains(file.ContentType))
                {
                    throw new InvalidOperationException($"Niedozwolony format pliku: {file.Name}");
                }

                using var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                var image = new Image
                {
                    Name = moveName,
                    ImageData = memoryStream.ToArray()
                };

                uploadedImages.Add(image);
            }

            return uploadedImages;
        }
    }
}