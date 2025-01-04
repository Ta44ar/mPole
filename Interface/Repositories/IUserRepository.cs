using mPole.Data.Models;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<ICollection<ApplicationUser>> GetAllUsersAsync();
    Task<ICollection<string?>> GetExistingRolesAsync();
    Task<ICollection<Class>> GetClassesByUserAsync(string userId);
    Task<ICollection<Class>> GetClassesByInstructorAsync(string name);
}
