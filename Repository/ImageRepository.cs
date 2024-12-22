using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;
using mPole.Interface.Repositories;

namespace mPole.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddImageAsync(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task<Image> GetImageByMoveIdAsync(int moveId)
        {
            var image = await _context.Images
                                .Where(i => i.Move.Id == moveId)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

            if (image == null)
            {
                throw new ArgumentNullException();
            }

            return image;
        }
    }
}