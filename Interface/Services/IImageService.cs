using Microsoft.AspNetCore.Components.Forms;
using mPole.Data.Models;

namespace mPole.Interface.Services
{
    public interface IImageService
    {
        string ImageBase64(byte[] imageData);
        Task<IList<Image>> UploadImagesAsync(List<IBrowserFile> files, string moveName);
    }
}