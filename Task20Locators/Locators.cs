using OpenQA.Selenium;

namespace Task20Locators
{
    public class Locators
    {
        public By EnterLoginForm = By.ClassName("enter");
        public By Login = By.CssSelector("input[type='text']");
        public By Password = By.Name("password");
        public By byId = By.Id("id");
        public By byLinkText = By.LinkText("id");
        public By byPartialTextLink = By.PartialLinkText("partialLink");
    }
}