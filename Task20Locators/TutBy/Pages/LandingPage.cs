using Helpers.Task20Locators.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using Task20Locators.Base;

namespace Pages.Task20Locators.TutBy
{
    public class LandingPage : PageBase
    {
        // Locators for tut.by landing page
        public readonly static By EnterLoginFormButtonLocator = By.ClassName("enter");
        public readonly static By LoginInputLocator = By.CssSelector("input[type='text']");
        public readonly static By PasswordInputLocator = By.Name("password");
        public readonly static By LoginButtonLocator = By.XPath("//input[@class='button auth__enter']");
        public readonly static By UsernameLabelLocator = By.XPath("//span[@class='uname']");
        public readonly static By LogoutLinkLocator = By.XPath("//a[@class='button wide auth__reg']");

        // Prepared IWebElements for assertions
        public IWebElement UsernameLabel => WaitFindElement(EnterLoginFormButtonLocator);
        public IWebElement EnterLoginFormButton => WaitFindElement(EnterLoginFormButtonLocator);

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

        // Method to go to tut.by landing page
        public LandingPage OpenLandingPage()
        {
            TestBase.GoToUrl();

            return new LandingPage();
        }

        // Method to open login form
        public LandingPage OpenLoginForm()
        {
            WaitFindElement(EnterLoginFormButtonLocator).Click();

            return this;
        }
        // Method to login either with credentials from one dataset of from different datasets
        public LandingPage SubmitLoginForm(Dataset loginDataset, Dataset? passwordDataset = null)
        {
            if (passwordDataset == null)
            {
                passwordDataset = loginDataset;
            }                

            WaitFindElement(LoginInputLocator).SendKeys(ExcelReader.GetFromExcel(loginDataset, Field.Login));
            WaitFindElement(PasswordInputLocator).SendKeys(ExcelReader.GetFromExcel((Dataset)passwordDataset, Field.Password));
            WaitFindElement(LoginButtonLocator).Click();

            return this;
        }

        // Method to logout
        public LandingPage Logout()
        {
            WaitFindElement(UsernameLabelLocator).Click();
            WaitFindElement(LogoutLinkLocator).Click();

            return this;
        }
    }
}