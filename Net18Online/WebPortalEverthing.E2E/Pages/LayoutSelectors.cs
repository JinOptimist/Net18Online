using OpenQA.Selenium;

namespace WebPortalEverthing.E2E.Pages
{
    public class LayoutSelectors
    {
        public static readonly By LoginLink = By.ClassName("login-link");
        public static readonly By LogoutLink = By.ClassName("logout-link");
        public static readonly By RegistrationLink = By.ClassName("registration-link");

        public static readonly By UserName = By.CssSelector("#UserName");
        public static readonly By Password = By.CssSelector("#Password");

        public static readonly By SubmitButton = By.CssSelector("button[type=submit]");

        /// <summary>
        /// Coffe selectors section
        /// </summary>
        public static readonly By CreateCoffeLink = By.ClassName("create-link");
        public static readonly By CoffeObjects = By.ClassName("coffe-object");
        public static readonly By RemoveCoffeObject = By.ClassName("object-delete");

        public static readonly By CreateCoffeName = By.CssSelector("#Coffe");
        public static readonly By CreateCoffeUrl = By.CssSelector("#Url");
        public static readonly By CreateCoffeCost = By.CssSelector("#Cost");

        public static readonly By CoffeCreateSubmitButton = By.CssSelector("#submit-link");

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

        
    }
}
