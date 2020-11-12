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
    /// Summary description for CICS-2071PriceToleranceFieldSelectedValue
    /// </summary>
    [CodedUITest]
    public class CISC_2071PriceTolerenceFieldSelectedValue : TestBase
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
        public void CISC_2071PriceToleranceFieldSelectedValueTest()
        {

            new OrderRequestHelper(userName).CloseAllActiveOrders();

            application = new ATProDebugApp();
            application.StartAppWithLogin(userName, kvps: kvps);
            var createWin = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM(market.Name);
            createWin.OrderTypesRadComboBox.SelectItem(Enums.OrderTypes.Market);
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsEnabled,
                "Error: PriceTolerance combobox should be enabled");
            Assert.IsTrue(createWin.PriceToleranceRadComboBoxLabel.IsVisible,
            "Error: PriceToleranceLabel should be visible");
            createWin.PriceToleranceComboBox.SelectItem(Enums.comboPriceToleranceValues.c6);
            Assert.AreEqual(Enums.comboPriceToleranceValues.c6, createWin.PriceToleranceComboBox.Text,
                "Error: value 6 should be placed");
            createWin.ClickSubmitButton();

            var createWin2 = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM(market.Name);
            createWin2.OrderTypesRadComboBox.SelectItem(Enums.OrderTypes.Market);
            Assert.AreEqual(Enums.comboPriceToleranceValues.c6, createWin2.PriceToleranceComboBox.Text,
                "Error: value 6 should be placed");
            createWin2.ClickSubmitButton();
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

