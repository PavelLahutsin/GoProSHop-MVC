using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

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
            
            PageFactory.InitElements(webDriver, this);
        }

        [FindsBy(How = How.Id, Using = "Name")]
        private IWebElement _feedbackUserName;

        [FindsBy(How = How.Id, Using = "SurName")]
        private IWebElement _feedbackSurName;

        [FindsBy(How = How.Id, Using = "Email")]
        private IWebElement _feedbackEmail;

        [FindsBy(How = How.Id, Using = "Rate5")]
        private IWebElement _feedbackRate5;

        [FindsBy(How = How.Id, Using = "Message")]
        private IWebElement _message;

        [FindsBy(How = How.Id, Using = "submit-feedback")]
        private IWebElement _submitFeedback;

        [FindsBy(How = How.ClassName, Using = "table-notification-message")]
        private IWebElement _notificationMessage;

        
        public void MoveToFeedbackUserName()
        {
            _action.MoveToElement(_feedbackUserName);
            _action.Perform();
        }

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
