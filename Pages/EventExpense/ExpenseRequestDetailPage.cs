using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.EventExpense
{
    class ExpenseRequestDetailPage : BaseClass
    {
        By btnSubmitForApproval = By.CssSelector("input[value='Submit for Approval']");
        By valEventRequestor = By.CssSelector("span[id*='j_id71:j_id72']  > a");
        By valExpenseRequestStatus = By.CssSelector("span[id*='j_id71:j_id73']");
        By valEventName = By.CssSelector("span[id*='j_id93:j_id94']");
        By valEventContact = By.CssSelector("span[id*='j_id71:j_id78']  > a");
        By valProductType = By.CssSelector("span[id*='j_id71:j_id80']");
        By valIndustryGroup = By.CssSelector("span[id*='j_id71:j_id77']");
        By valEventCity = By.CssSelector("span[id*='j_id93:j_id96']");        
        By valEventType = By.CssSelector("span[id*='j_id100:j_id102']");
        By valEventFormat = By.CssSelector("span[id*='j_id100:j_id104']");
        By valLOB = By.CssSelector("span[id*='j_id100:j_id101']");
        By valNoOfGuest = By.CssSelector("span[id*='j_id100:j_id111']");
        By valHLInternalOppName = By.CssSelector("span[id*='j_id159:j_id165'] > a");
        By valExpensePreAppNumber = By.CssSelector("span[id*='j_id71:j_id75']");
        By btnBackToExpRequestList = By.CssSelector("input[value='Back To Expense Request List']");
        By valApproverName = By.CssSelector("a[id='lookup00531000006z6tJ00N5A00000HEU2u']");
        By valSecondLevelApproverName = By.CssSelector("span[id*='j_id192:0:j_id231'] > a");
        //By btnApprove = By.CssSelector("input[id*='j_id59:j_id63']");
        By btnReject = By.CssSelector("input[value='Reject']");
        By btnRequestMoreInfo = By.CssSelector("input[value='Request More Information']");
        By valApprovedUnderApprovalHistory = By.XPath("//*[text()='Approval History']/..//following-sibling::div//*[contains(text(),'Response')]/../../..//following-sibling::tbody/tr[1]/td[3]");
        By valApprovedUnderApprovalHistoryDualApprover = By.XPath("//*[text()='Approval History']/..//following-sibling::div//*[contains(text(),'Response')]/../../..//following-sibling::tbody/tr[2]/td[3]");
        By valResponseUnderApprovalHistory = By.XPath("//*[text()='Approval History']/..//following-sibling::div//*[contains(text(),'Response')]/../../..//following-sibling::tbody/tr[1]/td[3]");
        By btnDelete = By.CssSelector("input[id*='j_id59:j_id69']");
        By btnOKForNotes = By.CssSelector("input[value='OK']");
        By valStatus = By.CssSelector("span[id*='j_id71:j_id73']");
        By txtNotes = By.CssSelector("textarea[name*='j_id200:j_id201']");
        By btnEdit = By.CssSelector("input[value='Edit']");
        By txtEventCity = By.CssSelector("input[id*='j_id71:j_id76']");
        By btnSave = By.CssSelector("input[name*='bottom:j_id61']");
        By valRequestor = By.CssSelector("span[id*='j_id71:j_id72'] > a");
        By btnApprove = By.CssSelector("input[value='Approve']");

        
        //Function to get expense report status
        public string GetEventRequestor()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventRequestor, 60);
            string statusEventRequestor = driver.FindElement(valEventRequestor).Text;
            return statusEventRequestor;
        }

        //Function to click submit for approval button
        public void ClickSubmitForApproval()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForApproval);
            driver.FindElement(btnSubmitForApproval).Click();
        }

        //Function to get expense report status
        public string GetExpenseReportStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExpenseRequestStatus, 60);
            string statusExpenseRequest = driver.FindElement(valExpenseRequestStatus).Text;
            return statusExpenseRequest;
        }

        //Function to get event name
        public string GetEventName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventName, 60);
            string eventName = driver.FindElement(valEventName).Text;
            return eventName;
        }

        // Function to get event contact
        public string GetEventContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventContact, 60);
            string eventContact = driver.FindElement(valEventContact).Text;
            return eventContact;
        }

        // Function to get event product type
        public string GetProductType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProductType, 60);
            string productType = driver.FindElement(valProductType).Text;
            return productType;
        }

        // Function to get industry group value
        public string GetIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIndustryGroup, 60);
            string productType = driver.FindElement(valIndustryGroup).Text;
            return productType;
        }

        // Function to get industry group value
        public string GetEventCity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventCity, 60);
            string eventCity = driver.FindElement(valEventCity).Text;
            return eventCity;
        }


        // Function to get event type 
        public string GetEventType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventType, 60);
            string eventType = driver.FindElement(valEventType).Text;
            return eventType;
        }

        // Function to get event format value 
        public string GetEventFormat()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventFormat, 60);
            string eventFormat = driver.FindElement(valEventFormat).Text;
            return eventFormat;
        }

        // Function to get LOB 
        public string GetLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOB, 60);
            string valLineOfBusiness = driver.FindElement(valLOB).Text;
            return valLineOfBusiness;
        }

        // Function to get Number Of Guest 
        public string GetNumberOfGuest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNoOfGuest, 60);
            string NumberOfGuest = driver.FindElement(valNoOfGuest).Text;
            return NumberOfGuest;
        }

        //Function to get HL INternal Opportunity Name
        public string GetHLInternalOpportunityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valHLInternalOppName, 60);
            string HLInternalOppName = driver.FindElement(valHLInternalOppName).Text;
            return HLInternalOppName;
        }

        //Function to click Back to Expense Request List
        public void ClickBackToExpenseRequestList(int windowId)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToExpRequestList);
            driver.FindElement(btnBackToExpRequestList).Click();
            Thread.Sleep(2000);
            CustomFunctions.SwitchToWindow(driver, windowId);
            Thread.Sleep(2000);
        }

        //Function to get Expense PreApprover Number
        public string GetExpensePreApproverNumberFromDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExpensePreAppNumber, 60);
            string expensePreApproveNo = driver.FindElement(valExpensePreAppNumber).Text;
            return expensePreApproveNo;

        }

        // Function to select First level Approver
        public void SelectFirstLevelApprover(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, windowId);
            WebDriverWaits.WaitUntilEleVisible(driver, valApproverName, 120);
            driver.FindElement(valApproverName).Click();
        }

        // Function to select second level approver
        public void SelectSecondLevelApprover(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, windowId);
            WebDriverWaits.WaitUntilEleVisible(driver, valSecondLevelApproverName, 120);
            driver.FindElement(valSecondLevelApproverName).Click();
        }

        //Function to approve expense request
        public void ApproveExpenseRequest(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, windowId);

            WebDriverWaits.WaitUntilEleVisible(driver, btnApprove, 120);
            driver.FindElement(btnApprove).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(3000);
        }

        // Function to reject expense request
        public void RejectExpenseRequest(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, windowId);

            WebDriverWaits.WaitUntilEleVisible(driver, btnReject, 120);
            driver.FindElement(btnReject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtNotes, 40);
            driver.FindElement(txtNotes).SendKeys("Test notes");

            WebDriverWaits.WaitUntilEleVisible(driver, btnOKForNotes, 120);
            driver.FindElement(btnOKForNotes).Click();
            Thread.Sleep(3000);

        }

        // Function to reject expense request
        public void RequestMoreInfoForExpenseRequest(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, windowId);

            WebDriverWaits.WaitUntilEleVisible(driver, btnRequestMoreInfo, 120);
            driver.FindElement(btnRequestMoreInfo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtNotes, 40);
            driver.FindElement(txtNotes).SendKeys("Test notes");

            WebDriverWaits.WaitUntilEleVisible(driver, btnOKForNotes, 120);
            driver.FindElement(btnOKForNotes).Click();
            Thread.Sleep(2000);

        }
        // Function to get expense request status
        public string GetExpenseRequestStatus(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, windowId);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 60);
            string valExpenseRequestStatus = driver.FindElement(valStatus).Text;
            return valExpenseRequestStatus;

        }

        // Funtion to validate delete button and approved 
        public string ValidateDeleteAndApproval()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDelete, 60);
            WebDriverWaits.WaitUntilEleVisible(driver, valApprovedUnderApprovalHistory, 60);
            string ApprovedValUnderApprovalHistory = driver.FindElement(valApprovedUnderApprovalHistory).Text;
            return ApprovedValUnderApprovalHistory;
        }

        public string ValidateDeleteAndApprovalDualApprover()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valApprovedUnderApprovalHistoryDualApprover, 60);
            string ApprovedValUnderApprovalHistory = driver.FindElement(valApprovedUnderApprovalHistoryDualApprover).Text;
            return ApprovedValUnderApprovalHistory;
        }
        // Funtion to validate response 
        public string ValidateResponseUnderApprovalHistory()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valResponseUnderApprovalHistory, 60);
            string ResponseUnderApprovalHistory = driver.FindElement(valResponseUnderApprovalHistory).Text;
            return ResponseUnderApprovalHistory;
        }


        //Function to delete expense request
        public void DeleteExpenseRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDelete, 120);
            driver.FindElement(btnDelete).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnOKForNotes, 120);
            driver.FindElement(btnOKForNotes).Click();
            Thread.Sleep(2000);
        }

        //Function to edit expense request
        public void EditExpenseRequest(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtEventCity);
            driver.FindElement(txtEventCity).Clear();
            driver.FindElement(txtEventCity).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 17));

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
        }

        //Get value of requestor from event expense detail page
        public string GetRequestor()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRequestor, 60);
            string requestorValue = driver.FindElement(valRequestor).Text;
            return requestorValue;
        }

        public bool VerifyApproveButton()
        {
            return CustomFunctions.IsElementPresent(driver, btnApprove);

        }

        public bool VerifyRejectButton()
        {
            return CustomFunctions.IsElementPresent(driver, btnReject);
        }

        public bool VerifyBackToExpenseRequestButton()
        {
            return CustomFunctions.IsElementPresent(driver, btnBackToExpRequestList);
        }

        public bool VerifyRequestForMoreInformation()
        {
            return CustomFunctions.IsElementPresent(driver, btnRequestMoreInfo);
        }
    }
}