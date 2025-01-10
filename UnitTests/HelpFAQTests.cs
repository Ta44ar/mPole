using Microsoft.Extensions.Localization;
using Moq;
using mPole.Components.Layout;
using mPole.Components.Pages;
using mPole.Resources;
using MudBlazor;
using Xunit;

namespace mPole_UnitTests_BussinessLogic
{
    public class HelpFAQTests
    {
        private readonly Mock<IStringLocalizer<SharedResource>> _mockLocalizer;
        private readonly Mock<ISnackbar> _mockSnackbar;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly HelpFAQ _component;

        public HelpFAQTests()
        {
            _mockLocalizer = new Mock<IStringLocalizer<SharedResource>>();
            _mockSnackbar = new Mock<ISnackbar>();
            _mockEmailService = new Mock<IEmailService>();
            _component = new HelpFAQ
            {
                Res = _mockLocalizer.Object,
                Snackbar = _mockSnackbar.Object,
                EmailService = _mockEmailService.Object,
            };

            _mockLocalizer.Setup(x => x["EmailNewQuestion"]).Returns(new LocalizedString("EmailNewQuestion", "New question received"));
            _mockLocalizer.Setup(x => x["SuccessEmailSend"]).Returns(new LocalizedString("SuccessEmailSend", "Email sent successfully"));
            _mockLocalizer.Setup(x => x["ErrorDuringEmailSend"]).Returns(new LocalizedString("ErrorDuringEmailSend", "Error during email send"));
        }

        [Fact]
        public async Task SendEmailAsync_ShouldShowSuccessMessage_WhenEmailIsSentSuccessfully()
        {
            // Arrange
            var questionModel = new QuestionModel { Email = "test@example.com", Question = "Test question" };
            _component.questionModel = questionModel;
            _component.MainLayout = new MainLayout { userName = "test@example.com" };

            // Act
            await _component.SendEmailAsync();

            // Assert
            _mockSnackbar.Verify(s => s.Add(It.Is<string>(msg => msg == "Email sent successfully"), It.Is<Severity>(sev => sev == Severity.Success), null, null), Times.Once);
        }

        [Fact]
        public async Task SendEmailAsync_ShouldShowErrorMessage_WhenEmailSendingFails()
        {
            // Arrange
            var questionModel = new QuestionModel { Email = "test@example.com", Question = "Test question" };
            _component.questionModel = questionModel;
            _component.MainLayout = new MainLayout { userName = "test@example.com" };

            _mockEmailService.Setup(es => es.SendTemplatedEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("SMTP error"));

            // Act
            await _component.SendEmailAsync();

            // Assert
            _mockSnackbar.Verify(s => s.Add(It.Is<string>(msg => msg.Contains("Error during email send: SMTP error")), It.Is<Severity>(sev => sev == Severity.Error), null, null), Times.Once);
        }
    }
}