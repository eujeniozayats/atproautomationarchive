using ATProAutomation.ATProCommon;
using ATProAutomation.ATProCommon.Windows;
using ATProAutomation.BasicMethods;
using ATProAutomation.Resources.Translation;
using Core;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ATProAutomation.Tests.CPT.DifferentCases
{
    /// <summary>
    /// Summary description for CICS-2072TranslationForPriceToleranceField
    /// </summary>
    [CodedUITest]
    public class CISC_2072TranslationForPriceToleranceField : TestBase
    {
        private Dictionary<string, string> kvps = new Dictionary<string, string>()
        {
            {Consts.PRICETOLERANCE_KEY , true.ToString()},

        };
        private Market market = Consts.Market11();
        private string userName = app.Default.login1;
        private ATProDebugApp application;
       

        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Daily)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VM1)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestMethod]
        [PostSharpAspect]
        public void CISC_2072TranslationForPriceToleranceFieldTest()
        {
            new OrderRequestHelper(userName).CloseAllActiveOrders();

            application = new ATProDebugApp();
            application.StartAppWithLogin(userName, kvps: kvps);
          
            var createWin2 = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM(market.Name);
            createWin2.OrderTypesRadComboBox.SelectItem(Enums.OrderTypes.Market);

            application.ExecuteForeachLanguageMainMenu(() =>
            {
                createWin2.PriceToleranceComboBox.SelectItem(Enums.comboPriceToleranceValues.Other);
                Assert.AreEqual(translations.PriceTolerance, createWin2.PriceToleranceLbl.Text,
                    "Error: PriceTolerance is not transalted");
                Assert.AreEqual(translations.Other, createWin2.PriceToleranceComboBox.Text,
                    "Error: Other from the tolerance list is not transalted");
                Assert.AreEqual(translations.ToleranceValue, createWin2.PriceToleranceValueLbl.Text,
                    "Error: Price Tolerance Value is not translated");
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

