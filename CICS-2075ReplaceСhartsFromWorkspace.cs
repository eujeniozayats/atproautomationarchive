using ATProAutomation.ATProCommon;
using ATProAutomation.BasicMethods;
using ATProAutomation.ATProCommon.Windows;
using Core;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ConstsCore = Core.EnvironmentSettings.Consts;


namespace ATProAutomation.Tests.CPT.KPI_Performance_Monitoring
{
    [CodedUITest]
    public class CICS_2075ReplaceСhartsFromWorkspaceIfMarkeIDPresentInKVP : TestBase
    {
        private Dictionary<string, string> kvps = new Dictionary<string, string>()
        {
            {Consts.REPLACED_CHARTS_KEY, "[{401156672, 401560141}]"},

        };
        private Dictionary<string, string> kvps1 = new Dictionary<string, string>()
        {
            {Consts.REPLACED_CHARTS_KEY, null},
        };
        private Market market = Consts.Market11();
        private string userName = app.Default.login1;
        private string password = ConstsCore.DEFAULT_PASSWORD;

        private ATProDebugApp application;

        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Daily)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VM2)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_2075ReplaceСhartsFromWorkspaceIfMarkeIDPresentInKVPTest()
        {
            application = new ATProDebugApp();
            application.StartAppWithLogin(userName, kvps: kvps1);
            var chartWin = application.MarketSearchTab.Table.OpenNewChartFormCM("AUD/USD");
            application.Logout();

            var loginWin = new LoginWindow(application);
            Wait.UntilNoException(() => loginWin.SpoofKVPTextBox.Click());

            new SpoofKVPWindow(application).AddNewKVP(kvps);
            loginWin.PasswordTextBox.Text = password;
            loginWin.ClickOkButton();
            application.WaitTillMarketsLoad();
            var chartWindow = new ChartWindow(application);
            Assert.IsTrue(chartWindow.HeaderTextBlock.Text.Contains("US Crude Oil CFD"),
                "Error: Chart window should be related to US Crude Oil CFD");
            application.MainMenu.Customize.DesktopWorkspaces.Switch.Click();
            application.MainMenu.Customize.DesktopWorkspaces.Switch.FX.Click();
            application.MainMenu.Customize.DesktopWorkspaces.Switch.Default.Click();
            application.WaitTillMarketsLoad();
            var chartWindow1 = new ChartWindow(application);
            Assert.IsTrue(chartWindow1.HeaderTextBlock.Text.Contains("US Crude Oil CFD"),
                "Error: Chart window should be related to US Crude Oil CFD");

        }
    }
}
