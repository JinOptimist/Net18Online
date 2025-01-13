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
using WebPortalEverthing.E2E.LoadTest.DataBase;
using Serilog;
using System.IO;

namespace WebPortalEverthing.E2E.LoadTest.AuthTests
{
    public class RegistrationTests
    {

        private IWebDriver _webDriver;

        private static readonly IEnumerable<string> ROLS = Enum.GetValues(typeof(Role)).Cast<Role>().Select(r => r.ToString());

        private static readonly List<string> usernames = new();

        private DbHelper dbHelper = new();



        //[SetUp] Before each test
        [OneTimeSetUp]
        public void Setup()
        {
            // Установка драйвера
            new DriverManager().SetUpDriver(new ChromeConfig());

            _webDriver = new ChromeDriver();

            // Инициализация Serilog
            //dotnet add package Serilog.Sinks.Console
            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();
            string logMessage = "Executing RegistrationTest: " + Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "WebPortalEverthing.E2E.LoadTest", "AuthTests", "RegistrationTests.cs");
            Log.Information(logMessage);
        }

        [Test]
        public void RegistrationWithValidCredentials()
        {
            // Открытие страницы
            _webDriver.Navigate().GoToUrl(CommonConstants.registrationUrl);

            //нужная страница загружена
            Assert.That(_webDriver.Url, Is.EqualTo(CommonConstants.registrationUrl));

            // Явное ожидание
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));

            var usernameInput = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.UsernameElement));
            var emailInput = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.EmailElement));
            var passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.PasswordElement));
            var roleInput = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.RoleElement));
            var registerButton = wait.Until(ExpectedConditions.ElementIsVisible(RegistrationLoadUserViewSelectors.RegisterButton));

            // Ввод данных в поля
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");

            string username = $"testuser_{timestamp}";
            usernames.Add(username);

            usernameInput.SendKeys(username);
            emailInput.SendKeys($"testuser_{timestamp}@example.com");
            passwordInput.SendKeys($"Password_{timestamp}");

            roleInput.SendKeys(Role.User.ToString());

            // Клик по кнопке регистрации
            registerButton.Click();

            // редирект на страницу логина или наличие сообщения об успешной регистрации
            Assert.That(wait.Until(ExpectedConditions.UrlContains(CommonConstants.loginUrl)));

            // Проверка перенаправления на страницу авторизации
            Assert.That(wait.Until(ExpectedConditions.UrlContains("LoginLoadUserView")));
        }

        [Test]
        [TestCaseSource(nameof(ROLS))]//берёт все роли из Enum и подставляет в TestCases

        public void RegistrationRoleCasesWithValidCredentials(string role)
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
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");

            Console.WriteLine($"Current URL: {_webDriver.Url}");


            usernameInput.SendKeys($"testuser_{timestamp}");
            emailInput.SendKeys($"testuser_{timestamp}@example.com");
            passwordInput.SendKeys($"Password_{timestamp}");

            roleInput.SendKeys(role);

            // Клик по кнопке регистрации
            registerButton.Click();

        }

        //    [TearDown]//after each test
        [OneTimeTearDown]
        public void TearDown()
        {
            // Ожидание, пока текущий URL будет содержать ожидаемую строку
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            wait.Until(driver =>
            {
                string currentUrl = driver.Url; // Получаем текущий URL
                Console.WriteLine($"Current URL: {currentUrl}"); // Логируем текущий URL для диагностики
                return currentUrl.Contains(CommonConstants.loginUrl); // Проверяем, содержит ли он ожидаемую строку
            });

            // Убедитесь, что редирект произошел
            Assert.That(_webDriver.Url, Does.Contain("LoginLoadUserView"), "Редирект не произошел.");
            _webDriver.Close();

            foreach (var username in usernames)
            {
                int deletedUserCount = dbHelper.DeleteUserByUsername(username);
                Log.Information($"Deleted {deletedUserCount} username {username}");
            }

            Log.CloseAndFlush();
        }


    }
}
