using Helpers.Task20Locators.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Task20Locators.Base
{
    [SetUpFixture]
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

        [TearDown]
        public void TestTearDown()
        {
            // General actions after each test
        }
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


