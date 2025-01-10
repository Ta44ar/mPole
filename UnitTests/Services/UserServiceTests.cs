using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace mPole_UnitTests_Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IServiceScopeFactory> _mockServiceScopeFactory;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockServiceScopeFactory = new Mock<IServiceScopeFactory>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _userService = new UserService(
                _mockUserRepository.Object,
                _mockServiceScopeFactory.Object,
                _mockHttpContextAccessor.Object
            );
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = "user1";
            var user = new ApplicationUser { Id = userId };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "user1";

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByNameAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userName = "testuser";
            var user = new ApplicationUser { UserName = userName };

            _mockUserRepository.Setup(r => r.GetUserByNameAsync(userName)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByNameAsync(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userName, result.UserName);
        }

        [Fact]
        public async Task GetUserByNameAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userName = "testuser";

            _mockUserRepository.Setup(r => r.GetUserByNameAsync(userName)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _userService.GetUserByNameAsync(userName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCurrentUserAsync_ShouldReturnCurrentUser_WhenUserIsAuthenticated()
        {
            // Arrange
            var userId = "user1";
            var user = new ApplicationUser { Id = userId };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            _mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(new DefaultHttpContext { User = claimsPrincipal });

            var mockScope = new Mock<IServiceScope>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            mockScope.Setup(s => s.ServiceProvider).Returns(mockServiceProvider.Object);
            _mockServiceScopeFactory.Setup(f => f.CreateScope()).Returns(mockScope.Object);

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            mockServiceProvider.Setup(p => p.GetService(typeof(UserManager<ApplicationUser>))).Returns(mockUserManager.Object);

            mockUserManager.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetCurrentUserAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetCurrentUserAsync_ShouldReturnNull_WhenUserIsNotAuthenticated()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(new DefaultHttpContext { User = new ClaimsPrincipal() });

            // Act
            var result = await _userService.GetCurrentUserAsync();

            // Assert
            Assert.Null(result);
        }
    }
}