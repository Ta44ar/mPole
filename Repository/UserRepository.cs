using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mPole.Data.DbContext;
using mPole.Data.Models;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
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
        var allRoles = await _context.Roles
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