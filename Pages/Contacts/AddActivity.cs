using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.Contact
{
    class AddActivity : BaseClass
    {
        By lnkContacts = By.CssSelector("a[title*='Contacts Tab']");
        By btnAddActivity = By.CssSelector("td[class='pbButton center'] > input[value='Add Activity']");
        By comboAcitivityType = By.CssSelector("select[id*='j_id31:eventType']");
        By txtActivitySubject = By.CssSelector("div[class='requiredInput'] > input[id*='j_id53:eventSubject']");
        By btnActivitySave = By.CssSelector("td[class='pbButton '] > input:nth-child(1)");
        By errMsgNoExternalContact = By.CssSelector("td.messageCell:nth-child(2) > div");
        By errMsgCompaniesDiscussedRequired = By.CssSelector("td.messageCell:nth-child(2) > div");
        By chkBoxNoExternalContact = By.CssSelector("input[id*='chkNoExtnContact']");
        By txtInternalMeetingWMentee = By.CssSelector("td[class='data2Col first last'] > span > input[type='text']");
        By selInternalMeetingWMentee = By.XPath("//a[normalize-space()='John Taylor']");
        By txtCompaniesDiscussed = By.XPath("//input[@id='j_id0:frmActivityEvent:activityedit:pbsCompaniesDiscussed:j_id168:inputTxtId']");
        By selCompaniesDiscussed = By.XPath("//a[@class='ui-corner-all']/b");
        By selLookupContact = By.XPath("/html/body/ul[6]/li");
       // By selLookupContact = By.CssSelector("body > ul:nth-child(8)");


          By valCompanyDiscussed = By.CssSelector("span[id*='j_id85:0:j_id117']");
        //By btnAddActivityOnContactDetail = By.CssSelector("td[id*='topButtonRow'] > input[value='Add Activity']");
        By btnAddActivityOnContactDetail = By.CssSelector("input[value='Add Activity']");
        By txtLookupOpportunity = By.CssSelector("span[id*='j_id190:j_id192']  > input[type='text']");
        By selLookupOpportunity = By.XPath("(//a[@class='ui-corner-all']/b)[2]");
        By chkBoxScheduleFollowUp = By.CssSelector("input[id*='isFollowup']");
        By comboFollowupType = By.CssSelector("select[id*='followupType']");
        By txtFollowUpDate = By.CssSelector("input[id*='followupStartDate']");
        By comboFollowUpStartTime = By.CssSelector("select[id*='followupStartTime']");
        By comboFollowUpEndTime = By.CssSelector("select[id*='followupEndTime']");
        By txtFollowUpComment = By.CssSelector("textarea[name*='j_id228:j_id230']");
        By btnCancel = By.CssSelector("td[class='pbButton '] > input[value='Cancel']");
        By valDefaultExternalContact = By.CssSelector("td[id*='j_id84:0:j_id90'] > a");
        By valDefaultHLAttendee = By.XPath("//h3[text()='HL Attendees']/following::div/table/tbody/tr/td/table/tbody/tr/td[3]/span");
        By valDefaultHLAttendee1 = By.XPath("//h3[text()='HL Attendees']/following::div/table/tbody/tr/td/table/tbody/tr/td[3]/span");
        By txtDescription = By.CssSelector("textarea[name*='activitydetails:j_id55']");
        By txtInternalNotes = By.CssSelector("textarea[name*='activitydetails:j_id56']");
        By chkboxPrivate = By.CssSelector("input[id*='j_id31:j_id37']");
        By btnAddNewContact = By.CssSelector("input[id*='newContact']");
        By btnAddNewCompany = By.CssSelector("input[id*='newCompany']");
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
        By chkBoxInheritCompanyAdd = By.CssSelector("input[id*='InheritCompanyAddress']");
        By btnSaveIgnoreAlert = By.CssSelector("input[value='Save (Ignore Alert)']");
        By txtExternalLookupContact = By.CssSelector("input[id*='inputContactId'][type='text']");
        By txtCompanyName = By.CssSelector("#j_id0\\:j_id1\\:j_id2\\:formId\\:newAccount\\:newAccountSection\\:j_id143");

        //ExtentReport extentReports = new ExtentReport();
        ContactCreatePage createContact = new ContactCreatePage();
        
        public string AddAnActivityWithNewContact(string file)
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

            // To create contact  
            CustomFunctions.ActionClicks(driver, popUpLookup, 20);
            driver.FindElement(popUpLookup).Click();
            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);

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
            if(CustomFunctions.IsElementPresent(driver, btnSaveIgnoreAlert))
            {
                driver.FindElement(btnSaveIgnoreAlert).Click();
            }
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(3000);

            //Get value of HL Attendee
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultHLAttendee, 60);
            string HL_Attendee = driver.FindElement(valDefaultHLAttendee).Text;

            string valCompaniesDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 6);
            driver.FindElement(txtCompaniesDiscussed).SendKeys(valCompaniesDiscussed);
            Thread.Sleep(3000);
            driver.FindElement(selCompaniesDiscussed).Click();

            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
            return HL_Attendee;
        }

        public string AddAnActivityWithNewCompany(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            //extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity);
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(1000);
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadData(excelPath, "Activity", 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 2));

            //Add Internal Notes
            driver.FindElement(txtInternalNotes).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 5));
            //Add External Attendee
            WebDriverWaits.WaitUntilEleVisible(driver, txtExternalLookupContact,120);
            driver.FindElement(txtExternalLookupContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7));
            WebDriverWaits.WaitUntilEleVisible(driver, selLookupContact);
            driver.FindElement(selLookupContact).Click();
            Thread.Sleep(4000);
            //Add New Contact
            driver.FindElement(btnAddNewCompany).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().Frame("iframeContentId");
            Thread.Sleep(3000);
            // WebDriverWaits.WaitUntilEleVisible(driver, newContactPopup);
            bool isCompanyPopupPresent = CustomFunctions.IsElementPresent(driver, newCompanyPopup);
            Assert.AreEqual(true, isCompanyPopupPresent);
            //extentReports.CreateLog("Add New Company page to create new company is displayed ");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 8));
            
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
            
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
            return HL_Attendee;
        }

        public string AddAnActivity(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity);
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(1000);
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadData(excelPath, "Activity", 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 2));

            //Get value of HL Attendee
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultHLAttendee, 60);
            string HL_Attendee = driver.FindElement(valDefaultHLAttendee).Text;

            if (ReadExcelData.ReadData(excelPath, "Users", 1).Contains("Internal"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtInternalMeetingWMentee);
                driver.FindElement(txtInternalMeetingWMentee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 3));
                Thread.Sleep(5000);
                CustomFunctions.SelectValueFromDropdown(driver, ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 3)).Click();
            }
            else
            {
                Thread.Sleep(1000);
               WebDriverWaits.WaitUntilEleVisible(driver, chkBoxNoExternalContact);
                driver.FindElement(chkBoxNoExternalContact).Click();

                string valCompaniesDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 6);
                driver.FindElement(txtCompaniesDiscussed).SendKeys(valCompaniesDiscussed);
                Thread.Sleep(3000);
                driver.FindElement(selCompaniesDiscussed).Click();
            }
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();

            return HL_Attendee;
        }

        public void AddPrivateActivity(string file)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivityOnContactDetail,120);
            driver.FindElement(btnAddActivityOnContactDetail).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadData(excelPath, "Activity", 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 2));

            driver.FindElement(chkboxPrivate).Click();
            driver.FindElement(txtDescription).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 3));
            driver.FindElement(txtInternalNotes).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 5));
            string valCompaniesDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 6);
            driver.FindElement(txtCompaniesDiscussed).SendKeys(valCompaniesDiscussed);
            Thread.Sleep(3000);
            driver.FindElement(selCompaniesDiscussed).Click();

            Thread.Sleep(3000);


            try {
                Console.WriteLine(driver.FindElement(By.CssSelector("td[id*='j_id84:0:j_id90'] > a")).Text);
                (driver.FindElement(By.CssSelector("td[id*='j_id84:0:j_id90'] > a")).Text).Equals("Test External");
            }
           catch(Exception e)
            {
                //Add External Attendee
                WebDriverWaits.WaitUntilEleVisible(driver, txtExternalLookupContact, 120);
                driver.FindElement(txtExternalLookupContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7));
                WebDriverWaits.WaitUntilEleVisible(driver, selLookupContact);
                driver.FindElement(selLookupContact).Click();
                Thread.Sleep(4000);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
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
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();

            return getDate;
        }



        // Validate mark no external contact error message
        public void ValidateMarkNoExternalContactError(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity);
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(1000);
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadData(excelPath, "Activity", 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
        }

        //Function to get error message related to no external contact availability
        public string GetNoExternalContactErrorMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, errMsgNoExternalContact, 60);
            string errorMsg = driver.FindElement(errMsgNoExternalContact).Text;
            Thread.Sleep(2000);
            return errorMsg;
        }

        //Validate companies discussed required error message
        public void ValidateCompaniesDiscussedRequiredErrorMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkBoxNoExternalContact);
            driver.FindElement(chkBoxNoExternalContact).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();

        }

        // Function to get companies discussed required error msg
        public string GetCompaniesDiscussedRequiredErrorMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, errMsgCompaniesDiscussedRequired, 60);
            string errorMsg = driver.FindElement(errMsgCompaniesDiscussedRequired).Text;
            Thread.Sleep(2000);
            return errorMsg;
        }

        public string GetDefaultExternalAttendee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivityOnContactDetail);
            driver.FindElement(btnAddActivityOnContactDetail).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultExternalContact, 60);
            string defaultExtContact = driver.FindElement(valDefaultExternalContact).Text;

            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();

            return defaultExtContact;
        }

        public string GetDefaultHLAttendee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivityOnContactDetail);
            driver.FindElement(btnAddActivityOnContactDetail).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultHLAttendee1, 60);
            string HL_Attendee = driver.FindElement(valDefaultHLAttendee1).Text;

            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();

            return HL_Attendee;
        }

        public string GetDefaultHLAttendee1()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivityOnContactDetail);
            driver.FindElement(btnAddActivityOnContactDetail).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultHLAttendee1, 60);
            string HL_Attendee = driver.FindElement(valDefaultHLAttendee1).Text;

            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();

            return HL_Attendee;
        }
    }
}