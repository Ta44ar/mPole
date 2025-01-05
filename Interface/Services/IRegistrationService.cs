using mPole.Data.DTOs;
using mPole.Data.Models;

public interface IRegistrationService
{
    Task RegisterForClassAsync(RegisterForClassDto dto);
    Task<ICollection<Class>> GetClassesByUserIdAsync(string userId, RegistrationStatus status);
}