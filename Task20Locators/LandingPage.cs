using OpenQA.Selenium;
using Task20Locators.Base;

namespace Task20Locators
{
    public static class LandingPage
    {
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
        
        public static void OpenLandingPage()
        {
            TestBase.GoToUrl();
        }

        public static void OpenLoginForm()
        {
            TestBase.FindElement(EnterLoginFormButton).Click();
        }

        public static void SubmitLoginForm(string login, string password)
        {
            TestBase.FindElement(LoginInput).SendKeys(login);
            TestBase.FindElement(PasswordInput).SendKeys(password);
            TestBase.FindElement(LoginButton).Click();
        }

        public static void LoginWithValidCredentials()
        {
            SubmitLoginForm("seleniumtraining@tut.by", "Task20OfTheTraining");
        }
    }
}