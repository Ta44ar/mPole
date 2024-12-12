using Microsoft.EntityFrameworkCore;
using mPole.Data.Models;
using mPole.Interface.Repositories;

namespace mPole.Data.Repositories
{
    public class MoveRepository : IMoveRepository
    {
        private readonly ApplicationDbContext _context;

        public MoveRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Move> Moves =>
            _context.Moves.Include(t => t.Images);

        public async Task AddAsync(Move move, CancellationToken cancellationToken)
        {
            await _context.AddAsync(move);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var moveToDelete = await _context.Moves.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

            if (moveToDelete != null)
            {
                _context.Remove(moveToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(Move move, CancellationToken cancellationToken)
        {
            if (move == null)
            {
                throw new ArgumentNullException(nameof(move));
            }

            Move entity = Moves.FirstOrDefault(m => m.Id == move.Id);

            if (entity == null)
            {
                throw new Exception($"Move with id {move.Id} not found");
            }

            entity.Name = move.Name;
            entity.Description = move.Description;
            entity.DifficultyLevel = move.DifficultyLevel;
            entity.Images = UpdateMoveImages(entity, move.Images);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private ICollection<Image> UpdateMoveImages(Move entity, ICollection<Image> images)
        {
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

        public async Task<ICollection<Move>> GetAllMovesAsync()
        {
            var moves = await _context.Moves
                                    .Include(m => m.Images)
                                    .AsNoTracking()
                                    .Select(m => new Move
                                    {
                                        Id = m.Id,
                                        Name = m.Name,
                                        DifficultyLevel = m.DifficultyLevel,
                                        Images = m.Images
                                    })
                                    .ToListAsync();

            if (moves == null)
            {
                throw new ArgumentNullException($"No moves found");
            }

            return moves;
        }


        public async Task<Move> GetMoveByIdAsync(int moveId)
        {
            var move = await _context.Moves
                                .Where(m => m.Id == moveId)
                                .Include(m => m.Images)
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
