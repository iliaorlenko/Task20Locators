using NUnit.Framework;
using Task20Locators.Base;

namespace Task20Locators.Frames
{
    [TestFixture]
    public class FramesTests : TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            FramesPageObject.OpenFramesPage();
        }

        [Test]
        public void Test()
        {
            FramesPageObject.SwitchToFrame();
            FramesPageObject.ClearContentInput();

            // This doesn't work for some reason, "World!" doesn't become bold, Bold button disabled after switching back to the frame
            //FramesPageObject.PopulateContentInput("Hello ");
            //FramesPageObject.SwithcToDefaultContent();
            //FramesPageObject.ClickBoldFontButton();
            //FramesPageObject.SwitchToFrame();
            //FramesPageObject.PopulateContentInput("World!");

            // This option works fine
            FramesPageObject.PopulateContentInput("Hello World!");
            FramesPageObject.SelectLastEnteredWord();
            FramesPageObject.SwithcToDefaultContent();
            FramesPageObject.ClickBoldFontButton();

            Assert.True(FramesPageObject.GetTextFromContentInput() == "Hello World!");
        }
    }
}
