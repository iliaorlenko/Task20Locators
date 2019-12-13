using Helpers.Task20Locators.Base;
using OpenQA.Selenium;
using Task20Locators.Base;
using SeleniumExtras.PageObjects;

namespace Pages.Task20Locators.TutBy
{
    public class LandingPage
    {
        // Page elements for tut.by landing page
        [FindsBy(How = How.ClassName, Using = "enter")]
        public IWebElement EnterLoginFormButton;

        [FindsBy(How = How.CssSelector, Using = "input[type='text']")]
        public IWebElement LoginInput;

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement PasswordInput;

        [FindsBy(How = How.XPath, Using = "//input[@class='button auth__enter']")]
        public IWebElement LoginButton;

        [FindsBy(How = How.XPath, Using = "//span[@class='uname']")]
        public IWebElement UsernameLabel;

        [FindsBy(How = How.XPath, Using = "//a[@class='button wide auth__reg']")]
        public IWebElement LogoutLink;

        // Method to go to tut.by landing page
        public LandingPage OpenLandingPage()
        {
            TestBase.GoToUrl();
            PageFactory.InitElements(DriverContext.Driver, this);
            return this;
        }

        // Method to open login form
        public LandingPage OpenLoginForm()
        {
            EnterLoginFormButton.Click();
            return this;
        }

        // Method to login either with credentials from one dataset of from different datasets
        public LandingPage SubmitLoginForm(Dataset loginDataset, Dataset? passwordDataset = null)
        {
            if (passwordDataset == null)
                passwordDataset = loginDataset;

            LoginInput.SendKeys(ExcelReader.GetFromExcel(loginDataset, Field.Login));
            PasswordInput.SendKeys(ExcelReader.GetFromExcel((Dataset)passwordDataset, Field.Password));
            LoginButton.Click();
            return this;
        }

        // Method to logout
        public LandingPage Logout()
        {
            UsernameLabel.Click();
            LogoutLink.Click();
            return this;
        }
    }
}