using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal.Commands;
using OpenQA.Selenium;

//нужно установить пакет WebDriverManager
//Install-Package WebDriverManager

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebPortalEverthing.E2E.LoadTest.Pages;
using Enums.Users;

namespace WebPortalEverthing.E2E.LoadTest.AuthTests
{
    public class RegistrationTests
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
        public void RegistrationWithValidCredentials()
        {


            // Открытие страницы
            _webDriver.Navigate().GoToUrl(CommonConstants.registrationUrl);

            //нужная страница загружена
            Assert.That(_webDriver.Url, Is.EqualTo(CommonConstants.registrationUrl));

            // Явное ожидание
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));

            var usernameInput = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.UsernameElement));
            var emailInput = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.EmailElement));
            var passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.PasswordElement));
            var roleInput = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.RoleElement));
            var registerButton = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.RegisterButton));

            // Ввод данных в поля
            usernameInput.SendKeys("testuser");
            emailInput.SendKeys("testuser@example.com");
            passwordInput.SendKeys("securePassword123");
            roleInput.SendKeys(Role.User.ToString());

            // Клик по кнопке регистрации
            registerButton.Click();

            // редирект на страницу логина или наличие сообщения об успешной регистрации
            Assert.That(wait.Until(ExpectedConditions.UrlContains(CommonConstants.loginUrl)));

            // Проверка перенаправления на страницу авторизации
            Assert.That(wait.Until(ExpectedConditions.UrlContains("LoginLoadUserView")));
        }

        //    [TearDown]//after each test
        [OneTimeTearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }


    }
}
