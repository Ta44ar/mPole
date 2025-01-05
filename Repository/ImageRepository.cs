using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;
using mPole.Interface.Repositories;

namespace mPole.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ImageRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task AddImageAsync(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Images.AddAsync(image);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Image> GetImageByMoveIdAsync(int moveId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var image = await context.Images
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
}
