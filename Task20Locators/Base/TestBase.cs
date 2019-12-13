using Helpers.Task20Locators.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Task20Locators.Base
{
    public class TestBase
    {
        public static string GetFromExcel(Dataset datasetName, Field field) => ExcelReader.GetFromExcel(datasetName, field);
        
        public static void GoToUrl(string url = "https://tut.by")
        {
            DriverContext.Driver.Navigate().GoToUrl(url);
        }

        public void GoToPreviousPage()
        {
            DriverContext.Driver.Navigate().Back();
        }

        [SetUp]
        public void TestSetUp()
        {
            // General actions before each test
        }

        [TearDown]
        public void TestTearDown()
        {
            // General actions after each test
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

        public static IWebElement FindElement(By locator) => DriverContext.Driver.FindElement(locator);

        public static ICollection<IWebElement> FindElements(By locator) => DriverContext.Driver.FindElements(locator);
    }

    // Enums just to not passing data parameters as strings
    public enum Dataset
    {
        FirstUser,
        SecondUser
    }

    public enum Field
    {
        Login,
        Password,
        Username
    }
}


