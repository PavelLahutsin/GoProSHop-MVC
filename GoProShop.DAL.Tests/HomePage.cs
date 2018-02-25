using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

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

        private IWebElement _shoppingCart => _webDriver.FindElement(By.Id("shopping-cart"));

        private IWebElement _submitFeedback => _webDriver.FindElement(By.Id("submit-feedback"));

        private IWebElement _notificationMessage => _webDriver.FindElement(By.ClassName("table-notification-message"));

        private IWebElement _catalogMenuItem1 => _webDriver.FindElement(By.Id("catalog-menu-item-1"));

        private IWebElement _catalogSubMenuItem1 => _webDriver.FindElement(By.Id("catalog-submenu-item-1"));       

        private IWebElement _catalogMenu => _webDriver.FindElement(By.Id("catalog-menu"));

        private IWebElement _modalWindow => _webDriver.FindElement(By.Id("mainModal"));

        private IWebElement _checkOutButton => _webDriver.FindElement(By.XPath("//*[@data-test-id='checkout-button']"));

        private IWebElement _modalCheckOutButton => _modalWindow.FindElement(By.CssSelector("a.btn-success"));

        private IWebElement _checkoutContinueButton => _modalWindow.FindElement(By.XPath("//*[@data-test-id='checkout-continue-button']"));

        private IWebElement _checkoutForm => _modalWindow.FindElement(By.XPath("//form[@id='create-order-form']"));

        private IWebElement _paymentTypeErrorMessage => _modalWindow.FindElementSafe(By.XPath("//*[@data-test-id='payment-type-error-message']"));

        private IWebElement _emailErrorMessage => _modalWindow.FindElementSafe(By.XPath("//*[@data-test-id='email-error-message']"));

        private IWebElement _phoneErrorMessage => _modalWindow.FindElementSafe(By.XPath("//*[@data-test-id='phone-error-message']"));

        private IWebElement _deliveryTypeErrorMessage => _modalWindow.FindElementSafe(By.XPath("//*[@data-test-id='delivery-type-error-message']"));

        private IWebElement _orderDateErrorMessage => _modalWindow.FindElementSafe(By.XPath("//*[@data-test-id='order-date-error-message']"));

        private IWebElement _paymentTypeSelect => _webDriver.FindElement(By.XPath("//*[@data-test-id='payment-type-select']"));

        private IWebElement _deliveryTypeSelect => _webDriver.FindElement(By.XPath("//*[@data-test-id='delivery-type-select']"));

        private IWebElement _customerNameInput => _webDriver.FindElement(By.XPath("//*[@data-test-id='customer-name-input']"));

        private IWebElement _emailInput => _webDriver.FindElement(By.XPath("//*[@data-test-id='email-input']"));

        private IWebElement _phoneInput => _webDriver.FindElement(By.XPath("//*[@data-test-id='phone-input']"));

        private IWebElement _orderDateInput => _webDriver.FindElement(By.XPath("//*[@data-test-id='order-date-input']"));

        private IWebElement _addressInput => _webDriver.FindElement(By.XPath("//*[@data-test-id='address-input']"));

        private IList<IWebElement> _menuItems => _webDriver.FindElements(By.CssSelector(".catalog-main-menu > li"));

        private IList<IWebElement> _products => _webDriver.FindElements(By.ClassName("catalog-item-box"));

        private IList<IWebElement> _modalCartProducts => _webDriver.FindElements(By.CssSelector("#mainModal a.cart-delete-link"));

        public void InputUserName(string value) => _feedbackUserName.SendKeys(value);

        public void InputSurname(string value) => _feedbackSurName.SendKeys(value);

        public void InputEmail(string value) => _feedbackEmail.SendKeys(value);

        public void InputOrderEmail(string value) => _emailInput.SendKeys(value);

        public void InputOrderCustomerName(string value) => _customerNameInput.SendKeys(value);

        public void InputOrderAddress(string value) => _addressInput.SendKeys(value);

        public void InputOrderDate(string value) => _orderDateInput.SendKeys(value);

        public void InputOrderPhone(string value) => _phoneInput.SendKeys(value);

        public void InputMessage(string value) => _message.SendKeys(value);

        public void DeleteShoppingCartProduct(int index) => _modalCartProducts[index].Click();

        // Events

        public void ClickCatalogMenu() => _catalogMenu.Click();

        public void ClickCatalogSubMenuItem1() => _catalogSubMenuItem1.Click();

        public void ClickProductButton(int index) => GetElementFromList(_products, index, "a.btn.btn-success").Click();

        public void ClickStarRate5() => _feedbackRate5.Click();

        public void SubmitFeedback() => _submitFeedback.Submit();

        public void ClickShoppingCart() => _shoppingCart.Click();

        public void ClickCheckOutContinueButton() => _checkoutContinueButton.Click();

        public void ClickModalCheckOutButton() => _modalCheckOutButton.Click();

        public void ClickCheckOutButtton() => _checkOutButton.Click();

        public void SelectOrderPaymentType(int index) => SelectElementByIndex(_paymentTypeSelect, index);

        public void SelectOrderDeliveryType(int index) => SelectElementByIndex(_deliveryTypeSelect, index);

        // Css Attributes

        public int GetMenuListCount() => _menuItems.Count();     

        public string GetCatalogSubMenuItem1Href() => _catalogSubMenuItem1.GetAttribute("href");

        public int ShoppingCartCount => int.Parse(_shoppingCart.FindElement(By.CssSelector("span.badge-shopping-cart")).Text);

        public int ModalCartProductsCount => _modalCartProducts.Count;

        // Boolean Values

        public bool IsNotificationDisplayed() => _notificationMessage?.Displayed ?? false;

        public bool IsModalWindowDisplayed => _modalWindow?.Displayed ?? false;

        public bool IsCheckOutFormDisplayed => _checkoutForm?.Displayed ?? false;

        public bool IsPaymentTypeErrorMessageDisplayed => _paymentTypeErrorMessage?.Displayed ?? false;

        public bool IsOrderDateErrorMessageDisplayed => _orderDateErrorMessage?.Displayed ?? false;

        public bool IsEmailErrorMessageDisplayed => _emailErrorMessage?.Displayed ?? false;

        public bool IsPhoneErrorMessageDisplayed => _phoneErrorMessage?.Displayed ?? false;

        public bool IsDeliveryTypeErrorMessageDisplayed => _deliveryTypeErrorMessage?.Displayed ?? false;

        // Actions

        public void MoveToCatalogMenuItem1() => _action.MoveToElement(_menuItems[0]).Perform();

        public void MoveToProductButton(int index) => _action.MoveToElement(GetElementFromList(_products, index, "a.btn.btn-success")).Perform();

        // Helpers
        private IWebElement GetElementFromList(IList<IWebElement> webElements, int index, string cssSelector)
            => webElements[index].FindElement(By.CssSelector(cssSelector));

        private void SelectElementByIndex(IWebElement element, int index) => new SelectElement(element).SelectByIndex(index);
    }

}
