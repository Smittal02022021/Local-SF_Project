using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;


namespace SalesForce_Project.Pages.Reports
{
    class ReportHomePage : BaseClass
    {
        By lnkReports = By.CssSelector("a[title*='Reports Tab']");
        By txtFindAFolder = By.CssSelector("input[id='ext-comp-1001']");
        By matchedFolderResult = By.CssSelector("span[unselectable='on'] > div");
        By txtFindAReportAndDashboard = By.CssSelector("input[id='ext-comp-1017']");
        By matchedReportResult = By.CssSelector("div[class='nameFieldContainer descrContainer'] > a");
        By btnHideDetails = By.CssSelector("input[value='Hide Details']");
        By btnShowDetails = By.CssSelector("input[value='Show Details']");
        //By chkboxOfCreatedByUser = By.XPath("$"//*[text()='{user}']/../../../td[1]/input"));
        By btnDrillDown = By.CssSelector("input[value='Drill Down']");
        By colCompanyName = By.CssSelector("table[class='reportTable tabularReportTable'] > tbody > tr[class='even'] > td:nth-child(4)");
        By fromDate = By.CssSelector("input[id='colDt_s']");
        By toDate = By.CssSelector("input[id='colDt_e']");
        By btnRunReport = By.CssSelector("span[id='runMuttonLabel']");
        By valDuplicateRecordSetName = By.CssSelector("tr[class*='breakRowClass1'] > td > span[class='groupvalue']");
        By chkboxReportRecord = By.CssSelector("td[class*='drilldown'] >input");
        By DataHygieneFolderTitle = By.CssSelector("span[id='ext-gen503']");
        By ReportDuplicateRuleAccount = By.CssSelector("h1[class='noSecondHeader pageType']");
        By valCreatedByFullName = By.CssSelector("span[class='groupvalue']");
        By valLabelGroupByDupRecord = By.CssSelector("tr[class='breakRowClass0 breakRowClass0Top'] > td:nth-child(2) > strong:nth-child(1)");
        By btnSavePageSetting = By.CssSelector("input[id='RPPSaveButton']");
        By btnNeverUpdate = By.CssSelector("div[id='stateCountryPicklistWarning_buttons'] > input[value='Never Update']");
        By btnStateCountryPicklistWarning = By.XPath("//input[@id='buttonNever']");

        string dir = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\";

        //To Click on Company tab
        public void ClickReportsTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkReports);
            driver.FindElement(lnkReports).Click();
        }

        //To Click on Company tab
        public void DismissStateCountryPicklistWarningPopup()
        {
            Thread.Sleep(5000);
            if (CustomFunctions.IsElementPresent(driver, btnStateCountryPicklistWarning))
            {
                driver.FindElement(btnStateCountryPicklistWarning).Click();
                Thread.Sleep(2000);
            }
        }

        public void ClickSaveButtonOfPageSettings()
        {
            Thread.Sleep(2000);
            if(CustomFunctions.IsElementPresent(driver, btnSavePageSetting))
            {
                driver.FindElement(btnSavePageSetting).Click();
            }
        }
        public void ClickNeverUpdateOfPageSettings()
        {
            Thread.Sleep(2000);
            if (CustomFunctions.IsElementPresent(driver, btnNeverUpdate))
            {
                driver.FindElement(btnNeverUpdate).Click();
            }
        }

        // To Search Reports folder
        public string SearchReportsFolder(string file)
        {
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtFindAFolder);
            string reportName = ReadExcelData.ReadData(excelPath, "Report", 1);
            driver.FindElement(txtFindAFolder).SendKeys(reportName);
            Thread.Sleep(3000);
            try
            {
                string result = driver.FindElement(matchedFolderResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedFolderResult).Click();
                Thread.Sleep(4000);
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }

        // To Search Reports And Dashboard
        public string SearchReportsAndDashboard(string file, int row)
        {
            string excelPath = dir + file;
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtFindAReportAndDashboard);
            driver.FindElement(txtFindAReportAndDashboard).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Report", row, 2));
            Thread.Sleep(2000);
            try
            {
                string result = driver.FindElement(matchedReportResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedReportResult).Click();
                Thread.Sleep(2000);
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }

        // To handle alert of pickLists Alert
        public void handlePickListsAlert()
        {
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);

        }

        public void clickHideDetails()
        {
            //Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnHideDetails, 140);
            driver.FindElement(btnHideDetails).Click();
            Thread.Sleep(1000);
        }

        public bool btnShowDetailsPresent()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnShowDetails, 140);
            bool btnPresent = CustomFunctions.IsElementPresent(driver, btnShowDetails);
            return btnPresent;
        }

        public void GetDuplicateCompaniesList(string file, string ReportType, string user)
        {
            string excelPath = dir + file;
            if (ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 2, 2).Contains(ReportType))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, By.XPath($"//*[text()='{user}']/../../../td[1]/input"));
                driver.FindElement(By.XPath($"//*[text()='{user}']/../../../td[1]/input")).Click();
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkboxReportRecord);
                driver.FindElement(chkboxReportRecord).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnDrillDown);
            driver.FindElement(btnDrillDown).Click();

            string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, fromDate);
            driver.FindElement(fromDate).Clear();
            driver.FindElement(fromDate).SendKeys(getDate);
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, toDate);
            driver.FindElement(toDate).Clear();
            driver.FindElement(toDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, btnShowDetails);
            driver.FindElement(btnShowDetails).Click();

        }

        public void GetDuplicateContactsList(string file, string ReportType, string user)
        {
            string excelPath = dir + file;
            if (ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 2, 2).Contains(ReportType))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, By.XPath($"//*[text()='{user}']/../../td[1]/input"));
                driver.FindElement(By.XPath($"//*[text()='{user}']/../../td[1]/input")).Click();
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkboxReportRecord);
                driver.FindElement(chkboxReportRecord).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnDrillDown);
            driver.FindElement(btnDrillDown).Click();

            string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy").Replace('-','/');
            WebDriverWaits.WaitUntilEleVisible(driver, fromDate);
            driver.FindElement(fromDate).Clear();
            driver.FindElement(fromDate).SendKeys(getDate);
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, toDate);
            driver.FindElement(toDate).Clear();
            driver.FindElement(toDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, btnShowDetails);
            driver.FindElement(btnShowDetails).Click();

        }

        public void ClickRunReport()
        {
            string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, fromDate);
            driver.FindElement(fromDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, toDate);
            driver.FindElement(toDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, btnRunReport);
            driver.FindElement(btnRunReport).Click();
            Thread.Sleep(2000);
        }

        public string GetCompanyValue(int row)
        {
            return driver.FindElement(By.XPath($"//*[@id='fchArea']/table/tbody/tr[{row}]/td[4]/a")).Text;
        }

        public string GetContactValue(int row)
        {
            return driver.FindElement(By.XPath($"//*[@id='fchArea']/table/tbody/tr[{row}]/td[4]/a")).Text;
        }

        public int GetCompanyColumnList()
        {
            return driver.FindElements(By.XPath("//*[@id='fchArea']/table/tbody/tr")).Count;
        }

        public int GetContactColumnList()
        {
            return driver.FindElements(By.XPath("//*[@id='fchArea']/table/tbody/tr")).Count;
        }

        public void showDupRecordsSetDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkboxReportRecord);
            driver.FindElement(chkboxReportRecord).Click();

        }

        public string GetDataHygieneFolderTitle()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, DataHygieneFolderTitle, 60);
            string DataHygieneFolder = driver.FindElement(DataHygieneFolderTitle).Text;
            return DataHygieneFolder;            
        }

        public string GetReportDuplicateRuleAccountTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, ReportDuplicateRuleAccount, 60);
            string DuplicateRuleAccountCreatedBy = driver.FindElement(ReportDuplicateRuleAccount).Text;
            return DuplicateRuleAccountCreatedBy;
        }
        
        public string GetCreatedByUserAfterShowDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCreatedByFullName, 60);
            string CreatedByFullName = driver.FindElement(valCreatedByFullName).Text;
            return CreatedByFullName;
            
        }
        public string GetLabelGroupBy()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valLabelGroupByDupRecord, 60);
            string CreatedDateLabel = driver.FindElement(valLabelGroupByDupRecord).Text.Split(':')[0].Trim();
            return CreatedDateLabel;

        }
    }
}
