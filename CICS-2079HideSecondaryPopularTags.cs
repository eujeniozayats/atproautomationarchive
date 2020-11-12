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
    public class CICS_2079HideSecondaryPopularTags : TestBase
    {
        private Dictionary<string, string> kvps = new Dictionary<string, string>()
        {
            { Consts.POPULAR_MRKT, null},
        };
        private Dictionary<string, string> kvps1 = new Dictionary<string, string>()
        {
            {Consts.POPULAR_MRKT, 152.ToString()},
        };
        private Dictionary<string, string> kvps2 = new Dictionary<string, string>()
        {
            {Consts.POPULAR_MRKT, 146.ToString()},
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
        public void CICS_2079HideSecondaryPopularTagsTest()
        {
            application = new ATProDebugApp();
            application.StartAppWithLogin(userName, kvps: kvps);

            application.MarketSearchTab.SearchForMarketFilter.Popular.Click();
            Assert.IsTrue(application.MarketSearchTab.SearchForMarketFilter.Popular.IsSubmenuOpen,
                "Error: SubMenu should be opened");
            application.Logout();
            var loginWin = new LoginWindow(application);
            Wait.UntilNoException(() => loginWin.SpoofKVPTextBox.Click());

            new SpoofKVPWindow(application).AddNewKVP(kvps1);
            loginWin.PasswordTextBox.Text = password;
            loginWin.ClickOkButton();
            application.WaitTillMarketsLoad();
            application.MarketSearchTab.SearchForMarketFilter.Popular.Click();
            Assert.IsFalse(application.MarketSearchTab.SearchForMarketFilter.Popular.IsSubmenuOpen,
                "Error: SubMenu should not be opened");
            application.Logout();
            var loginWin1 = new LoginWindow(application);
            Wait.UntilNoException(() => loginWin1.SpoofKVPTextBox.Click());

            new SpoofKVPWindow(application).AddNewKVP(kvps2);
            loginWin1.PasswordTextBox.Text = password;
            loginWin1.ClickOkButton();
            application.WaitTillMarketsLoad();
            application.MarketSearchTab.SearchForMarketFilter.Popular.Click();
            Assert.IsFalse(application.MarketSearchTab.SearchForMarketFilter.Popular.IsSubmenuOpen,
                 "Error: SubMenu should not be opened");
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
