using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;
using mPole.Interface.Repositories;

namespace mPole.Data.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TrainingRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task AddAsync(Training training, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.AddAsync(training);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var trainingToDelete = await context.Trainings.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

                if (trainingToDelete != null)
                {
                    context.Remove(trainingToDelete);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public async Task UpdateAsync(Training training, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (training == null)
                {
                    throw new ArgumentNullException(nameof(training));
                }

                Training entity = await context.Trainings.FirstOrDefaultAsync(t => t.Id == training.Id, cancellationToken);

                if (entity == null)
                {
                    throw new Exception($"Training with id {training.Id} not found");
                }

                entity.Name = training.Name;
                entity.Description = training.Description;
                entity.Type = training.Type;
                entity.Level = training.Level;
                entity.ImageUrl = training.ImageUrl;
                entity.Moves = training.Moves;

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ICollection<Training>> GetAllTrainingsAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var trainings = await context.Trainings
                                        .Include(t => t.Moves)
                                        .Include(t => t.Classes)
                                        .AsNoTracking()
                                        .ToListAsync();

                if (trainings == null)
                {
                    throw new ArgumentNullException($"No trainings found");
                }

                return trainings;
            }
        }

        public async Task<Training> GetTrainingByIdAsync(int trainingId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var training = await context.Trainings
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
}
