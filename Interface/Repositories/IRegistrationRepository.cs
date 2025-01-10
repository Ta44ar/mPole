using mPole.Data.Models;

public interface IRegistrationRepository
{
    Task AddAsync(Registration registration);
    Task<ICollection<ApplicationUser>> GetRegisteredUsersByClassIdAsync(int classId);
    Task RemoveOldClassRegistrationsAsync(int classId);
    Task<bool> IsUserRegisteredForClassAsync(string userId, int classId);
    Task DeleteAsync(Registration registration);
    Task<Registration?> GetRegistrationAsync(string userId, int classId);
    Task<ICollection<Registration>> GetRegistrationsByClassIdAsync(int classId); // Dodaj tê metodê
}