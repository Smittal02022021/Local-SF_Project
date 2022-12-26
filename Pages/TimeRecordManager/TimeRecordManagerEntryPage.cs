using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.TimeRecordManager
{
    class TimeRecordManagerEntryPage : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();


        By tabStaffTimeSheet = By.CssSelector("li[id*='staff'] > a");
        By tabWeeklyEntryMatrix = By.CssSelector("li[id*='mass'] > a");
        By tabSummaryLogs = By.CssSelector("li[id*='summary'] > a");
        By tabDetailLogs = By.CssSelector("li[id*='view'] > a");
        By comboSelectProject = By.CssSelector("select[class='slds-select']");
        By txtEnterSundayTime = By.CssSelector("table > tr > td:nth-child(2) > div[class='activityRecordEntry'] > div > div > div > input");
        By comboSelectActivity = By.CssSelector("table > tr > td:nth-child(2) > div[class='activityRecordEntry'] > div > div:nth-child(2) > div > select");
        By comboLogActivity = By.CssSelector("div[class*='medium'] > select[class*='uiInput--select']");
        By btnClearTimeEntry = By.CssSelector("div[data-key*='a4C'] button[class*='slds-button']");
        By valTimeRecordManagerTitle = By.CssSelector("div[class='slds-text-heading--medium']");
        By comboDefaultSelectProject = By.CssSelector("select[class='slds-select'] > option:nth-child(1)");
        By txtSummaryLogsAddRecordDate = By.CssSelector("input[class*='date input']");
        By txtEnterSummaryLogEntryTime = By.CssSelector("input[class*='uiInput--input']");
        By linkSalesforceAdministrator = By.XPath("//*[text()='Salesforce Administrator']/../../p[@data-aura-rendered-by='983:78;a']/a");
        By valSelectedStaffTitle = By.CssSelector("div[class='timeSheet'] > div[class*='heading']");
        By btnAdd = By.CssSelector("span[dir='ltr']");
        By valProjectOrEngagement = By.CssSelector("tr[class*='parent'] > td:nth-child(2)");
        By valActivity = By.CssSelector("tr[class*='parent'] > td:nth-child(3)");
        By valActivityInDetailLogs = By.CssSelector("tr[class*='parent'] > td:nth-child(3) > select > option[selected='selected']");
        By valDefaultDollar = By.CssSelector("tr[class*='parent'] > td:nth-child(5) > span:nth-child(2)");
        By valDefaultRateDetailLogs = By.CssSelector("tr[class*='parent'] > td:nth-child(5) > div > input");
        By valEnteredHours = By.CssSelector("tr[class*='parent'] > td:nth-child(4) > span");
        By valEnteredHoursInDetailLogs = By.CssSelector("tr[class*='parent'] > td:nth-child(4) > div > input");
        By valTotalAmount = By.CssSelector("tr[class*='parent'] > td:nth-child(6) > span:nth-child(2)");
        By btnCross = By.CssSelector("div[data-key*='a4C'] button[class*='slds-button']");
        By btnCrossDeleteRecord = By.CssSelector("td[class*='slds-cell-shrink'] > button[class*='slds-button']");
        By tableSummaryLog = By.CssSelector("table[class='slds-table'] > tbody");
        By comboBoxProjectInLog = By.CssSelector("select[class='slds-select'] > option");
        By txtWeeklyEntry = By.XPath("//div[starts-with(@data-key,'a4C')]//parent::div/div/input");
        By drpdownFuturePeriod = By.XPath("//div[@data-aura-class='cTimeRecordPeriodPicker']/select/option[@selected='selected']//preceding::option");
        By txtCurrentTimePeriod = By.CssSelector("div[class*='cTimeRecordPeriodPicker']");
        By drpdwnSelectPreSetTemplate = By.CssSelector("select[class*='slds-input']");
        By EnterDateSummaryLog = By.CssSelector("input[class*='date input']");
        By EnterHoursSummaryLog = By.CssSelector("input[placeholder='hrs']");
        By AddBtnSummaryLog = By.CssSelector("span[class*='bBody']");
        By TxtSuccessMsg = By.XPath("//h4[text()='Success -']");


        public void GoToWeeklyEntryMatrix()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(20000);
        }

        public void EnterWeeklyEntryMatrix(string selectProject, string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(6000);
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            driver.FindElement(comboSelectProject).SendKeys(selectProject);

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSundayTime);
            driver.FindElement(txtEnterSundayTime).SendKeys(ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity);
            driver.FindElement(comboSelectActivity).SendKeys(ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 3));
        }
        public void GoToSummaryLogs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);
        }

        public void GoToSummaryLog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(5000);
        }

        public void EnterSummaryLogs(string selectProject, string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            driver.FindElement(comboSelectProject).SendKeys(selectProject);

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "SummaryLogs", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(ReadExcelData.ReadData(excelPath, "SummaryLogs", 3));
        }

        public void EnterSummaryLogs1(string selectProject, string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            driver.FindElement(comboSelectProject).SendKeys(selectProject);

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "SummaryLogs", 2));

        }

        public void ClickAddButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog, 120);
        }

        public void GoToDetailLogs()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            driver.FindElement(tabDetailLogs).Click();
            Thread.Sleep(5000);
        }

        public void EnterDetailLogs(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            driver.FindElement(tabDetailLogs).Click();
            Thread.Sleep(5000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            driver.FindElement(comboSelectProject).SendKeys(selectProject);

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "DetailLogs", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(ReadExcelData.ReadData(excelPath, "DetailLogs", 3));
        }

        public void EnterDetailLogs1(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            driver.FindElement(tabDetailLogs).Click();
            Thread.Sleep(5000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            driver.FindElement(comboSelectProject).SendKeys(selectProject);

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "DetailLogs", 2));
        }

        public string GetSundayTimeEntry()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSundayTime, 120);
            string sundayTimeEntryValue = driver.FindElement(txtEnterSundayTime).GetAttribute("value");
            return sundayTimeEntryValue;
        }

        public string GetSummaryLogsTimeEntry()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime, 80);
            string SummaryLogEntryTime = driver.FindElement(txtEnterSummaryLogEntryTime).GetAttribute("value");
            return SummaryLogEntryTime;
        }
        public string GetDetailLogsTimeEntry()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime, 80);
            string DetailLogEntryTime = driver.FindElement(txtEnterSummaryLogEntryTime).GetAttribute("value");
            return DetailLogEntryTime;
        }
        public void ClearTimeRecord()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClearTimeEntry);
            driver.FindElement(btnClearTimeEntry).Click();
            Thread.Sleep(5000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetPeopleOrUserName(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string peopleFromExcel = ReadExcelData.ReadData(excelPath, "Users", 2).Split('*')[0].Trim();
            return peopleFromExcel;
        }

        public string GetTimeRecordManagerTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTimeRecordManagerTitle, 80);
            string TimeRecordManagerTitle = driver.FindElement(valTimeRecordManagerTitle).Text.Split(':')[0].Trim();
            return TimeRecordManagerTitle;
        }

        public string GetDefaultSelectedProjectOption()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboDefaultSelectProject, 80);
            string DefaultSelectProject = driver.FindElement(comboDefaultSelectProject).Text;
            return DefaultSelectProject;
        }

        public void SelectStaffMember(string name)
        {
            Thread.Sleep(8000);
            driver.FindElement(By.XPath($"//a[text()='{name}']")).Click();
            Thread.Sleep(20000);
        }

        public string GetSelectedStaffMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedStaffTitle, 80);
            string SelectedStaffTitle = driver.FindElement(valSelectedStaffTitle).Text;
            return SelectedStaffTitle;
        }

        public string GetProjectOrEngagementValue()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectOrEngagement, 80);
            string ProjectOrEngagement = driver.FindElement(valProjectOrEngagement).Text;
            return ProjectOrEngagement;
        }

        public string GetSelectedActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valActivity, 80);
            string ActivityValue = driver.FindElement(valActivity).Text;
            return ActivityValue;
        }

        public string GetSelectedActivityOnDetailLog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valActivityInDetailLogs, 80);
            string ActivityValueInDetail = driver.FindElement(valActivityInDetailLogs).Text;
            return ActivityValueInDetail;
        }

        public string GetDefaultRateForStaff()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultDollar, 80);
            string DefaultDollar = driver.FindElement(valDefaultDollar).Text;
            return DefaultDollar;
        }

        public string GetEnteredHoursInDetailLogs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHoursInDetailLogs, 80);
            string enteredHours = driver.FindElement(valEnteredHoursInDetailLogs).GetAttribute("value");
            return enteredHours;
        }

        public string GetDefaultRateForStaffInDetailLogs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultRateDetailLogs, 80);
            string DefaultDollar = driver.FindElement(valDefaultRateDetailLogs).GetAttribute("value");
            return DefaultDollar;
        }

        public string GetEnteredHoursInSummaryLog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHours, 80);
            string EnteredHours = driver.FindElement(valEnteredHours).Text;
            return EnteredHours;
        }

        public double GetDefaultRateForStaffValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultDollar, 80);
            return double.Parse(driver.FindElement(valDefaultDollar).Text);
        }

        public double GetDefaultRateForStaffValueInDetailLogs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultRateDetailLogs, 80);
            return double.Parse(driver.FindElement(valDefaultRateDetailLogs).GetAttribute("value"));
        }

        public double GetEnteredHoursInSummaryLogValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHours, 80);
            return double.Parse(driver.FindElement(valEnteredHours).Text);
        }

        public double GetEnteredHoursInDetailLogValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHoursInDetailLogs, 80);
            return double.Parse(driver.FindElement(valEnteredHoursInDetailLogs).GetAttribute("value"));
        }

        public void EditEnteredHoursInDetailLog(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(6000);
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHoursInDetailLogs, 80);
            driver.FindElement(valEnteredHoursInDetailLogs).Clear();
            driver.FindElement(valEnteredHoursInDetailLogs).SendKeys(ReadExcelData.ReadData(excelPath, "Update_Hours", 1));
            Thread.Sleep(2000);
        }

        public double GetTotalAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalAmount, 80);
            return double.Parse(driver.FindElement(valTotalAmount).Text);
        }

        //Delete time entry from weekly entry matrix
        public void DeleteTimeEntry()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(2000);
            IList<IWebElement> elements = driver.FindElements(btnCross);
            for (int i = 0; i < elements.Count; i++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCross);
                driver.FindElement(btnCross).Click();
                Thread.Sleep(5000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(2000);
            }
        }

        public void RemoveRecordFromDetailLogs()
        {
            IList<IWebElement> elements = driver.FindElements(btnCrossDeleteRecord);
            for (int i = 0; i < elements.Count; i++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCrossDeleteRecord);
                driver.FindElement(btnCrossDeleteRecord).Click();
                Thread.Sleep(3000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(5000);
            }
        }

        public bool VerifyProjectDisableAfterOppToEngagementConversion()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            bool isDisabled = CustomFunctions.isAttributePresent(driver, driver.FindElement(comboSelectProject), "disabled");
            return isDisabled;
        }

        public string VerifyEngagementProjectExist(string engagementProject)
        {
            //IWebElement 
            IList<IWebElement> elements = driver.FindElements(comboSelectProject);

            if (elements[1].Text.Contains(engagementProject))
            {
                return "Project is present";
            }
            else
            {
                return "Project not present";
            }
        }

        public string VerifyEngagementProjectExistInLogs(string engagementProject)
        {
            //IWebElement 
            IWebElement element = driver.FindElement(comboSelectProject);

            if (element.Text.Contains(engagementProject))
            {
                return "Project is present";
            }
            else
            {
                return "Project not present";
            }
        }

        public void SelectProjectWeeklyEntryMatrix(string selectProject, string file)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject, 220);
            driver.FindElement(comboSelectProject).SendKeys(selectProject);
            Thread.Sleep(3000);
        }

        //Get Border color from entered time entry
        public void GetBorderColorTimeEntry(string weekday)
        {

            Thread.Sleep(20000);

            switch (weekday)
            {
                case "Mon":

                    string color = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color);
                    Assert.AreEqual(color, "rgb(194, 57, 52)");
                    break;
                case "Tue":

                    string color1 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color1);
                    Assert.AreEqual(color1, "rgb(194, 57, 52)");
                    break;
                case "Wed":

                    string color2 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color2);
                    Assert.AreEqual(color2, "rgb(194, 57, 52)");
                    break;


                case "Thu":

                    string color3 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color3);
                    Assert.AreEqual(color3, "rgb(194, 57, 52)");
                    break;
                case "Fri":

                    string color4 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color4);
                    Assert.AreEqual(color4, "rgb(194, 57, 52)");
                    break;

                case "Sat":

                    string color5 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color5);
                    Assert.AreEqual(color5, "rgb(194, 57, 52)");
                    break;
                case "Sun":

                    string color6 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color6);
                    Assert.AreEqual(color6, "rgb(194, 57, 52)");
                    break;
            }
        }

        //Log Future Date Hours
        public string LogFutureDateHours(string file)
        {

            DateTime Time = DateTime.Now.AddDays(1);
            string format = "ddd";
            string week = Time.ToString(format);

            Console.WriteLine(Time.ToString(format));

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(6000);
            string excelPath = dir + file;


            string txtHours = ReadExcelData.ReadData(excelPath, "Update_Hours", 1).ToString();
            Console.WriteLine(txtHours);


            switch (Time.ToString(format))
            {
                case "Mon":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Tue":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Wed":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div[2]/div/select")), 2);
                    break;


                case "Thu":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Fri":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div[2]/div/select")), 2);

                    break;


                case "Sat":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Sun":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div[2]/div/select")), 2);

                    break;
            }
            return week;

        }

        //Compare weekly time entry 
        public void CompareWeeklyTimeEntry(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(2000);
            IList<IWebElement> elements = driver.FindElements(btnCross);
          
                WebDriverWaits.WaitUntilEleVisible(driver, txtWeeklyEntry);
                string txt = driver.FindElement(txtWeeklyEntry).GetAttribute("value");
                string ExlTimer = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Timer", 2,2).ToString();
                Assert.AreEqual(txt, ExlTimer);

          

        }

        public void SelectFutureTimePeriod()
        {

            driver.FindElement(txtCurrentTimePeriod).Click();
            driver.FindElement(drpdownFuturePeriod).Click();
        }

        //Verify activity drop down for future time period
        public void VerifyActivityDropDownForFuturePeriod(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IWebElement recordDropdown = driver.FindElement(drpdwnSelectPreSetTemplate);
            SelectElement select = new SelectElement(recordDropdown);
            int RowPreSetTemplateList = ReadExcelData.GetRowCount(excelPath, "Activity_List");

            for (int i = 2; i <= RowPreSetTemplateList; i++)
            {
                IList<IWebElement> options = select.Options;
                IWebElement PReSetTemplateOption = options[i - 2];
                string preSetListExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity_List", i, 1);
                Assert.AreEqual(PReSetTemplateOption.Text, preSetListExl);
            }
        }

        //Log Current Date Hours
        public string LogCurrentDateHours(string file)
        {
            DateTime Time = DateTime.Now.AddDays(0);
            string format = "ddd";
            string week = Time.ToString(format);

            Console.WriteLine(Time.ToString(format));

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(6000);
            string excelPath = dir + file;

            string txtHours = ReadExcelData.ReadData(excelPath, "Update_Timer", 1).ToString();
            Console.WriteLine(txtHours);

            switch (Time.ToString(format))
            {
                case "Mon":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div[2]/div/select")), 2);
                    Thread.Sleep(5000);
                    break;

                case "Tue":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div[2]/div/select")), 2);
                    Thread.Sleep(5000);
                    break;

                case "Wed":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div[2]/div/select")), 2);
                    Thread.Sleep(5000);
                    break;


                case "Thu":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div[2]/div/select")), 2);
                    Thread.Sleep(5000);
                    break;

                case "Fri":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div[2]/div/select")), 2);
                    Thread.Sleep(5000);
                    break;


                case "Sat":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div[2]/div/select")), 2);
                    Thread.Sleep(5000);
                    break;

                case "Sun":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div[2]/div/select")), 2);
                    Thread.Sleep(5000);
                    break;
            }
            return week;
        }

        public void EnterSummaryLogHours(string file) {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string TodayDate = DateTime.Now.AddDays(0).ToString("MMM dd, yyyy");
            driver.FindElement(EnterDateSummaryLog).SendKeys(TodayDate);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            driver.FindElement(comboSelectProject).SendKeys(ReadExcelData.ReadData(excelPath, "Project_Title", 1));

            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(ReadExcelData.ReadData(excelPath, "Project_Title", 2));
            driver.FindElement(EnterHoursSummaryLog).SendKeys(ReadExcelData.ReadData(excelPath, "Update_Hours", 2));
            driver.FindElement(AddBtnSummaryLog).Click();
            Thread.Sleep(5000);

        }
        
        public bool VerifySuccessMsgDisplay()
        {
            try
            {
                driver.FindElement(TxtSuccessMsg);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        } }



    }


    

