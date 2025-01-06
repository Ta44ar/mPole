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

        public Task<bool> IsUserRegisteredForClassAsync(string userId, int classId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                bool isUserRegistered = context.Registrations.Any(r => r.UserId == userId && r.ClassId == classId);

                return Task.FromResult(isUserRegistered);
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

        public async Task<Registration?> GetRegistrationAsync(string userId, int classId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                return await context.Registrations
                    .FirstOrDefaultAsync(r => r.UserId == userId && r.ClassId == classId);
            }
        }

        public async Task<ICollection<Registration>> GetRegistrationsByClassIdAsync(int classId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                return await context.Registrations
                    .Where(r => r.ClassId == classId)
                    .ToListAsync();
            }
        }
    }
}