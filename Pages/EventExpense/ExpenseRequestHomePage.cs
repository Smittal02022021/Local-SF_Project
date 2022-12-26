using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.EventExpense
{
    class ExpenseRequestHomePage : BaseClass
    {
        ExpenseRequestDetailPage expRequestDetail = new ExpenseRequestDetailPage();
            
        By valExpensePreAppNumber = By.CssSelector("td[id*='pbtableId1:0:j_id142'] > a");
        By valExpenseRequestStatus = By.CssSelector("span[id*='pbtableId1:0:j_id554']");
        By valApproverName = By.CssSelector("a[id='lookup00531000006z6tJ00N5A00000HEU2u']");
        By lnkExpenseRequest = By.CssSelector("a[title='Expense Request Tab']");
        By btnAllRequest = By.CssSelector("td[id*='asApproverTab3_lbl']");
        By txtExpenseReqNumberAdmin = By.CssSelector("input[name*='j_id417:j_id420']");
        By txtExpenseReqNumberStandard = By.CssSelector("input[name*='j_id96:j_id99']");
        By comboLOB = By.CssSelector("select[id*='srchFilterPBS:j_id91']");
        By ExpenseRequestList = By.CssSelector("table[id*='pbtableId1'] > tbody > tr");
        By btnApplyFilterStandard = By.CssSelector("input[id*='j_id71:j_id72']");
        By btnApplyFilter = By.CssSelector("input[id*='j_id392:j_id393']");
        By tblResults = By.CssSelector("table[id*='pbtableId3']");
        By tblResultsStandard = By.CssSelector("table[id*='pbtableId1']");
        By matchedResult = By.CssSelector("td[id*='pbtableId3:0:j_id459'] > a");
        By matchedResultMyRequest = By.CssSelector("td[id*='pbtableId1:0:j_id142'] > a");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By lnkExpRequest = By.XPath("//a[normalize-space()='Expense Request']");
        By btnRequestPendingMyApproval = By.CssSelector("td[id*='asApproverTab_lbl']");
        By tblResultsPendingMyApproval = By.CssSelector("table[id*='pbtableId2']");
        By matchedResultsPendingMyApproval = By.CssSelector("td[id*='pbtableId2:0:j_id302'] > a");
        By comboExpRequestStatus = By.CssSelector("select[name*='j_id103:j_id106'] > option:nth-child(4)");
        By tabMyRequests = By.CssSelector("td[id*='MainTab_lbl']");

        
        //Function to get Expense PreApprover Number
        public string GetExpensePreApproverNumberFromMyRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExpensePreAppNumber, 60);
            string expensePreApproveNo = driver.FindElement(valExpensePreAppNumber).Text;
            return expensePreApproveNo;

        }

        public string GetMyRequestStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExpenseRequestStatus, 60);
            string expenseRequestStatus = driver.FindElement(valExpenseRequestStatus).Text;
            return expenseRequestStatus;
        }

        public string GetApproverName(int currentWindowId,int nextWindowId)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExpensePreAppNumber, 60);
            driver.FindElement(valExpensePreAppNumber).Click();
            CustomFunctions.SwitchToWindow(driver, currentWindowId);
            WebDriverWaits.WaitUntilEleVisible(driver, valApproverName, 60);
            string approverName = driver.FindElement(valApproverName).Text;
            CustomFunctions.SwitchToWindow(driver, nextWindowId);
            Thread.Sleep(2000);
            return approverName;
        }

        public string SearchExpenseRequestByApproverNumberByAdmin(string preApproverNo)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkExpenseRequest, 120);
            driver.FindElement(lnkExpenseRequest).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllRequest, 120);
            driver.FindElement(btnAllRequest).Click();
           
            WebDriverWaits.WaitUntilEleVisible(driver, txtExpenseReqNumberAdmin);
            driver.FindElement(txtExpenseReqNumberAdmin).SendKeys(preApproverNo);

            WebDriverWaits.WaitUntilEleVisible(driver, btnApplyFilter, 120);
            driver.FindElement(btnApplyFilter).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                WebDriverWaits.WaitUntilEleVisible(driver, matchedResult, 80);
                CustomFunctions.ActionClicks(driver, matchedResult);               
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }

        public string SearchExpenseRequestByApproverNumber(string preApproverNo,string LOB)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkExpenseRequest, 120);
            driver.FindElement(lnkExpenseRequest).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 120);
            CustomFunctions.SelectByText(driver, driver.FindElement(comboLOB), LOB);
           // driver.FindElement(comboLOB).SendKeys(LOB);

            WebDriverWaits.WaitUntilEleVisible(driver, txtExpenseReqNumberStandard);
            driver.FindElement(txtExpenseReqNumberStandard).SendKeys(preApproverNo);

            WebDriverWaits.WaitUntilEleVisible(driver, comboExpRequestStatus, 120);
            driver.FindElement(comboExpRequestStatus).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnApplyFilterStandard, 120);
            driver.FindElement(btnApplyFilterStandard).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tblResultsStandard, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResultMyRequest).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                WebDriverWaits.WaitUntilEleVisible(driver, matchedResultMyRequest, 80);
                CustomFunctions.ActionClicks(driver, matchedResultMyRequest);
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }

        public string SearchExpenseRequestAssignedToApprover()
        { 
            WebDriverWaits.WaitUntilEleVisible(driver, shwAllTab, 120);
            driver.FindElement(shwAllTab).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkExpRequest, 120);
            driver.FindElement(lnkExpRequest).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnRequestPendingMyApproval, 120);
            driver.FindElement(btnRequestPendingMyApproval).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, tblResultsPendingMyApproval, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResultsPendingMyApproval).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResultsPendingMyApproval).Click();
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }
    
        public void ClickFirstApproverLink()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valApproverName, 120);
            driver.FindElement(valApproverName).Click();
            
        }

        public string CompareNewExpenseRequestInMyRequest(string expensePreApproverNumber)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabMyRequests, 120);
            driver.FindElement(tabMyRequests).Click();
            
            IList<IWebElement> element = driver.FindElements(ExpenseRequestList);
            int totalRows = element.Count;

            for (int i = 1; i <= totalRows;i++)
            {
                By expenseNumber = By.CssSelector($"table[id*='pbtableId1'] > tbody > tr:nth-child({i}) > td:nth-child(3) > a");
                IWebElement expensePreApproveNumber = driver.FindElement(expenseNumber);

                string expensePreApprovalNumber = expensePreApproveNumber.Text;
                if (expensePreApproverNumber.Equals(expensePreApprovalNumber))
                {
                    return "Event Expense Pre Approver number matches in My Requests";                  
                }
                else
                {
                    return "Event Expense Pre Approver number does not maches in My Requests";
                }                
            }
            return "List not found";
        }
    }
}