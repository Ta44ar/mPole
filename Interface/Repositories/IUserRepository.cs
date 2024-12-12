using mPole.Data.Models;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<List<ApplicationUser>> GetAllUsersAsync();
}