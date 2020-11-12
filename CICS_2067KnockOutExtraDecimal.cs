using ATProAutomation.ATProCommon;
using ATProAutomation.BasicMethods;
using Core;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ATProAutomation.Tests.ST.MarketSearch
{
    [CodedUITest]
    public class CICS_2067KnockOutExtraDecimal : TestBase
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
        public void CICS_2067KnockOutExtraDecimalTest()
        {
            application = new ATProDebugApp();
            application.StartAppWithLogin(userName);
            new OrderRequestHelper(userName).CloseAllActiveOrders();
            
            var marketOrderWindowLimit = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyForm(upMarketName);
            string num = marketOrderWindowLimit.SelectedKnockoutLevel.Text;
            var numCount = num.Length - num.IndexOf(".") - 1;
            Assert.AreEqual(2, numCount, 
                "Error: this market's knockout should contain 2 nums after dot");
            
        }
    }
}
