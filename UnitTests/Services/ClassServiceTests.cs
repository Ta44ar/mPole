using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;
using mPole.Service;

namespace mPole_UnitTests_Services
{
    public class ClassServiceTests
    {
        private readonly Mock<IClassRepository> _mockClassRepository;
        private readonly ClassService _classService;

        public ClassServiceTests()
        {
            _mockClassRepository = new Mock<IClassRepository>();
            _classService = new ClassService(_mockClassRepository.Object);
        }

        [Fact]
        public async Task AddNewClassAsync_ShouldAddClass_WhenDataIsValid()
        {
            // Arrange
            var classEntity = new Class { Id = 1, Location = "Test Location" };

            _mockClassRepository.Setup(r => r.AddAsync(classEntity, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            await _classService.AddNewClassAsync(classEntity, CancellationToken.None);

            // Assert
            _mockClassRepository.Verify(r => r.AddAsync(classEntity, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetClassByIdAsync_ShouldReturnClass_WhenClassExists()
        {
            // Arrange
            var classId = 1;
            var classEntity = new Class { Id = classId, Location = "Test Location" };

            _mockClassRepository.Setup(r => r.GetClassByIdAsync(classId, It.IsAny<CancellationToken>())).ReturnsAsync(classEntity);

            // Act
            var result = await _classService.GetClassByIdAsync(classId, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(classId, result.Id);
        }

        [Fact]
        public async Task GetClassByIdAsync_ShouldReturnNull_WhenClassDoesNotExist()
        {
            // Arrange
            var classId = 1;

            _mockClassRepository.Setup(r => r.GetClassByIdAsync(classId, It.IsAny<CancellationToken>())).ReturnsAsync((Class)null);

            // Act
            var result = await _classService.GetClassByIdAsync(classId, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateClassAsync_ShouldUpdateClass_WhenDataIsValid()
        {
            // Arrange
            var classEntity = new Class { Id = 1, Location = "Test Location" };

            _mockClassRepository.Setup(r => r.UpdateAsync(classEntity, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            await _classService.UpdateClassAsync(classEntity, CancellationToken.None);

            // Assert
            _mockClassRepository.Verify(r => r.UpdateAsync(classEntity, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteClassAsync_ShouldDeleteClass_WhenClassExists()
        {
            // Arrange
            var classId = 1;

            _mockClassRepository.Setup(r => r.DeleteAsync(classId, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            await _classService.DeleteClassAsync(classId, CancellationToken.None);

            // Assert
            _mockClassRepository.Verify(r => r.DeleteAsync(classId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}