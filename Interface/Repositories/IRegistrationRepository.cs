using mPole.Data.Models;

public interface IRegistrationRepository
{
    Task AddAsync(Registration registration);
    Task<ICollection<Class>> GetClassesByUserIdAsync(string userId, RegistrationStatus status);
}