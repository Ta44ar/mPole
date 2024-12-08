using mPole.Data.Models;

public interface IUserService
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<ICollection<ApplicationUser>> GetAllUsersAsync();
    Task<ICollection<string?>> GetExistingRolesAsync();
}