using mPole.Data.DTOs;
using mPole.Data.Models;

public interface IRegistrationService
{
    Task RegisterForClassAsync(RegisterForClassDto dto, CancellationToken cancellationToken);
    Task<ICollection<ApplicationUser>> GetRegisteredUsersByClassIdAsync(int classId);
    Task<bool> IsUserRegisteredAlready(string userId, int classId);
    Task ResignFromClassAsync(string userId, int classId, CancellationToken cancellationToken);
    Task<ICollection<Registration>> GetRegistrationsByClassIdAsync(int classId); // Dodaj tê metodê
}