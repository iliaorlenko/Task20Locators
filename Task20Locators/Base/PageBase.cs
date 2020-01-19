using OpenQA.Selenium;
using System.Collections.Generic;

namespace Task20Locators.Base
{
    public class PageBase
    {
        public DriverContext driverContext = new DriverContext();

        public IWebElement FindElement(By locator) => driverContext.GetLocalDriver().FindElement(locator);

        public ICollection<IWebElement> FindElements(By locator) => driverContext.LocalDriver.FindElements(locator);

        public void GoToUrl()
        {
            driverContext.LocalDriver.Navigate().GoToUrl(Settings.tutByMainPage);
        }
    }
}
