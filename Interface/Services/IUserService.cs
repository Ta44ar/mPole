using mPole.Data.Models;

public interface IUserService
{
    Task<ApplicationUser?> GetUserByIdAsync(string userId);
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<ICollection<ApplicationUser>> GetAllUsersAsync();
    Task<ICollection<string?>> GetExistingRolesAsync();
    Task<IEnumerable<ApplicationUser>> SearchTrainersAsync(string searchText, CancellationToken cancellationToken);
    Task<ApplicationUser> GetCurrentUserAsync();
    Task<ICollection<Class>> GetClassesByInstructorAsync(string name);
}