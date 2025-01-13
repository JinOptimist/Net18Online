using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalEverthing.E2E.LoadTest.Pages
{
    public static class RegistrationLoadUserViewSelectors
    {
        /* 
        Знак # указывает, что ищется элемент со значением атрибута id.
              Например: <input id="username" type="text">.
        Dev tools искать как id="username" */

        public static By UsernameElement = By.CssSelector(".login input");
        public static By EmailElement = By.CssSelector(".email input");
        public static By PasswordElement = By.CssSelector(".password input");
        public static By RoleElement = By.CssSelector(".role input");

        public static By RegisterButton = By.CssSelector("button[type='submit']");
    }
}
