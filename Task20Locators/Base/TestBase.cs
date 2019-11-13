using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Task20Locators.Base
{
    public class TestBase
    {
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

        public static IWebElement FindElement(By locator)
        {
            return DriverContext.Driver.FindElement(locator);
        }

        public static ICollection<IWebElement> FindElements(By locator)
        {
            return DriverContext.Driver.FindElements(locator);
        }
    }
}


