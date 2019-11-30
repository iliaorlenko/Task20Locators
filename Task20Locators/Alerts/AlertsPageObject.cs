using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task20Locators.Base;

namespace Task20Locators.Alerts
{
    public static class AlertsPageObject
    {
        public static By SimpleAlertButton = By.XPath("//button[@onclick='jsAlert()']");
        public static By ConfirmAlertButton = By.XPath("//button[@onclick='jsConfirm()']");
        public static By PromptAlertButton = By.XPath("//button[@onclick='jsPrompt()']");
        public static By ResultLabel = By.XPath("//p[@id='result']");
        public static string TextEnteredToPrompt = null;

        public static void ClickSimpleAlertButton() => DriverContext.Driver.FindElement(SimpleAlertButton).Click();
        public static void ClickConfirmAlertButton() => DriverContext.Driver.FindElement(ConfirmAlertButton).Click();
        public static void ClickPromptAlertButton() => DriverContext.Driver.FindElement(PromptAlertButton).Click();
        public static string GetResultText() => DriverContext.Driver.FindElement(ResultLabel).Text;
        public static void AcceptAlert() => DriverContext.Driver.SwitchTo().Alert().Accept();
        public static void DismissAlert() => DriverContext.Driver.SwitchTo().Alert().Dismiss();
        public static void PopulateAlertPrompt(string text)
        {
            DriverContext.Driver.SwitchTo().Alert().SendKeys(text);
            TextEnteredToPrompt = text;
        }
    }
}
