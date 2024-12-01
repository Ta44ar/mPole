using mPole.Data.Models;

public interface IUserService
{
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<List<ApplicationUser>> GetAllUsersAsync();
}