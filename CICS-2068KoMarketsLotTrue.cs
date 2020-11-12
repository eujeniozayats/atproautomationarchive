using ATProAutomation.ATProCommon;
using ATProAutomation.ATProCommon.Windows;
using ATProAutomation.BasicMethods;
using Core;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ConstsCore = Core.EnvironmentSettings.Consts;

namespace ATProAutomation.Tests.CPT.Orders
{
    [CodedUITest]
    public class CICS_2068KoMarketsLotTrue : TestBase
    {
        private Dictionary<string, string> kvps = new Dictionary<string, string>()
        {
            {Consts.SHOWKO_KEY, true.ToString()},
           
        };
        

        private string userName = app.Default.login12;
        private string password = ConstsCore.DEFAULT_PASSWORD;
        private const string KoMarketName = Consts.AuKoUpMarket;
        
        private ATProDebugApp application;
        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Daily)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VMX)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_2068KoMarketsLotTrueTest()
        {
            //2069
            application = new ATProDebugApp();
            application.StartAppWithLogin(userName, kvps: kvps);
                      
            application.MarketSearchTab.SearchForMarketFilter.AllMarkets.Click();
            application.MarketSearchTab.SearchMarketsRadComboBox.Text = KoMarketName;
            application.MarketSearchTab.SearchButtonClick();
            var createWin = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM(KoMarketName);
            Assert.IsTrue(createWin.KoLotLabel.IsVisible,
               "Error: \"1 lot = 10,000\" should be visible");
            createWin.ClickCancelButton();
            
        }


        #region Additional test attributes
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //}

        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //}
        #endregion
    }
}
