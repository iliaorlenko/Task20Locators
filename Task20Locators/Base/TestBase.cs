using Allure.Commons;
using Helpers.Task20Locators.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task20Locators.Base
{
    [TestFixture]
    public class TestBase : AllureReport
    {
        //private static string AllureConfigDir = Path.GetDirectoryName(typeof(AllureLifecycle).Assembly.Location);
        public static string GetFromExcel(Dataset datasetName, Field field) => ExcelReader.GetFromExcel(datasetName, field);

        DriverContext driverContext = new DriverContext();

        IWebDriver driver => driverContext.LocalDriver;

        public static IEnumerable<BrowserName> RunBrowser()
        {
            List<BrowserName> browsers = new List<BrowserName>();
            switch (DriverContext.selectedEnvironment)
            {
                case Environment.Local:
                    browsers.Add(BrowserName.Chrome);
                    browsers.Add(BrowserName.Edge);
                    browsers.Add(BrowserName.Firefox);
                    browsers.Add(BrowserName.IE);
                    break;
                case Environment.VM:
                    browsers.Add(BrowserName.Chrome);
                    browsers.Add(BrowserName.Firefox);
                    break;
                default:
                    foreach(BrowserName browser in Enum.GetValues(typeof(BrowserName)))
                    {
                        browsers.Add(browser);
                    }
                    break;
            }

            foreach (BrowserName browser in browsers)
            {
                yield return browser;
            }
        }

        public void GoToPreviousPage()
        {
            driverContext.LocalDriver.Navigate().Back();
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {

        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            driverContext.LocalDriver.Close();
            driverContext.LocalDriver.Quit();
        }

        [SetUp]
        public void TestSetUp()
        {
            string environment = TestContext.Parameters.Get("environment", "Local");
            string browserName = TestContext.Parameters.Get("browser", "Chrome");
            Environment env = (Environment)Enum.Parse(typeof(Environment), environment, true);
            BrowserName browser = (BrowserName)Enum.Parse(typeof(BrowserName), browserName, true);

            if(env == Environment.Local)
            {
                driverContext.LocalDriver = driverContext.GetLocalDriver(browser);
                DriverContext.selectedEnvironment = Environment.Local;
            }
            else
            {
                if(env == Environment.VM)
                {
                    driverContext.LocalDriver = driverContext.GetRemoteDriver(Environment.VM, browser);
                    DriverContext.selectedEnvironment = Environment.VM;
                }
                else
                {
                    string brVersion = TestContext.Parameters.Get("browserVersion", "79");
                    string opSys = TestContext.Parameters.Get("os", "Windows");
                    string osVersion = TestContext.Parameters.Get("osVersion", "10");
                    OS os = (OS)Enum.Parse(typeof(OS), opSys, true);

                    driverContext.LocalDriver = driverContext.GetRemoteDriver(env, browser, brVersion, os, osVersion);
                }
            }
        }

        [TearDown]
        public void TestTearDown()
        {
            //General actions after each test
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot();
            }
        }

        public ICollection<IWebElement> FindElements(By locator) => driverContext.LocalDriver.FindElements(locator);

        // Method for taking screenshots
        public void TakeScreenshot()
        {
            string dirName = Settings.baseDir + @"\TutBy\Screenshots\";
            string fileName = "Screenshot " + DateTime.Now.ToString("MM-dd-yyy hh_mm_ss tt") + ".png";

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            Screenshot screenshot = ((ITakesScreenshot)driverContext.LocalDriver).GetScreenshot();

            screenshot.SaveAsFile(dirName + fileName, ScreenshotImageFormat.Png);
        }
    }
}


