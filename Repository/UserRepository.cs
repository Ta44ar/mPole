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

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentNullException("No user with given ID found.");
            }

            return user;
        }
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
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProfileImage = u.ProfileImage
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

    public async Task<ICollection<Class>> GetClassesByUserIdAsync(string userId)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var classes = await context.Classes
                .Include(c => c.Trainer)
                .Include(c => c.Training)
                .Include(c => c.Registrations)
                .Where(c => c.Registrations.Any(r => r.UserId == userId))
                .AsNoTracking()
                .ToListAsync();

            return classes;
        }
    }
}