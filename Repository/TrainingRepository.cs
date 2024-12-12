using Microsoft.EntityFrameworkCore;
using mPole.Interface.Repositories;

namespace mPole.Data.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Training> Trainings =>
            _context.Trainings.Include(t => t.Moves);

        public async Task AddAsync(Training training, CancellationToken cancellationToken)
        {
            await _context.AddAsync(training);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var trainingToDelete = await _context.Trainings.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (trainingToDelete != null)
            {
                _context.Remove(trainingToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(Training training, CancellationToken cancellationToken)
        {
            if (training == null)
            {
                throw new ArgumentNullException(nameof(training));
            }

            Training entity = Trainings.FirstOrDefault(t => t.Id == training.Id);

            if (entity == null)
            {
                throw new Exception($"Training with id {training.Id} not found");
            }

            entity.Name = training.Name;
            entity.Description = training.Description;
            entity.Type = training.Type;
            entity.ImageUrl = training.ImageUrl;
            entity.Moves = training.Moves;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<Training>> GetAllTrainingsAsync()
        {
            var trainings = await _context.Trainings
                                    .Include(t => t.Moves)
                                    .AsNoTracking()
                                    .ToListAsync();

            if (trainings == null)
            {
                throw new ArgumentNullException($"No trainings found");
            }

            return trainings;
        }

        public async Task<Training> GetTrainingByIdAsync(int trainingId)
        {
            var training = await _context.Trainings
                                .Where(t => t.Id == trainingId)
                                .Include(t => t.Moves)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

            if (training == null)
            {
                throw new ArgumentNullException($"Training with id {trainingId} not found");
            }

            return training;
        }
    }
}