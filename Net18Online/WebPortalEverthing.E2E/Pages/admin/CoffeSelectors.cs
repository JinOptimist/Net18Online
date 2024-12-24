using OpenQA.Selenium;

namespace WebPortalEverthing.E2E.Pages.admin
{
    public class CoffeSelectors
    {
        public static By RemoveGeneratedCoffe(string id)
        {
            return By.CssSelector($".object-delete[data-id='{id}']"); 
        }
    }
}
