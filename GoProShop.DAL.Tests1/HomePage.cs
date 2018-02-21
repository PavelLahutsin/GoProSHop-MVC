using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GoProShop.DAL.Tests1
{
    public class HomePage
    {
        private readonly IWebDriver _webDriver;
        private readonly Actions _action;

        public HomePage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _action = new Actions(webDriver);
        }

        private IWebElement _feedbackUserName => _webDriver.FindElement(By.Id("Name"));

        private IWebElement _feedbackSurName => _webDriver.FindElement(By.Id("SurName"));

        private IWebElement _feedbackEmail => _webDriver.FindElement(By.Id("Email"));

        private IWebElement _feedbackRate5 => _webDriver.FindElement(By.Id("Rate5"));

        private IWebElement _message => _webDriver.FindElement(By.Id("Message"));

        private IWebElement _submitFeedback => _webDriver.FindElement(By.Id("submit-feedback"));

        private IWebElement _notificationMessage => _webDriver.FindElement(By.ClassName("table-notification-message"));

        public void InputUserName(string value)
        {
            _feedbackUserName.SendKeys(value);
        }

        public void InputSurname(string value)
        {
            _feedbackSurName.SendKeys(value);
        }

        public void InputEmail(string value)
        {
            _feedbackEmail.SendKeys(value);
        }

        public void InputMessage(string value)
        {
            _message.SendKeys(value);
        }

        public void ClickStarRate5()
        {
            _feedbackRate5.Click();
        }

        public void SubmitFeedback()
        {
            _submitFeedback.Submit();
        }

        public bool IsNotificationDisplayed()
        {
           return _notificationMessage.Displayed;
        }
        
    }
}
