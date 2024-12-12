using mPole.Data.Models;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<ICollection<ApplicationUser>> GetAllUsersAsync();
    Task<ICollection<string?>> GetExistingRolesAsync();
}