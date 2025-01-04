using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;

public class UserRepository : IUserRepository
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(IServiceScopeFactory serviceScopeFactory, UserManager<ApplicationUser> userManager)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        if (user == null)
        {
            throw new ArgumentNullException("No user with given username found.");
        }

        return user;
    }

    public async Task<ICollection<ApplicationUser>> GetAllUsersAsync()
    {
        var users = await _userManager.Users
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
                .Where(c => c.RegisteredUsers.Any(u => u.Id == userId))
                .ToListAsync();
        }
    }

    public async Task<ICollection<Class>> GetClassesByInstructorAsync(string instructorName)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Classes
                .Include(c => c.Training)
                .Where(c => c.Trainer == instructorName)
                .ToListAsync();
        }
    }
}
