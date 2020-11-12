using System.Linq;
using ATProAutomation.ATProCommon;
using ATProAutomation.ATProCommon.Windows;
using ATProAutomation.BasicMethods;
using ATProAutomation.Resources.Translation;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATProAutomation.Tests.CPT.Reports
{
    /// <summary>
    /// Summary description for CICS_1331NewPriceReportsWindow
    /// </summary>
    [CodedUITest]
    public class CICS_1331NewPriceReportsWindow : TestBase
    {
        private ATProDebugApp application;
        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Nightly)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VM1)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_1331NewPriceReportsWindowTest()
        {
            application = new ATProDebugApp();
            application.StartAppWithLogin1();
            string typeInto = "CAD/CHF";

            Assert.IsTrue(application.MainMenu.Reports.IsEnabled, "Error: expected that Reports menu item is enabled");
            Assert.IsTrue(application.MainMenu.Reports.PriceReport.IsEnabled,
                "Error: expected that Reports -> PriceReport menu item is enabled");
            application.MainMenu.Reports.PriceReport.Click();
            var priceReportWin = new PriceReportWindow(application);
            Assert.IsTrue(priceReportWin.Panel.SearchComboBox.IsVisible,
                "Error: SearchComboBox should be visible on PriceReportWindow");
            Assert.IsTrue(priceReportWin.Panel.SubmitButton.IsVisible,
                "Error: SubmitButton should be visible on PriceReportWindow");
            Assert.IsFalse(priceReportWin.Panel.SubmitButton.IsEnabled,
                "Error: SubmitButton should be disabled on PriceReportWindow");


            Assert.IsFalse(priceReportWin.Panel.Table.Rows.Any(), "Error: table should be empty on PriceReportWindow");
            Assert.IsTrue(priceReportWin.Panel.ExportExcelButton.IsVisible,
                "Error: ExportExcelButton should be visible on PriceReportWindow");
            Assert.IsTrue(priceReportWin.Panel.ExportPdfButton.IsVisible,
                "Error: ExportPdfButton should be visible on PriceReportWindow");
            Assert.IsFalse(priceReportWin.Panel.ExportExcelButton.IsEnabled,
                "Error: ExportExcelButton should be disabled on PriceReportWindow");
            Assert.IsFalse(priceReportWin.Panel.ExportPdfButton.IsEnabled,
                "Error: ExportPdfButton should be disabled on PriceReportWindow");
            Assert.IsTrue(priceReportWin.Panel.TillDateTimePicker.IsVisible,
                "Error: TillDateTimePicker should be visible on PriceReportWindow");
            priceReportWin.Panel.SearchComboBox.Click();
          


            //3.1 Select Arabic languge menu item
            application.ExecuteForeachLanguageMainMenu(() =>
            {
                Assert.AreEqual(translations.PriceReport, application.MainMenu.Reports.PriceReport.Text,
                    "Error: PriceReport menu is not transalted");
                priceReportWin = new PriceReportWindow(application);
                Assert.AreEqual(translations.PriceReport, priceReportWin.HeaderTextBlock.Text,
                    "Error: header (PriceReportWindow) is not translated ");
                Assert.AreEqual(translations.SelectMarket, priceReportWin.Panel.SelectMarketLabel.Text,
                    "Error: SelectMarketLabel (PriceReportWindow) is not translated ");
                
                Assert.AreEqual(translations.To, priceReportWin.Panel.ToLabel.Text,
                    "Error: ToLabel (PriceReportWindow) is not translated ");
                Assert.AreEqual(translations.Submit, priceReportWin.Panel.SubmitButton.Text,
                    "Error: SubmitButton (PriceReportWindow) is not translated ");
                Assert.AreEqual(translations.ExportToPDF, priceReportWin.Panel.ExportPdfButton.Text,
                    "Error: ExportPdfButton (PriceReportWindow) is not translated ");
                Assert.AreEqual(translations.ExportToExcel, priceReportWin.Panel.ExportExcelButton.Text,
                    "Error: ExportExcelButton (PriceReportWindow) is not translated ");
                Assert.IsTrue(priceReportWin.Panel.Table.IsHeaderExist(priceReportWin.Panel.Headers.Date),
                    "Error: Date column header (PriceReportWindow) is not translated " +
                    priceReportWin.Panel.Headers.Date);
                Assert.IsTrue(priceReportWin.Panel.Table.IsHeaderExist(priceReportWin.Panel.Headers.Mid),
                    "Error: Mid column header (PriceReportWindow) is not translated " + priceReportWin.Panel.Headers.Mid);
            }, Enums.Languages.JPN);
            priceReportWin.AttachWindowToTheMain();
            Assert.IsTrue(application.PriceReportPanel.Table.IsVisible, "Error: PriceReportPanel.Table should be visible");
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
        //}

        #endregion
    }
}
