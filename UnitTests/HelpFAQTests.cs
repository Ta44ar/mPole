using Xunit;
using Moq;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using mPole.Resources;
using mPole.Components.Pages;

namespace mPole_UnitTests_BussinessLogic
{
    public class HelpFAQTests
    {
        private readonly Mock<IStringLocalizer<SharedResource>> _mockLocalizer;
        private readonly Mock<ISnackbar> _mockSnackbar;
        private readonly HelpFAQ _component;

        public HelpFAQTests()
        {
            _mockLocalizer = new Mock<IStringLocalizer<SharedResource>>();
            _mockSnackbar = new Mock<ISnackbar>();
            _component = new HelpFAQ
            {
                Res = _mockLocalizer.Object,
                Snackbar = _mockSnackbar.Object
            };

            // Setup mock localizer to return expected values
            _mockLocalizer.Setup(x => x["ValidationEmailRequired"]).Returns(new LocalizedString("ValidationEmailRequired", "Email is required"));
            _mockLocalizer.Setup(x => x["ValidationEmailInvalid"]).Returns(new LocalizedString("ValidationEmailInvalid", "Invalid email address"));
            _mockLocalizer.Setup(x => x["ValidationQuestionRequired"]).Returns(new LocalizedString("ValidationQuestionRequired", "Question is required"));
        }


        [Fact]
        public async Task SubmitQuestion_ShouldShowSuccessMessage_WhenValidationSucceeds()
        {
            // Arrange
            var questionModel = new QuestionModel { Email = "test@example.com", Question = "Test question" };
            _component.questionModel = questionModel;

            // Act
            await _component.SubmitQuestion();

            // Assert
            _mockSnackbar.Verify(s => s.Add(It.IsAny<string>(), It.Is<Severity>(sev => sev == Severity.Success), null, null), Times.Once);
        }

        [Fact]
        public async Task SubmitQuestion_ShouldNotShowSuccessMessage_WhenValidationFails()
        {
            // Arrange
            var questionModel = new QuestionModel { Email = "", Question = "" }; // Invalid data
            _component.questionModel = questionModel;

            // Act
            await _component.SubmitQuestion();

            // Assert
            _mockSnackbar.Verify(s => s.Add(It.IsAny<string>(), It.Is<Severity>(sev => sev == Severity.Success), null, null), Times.Never);
        }
    }
}