using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebPortalEverthing.E2E.Pages;
using WebPortalEverthing.E2E.Pages.admin;

namespace WebPortalEverthing.E2E.CoffeShopTests
{
    public class CreateCoffeTests
    {
        public IWebDriver _webDriver;

        [OneTimeSetUp] 
        public void SetUp()
        {
            _webDriver = new ChromeDriver();
            _webDriver.LoginAsAdmin();
        }

        [Test]
        [TestCase("TestCoffe1", "https://kolyda.ru/d/kofe_amerikano.jpg", "2")]
        [TestCase("TestCoffe2", "https://kolyda.ru/d/kofe_amerikano.jpg", "4")]
        [TestCase("TestCoffe3", "https://kolyda.ru/d/kofe_amerikano.jpg", "16")]
        public void CreateCoffeObject(string name, string url, string cost)
        {
            _webDriver.Url = CommonConstants.COFFE_ADMIN_URL;

            var createLink = _webDriver.FindElement(LayoutSelectors.CreateCoffeLink);
            createLink.Click();

            var coffeName = _webDriver.FindElement(LayoutSelectors.CreateCoffeName);
            coffeName.SendKeys(name);

            var coffeUrl = _webDriver.FindElement(LayoutSelectors.CreateCoffeUrl);
            coffeUrl.SendKeys(url);

            var coffeCost = _webDriver.FindElement(LayoutSelectors.CreateCoffeCost);
            coffeCost.SendKeys(cost);
                
            var submitButton = _webDriver.FindElement(LayoutSelectors.CoffeCreateSubmitButton);
            submitButton.Click();

            var coffeObjects = _webDriver.FindElements(LayoutSelectors.CoffeObjects);
            var lastCoffeObject = coffeObjects[coffeObjects.Count - 1];

            var deleteCoffeLink = lastCoffeObject.FindElement(LayoutSelectors.RemoveCoffeObject);

            var coffeId = deleteCoffeLink.GetAttribute("data-id");

            var deleteButton = _webDriver.FindElement(CoffeSelectors.RemoveGeneratedCoffe(coffeId));
            deleteButton.Click();

            Assert.Pass();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _webDriver.Logout();
            _webDriver.Close();
        }
    }
}
