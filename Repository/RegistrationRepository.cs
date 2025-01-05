using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;

namespace mPole.Data.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RegistrationRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task AddAsync(Registration registration)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Registrations.AddAsync(registration);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Registration registration)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Registrations.Remove(registration);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<ApplicationUser>> GetRegisteredUsersByClassIdAsync(int classId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                return await context.Registrations
                .Where(r => r.ClassId == classId)
                .Select(r => r.User)
                .ToListAsync();
            }
        }

        public async Task RemoveOldClassRegistrationsAsync(int classId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var registrations = context.Registrations.Where(r => r.ClassId == classId).ToList();

                foreach (var registration in registrations)
                {
                    await DeleteAsync(registration);
                }
            }
        }
    }
}