using mPole.Data.Models;

namespace mPole.Interface.Repositories
{
    public interface IImageRepository
    {
        Task AddImageAsync(Image image);
        Task<Image> GetImageByMoveIdAsync(int moveId);
    }
}