using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.ActivitiesList
{
    class AddActivity1 : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        By btnAddActivity = By.CssSelector(" input[value='Add Activity']");
        By btnSaveandReturn = By.CssSelector(" input[value='Save and Return to Contact']");
        By txtErrorMessage = By.CssSelector("div[class='messageText']");
        By btnAddActivityOnContactDetail = By.CssSelector("input[value='Add Activity']");
        By comboAcitivityType = By.CssSelector("select[id*='j_id31:eventType']");
        By txtActivitySubject = By.CssSelector("div[class='requiredInput'] > input[id*='j_id53:eventSubject']");
        By txtCompaniesDiscussed = By.XPath("//input[@id='j_id0:frmActivityEvent:activityedit:pbsCompaniesDiscussed:j_id168:inputTxtId']");
        By selCompaniesDiscussed = By.XPath("//a[@class='ui-corner-all']/b");
        By txtLookupOpportunity = By.CssSelector("span[id*='j_id190:j_id192']  > input[type='text']");
        By selLookupOpportunity = By.XPath("(//a[@class='ui-corner-all']/b)[2]");
        By chkBoxScheduleFollowUp = By.CssSelector("input[id*='isFollowup']");
        By comboFollowupType = By.CssSelector("select[id*='followupType']");
        By txtFollowUpDate = By.CssSelector("input[id*='followupStartDate']");
        By txtFollowUpComment = By.CssSelector("textarea[name*='j_id232:j_id234']");


        By lnkContacts = By.CssSelector("a[title*='Contacts Tab']");
        By valDefaultHLAttendee = By.CssSelector("tbody[id*='pbsHLEmployees'] > tr > td:nth-child(3) > span");
        By btnActivitySave = By.CssSelector("td[class='pbButton '] > input:nth-child(1)");
        By btnAddNewContact = By.CssSelector("input[id*='newContact']");
        By chkBoxInheritCompanyAdd = By.CssSelector("input[id*='InheritCompanyAddress']");
        By btnSaveIgnoreAlert = By.CssSelector("input[value='Save (Ignore Alert)']");
        By btnGo = By.CssSelector("input[id*='btnGo']");
        By selFirstOption = By.CssSelector("td[id*='tblResults:0:j_id49'] > a");//("tr[class='dataRow even first'] > td:first-child a");
        By firstName = By.CssSelector("input[id *='FirstName']");
        By lastName = By.CssSelector("input[id*='LastName']");
        By comboSalutation = By.CssSelector("select[id*='Salutation']");
        By txtEmail = By.CssSelector("input[id*='pgBlockSectionAcctInfo:Email']");
        By txtPhone = By.CssSelector("input[id*='pgBlockSectionAcctInfo:Phone']");
        By txtSearchBox = By.CssSelector("input[id*='txtSearch']");
        By btnSave = By.CssSelector("div.pbHeader input[value='Save']");
        By popUpLookup = By.CssSelector("[title^='Company Name Lookup']");
        By newContactPopup = By.CssSelector("h2[class='mainTitle']");
        By newCompanyPopup = By.CssSelector("h2[class='mainTitle']");
        By lnkViewActivity = By.CssSelector("span[id*='j_id18']>a:nth-of-type(1)");

        By txtDate = By.CssSelector("input[name*='startDate']");
        By lnkActivities = By.CssSelector("span[id='activityList_link']");
        By lnkViewAll = By.XPath("//a[text()='View All']");////a[text()='View All']
        By headingActivitiesPage = By.CssSelector("h2[class='pageDescription']");

        By chkBoxNoExternalContact = By.CssSelector("input[id*='chkNoExtnContact']");
        By lnkNext = By.CssSelector("a[name*='j_id89']");
        By lnkPrevious = By.CssSelector("a[name*='j_id86']");
        By lnkPreviousDisabled = By.XPath("//span[contains(text(),'Previous')]");
        By errPage = By.CssSelector("span[id='theErrorPage:theError']");
        By drpdwnSelectType = By.CssSelector("select[name*='thefrm:pbActivityLog']");
        By btnSearch = By.CssSelector("input[name*='btnSearch']");
        By txtActivityType = By.CssSelector("span[id *= 'j_id36']");
        By btnAllActivities= By.CssSelector("input[name*='j_id9:j_id12']");
        By txtActivitySub= By.CssSelector("span[id*='j_id73']");
        By txtActivityDate = By.CssSelector("input[type='date']");
        By txtActivitSubjectOnViewAllPage= By.CssSelector("span[id*='j_id71']");
        By txtNoActivity = By.CssSelector("span[id*='noActivities']>center>h2>label");
        By txtActivityCount = By.CssSelector("span[id*='panActivitiesList']>div>table:nth-of-type(2)>tbody>tr>td:nth-of-type(4)>label");
        
        //Click Activities List Tab
        public void ClickAddActivityBtn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity);
            driver.FindElement(btnAddActivity).Click();
        }

        public void ClickSaveandReturnBtn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveandReturn);
            driver.FindElement(btnSaveandReturn).Click();
        }

        public string GetErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtErrorMessage);
            string errmsg=driver.FindElement(txtErrorMessage).Text;
            return errmsg;
        }


        //Add activity from contact detail page
        public string AddActivityFromContactDetail(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivityOnContactDetail);
            driver.FindElement(btnAddActivityOnContactDetail).Click();
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadData(excelPath, "Activity", 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 2));

            string valCompaniesDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 3);
            driver.FindElement(txtCompaniesDiscussed).SendKeys(valCompaniesDiscussed);
            Thread.Sleep(4000);
            driver.FindElement(selCompaniesDiscussed).Click();
            Thread.Sleep(1000);
            string valRelatedOpportunity = ReadExcelData.ReadData(excelPath, "Activity", 4);
            driver.FindElement(txtLookupOpportunity).SendKeys(valRelatedOpportunity);
            Thread.Sleep(6000);
            driver.FindElement(selLookupOpportunity).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkBoxScheduleFollowUp);
            driver.FindElement(chkBoxScheduleFollowUp).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, comboFollowupType);
            driver.FindElement(comboFollowupType).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 5));

            string getDate = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtFollowUpDate);
            driver.FindElement(txtFollowUpDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, txtFollowUpComment);
            driver.FindElement(txtFollowUpComment).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 6));

            Thread.Sleep(3000);
           
            return getDate;
        }


        public string AddFutureActivityWithNewContact(string file)
        {
            //extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity);
            driver.FindElement(btnAddActivity).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadData(excelPath, "Activity", 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 2));

            //Add New Contact
            driver.FindElement(btnAddNewContact).Click();

            driver.SwitchTo().Frame("iframeContentId");

            WebDriverWaits.WaitUntilEleVisible(driver, newContactPopup);
            bool isContactPopupPresent = CustomFunctions.IsElementPresent(driver, newContactPopup);
            Assert.AreEqual(true, isContactPopupPresent);
            //extentReports.CreateLog("Add New Contact page to create new Contact is displayed ");

            string checkBoxValue = driver.FindElement(chkBoxInheritCompanyAdd).GetAttribute("checked");
            Assert.AreEqual("true", checkBoxValue);
            //extentReports.CreateLog("The field “Inherit Company Address” under Address Information section is checked by default ");
            // To create contact  
            CustomFunctions.ActionClicks(driver, popUpLookup, 20);
            driver.FindElement(popUpLookup).Click();
            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);

            //string excelPath = dir + file;
            // Enter value in search box
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchBox);
            driver.FindElement(txtSearchBox).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
            //Click on Go button
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            // Select first option
            WebDriverWaits.WaitUntilEleVisible(driver, selFirstOption);
            CustomFunctions.ActionClicks(driver, selFirstOption);
            // Switch back to default window

            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().Frame("iframeContentId");
            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, firstName, 40);
            driver.FindElement(firstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, lastName, 40);
            driver.FindElement(lastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 40);
            driver.FindElement(txtEmail).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 5));
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhone, 40);
            driver.FindElement(txtPhone).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 6));

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
            if (CustomFunctions.IsElementPresent(driver, btnSaveIgnoreAlert))
            {
                driver.FindElement(btnSaveIgnoreAlert).Click();
            }
            driver.SwitchTo().DefaultContent();

            //Get value of HL Attendee
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultHLAttendee, 60);
            string HL_Attendee = driver.FindElement(valDefaultHLAttendee).Text;

            string valCompaniesDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 6);
            driver.FindElement(txtCompaniesDiscussed).SendKeys(valCompaniesDiscussed);
            Thread.Sleep(3000);
            //WebDriverWaits.WaitUntilEleVisible(driver, chkBoxNoExternalContact);
            //driver.FindElement(chkBoxNoExternalContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, selCompaniesDiscussed);
            driver.FindElement(selCompaniesDiscussed).Click();
            driver.FindElement(txtDate).Clear();
            string getDate = DateTime.Today.AddDays(3).ToString("MM/dd/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtDate);
            driver.FindElement(txtDate).SendKeys(getDate);
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
            return HL_Attendee;
        }

        public void ClickViewActivity() {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewActivity);
            driver.FindElement(lnkViewActivity).Click();

        }
        public void VerifyViewAllLink()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, lnkActivities);
            driver.FindElement(lnkActivities).Click();
            driver.SwitchTo().Frame("066i0000004ZLbF");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAll);
            Boolean bol = driver.FindElement(lnkViewAll).Displayed;
            Assert.IsTrue(bol);
            driver.SwitchTo().DefaultContent();

        }
        public void VerifyViewAllLinkOnHomePage() {

            
            driver.SwitchTo().Frame("066i0000004Z1AR");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAll);
           Boolean bol= driver.FindElement(lnkViewAll).Displayed;
            Assert.IsTrue(bol);
            driver.SwitchTo().DefaultContent();

        }
        public void ClickViewAllLink() {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkActivities);
            driver.FindElement(lnkActivities).Click();
            driver.SwitchTo().Frame("066i0000004ZLbF");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAll);
            driver.FindElement(lnkViewAll).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            string text = driver.FindElement(headingActivitiesPage).Text;
            Assert.AreEqual(text, "Activities");
            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().DefaultContent();
            
          

        }
        public void VerifyPreviousNextLink() {

            driver.SwitchTo().Frame("066i0000004ZLbF");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAll);
            driver.FindElement(lnkViewAll).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);
            

            Boolean bol1 = driver.FindElement(lnkPreviousDisabled).Displayed;
            Assert.IsTrue(bol1);
            //Console.WriteLine(bol1);

            
            WebDriverWaits.WaitUntilEleVisible(driver, lnkNext);
            driver.FindElement(lnkNext).Click();
            Thread.Sleep(2000);
           
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPrevious);
            Boolean bol=driver.FindElement(lnkPrevious).Enabled;
            Assert.IsTrue(bol);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPrevious);
            driver.FindElement(lnkPrevious).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkPreviousDisabled);

            Boolean bol2 = driver.FindElement(lnkPreviousDisabled).Displayed;
            Assert.IsTrue(bol2);
            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().DefaultContent();
            
        }
        public void VerifyFilter(string file) {

            driver.SwitchTo().Frame("066i0000004ZLbF");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAll);
            driver.FindElement(lnkViewAll).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string activitytypeexl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityTypeFilter",2, 1);
                CustomFunctions.SelectByText(driver, driver.FindElement(drpdwnSelectType), activitytypeexl);
            driver.FindElement(btnSearch).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivityType,120);
            string activitytype = driver.FindElement(txtActivityType).Text;
            Assert.AreEqual(activitytype, activitytypeexl);

            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().DefaultContent();

        }

        public void ClickViewAllLinkOnHomePage()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='066i0000004Z1AR']")));
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAll);
            driver.FindElement(lnkViewAll).Click();
            Thread.Sleep(5000);

            CustomFunctions.SwitchToWindow(driver, 1);
            string text = driver.FindElement(headingActivitiesPage).Text;
            Assert.AreEqual(text, "Activities");
            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().DefaultContent();
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

        }

        public void AddActivityFromViewAllLink() {
            driver.SwitchTo().Frame("066i0000004Z1AR");
            //driver.FindElement(btnAllActivities).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAll);
           // driver.FindElement(lnkViewAll).Click();
            driver.FindElement(lnkViewAll).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            string text = driver.FindElement(headingActivitiesPage).Text;
            Assert.AreEqual(text, "Activities");
            
            
        }

        public void VerifyPrivateActivityonHomePage(string file) {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.SwitchTo().Frame("066i0000004Z1AR");
            string subjecttext = driver.FindElement(txtActivitySub).Text;
            subjecttext.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2));
            driver.SwitchTo().DefaultContent();
        }

        public void VerifyDateFilter(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, lnkActivities);
            driver.FindElement(lnkActivities).Click();
            driver.SwitchTo().Frame("066i0000004ZLbF");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAll);
            driver.FindElement(lnkViewAll).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            
            string text = driver.FindElement(headingActivitiesPage).Text;
            Assert.AreEqual(text, "Activities");

            string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            driver.FindElement(txtActivityDate).SendKeys(getDate);
            driver.FindElement(btnSearch).Click();
            

            (driver.FindElement(txtActivitSubjectOnViewAllPage).Text).Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2));


            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().DefaultContent();
        }


        public string GetActivityCount()
        {
            string text = "0";
            driver.SwitchTo().Frame("066i0000004Z1AR");
            
            text = driver.FindElement(txtActivityCount).Text;
            
            driver.SwitchTo().DefaultContent();
            return text;
        }
    }

}