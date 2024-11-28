using Microsoft.AspNetCore.Components.Forms;
using mPole.Data.Models;

namespace mPole.Interface
{
    public interface IImageService
    {
        string ImageBase64(byte[] imageData);
        Task<List<Image>> UploadImagesAsync(List<IBrowserFile> files, string moveName);
    }
}