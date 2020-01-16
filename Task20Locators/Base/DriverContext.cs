using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using Task20Locators.Base.Helpers;

namespace Task20Locators.Base
{
    public class DriverContext
    {
        // Driver declaration
        public static IWebDriver Driver { get; private set; }

        private static DriverOptions options = null;

        // Variables for Allure
        public static string Browser;
        public static string OS;

        public static void InitializeDriver()
        {
            // Switch hub to know what env is going to be used
            switch (Settings.hub)
            {
                // If local machine, then initialize one of the drivers
                case Hub.Local:
                    switch (Settings.browserName)
                    {
                        case BrowserName.Chrome:
                            Driver = new ChromeDriver();
                            break;

                        case BrowserName.Edge:
                            Driver = new EdgeDriver();
                            break;

                        case BrowserName.Firefox:
                            Driver = new FirefoxDriver();
                            break;

                        case BrowserName.IE:
                            Driver = new InternetExplorerDriver();
                            break;

                        case BrowserName.Safari:
                            Driver = new SafariDriver();
                            break;
                    }
                    break;

                // If remote environment, then add browser specific options first, then initialize remote driver
                case Hub.BrowserStack:
                case Hub.SauceLabs:
                case Hub.VM:
                    switch (Settings.browserName)
                    {
                        case BrowserName.Chrome:
                            options = new ChromeOptions();
                            break;

                        case BrowserName.Edge:
                            options = new EdgeOptions();
                            break;

                        case BrowserName.Firefox:
                            options = new FirefoxOptions();
                            break;

                        case BrowserName.IE:
                            options = new InternetExplorerOptions();
                            break;

                        case BrowserName.Safari:
                            options = new SafariOptions();
                            break;
                    }

                    // If remote is not virtual machine hub, then add specific options
                    if (Settings.hub != Hub.VM)
                    {
                        options.AddGlobalCapability("browser_version", Settings.browserVersion);
                        options.AddGlobalCapability("os", Settings.os.ToString());
                        options.AddGlobalCapability("os_version", Settings.osVersion);
                    }

                    // Add browser name option for any of remote environments
                    options.AddGlobalCapability("browser_name", Settings.browserName.ToString());

                    Driver = new RemoteWebDriver(Settings.hubUri, options.ToCapabilities());

                    break;
            }

            Driver.Manage().Window.Maximize();

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        // Method to enable implicit wait
        public static void TurnOnImplicitWait(int timeoutInSeconds = 3)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
        }

        // Method to disable implicit wait
        public static void TurnOffImplicitWait()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }
    }
}
