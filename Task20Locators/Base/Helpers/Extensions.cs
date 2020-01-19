using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Task20Locators.Base.Helpers
{
    public static class Extensions
    {
        // Extension method for adding capabilities for any kind of browser-specific options classes
        public static void AddGlobalCapability(this DriverOptions driverOptions, string capabilityName, string capabilityValue)
        {
            switch (driverOptions)
            {
                case ChromeOptions chromeOptions:
                    chromeOptions.AddAdditionalCapability(capabilityName, capabilityValue, true);
                    chromeOptions.AddArguments("--headless", "--no-sandbox", "--disable-dev-shm-usage");
                    break;
                case FirefoxOptions firefoxOptions:
                    firefoxOptions.AddAdditionalCapability(capabilityName, capabilityValue, true);
                    break;
                case InternetExplorerOptions ieOptions:
                    ieOptions.AddAdditionalCapability(capabilityName, capabilityValue, true);
                    break;
                default:
                    driverOptions.AddAdditionalCapability(capabilityName, capabilityValue);
                    break;
            }
        }
    }
}
