using Helpers.Task20Locators.Base;
using NUnit.Framework;
using Pages.Task20Locators.TutBy;
using System.Collections;
using Task20Locators.Base;

namespace Tests.Task20Locators.TutBy
{
    [TestFixture]
    public class TutByTests : TestBase
    {
        public static IEnumerable LoginTestsData
        {
            get { return JsonReader.GetLoginTestsData(); }
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ExcelReader.PopulateInCollection(ExcelReader.excelPath, ExcelReader.loginTestsTableName);
            LandingPage.OpenLandingPage();
        }

        [Test]
        public void LoginWithValidCredentialsTest()
        {
            LandingPage.OpenLoginForm();
            LandingPage.SubmitLoginForm(Dataset.FirstUser);

            Assert.True(LandingPage.WaitFindElement(LandingPage.UsernameLabel).Text == GetFromExcel(Dataset.FirstUser, Field.Username), message: "Actual username label is not matched to expected.");
        }

        [Test]
        public void LogoutTest()
        {
            LandingPage.OpenLoginForm();
            LandingPage.SubmitLoginForm(Dataset.SecondUser);
            LandingPage.Logout();

            Assert.True(LandingPage.WaitFindElement(LandingPage.EnterLoginFormButton).Displayed, message: "Enter login form button is not displayed.");
        }
    }
}