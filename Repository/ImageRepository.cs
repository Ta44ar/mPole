using Microsoft.EntityFrameworkCore;
using mPole.Data.Models;
using mPole.Interface;

namespace mPole.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        private DefaultImage? defaultImage;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Image> GetImageByMoveIdAsync(int moveId)
        {
            var image = await _context.Images
                                .Where(i => i.Move.Id == moveId)
                                .FirstOrDefaultAsync();

            if (image == null)
            {
                throw new Exception($"Image for move with id {moveId} not found");
            }

            return image;
        }
    }
}