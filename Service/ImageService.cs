using Microsoft.AspNetCore.Components.Forms;
using mPole.Data.Models;
using mPole.Interface;

namespace mPole.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IList<string> allowedFormats = new List<string> { "image/jpeg", "image/png", "image/gif" };

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

        public async Task<IList<Image>> UploadImagesAsync(List<IBrowserFile> files, string moveName)
        {
            var uploadedImages = await Task.WhenAll(
                files
                    .Where(file => allowedFormats.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase))
                    .Select(async file =>
                    {
                        using var memoryStream = new MemoryStream();
                        await file.OpenReadStream().CopyToAsync(memoryStream);

                        return new Image
                        {
                            Name = moveName,
                            ImageData = memoryStream.ToArray()
                        };
                    })
            );

            return uploadedImages.ToList();
        }
    }
}