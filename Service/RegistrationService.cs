using Microsoft.Identity.Client;
using mPole.Data.DTOs;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IUserService _userService;
    private readonly IClassService _classService;

    public RegistrationService(IRegistrationRepository registrationRepository, IUserService userService, IClassService classService)
    {
        _registrationRepository = registrationRepository;
        _userService = userService;
        _classService = classService;
    }

    public async Task RegisterForClassAsync(RegisterForClassDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(dto.UserId);
        var classEntity = await _classService.GetClassByIdAsync(dto.ClassId, CancellationToken.None);

        if (user == null || classEntity == null)
        {
            throw new Exception("Invalid user or class.");
        }

        var registration = new Registration
        {
            UserId = dto.UserId,
            ClassId = dto.ClassId,
            RegistrationDate = DateTime.UtcNow,
            Status = dto.Status
        };

        await _registrationRepository.AddAsync(registration);
    }

    public async Task<ICollection<ApplicationUser>> GetRegisteredUsersByClassIdAsync(int classId)
    {
        return await _registrationRepository.GetRegisteredUsersByClassIdAsync(classId);
    }

    public async Task<bool> IsUserRegisteredAlready(string userId, int classId)
    {
        return await _registrationRepository.IsUserRegisteredForClassAsync(userId, classId);
    }

    public async Task ResignFromClassAsync(string userId, int classId, CancellationToken cancellationToken)
    {
        var registration = await _registrationRepository.GetRegistrationAsync(userId, classId);
        if (registration != null)
        {
            await _registrationRepository.DeleteAsync(registration);
        }
    }

    public async Task<ICollection<Registration>> GetRegistrationsByClassIdAsync(int classId)
    {
        return await _registrationRepository.GetRegistrationsByClassIdAsync(classId);
    }
}