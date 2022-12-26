using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System.Threading;

namespace SalesForce_Project.Pages.Engagement
{
    class ValuationPeriods : BaseClass
    {
        By btnNewEngValPeriod = By.CssSelector("input[value='New Engagement Valuation Period']");
        By lblNewEngValPeriod = By.CssSelector("h2[class='pageDescription']");
        By txtName = By.CssSelector("input[name*='id30']");
        By lnkValDate = By.CssSelector("tbody > tr:nth-child(3) > td:nth-child(2) > div > span > span > a");
        By lnkClientFinalDeadLine = By.CssSelector("tbody > tr:nth-child(7) > td:nth-child(2) > div > span > span > a");
        By btnSave = By.CssSelector("input[value='Save']");
        By lblEngValPeriodDetail = By.CssSelector("h2[class='mainTitle']");
        By btnNewEngValPeriodPosition = By.CssSelector("input[value='New Eng Valuation Period Position']");
        By btnCompany = By.CssSelector("span[class='lookupInput']>input[id*='CompanyField']");
        By comboAssetClasses = By.CssSelector("select[name*='id66']");
        By comboPositionIG = By.CssSelector("select[name*='PositionIG']");
        By comboPositionSector = By.CssSelector("select[name*='PositionS']");
        By comboToolUtilized = By.CssSelector("select[name*=':AutomationToolUtilizedId']");
        By txtReportFee = By.CssSelector("input[name*='id68']");
        By btnImportPositions = By.CssSelector("input[value='Import Positions']");
        By valPeriod = By.CssSelector("td[id*=':j_id182'] > a");
        By btnAddTeamMember = By.CssSelector("input[value='Add New Team Member']");
        By btnSaveTeamMember = By.CssSelector("input[value='Save Team Members']");
        By btnEdit = By.CssSelector("input[value='Edit']");
        By comboStatus = By.CssSelector("select[name*='PositionStatusID']");
        By msgErrorBox = By.CssSelector("div[class='message errorM3']");
        By msgError1 = By.CssSelector("span[id *= 'j_id18'] > ul > li:nth-child(1)");
        By msgError2 = By.CssSelector("span[id *= 'j_id18'] > ul > li:nth-child(2)");
        By valStatus = By.CssSelector("span[id*='StatusId']");
        By valFeeCompleted = By.CssSelector("span[id*='id66']");
        By valRevenueMonth = By.CssSelector("span[id*='id82']");
        By valCancelMonth = By.CssSelector("span[id*='id83']");
        By valRevenueYear = By.CssSelector("span[id*='id84']");
        By valCancelYear = By.CssSelector("span[id*='id85']");
        By valCancelYear1 = By.CssSelector("span[id*='id85']");
        By valCompletedDate = By.CssSelector("span[id*='id86']");
        By valCancelDate = By.CssSelector("span[id*='id87']");
        By btnBackToValuation = By.CssSelector("input[value='Back To Valuation Period']");
        By valPositionName = By.CssSelector("td[id*='id182']>a");
        By txtUpReportFee = By.CssSelector("input[name*='id38']");
        By btnVoidPosition = By.CssSelector("input[value='Void Position']");
        By msgCancel = By.CssSelector("div[id*='_id5']");
        By btnYes = By.CssSelector("input[value=' Yes ']");
        By linkDel = By.CssSelector("a[name*='id176']");
        By msgSuccess1 = By.CssSelector("div[id*='id8']");

        string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        //To Click New Engagement Valuation Period button
        public string ClickEngValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriod, 60);
            driver.FindElement(btnNewEngValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewEngValPeriod, 60);
            string title = driver.FindElement(lblNewEngValPeriod).Text;
            return title;
        }

        //Enter Engagement Valuation Period details and save it.
        public string EnterAndSaveEngValuationDetails()
        {
            string Name = "VP:" + CustomFunctions.RandomValue();
            driver.FindElement(txtName).SendKeys(Name);
            driver.FindElement(lnkValDate).Click();
            driver.FindElement(lnkClientFinalDeadLine).Click();
            driver.FindElement(btnSave).Click();
            return Name;
        }

        //Get title of Engagement Valuation Period Detail page
        public string GetEngValPeriodDetailTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 60);
            string title = driver.FindElement(lblEngValPeriodDetail).Text;
            return title;
        }

        //Enter Eng Valuation Period Position details
        public string EnterPeriodPositionDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriodPosition, 60);
            driver.FindElement(btnNewEngValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnCompany).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 5));
            driver.FindElement(comboPositionIG).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 8));
            driver.FindElement(comboPositionSector).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 9));
            driver.FindElement(comboToolUtilized).SendKeys("Yes");
            //driver.FindElement(txtReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 7));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngValPeriodDetail, 60);
            string value = driver.FindElement(valPeriod).Text;
            return value;
        }

        //Click on added Engagement Valuation Period Position
        public string ClickAddedValPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriod, 100);
            driver.FindElement(valPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngValPeriodDetail, 100);
            string title = driver.FindElement(lblEngValPeriodDetail).Text;
            return title;
        }

        //To add team members and save it. 
        public void ClickPositionAndSaveTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTeamMember, 80);
            driver.FindElement(btnAddTeamMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMember, 80);
            driver.FindElement(btnSaveTeamMember).Click();
            Thread.Sleep(3000);
        }

        //To update the status of Position
        public string UpdateStatusAndSave()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboStatus).SendKeys("Completed, Generate Accrual");
            driver.FindElement(btnSave).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgErrorBox, 90);
            string error1 = driver.FindElement(msgError1).Text;
            return error1;
        }

        //To fetch 2nd error message
        public string GetErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgErrorBox, 90);
            string error2 = driver.FindElement(msgError2).Text;
            return error2;
        }
        //To get Status of Position
        public string GetPositionStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 90);
            string status = driver.FindElement(valStatus).Text;
            return status;
        }
        //To get Fee Completed of Position
        public string GetFeeCompleted()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFeeCompleted, 120);
            string feeCompleted = driver.FindElement(valFeeCompleted).Text;
            return feeCompleted;
        }
        //To get Revenue Month of Position
        public string GetRevenueMonth()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueMonth, 90);
            string revenueMonth = driver.FindElement(valRevenueMonth).Text;
            return revenueMonth;
        }
        //To get Cancel Month of Position
        public string GetCancelMonth()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelMonth, 90);
            string cancelMonth = driver.FindElement(valCancelMonth).Text;
            return cancelMonth;
        }
        //To get Revenue Year of Position
        public string GetRevenueYear()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueYear, 90);
            string revenueYear = driver.FindElement(valRevenueYear).Text;
            return revenueYear;
        }
        //To get Cancel Year of Position
        public string GetCancelYear()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelYear, 90);
            string cancelYear = driver.FindElement(valCancelYear).Text;
            return cancelYear;
        }
        //To get Cancel Year of Position
        public string GetCancelYearWithDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelYear1, 90);
            string cancelYear = driver.FindElement(valCancelYear1).Text;
            return cancelYear;
        }
        //To get Completed Date of Position
        public string GetCompletedDate()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCompletedDate, 90);
            string compDate = driver.FindElement(valCompletedDate).Text;
            return compDate;
        }
        //To get Cancel Date of Position
        public string GetCancelDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelDate, 90);
            string cancelDate = driver.FindElement(valCancelDate).Text;
            return cancelDate;
        }
        //To  update Status and Report Fee of existing Position
        public string UpdateStatusAndReportFee(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValuation, 60);
            driver.FindElement(btnBackToValuation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valPositionName, 90);
            driver.FindElement(valPositionName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboStatus, 60);
            driver.FindElement(comboStatus).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 14));
            driver.FindElement(txtUpReportFee).Clear();
            driver.FindElement(txtUpReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 11));
            driver.FindElement(btnSave).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess1, 100);
            string msg = driver.FindElement(msgSuccess1).Text;
            return msg;

        }

        //Click Void Position and fetch cancellation message
        public string ClickVoidPositionAndGetMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnVoidPosition);
            driver.FindElement(btnVoidPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgCancel, 70);
            string message = driver.FindElement(msgCancel).Text;
            driver.FindElement(btnYes).Click();
            return message;
        }

        //To delete the position  
        public void DeletePosition()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValuation, 70);
            driver.FindElement(btnBackToValuation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkDel, 60);
            driver.FindElement(linkDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
    }
}


