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
        //private IWebDriver Driver { get; set; }

        private static DriverOptions options = null;

        public static Environment? selectedEnvironment = null;

        // Variables for Allure
        public static string Browser;
        public static string OS;

        public RemoteWebDriver RemoteDriver
        {
            get
            {
                return GetRemoteDriver(Environment.VM, BrowserName.Chrome, "79", Base.OS.Linux, "18.04");
            }
        }

        public IWebDriver LocalDriver
        {
            get
            {
                return GetLocalDriver();
            }
            set
            {
                LocalDriver = value;
            }
        }

        public void SetBrowser()
        {

        }

        private void SetCapabilities(BrowserName browser, string brVersion, OS os, string osVersion)
        {
            options.AddGlobalCapability("browser_name", browser.ToString());
            options.AddGlobalCapability("browser_version", brVersion);
            options.AddGlobalCapability("os", os.ToString());
            options.AddGlobalCapability("os_version", osVersion);
        }

        public RemoteWebDriver GetRemoteDriver(Environment env, BrowserName browser, string brVersion = null, OS? os = null, string osVersion = null)
        {
            RemoteWebDriver remoteDriver = null;
            DriverOptions options = null;
            InternetExplorerOptions ieOptions = new InternetExplorerOptions();
            SafariOptions sfOptions = new SafariOptions();

            switch (env)
            {
                case Environment.Local:
                    throw new Exception("Cannot initiate remote driver for local environment");

                case Environment.VM:
                case Environment.BrowserStack:
                case Environment.SauceLabs:
                    if (env == Environment.VM && browser != BrowserName.Chrome && browser != BrowserName.Firefox)
                    {
                        throw new Exception("Cannot initiate driver for specified browser on VM environment");
                    }

                    options.AddGlobalCapability("browserVersion", brVersion);
                    options.AddGlobalCapability("os", os.ToString());
                    options.AddGlobalCapability("osVersion", osVersion);

                    ieOptions.AddGlobalCapability("browserVersion", brVersion);
                    ieOptions.AddGlobalCapability("os", os.ToString());
                    ieOptions.AddGlobalCapability("osVersion", osVersion);

                    sfOptions.AddGlobalCapability("browserVersion", brVersion);
                    sfOptions.AddGlobalCapability("os", os.ToString());
                    sfOptions.AddGlobalCapability("osVersion", osVersion);

                    switch (browser)
                    {
                        case BrowserName.Chrome:
                            remoteDriver = new RemoteWebDriver(options = new ChromeOptions());
                            break;

                        case BrowserName.Edge:
                            remoteDriver = new RemoteWebDriver(options = new EdgeOptions());
                            break;

                        case BrowserName.Firefox:
                            remoteDriver = new RemoteWebDriver(options = new FirefoxOptions());
                            break;

                        case BrowserName.IE:
                            remoteDriver = new RemoteWebDriver(ieOptions);
                            break;

                        case BrowserName.Safari:
                            remoteDriver = new RemoteWebDriver(sfOptions);
                            break;
                    }
                    break;
            }
            return remoteDriver;
        }

        public IWebDriver GetLocalDriver(BrowserName browser = BrowserName.Chrome)
        {
            IWebDriver driver = null;
            // If local machine, then initialize one of the drivers
            switch (browser)
            {
                case BrowserName.Chrome:
                    driver = new ChromeDriver();
                    break;

                case BrowserName.Edge:
                    driver = new EdgeDriver();
                    break;

                case BrowserName.Firefox:
                    driver = new FirefoxDriver();
                    break;

                case BrowserName.IE:
                    driver = new InternetExplorerDriver();
                    break;

                case BrowserName.Safari:
                    throw new Exception("No Safari browser installed on local machine.");
            }

            driver.Manage().Window.Maximize();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            return driver;
        }

        // Method to enable implicit wait
        public void TurnOnImplicitWait(int timeoutInSeconds = 3)
        {
            LocalDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
        }

        // Method to disable implicit wait
        public void TurnOffImplicitWait()
        {
            LocalDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }
    }
}
