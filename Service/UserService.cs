using Microsoft.AspNetCore.Identity;
using mPole.Data.Models;
using System.Security.Claims;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
    {
        return await _userRepository.GetUserByNameAsync(userName);
    }

    public async Task<ICollection<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<ICollection<string?>> GetExistingRolesAsync()
    {
        return await _userRepository.GetExistingRolesAsync();
    }

    public async Task<IEnumerable<string>> SearchTrainersAsync(string value)
    {
        if (string.IsNullOrEmpty(value))
            return new List<string>();

        var users = await _userManager.GetUsersInRoleAsync("Instructor");

        if (users == null)
            return new List<string>();

        var trainers = users.Where(u => u.UserName.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                        .Select(u => u.FirstName + " " + u.LastName)
                        .ToList();
        return trainers;
    }

    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId == null)
        {
            return null;
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        return user;
    }

    public async Task<ICollection<Class>> GetClassesByUserAsync(string userId)
    {
        return await _userRepository.GetClassesByUserAsync(userId);
    }

    public async Task<ICollection<Class>> GetClassesByInstructorAsync(string instructorName)
    {
        return await _userRepository.GetClassesByInstructorAsync(instructorName);
    }
}
