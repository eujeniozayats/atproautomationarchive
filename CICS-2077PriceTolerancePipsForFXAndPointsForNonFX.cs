using ATProAutomation.ATProCommon;
using ATProAutomation.BasicMethods;
using Core;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ATProAutomation.Resources.Translation;

namespace ATProAutomation.Tests.CPT.DifferentCases
{
    /// <summary>
    /// Summary description for CICS-2077PriceTolerancePipsForFXAndPointsForNonFX
    /// </summary>
    [CodedUITest]
    public class CISC_2077PriceTolerancePipsForFXAndPointsForNonFX : TestBase
    {
        private Dictionary<string, string> kvps = new Dictionary<string, string>()
        {
            {Consts.PRICETOLERANCE_KEY , true.ToString()},

        };
        private Market market = Consts.Market11();
        private string userName = app.Default.login1;
        private ATProDebugApp application;
        private string marketFX = "AUD/USD";


        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Daily)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VM1)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestMethod]
        [PostSharpAspect]
        public void CISC_CISC_2077PriceTolerancePipsForFXAndPointsForNonFXTest()
        {
            var orderRequestHelper = new OrderRequestHelper(userName);
            orderRequestHelper.CloseAllActiveOrders();
            
            application = new ATProDebugApp();
            application.StartAppWithLogin(userName, kvps: kvps);
          
            var createWin2 = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM(marketFX);
            createWin2.OrderTypesRadComboBox.SelectItem(Enums.OrderTypes.Market);
            Assert.IsTrue(createWin2.priceTolerancePointsLabel.IsVisible,
                "Error: Price tolerance points label should be visible");
            Assert.IsTrue(createWin2.priceTolerancePointsLabel.Text.Contains(translations.PipValue1),
                "Error: Pip Value should present in the Price tolerance points label");
            application.ExecuteForeachLanguageMainMenu(() =>
            {
                Assert.IsTrue(createWin2.priceTolerancePointsLabel.Text.Contains(translations.PipValue1),
                    "Error: wrong translation ");
                
            });
            createWin2.ClickCancelButton();

            var createWin = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM("UK 100 DFT");
            createWin.OrderTypesRadComboBox.SelectItem(Enums.OrderTypes.Market);
            Assert.IsTrue(createWin.priceTolerancePointsLabel.IsVisible,
                "Error: Price tolerance points label should be visible");
            Assert.IsTrue(createWin.priceTolerancePointsLabel.Text.Contains(translations.PointValue),
                "Error: Pip Value should present in the Price tolerance points label");
            application.ExecuteForeachLanguageMainMenu(() =>
            {
                Assert.IsTrue(createWin.priceTolerancePointsLabel.Text.Contains(translations.PointValue),
                    "Error: wrong translation ");
                                                
               });
        }
    }

    #region Additional test attributes

    // You can use the following additional attributes as you write your tests:

    ////Use TestInitialize to run code before running each test 
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{        
    //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
    //}

    ////Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{        
    //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
    //}

    
    #endregion
}

