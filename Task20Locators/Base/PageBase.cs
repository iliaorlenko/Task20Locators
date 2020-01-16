using OpenQA.Selenium;
using System.Collections.Generic;

namespace Task20Locators.Base
{
    public class PageBase
    {
        public static IWebElement FindElement(By locator) => DriverContext.Driver.FindElement(locator);

        public static ICollection<IWebElement> FindElements(By locator) => DriverContext.Driver.FindElements(locator);
    }
}
