using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using mPole.Data.DTOs;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;

namespace mPole_UnitTests_Services
{
    public class RegistrationServiceTests
    {
        private readonly Mock<IRegistrationRepository> _mockRegistrationRepository;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IClassService> _mockClassService;
        private readonly RegistrationService _registrationService;

        public RegistrationServiceTests()
        {
            _mockRegistrationRepository = new Mock<IRegistrationRepository>();
            _mockUserService = new Mock<IUserService>();
            _mockClassService = new Mock<IClassService>();
            _registrationService = new RegistrationService(
                _mockRegistrationRepository.Object,
                _mockUserService.Object,
                _mockClassService.Object
            );
        }

        [Fact]
        public async Task RegisterForClassAsync_ShouldRegisterUser_WhenDataIsValid()
        {
            // Arrange
            var dto = new RegisterForClassDto
            {
                UserId = "user1",
                ClassId = 1,
                Status = RegistrationStatus.Confirmed
            };

            var user = new ApplicationUser { Id = "user1" };
            var classEntity = new Class { Id = 1 };

            _mockUserService.Setup(s => s.GetUserByIdAsync(dto.UserId)).ReturnsAsync(user);
            _mockClassService.Setup(s => s.GetClassByIdAsync(dto.ClassId, It.IsAny<CancellationToken>())).ReturnsAsync(classEntity);

            // Act
            await _registrationService.RegisterForClassAsync(dto, CancellationToken.None);

            // Assert
            _mockRegistrationRepository.Verify(r => r.AddAsync(It.Is<Registration>(reg =>
                reg.UserId == dto.UserId &&
                reg.ClassId == dto.ClassId &&
                reg.Status == dto.Status
            )), Times.Once);
        }

        [Fact]
        public async Task RegisterForClassAsync_ShouldThrowException_WhenUserOrClassIsInvalid()
        {
            // Arrange
            var dto = new RegisterForClassDto
            {
                UserId = "user1",
                ClassId = 1,
                Status = RegistrationStatus.Confirmed
            };

            _mockUserService.Setup(s => s.GetUserByIdAsync(dto.UserId)).ReturnsAsync((ApplicationUser)null);
            _mockClassService.Setup(s => s.GetClassByIdAsync(dto.ClassId, It.IsAny<CancellationToken>())).ReturnsAsync((Class)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _registrationService.RegisterForClassAsync(dto, CancellationToken.None));
        }

        [Fact]
        public async Task ResignFromClassAsync_ShouldRemoveRegistration_WhenRegistrationExists()
        {
            // Arrange
            var userId = "user1";
            var classId = 1;
            var registration = new Registration { UserId = userId, ClassId = classId };

            _mockRegistrationRepository.Setup(r => r.GetRegistrationAsync(userId, classId)).ReturnsAsync(registration);

            // Act
            await _registrationService.ResignFromClassAsync(userId, classId, CancellationToken.None);

            // Assert
            _mockRegistrationRepository.Verify(r => r.DeleteAsync(registration), Times.Once);
        }

        [Fact]
        public async Task ResignFromClassAsync_ShouldNotRemoveRegistration_WhenRegistrationDoesNotExist()
        {
            // Arrange
            var userId = "user1";
            var classId = 1;

            _mockRegistrationRepository.Setup(r => r.GetRegistrationAsync(userId, classId)).ReturnsAsync((Registration)null);

            // Act
            await _registrationService.ResignFromClassAsync(userId, classId, CancellationToken.None);

            // Assert
            _mockRegistrationRepository.Verify(r => r.DeleteAsync(It.IsAny<Registration>()), Times.Never);
        }
    }
}