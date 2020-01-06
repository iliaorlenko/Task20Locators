﻿using Allure.NUnit.Attributes;
using Helpers.Task20Locators.Base;
using NUnit.Framework;
using Pages.Task20Locators.TutBy;
using Task20Locators.Base;

namespace Tests.Task20Locators.TutBy
{
    [TestFixture]
    public class TutByTests : TestBase
    {
        // Landing page instance initialization
        LandingPage Landing; 

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Landing = new LandingPage();
            Landing.OpenLandingPage();
            ExcelReader.PopulateInCollection(ExcelReader.excelPath, ExcelReader.loginTestsTableName);
        }

        [Test]
        [
            AllureSubSuite("Login tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink(""),
            AllureTest("Log in with valid credentials"),
            AllureOwner("Ilya Orlenko")
        ]
        public void LoginWithValidCredentialsTest()
        {
            Landing.OpenLoginForm()
                .SubmitLoginForm(Dataset.FirstUser);

            Assert.True(Landing.UsernameLabel.Text == GetFromExcel(Dataset.FirstUser, Field.Username), message: "Actual username label is not matched to expected.");
        }

        [Test]
        [
            AllureSubSuite("Login tests"), 
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink(""),
            AllureTest("Log out of the account"),
            AllureOwner("Ilya Orlenko")
        ]
        public void LogoutTest()
        {
            Landing.OpenLoginForm()
                .SubmitLoginForm(Dataset.SecondUser)
                .Logout();
            
            Assert.True(Landing.EnterLoginFormButton.Displayed, message: "Enter login form button is not displayed.");
        }
    }
}