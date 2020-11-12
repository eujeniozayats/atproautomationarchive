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
    public class CICS_2076PriceToleranceKVPBased : TestBase
    {
        private Dictionary<string, string> kvps = new Dictionary<string, string>()
        {
            {Consts.PRICETOLERANCE_KEY, true.ToString()},

        };

        private string userName = app.Default.login1;
        private string password = ConstsCore.DEFAULT_PASSWORD;
        private Market market = Consts.Market11();

        private ATProDebugApp application;
        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Daily)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VMX)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_2076PriceToleranceKVPBasedTest()
        {
            application = new ATProDebugApp();
            application.StartAppWithLogin(userName, kvps: kvps);

            var createWin = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM("UK 100 DFT");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsVisible,
               "Error: PriceTolerance combobox should be visible");
            createWin.ClickCancelButton();
            application.Logout();
            var loginWin = new LoginWindow(application);
            Wait.UntilNoException(() => loginWin.SpoofKVPTextBox.Click());

            new SpoofKVPWindow(application).DeleteKVPs(kvps);
            loginWin.PasswordTextBox.Text = password;
            loginWin.ClickOkButton();
            application.WaitTillMarketsLoad();
            createWin = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM("UK 100 DFT");
            Assert.IsFalse(createWin.PriceToleranceComboBox.IsVisible,
                "Error: PriceTolerance combobox should not be visible");
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