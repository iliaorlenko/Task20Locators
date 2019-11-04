using OpenQA.Selenium;

namespace Task20Locators
{
    public class Locators
    {
        public By enterLoginForm = By.ClassName("enter");
        public By login = By.CssSelector("input[type='text']");
        public By password = By.Name("password");
        public By geotargetById = By.Id("geotarget_top_selector");
        public By townByLinkText = By.LinkText("Белоозерск");
        public By tvProgramByPartialTextLink = By.PartialLinkText("-прогр");
        public By menuByTagName = By.TagName("a");
        public By searchByXpath = By.XPath("//input[@id='search_from_str']");
        public By btnLogin = By.XPath("//input[@class='button auth__enter']");
        public By username = By.XPath("//span[@class='uname']");
    }
}