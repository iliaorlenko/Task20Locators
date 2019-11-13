using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace Task20Locators.Base
{
    public class DriverContext
    {
        public static IWebDriver Driver { get; protected set; }

        public static void InitializeDriver(BrowserType browserType = BrowserType.Chrome)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    Driver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    Driver = new FirefoxDriver();
                    break;
            }

            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
    }

    public enum BrowserType
    {
        Chrome,
        Firefox
    }
}
