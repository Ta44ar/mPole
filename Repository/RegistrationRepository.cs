using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using System.Threading;

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
                context.Registrations.Add(registration);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Class>> GetClassesByUserIdAsync(string userId, RegistrationStatus status)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                return await context.Registrations
                    .Where(r => r.UserId == userId && r.Status == status)
                    .Select(r => r.Class)
                    .ToListAsync();
            }
        }
    }
}