using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using OpenQA.Selenium.Chrome;


namespace WebPortalEverthing.E2E.LoadTest.Pages
{
    public static class LoginLoadUserViewSelectors
    {
        public static By LoginElement = By.CssSelector(".login input");
        public static By PasswordElement = By.CssSelector(".password input");
        public static By SignInButton = By.CssSelector("button[type='submit']");
    }
}
