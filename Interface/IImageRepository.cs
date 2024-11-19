using mPole.Data.Models;

namespace mPole.Interface
{
    public interface IImageRepository
    {
        Task<Image> GetImageByMoveIdAsync(int moveId);
    }
}