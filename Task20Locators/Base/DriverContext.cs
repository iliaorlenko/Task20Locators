using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Task20Locators.Base
{
    public class DriverContext
    {
        public static IWebDriver Driver { get; protected set; }
        //public static string Browser;
        //public static string OS = "Windows 10";
        //public static Uri VmHub = new Uri("http://localhost:4444/wd/hub");
        //public static Uri SauceLabHub = new Uri("http://ondemand.saucelabs.com:80/wd/hub");
        //public static Uri BrowserStackHub = new Uri($"http://{Settings.browserstackUser}:{Settings.browserstackKey}@hub-cloud.browserstack.com/wd/hub/");
        //public static DriverOptions Options;
        //public static DriverOptions BrowserOptions = null;
        //public static List<String, Object> browserstackOptions = new HashMap<String, Object>();


        public static void InitializeDriver()
        {
            switch (Settings.hub)
            {
                case Hub.Local:
                    switch (Settings.browserName)
                    {
                        case BrowserType.Chrome:
                            Driver = new ChromeDriver();
                            break;

                        case BrowserType.Firefox:
                            Driver = new FirefoxDriver();
                            break;

                        case BrowserType.Edge:
                            Driver = new EdgeDriver();
                            break;
                    }
                    break;

                case Hub.BrowserStack:
                case Hub.SauceLabs:

                    //RemoteSessionSettings a = new RemoteSessionSettings();


                    FirefoxOptions mainOpts = new FirefoxOptions()
                    {
                        UseLegacyImplementation = true
                    };

                    //switch (Settings.browserName)
                    //{
                    //    case BrowserType.Chrome:
                    //        mainOpts = new ChromeOptions();
                    //        break;
                    //    case BrowserType.Edge:
                    //        mainOpts = new EdgeOptions();
                    //        break;
                    //    case BrowserType.Firefox:
                    //        mainOpts = new FirefoxOptions();
                    //        break;
                    //}

                    //Dictionary<string, object> caps = new Dictionary<string, object>();
                    //caps.Add("os", "Windows");
                    //caps.Add("os_version", "8");

                    //RemoteSessionSettings caps = new RemoteSessionSettings();
                    //caps.AddMetadataSetting("os", Settings.os.ToString());
                    //caps.AddMetadataSetting("osVersion", Settings.osVersion.ToString());

                    DesiredCapabilities capability = new DesiredCapabilities();
                    capability.SetCapability("os", "Windows");
                    capability.SetCapability("os_version", "8.1");
                    capability.SetCapability("browser", Settings.browserName.ToString());
                    capability.SetCapability("browser_version", Settings.browserVersion.ToString());
                    capability.SetCapability("browserstack.local", "false");
                    capability.SetCapability("browserstack.selenium_version", "3.5.2");
                    capability.SetCapability("browserstack.user", "ilyaorlenko1");
                    capability.SetCapability("browserstack.key", "b7oqy11UFAU1HrPfUr1v");


                    //mainOpts.AddAdditionalCapability("browser_name", Settings.browserName);
                    //mainOpts.AddAdditionalCapability("browser_version", Settings.browserVersion);
                    //mainOpts.AddAdditionalCapability("bstack:options", caps);
                    //mainOpts.AddAdditionalCapability("os", "Windows");
                    //mainOpts.AddAdditionalCapability("os_version", "8");

                    Driver = new RemoteWebDriver(Settings.hubUri, capability);
                    break;

                case Hub.VM:
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
