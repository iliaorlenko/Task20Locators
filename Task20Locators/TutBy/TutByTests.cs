using NUnit.Framework;
using System.Collections;
using Task20Locators.Base;

namespace Task20Locators.TutBy
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

        // Data-driven test using flexible excel reader
        [Test]
        public void LoginWithValidCredentialsTest()
        {
            LandingPage.OpenLoginForm();
            LandingPage.SubmitLoginForm(Dataset.FirstUser);

            Assert.True(LandingPage.WaitFindElement(LandingPage.UsernameLabel).Text == GetFromExcel(Dataset.FirstUser, Field.Username));

            LandingPage.Logout();
        }

        // Data-driven test with several test cases using xml
        [TestCaseSource("LoginTestsData")]
        public void SeveralLoginTests(string login, string password, string username)
        {
            LandingPage.OpenLoginForm();

            // Thread.Sleep. It's not generally a waiter in Selenium WebDriver context, but if imagine it is I would say it's an explicit waiter
            // Because it declared explicitly in specific chunk of test code
            System.Threading.Thread.Sleep(1000);

            LandingPage.WaitFindElement(LandingPage.LoginInput).SendKeys(login);
            LandingPage.WaitFindElement(LandingPage.PasswordInput).SendKeys(password);
            LandingPage.WaitFindElement(LandingPage.LoginButton).Click();

            Assert.True(LandingPage.WaitFindElement(LandingPage.UsernameLabel).Text == username);

            LandingPage.Logout();
        }
    }
}