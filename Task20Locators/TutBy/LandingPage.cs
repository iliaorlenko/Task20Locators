using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using Task20Locators.Base;

namespace Task20Locators.TutBy
{
    public static class LandingPage
    {
        // Locators for tut.by
        public static By EnterLoginFormButton = By.ClassName("enter");
        public static By LoginInput = By.CssSelector("input[type='text']");
        public static By PasswordInput = By.Name("password");
        public static By GeotargetLink = By.Id("geotarget_top_selector");
        public static By TownLink = By.LinkText("Белоозерск");
        public static By TvProgramLink = By.PartialLinkText("-прогр");
        public static By MenuButton = By.TagName("a");
        public static By SearchInput = By.XPath("//input[@id='search_from_str']");
        public static By LoginButton = By.XPath("//input[@class='button auth__enter']");
        public static By UsernameLabel = By.XPath("//span[@class='uname']");
        public static By ErrorMessage = By.XPath("//div[@class='b-auth__error']");
        public static By LogoutLink = By.XPath("//a[@class='button wide auth__reg']");
        
        // Method to find element along with explicit wait
        public static IWebElement WaitFindElement(By locator)
        {
            // Set null as a default value for element expected to return
            IWebElement expectedElement = null;

            // Disable implicit wait in order to don't mix with explicit wait 
            DriverContext.TurnOffImplicitWait();

            // Initialize instance of explicit wait 
            WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(10));

            // Set custom polling interval
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);

            // Set condition for explicit wait
            wait.Until(condition =>
            {
                try
                {
                    expectedElement = DriverContext.Driver.FindElement(locator);
                    return expectedElement.Displayed;
                }
                catch (StaleElementReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
                catch (NoSuchElementException ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            });

            // Turn implicit wait back on
            DriverContext.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Return expected element
            return expectedElement;
        }

        // Method to login either with credentials from one dataset of from different datasets
        public static void SubmitLoginForm(Dataset loginDataset, Dataset? passwordDataset = null)
        {
            if (passwordDataset == null)
                passwordDataset = loginDataset;

            TestBase.FindElement(LoginInput).SendKeys(ExcelReader.GetFromExcel(loginDataset, Field.Login));
            TestBase.FindElement(PasswordInput).SendKeys(ExcelReader.GetFromExcel((Dataset)passwordDataset, Field.Password));
            TestBase.FindElement(LoginButton).Click();
        }

        // Method to go to tut.by landing page
        public static void OpenLandingPage() => TestBase.GoToUrl();

        // Method to open login form
        public static void OpenLoginForm() => TestBase.FindElement(EnterLoginFormButton).Click();

        // Method to logout
        public static void Logout()
        {
            TestBase.FindElement(UsernameLabel).Click();
            TestBase.FindElement(LogoutLink).Click();
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