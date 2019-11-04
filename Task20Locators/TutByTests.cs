using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

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

            ICollection<IWebElement> aElements = driver.FindElements(locators.menuByTagName);
            int totalAElementsNumber = aElements.Count;
            driver.FindElement(locators.tvProgramByPartialTextLink).Click();
            driver.Navigate().Back();
            driver.FindElement(locators.geotargetById).Click();
            driver.FindElement(locators.townByLinkText).Click();
            driver.FindElement(locators.searchByXpath).SendKeys(totalAElementsNumber.ToString());
            driver.FindElement(locators.enterLoginForm).Click();
            driver.FindElement(locators.login).SendKeys("seleniumtraining@tut.by");
            driver.FindElement(locators.password).SendKeys("Task20OfTheTraining");
            driver.FindElement(locators.btnLogin).Click();
            Assert.True(driver.FindElement(locators.username).Text == "Selenium Csharp");

            driver.Close();
            driver.Quit();
        }
    }
}