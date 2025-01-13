using OpenQA.Selenium;

namespace WebPortalEverthing.E2E.Pages.admin
{
    public class GameSelectors
    {
        public static By RemoveGeneratedGame(string id)
        {
            return By.CssSelector($".tag-delete[data-id='{id}']"); 
        }
    }
}
