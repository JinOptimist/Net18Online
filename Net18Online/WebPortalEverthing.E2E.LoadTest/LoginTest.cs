using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//нужно установить пакет WebDriverManager
//Install-Package WebDriverManager

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WebPortalEverthing.E2E.LoadTest
{
    public class LoginTest
    {
        [Test]
        public void Login()
        {
            // Автоматически устанавливает правильную версию ChromeDriver
            new DriverManager().SetUpDriver(new ChromeConfig());

            //// <summary>
            ///Можно вручную скачать драйвер и прописать к нему путь
            /// var chromeDriverPath = @"C:\Path\To\ChromeDriver"; // Укажите путь к chromedriver.exe
            ///var driver = new ChromeDriver(chromeDriverPath);
            /// </summary>

            using (var webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl(

                "https://localhost:7130/LoadTesting/ContenMetricsListView");
            }

            Assert.Pass();
        }
    }
}
