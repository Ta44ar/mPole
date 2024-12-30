using Microsoft.AspNetCore.Components.Forms;
using mPole.Data.Models;

namespace mPole.Interface.Services
{
    public interface IImageService
    {
        string ConvertToBase64FromByte(byte[] imageData);
        Task<IList<string>> ConvertToBase64FromBrowserFiles(IList<IBrowserFile> files);
        Task<IList<Image>> UploadImagesAsync(IList<IBrowserFile> files, Move move);
    }
}