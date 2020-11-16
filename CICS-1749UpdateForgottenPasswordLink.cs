using ATProAutomation.ATProCommon;
using ATProAutomation.ATProCommon.Windows;
using ATProAutomation.ATProCommon.Windows.ChromiumWindows;
using ATProAutomation.BasicMethods;
using ATProAutomation.Common;
using Core.EnvironmentSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ATProAutomation.Tests.ET.ForexPro
{
    [TestClass]
    public class CICS_1749UpdateForgottenPasswordLink : TestBase
    {
        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Daily)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VMX)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.ET)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_1749UpdateForgottenPasswordLinkTest()
        {
            var application = new ATProDebugApp();
            application.StartApp();
            var loginWindow = new LoginWindow(application);
            string URL = "https://trade-ppe.loginandtrade.com/forgottenpassword/resetpassword.aspx";
            string URLLIVE = "https://trade.loginandtrade.com/forgottenpassword/resetpassword.aspx";

            loginWindow.ExecuteActionForeachLanguage(() =>
            {
                FiddlerHelper.ListenByFiddler(() =>
                {
                    loginWindow.ForgottenPasswordLink.Click();
                    var forgottenPasswordWindow = new ChromiumWebBrowserWindow(application);
                    forgottenPasswordWindow.Close();
                });

                var address = $"{URL}?cu={GetCurrentShortCulture()}&theme=ci";
                FiddlerHelper.CheckRequestSent(address);
                FiddlerHelper.RequestsList.Clear();
            });

            application.SetENGLanguageInConfigFile();
            
            loginWindow.EnvironmentComboBox.SelectItem("Live");

            loginWindow.ExecuteActionForeachLanguage(() =>
            {
                FiddlerHelper.ListenByFiddler(() =>
                {
                    loginWindow.ForgottenPasswordLink.Click();
                    var forgottenPasswordWindow = new ChromiumWebBrowserWindow(application);
                    forgottenPasswordWindow.Close();
                });

                var address = $"{URLLIVE}?cu={GetCurrentShortCulture()}&theme=ci";
                FiddlerHelper.CheckRequestSent(address);
                FiddlerHelper.RequestsList.Clear();
            });
        }


        
        private string GetCurrentShortCulture()
        {
            var currentLanguage = ApplicationBase.GetCurrentLanguage();
            switch (currentLanguage)
            {
                case Enums.Languages.ENG:
                    return "en";
                case Enums.Languages.ARA:
                    return "en";
                case Enums.Languages.JPN:
                    return "ja-JP";
                case Enums.Languages.CHS:
                    return "zh-CHS";
                case Enums.Languages.DEU:
                    return "en";
                case Enums.Languages.PLK:
                    return "en";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}