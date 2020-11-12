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
    /// Summary description for CISC_2070PriceToleranceFieldInDealTicket
    /// </summary>
    [CodedUITest]
    public class CISC_2070PriceToleranceFieldInDealTicket : TestBase
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
        public void CISC_2070PriceToleranceFieldInDealTicketTest()
        {
            new OrderRequestHelper(userName).CloseAllActiveOrders();

            application = new ATProDebugApp();
            application.StartAppWithLogin(userName, kvps: kvps);
            var createWin = application.MarketSearchTab.Table.OpenCreateMarketOrderBuyFormCM("GBP/USD");
            string PriceToleranceDefaultValue = "2";  
            createWin.OrderTypesRadComboBox.SelectItem(Enums.OrderTypes.Market);
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsEnabled,
                "Error: PriceTolerance combobox should be enabled");
            Assert.IsTrue(createWin.PriceToleranceRadComboBoxLabel.IsVisible,
            "Error: PriceToleranceLabel should be visible");
            Assert.AreEqual(PriceToleranceDefaultValue, createWin.PriceToleranceComboBox.Text,
                "Error: default value for this market is 2");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.FillorKill),
                "Error: value: Fill or Kill should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c1),
                "Error: value: 1 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c2),
               "Error: value: 2 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c3),
               "Error: value: 3 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c4),
               "Error: value: 4 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c5),
               "Error: value: 5 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c6),
               "Error: value: 6 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c7),
               "Error: value: 7 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c8),
               "Error: value: 8 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c9),
               "Error: value: 9 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.c10),
               "Error: value: 10 should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.MarketOrder),
               "Error: value: Market Order should exist");
            Assert.IsTrue(createWin.PriceToleranceComboBox.IsItemExist(Enums.comboPriceToleranceValues.Other),
               "Error: value: Other should exist should exist");
            createWin.PriceToleranceComboBox.SelectItem(Enums.comboPriceToleranceValues.Other);
            Assert.IsTrue(createWin.PriceToleranceValueLabel.IsVisible,
            "Error: PriceToleranceValueLabel should be visible");
            Assert.IsTrue(createWin.PriceToleranceInfo.IsVisible,
                "Error: Info Tip should be visible");
            Assert.IsTrue(createWin.PriceToleranceInfo.ToolTip.Contains("\"Fill or Kill\" is 0"),
                "Error: Something wrong on the line 1");
            Assert.IsTrue(createWin.PriceToleranceInfo.ToolTip.Contains("1-10 select value"),
                "Error: Something wrong on the line 2");
            Assert.IsTrue(createWin.PriceToleranceInfo.ToolTip.Contains("\"Market Order\" is 1000"),
                "Error: Something wrong on the line 3");
            Assert.IsTrue(createWin.PriceToleranceInfo.ToolTip.Contains("Select \'Other\' to add another value"),
                "Error: Something wrong on the line 4");
            createWin.ClickSubmitButton();
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

