using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;

public class UserRepository : IUserRepository
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserRepository(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                throw new ArgumentNullException("No user with given username found.");
            }

            return user;
        }
    }

    public async Task<ICollection<ApplicationUser>> GetAllUsersAsync()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var users = await userManager.Users
                .AsNoTracking()
                .Select(u => new ApplicationUser
                {
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToListAsync();

            if (!users.Any())
            {
                return new List<ApplicationUser>();
            }

            return users;
        }
    }

    public async Task<ICollection<string?>> GetExistingRolesAsync()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var allRoles = await context.Roles
                .AsNoTracking()
                .Select(r => r.Name)
                .ToListAsync();

            if (!allRoles.Any())
            {
                throw new InvalidOperationException("No roles found.");
            }

            return allRoles;
        }
    }

    public async Task<ICollection<Class>> GetClassesByUserAsync(string userId)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Classes
                .Include(c => c.Training)
                .Include(c => c.Trainer)
                .Where(c => c.RegisteredUsers.Any(u => u.Id == userId))
                .AsNoTracking()
                .ToListAsync();
        }
    }

    public async Task<ICollection<Class>> GetClassesByInstructorAsync(string instructorId)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var classes = await context.Classes
                .Include(c => c.Trainer)
                .Include(c => c.Training)
                .Where(c => c.Trainer.Id == instructorId)
                .AsNoTracking()
                .ToListAsync();

            return classes;
        }
    }
}