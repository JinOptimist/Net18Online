using OpenQA.Selenium;

namespace WebPortalEverthing.E2E.Pages.admin
{
    public class GameSelectors
    {
        /// <summary>
        /// Game selectors section
        /// </summary>
        public static readonly By OpenInfoButton = By.ClassName("toggle-tags");
        public static readonly By GameDeleteButton = By.ClassName("tag-delete");
        public static readonly By GameObjects = By.ClassName("game-block");
        public static readonly By GameAddButton = By.CssSelector("button[type=submit]");


        public static readonly By CreateGameName = By.CssSelector("#Name");
        public static readonly By CreateGameUrl = By.CssSelector("#Url");
        public static readonly By CreateGameCost = By.CssSelector("#Cost");

        public static By RemoveGeneratedGame(string id)
        {
            return By.CssSelector($".tag-delete[data-id='{id}']"); 
        }
    }
}
