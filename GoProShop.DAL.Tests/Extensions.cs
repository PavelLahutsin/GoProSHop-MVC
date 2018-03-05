using OpenQA.Selenium;

namespace GoProShop.DAL.Tests1
{
    public static class Extensions
    { 
        public static IWebElement FindElementSafe(this IWebElement webElement, By by)
        {
            try
            {
                return webElement.FindElement(by);
            }
            catch
            {
                return null;
            }
        }
    }
}
