using System;
using ATProAutomation.ATProCommon;
using ATProAutomation.ATProCommon.Windows;
using ATProAutomation.BasicMethods;
using Core;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATProAutomation.Tests.CPT.Reports
{
    /// <summary>
    /// Summary description for CICS_1332PRWSearchBoxAndTimeSelector
    /// </summary>
    [CodedUITest]
    public class CICS_1332PRWSearchBoxAndTimeSelector : TestBase
    {
        string mark = "CAD/CHF";
        private ATProDebugApp application;
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VM1)] 
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Nightly)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_1332PRWSearchBoxAndTimeSelectorTest()
        {
            application = new ATProDebugApp();
            application.StartAppWithLogin1();
            application.MainMenu.Reports.PriceReport.Click();
            var priceReportWin = new PriceReportWindow(application);
            priceReportWin.Panel.SearchComboBox.Text = "CAD/CHF";
            Wait.UntilNoException(() => Assert.IsTrue(priceReportWin.Panel.SearchComboBox.IsDropDownOpen, "Error SearchComboBox dropdown should be opened"));
            priceReportWin.Panel.SearchComboBox.SelectItem(mark);
            Assert.AreEqual(mark, priceReportWin.Panel.SearchComboBox.SelectedValue, "Error: wrong SearchComboBox selected value");

            //- There is a "to" field with displayed current date/time that doesn't allow the user to pick a date/time. The date/time can't be edited. 
            Assert.IsFalse(priceReportWin.Panel.TillDateTimePicker.IsEnabled, "Error: Till DateTimePicker should not be enabled");
            Assert.AreEqual(DateTime.Now.Date, priceReportWin.Panel.TillDateTimePicker.Value.Date, "Error: TillDateTimePicker value is wrong ");

        
       
            
          
            
       
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
}
