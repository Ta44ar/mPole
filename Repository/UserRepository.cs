using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mPole.Data.Models;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
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

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userManager.Users
            .Select(u => new ApplicationUser
            {
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            })
            .ToListAsync();
    }
}