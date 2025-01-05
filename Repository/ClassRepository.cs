using mPole.Data.DbContext;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace mPole.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IRegistrationRepository _registrationRepository;

        public ClassRepository(IServiceScopeFactory serviceScopeFactory, IRegistrationRepository registrationRepository)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _registrationRepository = registrationRepository;
        }

        public async Task AddAsync(Class poleDanceClass, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Entry(poleDanceClass.Trainer).State = EntityState.Unchanged;
                context.Entry(poleDanceClass.Training).State = EntityState.Unchanged;

                await context.Classes.AddAsync(poleDanceClass, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var poleDanceClass = await context.Classes.FindAsync(new object[] { id }, cancellationToken);
                if (poleDanceClass != null)
                {
                    context.Classes.Remove(poleDanceClass);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public async Task UpdateAsync(Class poleDanceClass, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await _registrationRepository.RemoveOldClassRegistrationsAsync(poleDanceClass.Id);
                context.Classes.Update(poleDanceClass);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ICollection<Class>> GetAllClassesAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                return await context.Classes
                    .Include(c => c.Training)
                    .Include(c => c.Registrations)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }
        }

        public async Task<Class> GetClassByIdAsync(int classId, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var poleDanceClass = await context.Classes
                                    .Where(t => t.Id == classId)
                                    .Include(t => t.Trainer)
                                    .Include(t => t.Training)
                                    .Include(t => t.Registrations)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(cancellationToken);

                if (poleDanceClass == null)
                {
                    throw new ArgumentNullException($"Class with id {classId} not found");
                }

                return poleDanceClass;
            }
        }

        public async Task<ICollection<Class>> GetClassesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var classes = await context.Classes
                    .Where(c => c.Date >= startDate && c.Date <= endDate)
                    .AsNoTracking()
                    .Include(t => t.Trainer)
                    .Include(c => c.Training)
                    .Include(c => c.Registrations)
                    .ToListAsync(cancellationToken);

                if (classes == null)
                {
                    return new List<Class>();
                }
                else
                {
                    return classes;
                }
            }
        }
    }
}
