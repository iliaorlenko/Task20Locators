using Allure.Commons;
using Helpers.Task20Locators.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task20Locators.Base
{
    public class TestBase : AllureReport
    {
        //private static string AllureConfigDir = Path.GetDirectoryName(typeof(AllureLifecycle).Assembly.Location);
        public static string GetFromExcel(Dataset datasetName, Field field) => ExcelReader.GetFromExcel(datasetName, field);

        public static void GoToUrl()
        {
            DriverContext.Driver.Navigate().GoToUrl(Settings.tutByMainPage);
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
            string brVersion = TestContext.Parameters.Get("brVersion", "79");
            string opSys = TestContext.Parameters.Get("os", "Windows");
            string osVersion = TestContext.Parameters.Get("osVersion", "10");

            Environment env = (Environment)Enum.Parse(typeof(Environment), environment, true);
            BrowserName browser = (BrowserName)Enum.Parse(typeof(BrowserName), browserName, true);
            OS os = (OS)Enum.Parse(typeof(OS), opSys, true);

            DriverContext.InitializeDriver(env, browser, brVersion, os, osVersion);
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


