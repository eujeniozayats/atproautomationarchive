using ATProAutomation.ATProCommon;
using ATProAutomation.ATProCommon.Windows;
using ATProAutomation.BasicMethods;
using ATProAutomation.BasicMethods.ExcelMethods;
using ATProAutomation.BasicMethods.Win32Api;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

namespace ATProAutomation.Tests.CPT.Reports
{
    /// <summary>
    /// Summary description for CICS_1335PRWExportToPDFAndExcel
    /// </summary>
    [CodedUITest]
    public class CICS_1335PRWExportToPDFAndExcel : TestBase
    {
        private const string fileName = @"\ExportPDF1335.xlsx";
        private PriceReportWindow priceReportWin;
        //Market market = Consts.Market11();
        //private string market = Consts.FX_MARKET;
        string mark = "Wall Street CFD";
        private ATProDebugApp application;
        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Nightly)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VM1)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_1335PRWExportToPDFAndExcelTest()
        {
            application = new ATProDebugApp();
            application.StartAppWithLogin1();

            application.MainMenu.Reports.PriceReport.Click();
            priceReportWin = new PriceReportWindow(application);
            priceReportWin.Panel.SearchComboBox.Text = mark;
            Wait.UntilNoException(
                () =>
                    Assert.IsTrue(priceReportWin.Panel.SearchComboBox.IsDropDownOpen,
                        "Error SearchComboBox dropdown should be opened"));
            priceReportWin.Panel.SearchComboBox.SelectItem(mark);
            Assert.AreEqual(mark, priceReportWin.Panel.SearchComboBox.SelectedValue,
                "Error: wrong SearchComboBox selected value");
            priceReportWin.Panel.ClickSubmitButton();

            //priceReportWin.Panel.ExportPdfButton.Click();
            //CheckExportToPDF();

             priceReportWin.Panel.ExportExcelButton.Click();
             CheckExportToExcel();
        }

         private void CheckExportToExcel()
        {
            var filename = TestContext.DeploymentDirectory + fileName + DateTime.Now.ToString(" hh-mm-ss");
            new SaveFile32Window().SaveFile(filename);
            //When data is exported it is organised with the columns in the same order displayed in Trade History grids.
            var excelTable = ExcelParser.ReadDataFromExcel(filename + @".xlsx");
            var guiTable = priceReportWin.Panel.Table.ConvertToListOfDictionary();

            Assert.IsTrue(guiTable.All(guirow => excelTable.Any(t => t.SequenceEqual(guirow))), "Error: application.OrderHistoryTab exporting in excel is not succesfull");
            //this method working too
            // bool contains2 = guiTable.All(guirow => table.Any(excelRow => guirow.Keys.All(k => excelRow.ContainsKey(k) && object.Equals(excelRow[k], guirow[k]))));
        } 

        /* private void CheckExportToPDF()
        {
            var filename = TestContext.DeploymentDirectory + fileName + DateTime.Now.ToString(" hh-mm-ss");
            new SaveFile32Window().SaveFile(filename);

            var textFromPDF = pdfText(filename + @".pdf");//returning text from table in PDF without space
            var listTextFromGUI = String.Join(String.Empty, priceReportWin.Panel.Table.ConvertToListOfDictionary().Select(row => String.Join(String.Empty, row.Select(c => c.Value.Replace(" ", String.Empty))) + "\n"));
            StringAssert.Contains(textFromPDF, listTextFromGUI);
        }

        public static string pdfText(string path)
        {
            string s = String.Empty;
            PdfReader reader = Wait.UntilNoException(() => new PdfReader(path));
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                text = PdfTextExtractor.GetTextFromPage(reader, page, its);
                s += Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));

            }
            reader.Close();
            return s;
        } */

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
