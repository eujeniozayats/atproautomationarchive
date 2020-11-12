using ATProAutomation.ATProCommon;
using ATProAutomation.ATProCommon.Windows;
using ATProAutomation.BasicMethods;
using Core.EnvironmentSettings;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ATProAutomation.Tests.CPT.Reports
{
    /// <summary>
    /// Summary description for CICS_1334PRWSubmitBAndColumnFilter
    /// </summary>
    [CodedUITest]
    public class CICS_1334PRWSubmitBAndColumnFilter : TestBase
    {
        private string requestsFile = "Requests.txt";
        //1/4/2016 15:06:56.429
        private string requestsDateTimeFormat = "M/d/yyyy HH:mm:ss.fff";
        private ATProDebugApp application;
        [TestProperty(Enums.Tags.Runtime, Enums.Runtime.Nightly)]
        [TestProperty(Enums.Tags.Machine, Enums.Machines.VM1)]
        [TestProperty(Enums.Tags.Scope, Enums.Scopes.CPT)]
        [TestMethod]
        [PostSharpAspect]
        public void CICS_1334PRWSubmitBAndColumnFilterTest()
        {
            // var market = Consts.Market11();
            string mark = "CAD/CHF";
            application = new ATProDebugApp();
            application.StartAppWithLogin1();

            application.MainMenu.Reports.PriceReport.Click();
            var priceReportWin = new PriceReportWindow(application);
            priceReportWin.Panel.SearchComboBox.Text = "CAD/CHF";
            Wait.UntilNoException(() => Assert.IsTrue(priceReportWin.Panel.SearchComboBox.IsDropDownOpen, "Error SearchComboBox dropdown should be opened"));
            priceReportWin.Panel.SearchComboBox.SelectItem(mark);
            Assert.AreEqual(mark, priceReportWin.Panel.SearchComboBox.SelectedValue, "Error: wrong SearchComboBox selected value");
            priceReportWin.Panel.ClickSubmitButton();
            //Check that requests logs contains
            //"1/4/2016 15:06:56.429","Start request to String.Emptyhttps://ciapi.cityindex.com/TradingApi/market/99500/tickhistory?priceticks=50000String.Empty"
            string linePattern = "";
            if (URLs.Instance.CurrentServer == Core.Enums.TeamcityRunServers.Live)
                linePattern = @"Start request to \""{4}https:\/\/ciapi\.cityindex\.com\/TradingApi\/market\/\d+\/tickhistory\?priceticks=50000";
            else
                linePattern = @"Start request to \""{4}https:\/\/ciapipreprod\.cityindextest9\.co\.uk\:443\/TradingApi\/market\/\d+\/tickhistory\?priceticks=50000";
            var logsDir = new DirectoryInfo(application.LogsPath);
            var requestsTxtFile = logsDir.GetFiles()
                .FirstUntilNumberOfException(d => d.Name == requestsFile, "Can't find '" + requestsFile + "' file: "
                                                    + logsDir.ToString());

            var lastLine = File.ReadLines(requestsTxtFile.FullName).Last(l => Regex.IsMatch(l, linePattern));
            var lineDateTime = DateTime.ParseExact(lastLine.Split('"')[1], requestsDateTimeFormat, CultureInfo.InvariantCulture);
            Assert.IsTrue(
                ((DateTime.Now - lineDateTime) < new TimeSpan(0, 2, 0)) &&
                ((DateTime.Now - lineDateTime) > new TimeSpan(0, 0, 0)),
                "Error: needed request is not exist. " + lastLine);

            priceReportWin.Panel.Table.CheckDateTimeColumnSorting("Date");
            priceReportWin.Panel.Table.CheckDoubleColumnSorting("Mid");
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
