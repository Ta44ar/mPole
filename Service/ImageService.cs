using Microsoft.AspNetCore.Components.Forms;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;

namespace mPole.Service
{
    public class ImageService : IImageService
    {
        private readonly IList<string> allowedFormats = new List<string> { "image/jpeg", "image/png", "image/gif" };

        public ImageService()
        {
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
                if (!allowedFormats.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase))
                {
                    continue;
                }

                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.OpenReadStream(maxAllowedSize: 4096 * 4096).CopyToAsync(memoryStream);
                        var buffer = memoryStream.ToArray();

                        var base64String = Convert.ToBase64String(buffer);
                        base64Images.Add($"data:{file.ContentType};base64,{base64String}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {file.Name}: {ex.Message}");
                }
            }

            return base64Images;
        }

        public async Task<IList<Image>> UploadImagesAsync(IList<IBrowserFile> files, Move move)
        {
            var uploadedImages = new List<Image>();

            foreach (var file in files)
            {
                if (!allowedFormats.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"File {file.Name} is not in the allowed formats.");
                    continue;
                }

                try
                {
                    byte[] fileData;

                    using (var memoryStream = new MemoryStream())
                    using (var fileStream = file.OpenReadStream(maxAllowedSize: 4L * 1024 * 1024)) // 4MB limit
                    {
                        await fileStream.CopyToAsync(memoryStream);
                        fileData = memoryStream.ToArray();
                    }

                    var image = new Image
                    {
                        Move = move,
                        MoveId = move.Id,
                        Name = move.Name,
                        ImageData = fileData
                    };

                    uploadedImages.Add(image);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{file.Name}': {ex.Message}");
                }
            }

            return uploadedImages;
        }
    }
}