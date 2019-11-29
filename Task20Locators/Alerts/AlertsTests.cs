using NUnit.Framework;
using Task20Locators.Base;

namespace Task20Locators.Alerts
{
    [TestFixture]
    public class AlertsTests : TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        }

        [Test]
        public void SimpleAlertTest()
        {
            AlertsPageObject.ClickSimpleAlertButton();
            AlertsPageObject.AcceptAlert();

            Assert.True(AlertsPageObject.GetResultText() == "You successfuly clicked an alert");
        }

        [Test]
        public void ConfirmationAlertTest()
        {
            AlertsPageObject.ClickConfirmAlertButton();
            AlertsPageObject.DismissAlert();

            Assert.True(AlertsPageObject.GetResultText() == "You clicked: Cancel");
        }

        [Test]
        public void PromptAlertTest()
        {
            AlertsPageObject.ClickPromptAlertButton();
            AlertsPageObject.PopulateAlertPrompt("Some text");
            AlertsPageObject.AcceptAlert();

            Assert.True(AlertsPageObject.GetResultText() == "You entered: " + AlertsPageObject.TextEnteredToPrompt);
        }
    }
}
