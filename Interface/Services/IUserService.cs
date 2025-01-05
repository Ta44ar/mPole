using mPole.Data.Models;

public interface IUserService
{
    Task<ApplicationUser?> GetUserByIdAsync(string userId);
    Task<ApplicationUser?> GetUserByNameAsync(string userName);
    Task<ICollection<ApplicationUser>> GetAllUsersAsync();
    Task<ICollection<string?>> GetExistingRolesAsync();
    Task<IEnumerable<ApplicationUser>> SearchTrainersAsync(string searchText, CancellationToken cancellationToken);
    Task<IEnumerable<ApplicationUser>> SearchParticipantsAsync(string searchText, CancellationToken cancellationToken);
    Task<ApplicationUser> GetCurrentUserAsync();
    Task<ICollection<Class>> GetClassesByInstructorAsync(string instructorId);
    Task<ICollection<Class>> GetClassesByUserIdAsync(string userId, RegistrationStatus status);
}