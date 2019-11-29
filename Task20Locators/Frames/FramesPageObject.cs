using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Task20Locators.Base;

namespace Task20Locators.Frames
{
    public static class FramesPageObject
    {
        public static By ContentInput = By.XPath("//body[@id='tinymce']");
        public static By BoldFontButton = By.XPath("//i[@class='mce-ico mce-i-bold']");

        public static void OpenFramesPage() => TestBase.GoToUrl("https://the-internet.herokuapp.com/iframe");

        public static void SwitchToFrame() => DriverContext.Driver.SwitchTo().Frame(0);

        public static void SwithcToDefaultContent() => DriverContext.Driver.SwitchTo().DefaultContent();
        
        public static void ClearContentInput() => DriverContext.Driver.FindElement(ContentInput).Clear();

        public static void PopulateContentInput(string text) => DriverContext.Driver.FindElement(ContentInput).SendKeys(text);

        public static void ClickBoldFontButton() => DriverContext.Driver.FindElement(BoldFontButton).Click();

        public static string GetTextFromContentInput() => DriverContext.Driver.FindElement(ContentInput).Text;

        public static void SelectLastEnteredWord()
        {
            Actions action = new Actions(DriverContext.Driver);

            action.KeyDown(Keys.LeftShift).Build().Perform();
            action.KeyDown(Keys.LeftControl).Build().Perform();
            action.SendKeys(Keys.ArrowLeft).Build().Perform();
        }
    }
}
