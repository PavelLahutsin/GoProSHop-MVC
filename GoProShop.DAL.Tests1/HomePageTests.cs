using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using System.Threading;

namespace GoProShop.DAL.Tests1
{
    public class HomePageTests
    {
        private const string BaseUrl = "http://localhost:8888";
        private HomePage _homePage;
        private IWebDriver _driver;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _driver = new ChromeDriver("D:\\Apps");
            _driver.Url = BaseUrl;
            _homePage = new HomePage(_driver);
        }

        [Test]
        public void Submit_Feedback_When_All_Values_Are_Correct_Display_Notification_Message()
        { 
            // Arrange
            _homePage.InputUserName("testUser");
            _homePage.InputSurname("testSurname");
            _homePage.InputEmail("test@email.com");
            _homePage.ClickStarRate5();
            _homePage.InputMessage("testmessage");

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsTrue(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_Values_Are_Not_Correct_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName(string.Empty);
            _homePage.InputSurname(string.Empty);
            _homePage.InputEmail(string.Empty);
            _homePage.InputMessage(string.Empty);

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_UserName_Is_Too_Short_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName("tt");
            _homePage.InputSurname("testSurname");
            _homePage.InputEmail("test@email.com");
            _homePage.ClickStarRate5();
            _homePage.InputMessage("testmessage");

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_UserName_Is_Too_Long_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName(GetInvalidMessage());
            _homePage.InputSurname("testSurname");
            _homePage.InputEmail("test@email.com");
            _homePage.ClickStarRate5();
            _homePage.InputMessage("testmessage");

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_Surname_Is_Too_Short_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName("testUserName");
            _homePage.InputSurname("tt");
            _homePage.InputEmail("test@email.com");
            _homePage.ClickStarRate5();
            _homePage.InputMessage("testmessage");

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_Surname_Is_Too_Long_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName("testUserName");
            _homePage.InputSurname(GetInvalidMessage());
            _homePage.InputEmail("test@email.com");
            _homePage.ClickStarRate5();
            _homePage.InputMessage("testmessage");

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_Email_Is_Not_Valid_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName("testUserName");
            _homePage.InputSurname("testSurName");
            _homePage.InputEmail("te@dasdas");
            _homePage.ClickStarRate5();
            _homePage.InputMessage("testmessage");

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_Rating_Is_Not_Chosen_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName("testUserName");
            _homePage.InputSurname("testSurName");
            _homePage.InputEmail("te@dasdas.com");
            _homePage.InputMessage("testmessage");

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_Message_Is_Too_Short_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName("testUserName");
            _homePage.InputSurname("testSurName");
            _homePage.InputEmail("te@dasdas");
            _homePage.ClickStarRate5();
            _homePage.InputMessage("tt");

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [Test]
        public void Submit_Feedback_When_Message_Is_Too_Long_Not_Display_Notification_Message()
        {
            // Arrange
            _homePage.InputUserName("testUserName");
            _homePage.InputSurname("testSurName");
            _homePage.InputEmail("te@dasdas.com");
            _homePage.ClickStarRate5();
            _homePage.InputMessage(GetInvalidMessage());

            // Act
            _homePage.SubmitFeedback();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(_homePage.IsNotificationDisplayed());
        }

        [TearDown]
        public void AfterEachTest()
        {
            _driver.Navigate().Refresh();
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }

        private string GetInvalidMessage()
        {
            var sb = new StringBuilder("Test");

            for (int i = 0; i < 100; i++)
            {
                sb.Append("Test");
            }

            return sb.ToString();
        }
    }
}
