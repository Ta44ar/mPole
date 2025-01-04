using Microsoft.AspNetCore.Identity;
using mPole.Data.Models;
using System.Security.Claims;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, IServiceScopeFactory serviceScopeFactory, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _serviceScopeFactory = serviceScopeFactory;
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

    public async Task<IEnumerable<ApplicationUser>> SearchTrainersAsync(string searchText, CancellationToken cancellationToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var trainers = await userManager.GetUsersInRoleAsync("Instructor");

            if (trainers == null)
                return new List<ApplicationUser>();

            return trainers.Where(t => t.UserName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                       t.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                       t.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }
    }

    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return null;
        }

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return user;
        }
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