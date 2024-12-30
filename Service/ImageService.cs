using Microsoft.AspNetCore.Components.Forms;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;

namespace mPole.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IList<string> allowedFormats = new List<string> { "image/jpeg", "image/png", "image/gif" };

        public ImageService(IImageRepository imageRepository, IWebHostEnvironment environment)
        {
            _imageRepository = imageRepository;
        }

        public string ConvertToBase64FromByte(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return string.Empty;

            return $"data:image/png;base64,{Convert.ToBase64String(imageData)}";
        }

        public async Task<IList<string>> ConvertToBase64FromBrowserFiles(IList<IBrowserFile> files)
        {
            IList<string> base64Images = new List<string>();

            foreach (var file in files)
            {
                using (var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024)) // 1 MB
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    var buffer = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(buffer);
                    base64Images.Add($"data:{file.ContentType};base64,{base64String}");
                }
            }

            return base64Images;
        }


        public async Task<IList<Image>> UploadImagesAsync(IList<IBrowserFile> files, Move move)
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
                            Move = move,
                            MoveId = move.Id,
                            Name = move.Name,
                            ImageData = memoryStream.ToArray()
                        };
                    })
            );

            return uploadedImages.ToList();
        }
    }
}