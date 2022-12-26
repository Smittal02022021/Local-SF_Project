using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.EventExpense
{
    class LVExpenseRequestDetailPage : BaseClass
    {
        By btnSubmitForApprovalLWC = By.XPath("//button[text()='Submit for Approval (LWC)']");
        By btnDeleteLWC = By.XPath("(//button[text()='Delete(LWC)'])[1]");
        By btnReqDeleteLWC = By.XPath("(//button[text()='Delete(LWC)'])[2]");
        By btnOK = By.XPath("//button[text()='Ok']");
        By btnClone = By.XPath("//button[@name='Clone']");
        By btnEdit = By.XPath("//button[@name='Edit']");
        By btnApproveLWC = By.XPath("//button[text()='Approve(LWC)']");
        By btnRejectLWC = By.XPath("//button[text()='Reject(LWC)']");
        By btnReject = By.XPath("//button[text()='Reject']");
        By btnRequestMoreInformation = By.XPath("//button[text()='Request More Information']");

        //Requestor/Host Information
        By linkRequestor = By.XPath("(//span[text()='Requestor']/following::div/a/slot/slot/span)[1]");
        By lblStatus = By.XPath("(//span[text()='Status']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblCloneStatus = By.XPath("((//span[text()='Status'])[3]/following::div/span/slot/lightning-formatted-text)[1]");
        By lblTitle = By.XPath("(//span[text()='Title']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblExpensePreapprovalNumber = By.XPath("(//span[text()='Expense Preapproval Number']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblCloneExpPreAppNum = By.XPath("((//span[text()='Expense Preapproval Number'])[3]/following::div/span/slot/lightning-formatted-text)[1]");
        By linkPrimaryEmail = By.XPath("(//span[text()='Primary Email']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text/a)[1]");
        By linkEventContact = By.XPath("(//span[text()='Event Contact']/following::div/a/slot/slot/span)[1]");
        By lblIndustryGroup = By.XPath("(//span[text()='Industry Group']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblPrimaryPhoneNumber = By.XPath("(//span[text()='Primary phone number']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblOffice = By.XPath("(//span[text()='Office']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");

        //Event Information
        By linkEvent = By.XPath("(//span[text()='Event']/following::records-hoverable-link/div/a/slot/slot/span)[1]");
        By lblLOB = By.XPath("(//span[text()='LOB']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblEventType = By.XPath("(//span[text()='Event Type']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblEventFormat = By.XPath("(//span[text()='Event Format']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblStartDate = By.XPath("(//span[text()='Start Date']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblInternalOppNumber = By.XPath("(//span[text()='Internal Opportunity Number']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblClassification = By.XPath("(//span[text()='Classification']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblCity = By.XPath("(//span[text()='City']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblEventLocation = By.XPath("(//span[text()='Event Location']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblNumberOfGuests = By.XPath("(//span[text()='Number of guests']/following::div/span/slot/lightning-formatted-text)[1]");

        //Attendee Budget Information
        By lblDescriptionOfOtherCost = By.XPath("(//span[text()='Description of Other Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By btnEditEventInfo = By.XPath("//button[@title='Edit Description of Other Cost']");
        By txtDescriptOfOtherCost = By.XPath("//label[text()='Description of Other Cost']/../div/input");
        By lblExpectedAirfareCost = By.XPath("(//span[text()='Expected Airfare Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblExpectedRegistrationFee = By.XPath("(//span[text()='Expected Registration Fee']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblOtherCost = By.XPath("(//span[text()='Other Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblExpectedLodgingCost = By.XPath("(//span[text()='Expected Lodging Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblExpectedFBCost = By.XPath("(//span[text()='Expected F&B Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblTotalBudgetRequested = By.XPath("(//span[text()='Total Budget Requested']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");

        //Additional Info
        By lblAdditionalInfoNotes = By.XPath("(//span[text()='Notes'])[1]/following::div/span/slot/lightning-formatted-text");

        //General
        By btnSave = By.XPath("//button[@name='SaveEdit']");
        By btnCancel = By.XPath("//button[text()='Cancel']");
        By btnCloneCancel = By.XPath("//button[@name='CancelEdit']");
        By btnCloneSave = By.XPath("//button[@name='SaveEdit']");
        By lblMandatoryFieldWarningMsg = By.XPath("//div/strong[text()='Review the following fields']/following::ul/li/a[text()='Description of Other Cost']");
        By h2NewExpenseRequest = By.XPath("//h2[text()='New HL_Expense Request']");

        //Clone Elements
        By txtRequestor1 = By.XPath("(//label[text()='Requestor']/following::div/input)[1]");
        By lblTitle1 = By.XPath("((//span[text()='Title'])[3]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By linkPrimaryEmail1 = By.XPath("((//span[text()='Primary Email'])[3]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblIndustryGroup1 = By.XPath("((//span[text()='Industry Group'])[3]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblOffice1 = By.XPath("((//span[text()='Office'])[3]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblStatus1 = By.XPath("(//span[text()='Status']/following::div/span/slot/force-record-output-picklist)[1]");
        By txtEventContact1 = By.XPath("(//label[text()='Event Contact']/following::div/input)[1]");
        By lblPrimaryPhnNum1 = By.XPath("((//span[text()='Primary phone number'])[3]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");

        By txtEvent1 = By.XPath("(//label[text()='Event']/following::div/input)[1]");
        By lblLOB1 = By.XPath("((//span[text()='LOB'])[3]/following::div/span/slot/force-record-output-picklist)[1]");

        By txtNotes = By.XPath("//textarea");
        By lblApproverResponse = By.XPath("(//lst-formatted-text)[2]");
        By lblNotes = By.XPath("(//lightning-base-formatted-text)[1]");

        By txtAreaNotes = By.XPath("//label[text()='Notes']/following::div/textarea");
        By lblApproverEditExpReqErrorMsg = By.XPath("//ul[@class='errorsList slds-list_dotted slds-m-left_medium']/li");

        string dir = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\";

        public bool VerifyIfExpensePreapprovalNumberIsDisplayed()
        {
            Thread.Sleep(10000);
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, lblExpensePreapprovalNumber, 120);
            if (driver.FindElement(lblExpensePreapprovalNumber).Displayed)
            {
                result = true;
            }
            return result;
        }

        public string GetExpensePreapprovalNumber()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblExpensePreapprovalNumber, 120);
            string expensePreapprovalNum = driver.FindElement(lblExpensePreapprovalNumber).Text;
            Thread.Sleep(3000);
            return expensePreapprovalNum;
        }

        public string GetCloneExpensePreapprovalNumber()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblCloneExpPreAppNum, 120);
            string expensePreapprovalNum = driver.FindElement(lblCloneExpPreAppNum).Text;
            Thread.Sleep(3000);
            return expensePreapprovalNum;
        }

        public string GetEventTypeInfo()
        {
            Thread.Sleep(3000);
            string eventTypeInfo = driver.FindElement(lblEventType).Text;
            Thread.Sleep(3000);
            return eventTypeInfo;
        }

        public string GetEventFormatInfo()
        {
            Thread.Sleep(3000);
            string eventFormatInfo = driver.FindElement(lblEventFormat).Text;
            Thread.Sleep(3000);
            return eventFormatInfo;
        }

        public string GetEventStatusInfo()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 120);
            string eventStatusInfo = driver.FindElement(lblStatus).Text;
            Thread.Sleep(3000);
            return eventStatusInfo;
        }

        public string GetCloneEventStatusInfo()
        {
            Thread.Sleep(3000);
            string eventStatusInfo = driver.FindElement(lblCloneStatus).Text;
            Thread.Sleep(3000);
            return eventStatusInfo;
        }

        public void EditEventInformation(string file, int userRow)
        {
            Thread.Sleep(5000);
            string excelPath = dir + file;

            driver.FindElement(btnEditEventInfo).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtDescriptOfOtherCost).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtDescriptOfOtherCost).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 15));

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 120);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
        }

        public string GetUpdatedDescriptionOfOtherCost()
        {
            Thread.Sleep(3000);
            string updatedDescOfOtherCost = driver.FindElement(lblDescriptionOfOtherCost).Text;
            Thread.Sleep(3000);
            return updatedDescOfOtherCost;
        }

        public bool VerifyMandatoryFieldErrorMessageWhileTryingToEditEventInfo(string file, int userRow)
        {
            Thread.Sleep(3000);
            string excelPath = dir + file;

            Thread.Sleep(3000);
            driver.FindElement(btnEditEventInfo).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtDescriptOfOtherCost).Clear();
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblMandatoryFieldWarningMsg, 120);
            bool result = CustomFunctions.IsElementPresent(driver, lblMandatoryFieldWarningMsg);

            driver.FindElement(txtDescriptOfOtherCost).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 15));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyCloneButtonFunctionality()
        {
            Thread.Sleep(3000);
            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Get Expense Request details from the details page
            string requestor = driver.FindElement(linkRequestor).Text;
            string title = driver.FindElement(lblTitle).Text;
            string primaryEmail = driver.FindElement(linkPrimaryEmail).Text;
            string industryGroup = driver.FindElement(lblIndustryGroup).Text;
            string office = driver.FindElement(lblOffice).Text;
            string status = driver.FindElement(lblStatus).Text;
            string expensePreApprovalNum = driver.FindElement(lblExpensePreapprovalNumber).Text;
            string eventContact = driver.FindElement(linkEventContact).Text;
            string primaryPhnNum = driver.FindElement(lblPrimaryPhoneNumber).Text;

            string eventName = driver.FindElement(linkEvent).Text;
            string lob = driver.FindElement(lblLOB).Text;
            string eventType = driver.FindElement(lblEventType).Text;
            string eventFormat = driver.FindElement(lblEventFormat).Text;
            string startDate = driver.FindElement(lblStartDate).Text;
            string internalOppNum = driver.FindElement(lblInternalOppNumber).Text;
            string classfication = driver.FindElement(lblClassification).Text;
            string city = driver.FindElement(lblCity).Text;
            string eventLoc = driver.FindElement(lblEventLocation).Text;
            string numOfGuests = driver.FindElement(lblNumberOfGuests).Text;

            string expAirfareCost = driver.FindElement(lblExpectedAirfareCost).Text;
            string expRegFee = driver.FindElement(lblExpectedRegistrationFee).Text;
            string otherCost = driver.FindElement(lblOtherCost).Text;
            string descOfOtherCost = driver.FindElement(lblDescriptionOfOtherCost).Text;
            string expLodgingCost = driver.FindElement(lblExpectedLodgingCost).Text;
            string expFBCost = driver.FindElement(lblExpectedFBCost).Text;
            string totalbudgetReq = driver.FindElement(lblTotalBudgetRequested).Text;

            driver.FindElement(btnClone).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, h2NewExpenseRequest, 120);

            string requestor1 = driver.FindElement(txtRequestor1).GetAttribute("data-value");
            string title1 = driver.FindElement(lblTitle1).Text;
            string primaryEmail1 = driver.FindElement(linkPrimaryEmail1).Text;
            string industryGroup1 = driver.FindElement(lblIndustryGroup1).Text;
            string office1 = driver.FindElement(lblOffice1).Text;
            string status1 = driver.FindElement(lblStatus1).Text;
            string eventContact1 = driver.FindElement(txtEventContact1).GetAttribute("data-value");
            string primaryPhnNum1 = driver.FindElement(lblPrimaryPhnNum1).Text;

            string event1 = driver.FindElement(txtEvent1).GetAttribute("data-value");
            string lob1 = driver.FindElement(lblLOB1).Text;

            if (CustomFunctions.IsElementPresent(driver, h2NewExpenseRequest) == true)
            {
                if(requestor==requestor1 && title==title1 && primaryEmail==primaryEmail1 && industryGroup==industryGroup1 && office==office1 && status==status1 && eventContact==eventContact1 && primaryPhnNum==primaryPhnNum1)
                {
                    if(eventName == event1 && lob==lob1)
                    {
                        result = true;
                    }
                }
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnCloneCancel, 120);
            driver.FindElement(btnCloneCancel).Click();
            Thread.Sleep(3000);

            return result;
        }

        public void CreateCloneExpenseRequest()
        {
            driver.FindElement(btnClone).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnCloneSave, 120);
            driver.FindElement(btnCloneSave).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyDeleteExpenseRequestFunctionality()
        {
            bool result = false;

            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteLWC, 120);
            driver.FindElement(btnDeleteLWC).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            driver.FindElement(txtNotes).SendKeys("Test");
            driver.FindElement(btnOK).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 120);
            Thread.Sleep(3000);
            if (driver.FindElement(lblStatus).Text=="Deleted")
            {
                result = true;
            }

            return result;
        }

        public bool VerifyDeleteExpenseRequestFunctionalityAsRequestor()
        {
            bool result = false;

            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnReqDeleteLWC, 120);
            driver.FindElement(btnReqDeleteLWC).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            driver.FindElement(txtNotes).SendKeys("Test");
            driver.FindElement(btnOK).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lblCloneStatus, 120);
            Thread.Sleep(3000);
            if (driver.FindElement(lblCloneStatus).Text == "Deleted")
            {
                result = true;
            }

            return result;
        }

        public string GetApproverResponseFromApprovalHistorySection()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblApproverResponse, 120);
            string approverResp = driver.FindElement(lblApproverResponse).Text;

            return approverResp;
        }

        public string GetApproverResponseFromApprovalHistorySectionForApprover()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//table[@aria-label='Event Expense Approval History']/tbody/tr"));
            int size = elements.Count;

            By lblApproverResponseForApprover = By.XPath($"//table[@aria-label='Event Expense Approval History']/tbody/tr[{size}]/td[2]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text");
            string approverResp = driver.FindElement(lblApproverResponseForApprover).Text;

            return approverResp;
        }

        public string GetNotesFromApprovalHistorySection()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblNotes, 120);
            string notes = driver.FindElement(lblNotes).Text;

            return notes;
        }

        public string GetNotesFromApprovalHistorySectionForApprover()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(5000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//table[@aria-label='Event Expense Approval History']/tbody/tr"));
            int size = elements.Count;

            By lblAppNotes = By.XPath($"//table[@aria-label='Event Expense Approval History']/tbody/tr[{size}]/td[3]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lightning-base-formatted-text");

            WebDriverWaits.WaitUntilEleVisible(driver, lblAppNotes, 120);
            string notes = driver.FindElement(lblAppNotes).Text;

            return notes;
        }

        public bool SubmitExpenseRequestLWCForApproval()
        {
            bool result = false;

            Thread.Sleep(3000);
            //CustomFunctions.SwitchToWindow(driver, 1);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForApprovalLWC, 120);
            driver.FindElement(btnSubmitForApprovalLWC).Click();
            Thread.Sleep(8000);

            //WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            //driver.FindElement(btnOK).Click();

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 120);
            Thread.Sleep(3000);
            if (driver.FindElement(lblStatus).Text == "Waiting for Approval")
            {
                result = true;
                //CustomFunctions.SwitchToWindow(driver, 0);
            }

            return result;
        }

        public void EditExpenseRequestAsApprover(string notes)
        {
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaNotes, 120);
            driver.FindElement(txtAreaNotes).Clear();
            driver.FindElement(txtAreaNotes).SendKeys(notes);

            Thread.Sleep(5000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
        }

        public bool VerifyApproverIsNotAbleToEditExpenseRequest(string msg)
        {
            bool result = false;
            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaNotes, 120);
            driver.FindElement(txtAreaNotes).Clear();
            driver.FindElement(txtAreaNotes).SendKeys("Test Notes");

            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblApproverEditExpReqErrorMsg, 120);
            string errorMsg = driver.FindElement(lblApproverEditExpReqErrorMsg).Text;
            if (errorMsg == msg)
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);

            return result;
        }

        public string GetApproverNotesDetailsUnderAdditionalInfo()
        {
            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAdditionalInfoNotes, 120);
            string approverNotes = driver.FindElement(lblAdditionalInfoNotes).Text;
            Thread.Sleep(3000);
            return approverNotes;
        }

        public bool VerifyNecessaryButtonsAreDisplayedWhenApproverLandsOnExpenseDetailPage()
        {
            bool result = false;
            Thread.Sleep(3000);
            if(driver.FindElement(btnDeleteLWC).Displayed && driver.FindElement(btnEdit).Displayed && driver.FindElement(btnClone).Displayed && driver.FindElement(btnApproveLWC).Displayed && driver.FindElement(btnRejectLWC).Displayed && driver.FindElement(btnRequestMoreInformation).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void RejectExpenseRequest(string notes)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRejectLWC, 120);
            driver.FindElement(btnRejectLWC).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaNotes, 120);
            driver.FindElement(txtAreaNotes).SendKeys(notes);
            Thread.Sleep(3000);
            driver.FindElement(btnReject).Click();
            Thread.Sleep(5000);
        }

        public void RequestForMoreInformation(string notes)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRequestMoreInformation, 120);
            driver.FindElement(btnRequestMoreInformation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaNotes, 120);
            driver.FindElement(txtAreaNotes).SendKeys(notes);
            Thread.Sleep(3000);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(5000);
        }

        public void ApproveExpenseRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnApproveLWC, 120);
            driver.FindElement(btnApproveLWC).Click();
            Thread.Sleep(8000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(10000);
        }
    }
}