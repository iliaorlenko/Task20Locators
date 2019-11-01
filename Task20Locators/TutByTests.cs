using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Task20Locators
{
    [TestFixture]
    public class TutByTests
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://tut.by";

            Locators locators = new Locators();

            driver.FindElement(locators.EnterLoginForm).Click();
            driver.FindElement(locators.Login).SendKeys("test");
            driver.FindElement(locators.Password).SendKeys("test");

            System.Threading.Thread.Sleep(10000);

        }
    }
}