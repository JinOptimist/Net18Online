using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

//нужно установить пакет WebDriverManager
//Install-Package WebDriverManager

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WebPortalEverthing.E2E.LoadTest
{
    public class LoginTest
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            // Установка драйвера
            new DriverManager().SetUpDriver(new ChromeConfig());

            using (var webDriver = new ChromeDriver())
            {
                // Открытие страницы
                webDriver.Navigate().GoToUrl("https://localhost:7130/LoadAuth/LoginLoadUserView");

                // Явное ожидание
                var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

                // Поиск поля логина
                var loginDiv = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".login")));
                var loginInput = loginDiv.FindElement(By.TagName("input"));

                // Поиск поля пароля
                var passwordDiv = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".password")));
                var passwordInput = passwordDiv.FindElement(By.TagName("input"));

                // Ввод данных
                loginInput.SendKeys("qa3");
                passwordInput.SendKeys("qa3");

                // Поиск и нажатие на кнопку
                var signInButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[type='submit']")));
                signInButton.Click();

                // Проверка успешной авторизации (пример: URL содержит 'Home')
                Assert.That(wait.Until(ExpectedConditions.UrlContains("IndexLoadVolumeView")));
            }
        }
    }
}
