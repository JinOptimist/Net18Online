using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

//нужно установить пакет WebDriverManager
//Install-Package WebDriverManager

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebPortalEverthing.E2E.LoadTest.Pages;

namespace WebPortalEverthing.E2E.LoadTest
{
    public class LoginTest
    {
        private IWebDriver _webDriver;

        //[SetUp] Before each test
        [OneTimeSetUp]
        public void Setup()
        {
            // Установка драйвера
            new DriverManager().SetUpDriver(new ChromeConfig());

            _webDriver = new ChromeDriver();
        }

        [Test]
        public void LoginWithValidCredentials()
        {


            // Открытие страницы
            _webDriver.Navigate().GoToUrl(CommonConstants.loginUrl);

            //нужная страница загружена
            Assert.That(_webDriver.Url, Is.EqualTo(CommonConstants.loginUrl));

            // Явное ожидание
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));

            // Поиск и взаимодействие с элементами
            var loginInput = wait.Until(ExpectedConditions.ElementIsVisible(LoginLoadUserViewSelectors.LoginElement));
            var passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(LoginLoadUserViewSelectors.PasswordElement));
            var signInButton = wait.Until(ExpectedConditions.ElementIsVisible(LoginLoadUserViewSelectors.SignInButton));

            // Ввод данных
            loginInput.SendKeys("admin");
            passwordInput.SendKeys("admin");

            // Клик по кнопке входа
            signInButton.Click();

            // Проверка успешной авторизации
            Assert.That(wait.Until(ExpectedConditions.UrlContains("IndexLoadVolumeView")));
        }

        //    [TearDown]//after each test
        [OneTimeTearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}
