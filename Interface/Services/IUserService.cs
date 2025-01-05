using mPole.Data.Models;

public interface IUserService
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<ICollection<ApplicationUser>> GetAllUsersAsync();
    Task<ICollection<string?>> GetExistingRolesAsync();
    Task<IEnumerable<string>> SearchTrainersAsync(string value);
    Task<ApplicationUser> GetCurrentUserAsync();
    Task<ICollection<Class>> GetClassesByUserAsync(string userId);
    Task<ICollection<Class>> GetClassesByInstructorAsync(string name);
}
