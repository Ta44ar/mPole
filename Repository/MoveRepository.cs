using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using System.Threading;

namespace mPole.Data.Repositories
{
    public class MoveRepository : IMoveRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MoveRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IQueryable<Move> GetMoves()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                return context.Moves.Include(t => t.Images);
            }
        }

        public async Task AddAsync(Move move, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.AddAsync(move);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var moveToDelete = await context.Moves.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

                if (moveToDelete != null)
                {
                    context.Remove(moveToDelete);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public async Task UpdateAsync(Move move, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (move == null)
                {
                    throw new ArgumentNullException(nameof(move));
                }

                Move entity = await context.Moves.FirstOrDefaultAsync(m => m.Id == move.Id, cancellationToken);

                if (entity == null)
                {
                    throw new Exception($"Move with id {move.Id} not found");
                }

                entity.Name = move.Name;
                entity.Description = move.Description;
                entity.DifficultyLevel = move.DifficultyLevel;
                entity.Images = UpdateMoveImages(entity, move.Images);

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        private ICollection<Image> UpdateMoveImages(Move entity, ICollection<Image> images)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var imagesToSave = new List<Image>();

                foreach (var image in images)
                {
                    var entityImage = entity.Images.FirstOrDefault(i => i.Id == image.Id);

                    if (entityImage == null)
                    {
                        imagesToSave.Add(new Image()
                        {
                            Name = image.Name,
                            ImageData = image.ImageData
                        });
                    }
                    else
                    {
                        imagesToSave.Add(entityImage);
                    }
                }
                return imagesToSave;
            }
        }

        public async Task<ICollection<Move>> GetAllMovesAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var moves = await context.Moves
                                        .Include(m => m.Images)
                                        .AsNoTracking()
                                        .ToListAsync();

                if (moves == null)
                {
                    throw new ArgumentNullException($"No moves found");
                }

                return moves;
            }
        }

        public async Task<Move> GetMoveByIdAsync(int moveId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var move = await context.Moves
                                    .Where(m => m.Id == moveId)
                                    .Include(m => m.Images)
                                    .Include(m => m.Trainings)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();

                if (move == null)
                {
                    throw new ArgumentNullException($"Move with id {moveId} not found");
                }

                return move;
            }
        }
    }
}
