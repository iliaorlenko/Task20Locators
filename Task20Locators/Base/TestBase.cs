using Allure.Commons;
using Helpers.Task20Locators.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task20Locators.Base
{
    [SetUpFixture]
    public class TestBase : AllureReport
    {
        private static string AllureConfigDir = Path.GetDirectoryName(typeof(AllureLifecycle).Assembly.Location);
        public static string GetFromExcel(Dataset datasetName, Field field) => ExcelReader.GetFromExcel(datasetName, field);
        
        public static void GoToUrl(string url = "https://tut.by")
        {
            DriverContext.Driver.Navigate().GoToUrl(url);
        }

        public void GoToPreviousPage()
        {
            DriverContext.Driver.Navigate().Back();
        }

        [OneTimeSetUp]
        public static void GlobalSetup()
        {
            DriverContext.InitializeDriver();
        }

        [OneTimeTearDown]
        public static void GlobalTeardown()
        {
            DriverContext.Driver.Close();
        }

        [SetUp]
        public void TestSetUp()
        {
            // General actions before each test
            if(TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                
            }
        }

        public static ICollection<IWebElement> FindElements(By locator) => DriverContext.Driver.FindElements(locator);

        public static void TakeScreenshot()
        {
            string dirName = Settings.baseDir + @"\TutBy\Screenshots\";
            string fileName = "Screenshot " + DateTime.Now.ToString("MM-dd-yyy hh_mm_ss tt") + ".png";

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            Screenshot screenshot = ((ITakesScreenshot)DriverContext.Driver).GetScreenshot();

            screenshot.SaveAsFile(dirName + fileName);
        }
    }
}


