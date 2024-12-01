using mPole.Data.Models;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
    {
        return await _userRepository.GetUserByNameAsync(userName);
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }
}