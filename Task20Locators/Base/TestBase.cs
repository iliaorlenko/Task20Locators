using Allure.Commons;
using Helpers.Task20Locators.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.IO;
using Task20Locators.Base.Helpers;

namespace Task20Locators.Base
{
    //[SetUpFixture]
    public class TestBase : AllureReport
    {
        //private static string AllureConfigDir = Path.GetDirectoryName(typeof(AllureLifecycle).Assembly.Location);
        public static string GetFromExcel(Dataset datasetName, Field field) => ExcelReader.GetFromExcel(datasetName, field);

        public static void GoToUrl()
        {
            DriverContext.Driver.Navigate().GoToUrl(Settings.tutByUrl);
        }

        public void GoToPreviousPage()
        {
            DriverContext.Driver.Navigate().Back();
        }

        [OneTimeSetUp]
        public static void GlobalSetup()
        {
            string environment = TestContext.Parameters.Get("env", "Local");
            string browserName = TestContext.Parameters.Get("browser", "Chrome");
            string browserVersion = TestContext.Parameters.Get("brVersion", "79");
            string os = TestContext.Parameters.Get("os", "Windows");
            string osVersion = TestContext.Parameters.Get("osVersion", "10");

            Environment env = (Environment)Enum.Parse(typeof(Environment), environment, true);
            BrowserName browser = (BrowserName)Enum.Parse(typeof(BrowserName), browserName, true);
            OS opSys = (OS)Enum.Parse(typeof(OS), os, true);

            DriverContext.InitializeDriver(Environment.BrowserStack, browser);

            //DriverContext.InitializeDriver(Environment.BrowserStack, BrowserName.Chrome);
        }

        [OneTimeTearDown]
        public static void GlobalTeardown()
        {
            DriverContext.Driver.Close();
            DriverContext.Driver.Quit();
        }

        [SetUp]
        public void TestSetUp()
        {
            //string environment = TestContext.Parameters.Get("env", "Local");
            //string browserName = TestContext.Parameters.Get("browser", "Chrome");
            //string browserVersion = TestContext.Parameters.Get("brVersion", "79");
            //string os = TestContext.Parameters.Get("os", "Windows");
            //string osVersion = TestContext.Parameters.Get("osVersion", "10");

            //Environment env = (Environment)Enum.Parse(typeof(Environment), environment, true);
            //BrowserName browser = (BrowserName)Enum.Parse(typeof(BrowserName), browserName, true);
            //OS opSys = (OS)Enum.Parse(typeof(OS), os, true);

            //DriverContext.InitializeDriver(env, browser);
        }

        [TearDown]
        public void TestTearDown()
        {
            // General actions after each test
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot();
            }
        }

        public static ICollection<IWebElement> FindElements(By locator) => DriverContext.Driver.FindElements(locator);

        // Method for taking screenshots
        public static void TakeScreenshot()
        {
            string dirName = Settings.baseDir + @"\TutBy\Screenshots\";
            string fileName = "Screenshot " + DateTime.Now.ToString("MM-dd-yyy hh_mm_ss tt") + ".png";

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            Screenshot screenshot = ((ITakesScreenshot)DriverContext.Driver).GetScreenshot();

            screenshot.SaveAsFile(dirName + fileName, ScreenshotImageFormat.Png);
        }
    }
}


