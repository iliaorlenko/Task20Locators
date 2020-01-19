using Allure.NUnit.Attributes;
using Helpers.Task20Locators.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using Pages.Task20Locators.TutBy;
using Task20Locators.Base;

namespace Tests.Task20Locators.TutBy
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixtureSource(typeof(TestBase), "RunBrowser")]
    public class TutByTests : TestBase
    {
        public TutByTests() : base() { }

        LandingPage Landing;

        [SetUp]
        public void LoginTestsSetUp()
        {
            Landing.Logout();
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ExcelReader.CreateDataCollection(ExcelReader.excelPath, ExcelReader.loginTestsTableName);
            Landing = new LandingPage();
            Landing.OpenLandingPage();
        }

        [Test, Description("Login with valid credentials"), Parallelizable(ParallelScope.Children)]
        [
            AllureSubSuite("Login functionality tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink("ID-1"),
            AllureTest("Login with valid credentials"),
            AllureOwner("Ilya Orlenko"),
        ]
        public void LoginWithValidCredentialsTest()
        {
            Landing.OpenLoginForm()
                .SubmitLoginForm(Dataset.FirstUser);

            Assert.True(Landing.UsernameLabel.Text == GetFromExcel(Dataset.FirstUser, Field.Username), message: "Actual username label is not matched to expected.");

            Landing.Logout();
        }

        [Test, Parallelizable(ParallelScope.Children)]
        [
            AllureSubSuite("Login functionality tests"),
            AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
            AllureLink("ID-2"),
            AllureTest("Logout from the account"),
            AllureOwner("Ilya Orlenko")
        ]
        public void LogoutTest()
        {
            Landing.OpenLoginForm()
                .SubmitLoginForm(Dataset.SecondUser)
                .Logout();

            Assert.True(Landing.EnterLoginFormButton.Displayed, message: "Enter login form button is not displayed.");
        }

        //[Test, Parallelizable(ParallelScope.Children)]
        //[
        //    AllureSubSuite("Login functionality tests"),
        //    AllureSeverity(Allure.Commons.Model.SeverityLevel.Blocker),
        //    AllureLink("ID-3"),
        //    AllureTest("Failure simulation test"),
        //    AllureOwner("Ilya Orlenko")
        //]
        //public void FailTest()
        //{
        //    Landing.OpenLoginForm()
        //        .SubmitLoginForm(Dataset.FirstUser);

        //        Assert.AreEqual(Landing.UsernameLabel.Text, GetFromExcel(Dataset.FirstUser, Field.Username) + "Fail", message: "Actual username label is not matched to expected.");
        //}
    }
}