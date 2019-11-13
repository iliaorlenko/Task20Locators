using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Task20Locators.Base;

namespace Task20Locators
{
    [TestFixture]
    public class TutByTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentialsTest()
        {
            LandingPage.OpenLandingPage();
            LandingPage.OpenLoginForm();
            LandingPage.LoginWithValidCredentials();

            Assert.True(FindElement(LandingPage.UsernameLabel).Text == "Selenium Csharp");
        }
    }
}