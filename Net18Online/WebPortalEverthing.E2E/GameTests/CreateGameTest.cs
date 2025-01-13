using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortalEverthing.E2E.Pages;
using WebPortalEverthing.E2E.Pages.admin;

namespace WebPortalEverthing.E2E.GameTests
{
    public class CreateGameTest
    {
        public IWebDriver _webDriver;
        public WebDriverWait _wait;

        [OneTimeSetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(100));
            _webDriver.Url = CommonConstants.BASE_URL;
        }
        [Test]
        [TestCase("SmileTest1", "https://kolyda.ru/d/kofe_amerikano.jpg", 20)]
        public void CreateGame(string gamename, string url, int cost)
        {
            _webDriver.LoginAsAdmin();

            _webDriver.Url = CommonConstants.GAME_CREATE_URL;

            var gameName = _webDriver.FindElement(LayoutSelectors.CreateGameName);
            gameName.SendKeys(gamename);

            var gameUrl = _webDriver.FindElement(LayoutSelectors.CreateGameUrl);
            gameUrl.SendKeys(url);


            var gameCost = _webDriver.FindElement(LayoutSelectors.CreateGameCost);
            gameCost.SendKeys(Convert.ToString(cost));

            var createButton = _webDriver.FindElement(LayoutSelectors.GameAddButton);
            createButton.Click();

            var gameObjects = _wait.Until(driver => driver.FindElements(LayoutSelectors.GameObjects));


            var lastGameObject = _wait.Until(driver => gameObjects[gameObjects.Count - 2]);

            var infoBar = _wait.Until(driver => lastGameObject.FindElement(LayoutSelectors.OpenInfoButton));
            infoBar.Click();

            var deleteGameLink = _wait.Until(x => lastGameObject.FindElement(LayoutSelectors.GameDeleteButton));

            var gameId = deleteGameLink.GetAttribute("data-id");

            var deleteButton = _wait.Until(driver => driver.FindElement(GameSelectors.RemoveGeneratedGame(gameId)));
            deleteButton.Click();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}
