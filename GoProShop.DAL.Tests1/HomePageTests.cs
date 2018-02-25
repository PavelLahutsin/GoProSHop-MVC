using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using System.Threading;

namespace GoProShop.DAL.Tests1
{
    [TestFixture]
    public class HomePageTests
    {
        private const string BaseUrl = "http://goproshop.test/";
        private HomePage _homePage;
        private IWebDriver _driver;
        
        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            _driver = new ChromeDriver("D:\\Apps");
            _driver.Manage().Window.Maximize();
            _homePage = new HomePage(_driver);
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
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

        [Test]
        public void Click_Some_Product_Buttons_Should_Increase_ShoppingCart_Quantity()
        {
            // Arrange
            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            Thread.Sleep(1500);
            _homePage.MoveToProductButton(1);
            _homePage.ClickProductButton(1);
            Thread.Sleep(1500);
            _homePage.MoveToProductButton(2);
            _homePage.ClickProductButton(2);
            Thread.Sleep(1500);

            // Act
            var result = _homePage.ShoppingCartCount;

            //Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Click_ShoppingCart_Should_Open_Modal_Window()
        {
            // Arrange
            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            Thread.Sleep(1500);

            //Act
            _homePage.ClickShoppingCart();
            Thread.Sleep(2000);
            
            //Assert
            Assert.IsTrue(_homePage.IsModalWindowDisplayed);
        }

        [Test]
        public void Click_ShoppingCart_Modal_Cart_Products_Count_Should_Grater_Zero()
        {
            // Arrange
            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            Thread.Sleep(1500);
            _homePage.MoveToProductButton(1);
            _homePage.ClickProductButton(1);
            Thread.Sleep(1500);
            _homePage.MoveToProductButton(2);
            _homePage.ClickProductButton(2);
            Thread.Sleep(1500);

            //Act
            _homePage.ClickShoppingCart();
            Thread.Sleep(1500);

            //Assert
            Assert.AreEqual(3, _homePage.ModalCartProductsCount);
        }

        [Test]
        public void Click_Modal_CheckOut_Button_Should_Redirect_To_Checkout_Page()
        {
            // Arrange
            var expectedResult = "http://goproshop.test/Cart/CheckOut";

            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            Thread.Sleep(1500);
            _homePage.ClickShoppingCart();
            Thread.Sleep(1500);

            //Act
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(1500);

            //Assert
            Assert.AreEqual(expectedResult, _driver.Url);
        }

        [Test]
        public void Click_Modal_CheckOut_Continue_Button_Should_Redirect_To_Checkout_Page()
        {
            // Arrange
            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            Thread.Sleep(800);
            _homePage.ClickShoppingCart();
            Thread.Sleep(800);
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(800);

            // Act
            _homePage.ClickCheckOutContinueButton();
            Thread.Sleep(800);

            //Assert
            Assert.IsTrue(_homePage.IsCheckOutFormDisplayed);
        }

        [Test]
        public void Click_CheckOut_Button_When_All_Inputs_Are_Empty()
        {
            // Arrange
            var expectedResult = new CreateOrderResult
            {
                IsOrderDateErrorMessageDisplayed = true,
                IsDeliveryTypeErrorMessageDisplayed = true,
                IsEmailErrorMessageDisplayed = true,
                IsPaymentTypeErrorMessageDisplayed = true,
                IsPhoneErrorMessageDisplayed = true
            };

            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            _homePage.ClickShoppingCart();
            Thread.Sleep(800);
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(800);
            _homePage.ClickCheckOutContinueButton();
            Thread.Sleep(1500);

            //Act
            _homePage.ClickCheckOutButtton();
            Thread.Sleep(1500);
            var actualResult = new CreateOrderResult
            {
                IsDeliveryTypeErrorMessageDisplayed = _homePage.IsDeliveryTypeErrorMessageDisplayed,
                IsEmailErrorMessageDisplayed = _homePage.IsEmailErrorMessageDisplayed,
                IsPaymentTypeErrorMessageDisplayed = _homePage.IsPaymentTypeErrorMessageDisplayed,
                IsOrderDateErrorMessageDisplayed = _homePage.IsOrderDateErrorMessageDisplayed,
                IsPhoneErrorMessageDisplayed = _homePage.IsPhoneErrorMessageDisplayed
            };

            //Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Test]
        public void Click_CheckOut_Button_When_PaymentType_Is_Not_Selected()
        {
            // Arrange
            var expectedResult = new CreateOrderResult
            {
                IsOrderDateErrorMessageDisplayed = false,
                IsDeliveryTypeErrorMessageDisplayed = false,
                IsEmailErrorMessageDisplayed = false,
                IsPaymentTypeErrorMessageDisplayed = true,
                IsPhoneErrorMessageDisplayed = false
            };

            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            _homePage.ClickShoppingCart();
            Thread.Sleep(800);
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(800);
            _homePage.ClickCheckOutContinueButton();
            Thread.Sleep(1500);
            _homePage.InputOrderCustomerName("testName");
            _homePage.InputOrderEmail("test@test.com");
            _homePage.InputOrderPhone("testPhone");
            _homePage.SelectOrderDeliveryType(1);
            _homePage.InputOrderAddress("testaddress");
            _homePage.InputOrderDate("2018/01/01");
            //Act
            _homePage.ClickCheckOutButtton();
            Thread.Sleep(2000);
            var actualResult = new CreateOrderResult
            {
                IsDeliveryTypeErrorMessageDisplayed = _homePage.IsDeliveryTypeErrorMessageDisplayed,
                IsEmailErrorMessageDisplayed = _homePage.IsEmailErrorMessageDisplayed,
                IsPaymentTypeErrorMessageDisplayed = _homePage.IsPaymentTypeErrorMessageDisplayed,
                IsOrderDateErrorMessageDisplayed = _homePage.IsOrderDateErrorMessageDisplayed,
                IsPhoneErrorMessageDisplayed = _homePage.IsPhoneErrorMessageDisplayed
            };

            //Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Test]
        public void Click_CheckOut_Button_When_Email_Is_Empty_Should_Display_Email_Erorr_Message()
        {
            // Arrange
            var expectedResult = new CreateOrderResult
            {
                IsOrderDateErrorMessageDisplayed = false,
                IsDeliveryTypeErrorMessageDisplayed = false,
                IsEmailErrorMessageDisplayed = true,
                IsPaymentTypeErrorMessageDisplayed = false,
                IsPhoneErrorMessageDisplayed = false
            };

            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            _homePage.ClickShoppingCart();
            Thread.Sleep(800);
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(800);
            _homePage.ClickCheckOutContinueButton();
            Thread.Sleep(1500);
            _homePage.SelectOrderPaymentType(1);
            _homePage.InputOrderCustomerName("testName");
            _homePage.InputOrderPhone("testPhone");
            _homePage.SelectOrderDeliveryType(1);
            _homePage.InputOrderAddress("testaddress");
            _homePage.InputOrderDate("2018/01/01");
            //Act
            _homePage.ClickCheckOutButtton();
            Thread.Sleep(2000);
            var actualResult = new CreateOrderResult
            {
                IsDeliveryTypeErrorMessageDisplayed = _homePage.IsDeliveryTypeErrorMessageDisplayed,
                IsEmailErrorMessageDisplayed = _homePage.IsEmailErrorMessageDisplayed,
                IsPaymentTypeErrorMessageDisplayed = _homePage.IsPaymentTypeErrorMessageDisplayed,
                IsOrderDateErrorMessageDisplayed = _homePage.IsOrderDateErrorMessageDisplayed,
                IsPhoneErrorMessageDisplayed = _homePage.IsPhoneErrorMessageDisplayed
            };

            //Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Test]
        public void Click_CheckOut_Button_When_Phone_Is_Empty_Should_Display_Phone_Erorr_Message()
        {
            // Arrange
            var expectedResult = new CreateOrderResult
            {
                IsOrderDateErrorMessageDisplayed = false,
                IsDeliveryTypeErrorMessageDisplayed = false,
                IsEmailErrorMessageDisplayed = false,
                IsPaymentTypeErrorMessageDisplayed = false,
                IsPhoneErrorMessageDisplayed = true
            };

            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            _homePage.ClickShoppingCart();
            Thread.Sleep(800);
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(800);
            _homePage.ClickCheckOutContinueButton();
            Thread.Sleep(1500);
            _homePage.SelectOrderPaymentType(1);
            _homePage.InputOrderCustomerName("testName");
            _homePage.InputOrderEmail("test@test.com");
            _homePage.SelectOrderDeliveryType(1);
            _homePage.InputOrderAddress("testaddress");
            _homePage.InputOrderDate("2018/01/01");
            //Act
            _homePage.ClickCheckOutButtton();
            Thread.Sleep(2000);
            var actualResult = new CreateOrderResult
            {
                IsDeliveryTypeErrorMessageDisplayed = _homePage.IsDeliveryTypeErrorMessageDisplayed,
                IsEmailErrorMessageDisplayed = _homePage.IsEmailErrorMessageDisplayed,
                IsPaymentTypeErrorMessageDisplayed = _homePage.IsPaymentTypeErrorMessageDisplayed,
                IsOrderDateErrorMessageDisplayed = _homePage.IsOrderDateErrorMessageDisplayed,
                IsPhoneErrorMessageDisplayed = _homePage.IsPhoneErrorMessageDisplayed
            };

            //Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Test]
        public void Click_CheckOut_Button_When_DeliveryType_Is_Not_Selected_Should_Display_DeliveryType_Error_Message()
        {
            // Arrange
            var expectedResult = new CreateOrderResult
            {
                IsOrderDateErrorMessageDisplayed = false,
                IsDeliveryTypeErrorMessageDisplayed = true,
                IsEmailErrorMessageDisplayed = false,
                IsPaymentTypeErrorMessageDisplayed = false,
                IsPhoneErrorMessageDisplayed = false
            };

            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            _homePage.ClickShoppingCart();
            Thread.Sleep(800);
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(800);
            _homePage.ClickCheckOutContinueButton();
            Thread.Sleep(1500);
            _homePage.SelectOrderPaymentType(1);
            _homePage.InputOrderCustomerName("testName");
            _homePage.InputOrderEmail("test@test.com");
            _homePage.InputOrderPhone("testPhone");
            _homePage.InputOrderAddress("testaddress");
            _homePage.InputOrderDate("2018/01/01");
            //Act
            _homePage.ClickCheckOutButtton();
            Thread.Sleep(2000);
            var actualResult = new CreateOrderResult
            {
                IsDeliveryTypeErrorMessageDisplayed = _homePage.IsDeliveryTypeErrorMessageDisplayed,
                IsEmailErrorMessageDisplayed = _homePage.IsEmailErrorMessageDisplayed,
                IsPaymentTypeErrorMessageDisplayed = _homePage.IsPaymentTypeErrorMessageDisplayed,
                IsOrderDateErrorMessageDisplayed = _homePage.IsOrderDateErrorMessageDisplayed,
                IsPhoneErrorMessageDisplayed = _homePage.IsPhoneErrorMessageDisplayed
            };

            //Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Test]
        public void Click_CheckOut_Button_When_OrderDate_Is_Empty_Should_Display_OrderDate_Error_Message()
        {
            // Arrange
            var expectedResult = new CreateOrderResult
            {
                IsOrderDateErrorMessageDisplayed = true,
                IsDeliveryTypeErrorMessageDisplayed = false,
                IsEmailErrorMessageDisplayed = false,
                IsPaymentTypeErrorMessageDisplayed = false,
                IsPhoneErrorMessageDisplayed = false
            };

            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            _homePage.ClickShoppingCart();
            Thread.Sleep(800);
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(800);
            _homePage.ClickCheckOutContinueButton();
            Thread.Sleep(1500);
            _homePage.SelectOrderPaymentType(1);
            _homePage.InputOrderCustomerName("testName");
            _homePage.InputOrderEmail("test@test.com");
            _homePage.SelectOrderDeliveryType(1);
            _homePage.InputOrderPhone("testPhone");
            _homePage.InputOrderAddress("testaddress");
            //Act
            _homePage.ClickCheckOutButtton();
            Thread.Sleep(2000);
            var actualResult = new CreateOrderResult
            {
                IsDeliveryTypeErrorMessageDisplayed = _homePage.IsDeliveryTypeErrorMessageDisplayed,
                IsEmailErrorMessageDisplayed = _homePage.IsEmailErrorMessageDisplayed,
                IsPaymentTypeErrorMessageDisplayed = _homePage.IsPaymentTypeErrorMessageDisplayed,
                IsOrderDateErrorMessageDisplayed = _homePage.IsOrderDateErrorMessageDisplayed,
                IsPhoneErrorMessageDisplayed = _homePage.IsPhoneErrorMessageDisplayed
            };

            //Assert
            expectedResult.Should().BeEquivalentTo(actualResult);
        }

        [Test]
        public void Click_CheckOut_Button_When_All_Inputs_Are_Valid()
        {
            // Arrange
            var redirectUrl = "http://goproshop.test/Order/SuccessOrder";
            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            _homePage.ClickShoppingCart();
            Thread.Sleep(800);
            _homePage.ClickModalCheckOutButton();
            Thread.Sleep(800);
            _homePage.ClickCheckOutContinueButton();
            Thread.Sleep(800);
            _homePage.SelectOrderPaymentType(1);
            _homePage.InputOrderCustomerName("testName");
            _homePage.InputOrderEmail("test@test.com");
            _homePage.InputOrderPhone("testPhone");
            _homePage.SelectOrderDeliveryType(1);
            _homePage.InputOrderAddress("testaddress");
            _homePage.InputOrderDate("2018/01/01");
            _homePage.ClickCheckOutButtton();
            Thread.Sleep(4000);

            //Assert
            Assert.IsTrue(_driver.Url.StartsWith(redirectUrl));
        }

        [Test]
        public void Delete_Product_From_Shopping_Cart_Should_Delete()
        {
            // Arrange
            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            Thread.Sleep(1500);
            _homePage.MoveToProductButton(1);
            _homePage.ClickProductButton(1);
            Thread.Sleep(1500);
            _homePage.MoveToProductButton(2);
            _homePage.ClickProductButton(2);
            Thread.Sleep(1500);
            _homePage.ClickShoppingCart();
            Thread.Sleep(1500);

            //Act
            _homePage.DeleteShoppingCartProduct(0);
            Thread.Sleep(1500);

            //Assert
            Assert.AreEqual(2, _homePage.ModalCartProductsCount);
        }

        [Test]
        public void Delete_All_Products_From_Shopping_Cart_Should_Delete()
        {
            // Arrange
            _homePage.ClickCatalogMenu();
            _homePage.MoveToCatalogMenuItem1();
            _homePage.MoveToProductButton(0);
            _homePage.ClickProductButton(0);
            Thread.Sleep(1500);
            _homePage.MoveToProductButton(1);
            _homePage.ClickProductButton(1);
            Thread.Sleep(1500);
            _homePage.MoveToProductButton(2);
            _homePage.ClickProductButton(2);
            Thread.Sleep(1500);
            _homePage.ClickShoppingCart();
            Thread.Sleep(1500);

            //Act
            _homePage.DeleteShoppingCartProduct(0);
            Thread.Sleep(1500);
            _homePage.DeleteShoppingCartProduct(0);
            Thread.Sleep(1500);
            _homePage.DeleteShoppingCartProduct(0);
            Thread.Sleep(1500);

            //Assert
            Assert.AreEqual(0, _homePage.ModalCartProductsCount);
        }


        [TearDown]
        public void AfterEachTest()
        {
          
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            _driver.Close();
        }

        private string GetInvalidMessage()
        {
            var sb = new StringBuilder("Test");

            for (int i = 0; i < 67; i++)
            {
                sb.Append("Test");
            }

            return sb.ToString();
        }
    }
}
