using ATProAutomation.ATProCommon;
using ATProAutomation.BasicMethods;
using ATProAutomation.Resources.Translation;
using Core;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ATProAutomation.Tests.ST.MarketSearch
{
    [CodedUITest]
    public class CICS_1967JpyPlCheck : TestBase
    {
        Market market = Consts.Market11();
        private const string upMarketName = Consts.JpyFxKoUpMarket;
        private string userName = app.Default.forexJpLogin;
        private ATProDebugApp application;

        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Daily)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VM1)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.ST)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_1967JpyPlCheckTest()
        {
            application = new ATProDebugApp();
            application.StartAppWithLogin(userName);

            new OrderRequestHelper(userName).CloseAllActiveOrders();


            var marketOrderWindowLimit = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyForm(upMarketName);
            var koLevel = marketOrderWindowLimit.SelectedKnockoutLevel.Text;
            marketOrderWindowLimit.ExpandStopLimit();
            marketOrderWindowLimit.CurrentStopLimit.LimitCheckBox.Check();
            marketOrderWindowLimit.CurrentStopLimit.PointsRadioButton.Click();
            marketOrderWindowLimit.CurrentStopLimit.PointsLimit.Value = 10;
            marketOrderWindowLimit.CurrentStopLimit.EstimatedRadioButton.Click();
            Assert.AreEqual("100", marketOrderWindowLimit.CurrentStopLimit.EstimatedLimit.Text,
                "Error: Value in the estimated field should be 100");
            marketOrderWindowLimit.ClickSubmitButton();

            var win = application.OpenPositionsTab.OpenEditMarketOrderWindow(upMarketName + " " + koLevel);
            win.ExpandStopLimit();
            win.CurrentStopLimit.LimitCheckBox.Check();
            win.CurrentStopLimit.EstimatedRadioButton.Click();
            win.CurrentStopLimit.EstimatedLimit.Value = 100;
            win.CurrentStopLimit.PointsRadioButton.Click();
            Assert.AreEqual("10", win.CurrentStopLimit.PointsLimit.Text,
                "Error: Value in the pips field should be 10");
            win.ClickCancelButton();
            new OrderRequestHelper(userName).CloseAllActiveOrders();

            var marketOrderWindowStop = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyForm(upMarketName);
            var koLevel2 = marketOrderWindowStop.SelectedKnockoutLevel.Text;
            marketOrderWindowStop.ExpandStopLimit();
            marketOrderWindowStop.CurrentStopLimit.StopCheckBox.Check();
            marketOrderWindowStop.CurrentStopLimit.PointsRadioButton.Click();
            marketOrderWindowStop.CurrentStopLimit.PointsStop.Value = 10;
            marketOrderWindowStop.CurrentStopLimit.EstimatedRadioButton.Click();
            Assert.AreEqual("-100", marketOrderWindowStop.CurrentStopLimit.EstimatedStop.Text,
                "Error: Value in the estimated field should be -100");
            marketOrderWindowStop.ClickSubmitButton();

            var winStop = application.OpenPositionsTab.OpenEditMarketOrderWindow(upMarketName + " " + koLevel);
            winStop.ExpandStopLimit();
            winStop.CurrentStopLimit.StopCheckBox.Check();
            winStop.CurrentStopLimit.EstimatedRadioButton.Click();
            winStop.CurrentStopLimit.EstimatedStop.Value = -100;
            winStop.CurrentStopLimit.PointsRadioButton.Click();
            Assert.AreEqual("10", winStop.CurrentStopLimit.PointsStop.Text,
                "Error: Value in the pips field should be 10");
            winStop.ClickCancelButton();
            new OrderRequestHelper(userName).CloseAllActiveOrders();


        }
    }
}
