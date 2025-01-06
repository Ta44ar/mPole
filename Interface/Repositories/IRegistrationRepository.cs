using mPole.Data.Models;

public interface IRegistrationRepository
{
    Task AddAsync(Registration registration);
    Task<ICollection<ApplicationUser>> GetRegisteredUsersByClassIdAsync(int classId);
    Task RemoveOldClassRegistrationsAsync(int classId);
}