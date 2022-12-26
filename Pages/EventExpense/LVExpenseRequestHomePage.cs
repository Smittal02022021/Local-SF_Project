using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.EventExpense
{
    class LVExpenseRequestHomePage : BaseClass
    {
        //Tabs & Labels
        By valExpenseRequest = By.XPath("//h2[@class='slds-card__header-title']/span");
        By tabExpenseRequestLWC = By.XPath("//a[@title='Expense Request(LWC)']/span");
        By valMyRequestsTab = By.XPath("//a[@data-label='My Requests']");
        By valRequestsPendingMyApprovalTab = By.XPath("//a[@data-label='Requests Pending My Approval']");
        By valAllRequestsTab = By.XPath("//a[@data-label='All Requests']");

        //Filters
        By txtExpenseRequestNumber = By.XPath("(//label[text()='Expense Request Number']/following::div/input)[1]");
        By txtExpReqNumberApprovalTab = By.XPath("(//label[text()='Expense Request Number']/following::div/input)[6]");
        By comboCriteria = By.XPath("//label[text()='Criteria']/following::div[3]/button");
        By comboProductType = By.XPath("//label[text()='Product Type']/following::div[3]/button");
        By comboSubmissionDate = By.XPath("//label[text()='Submission Date']/following::div[3]/button");
        By txtRequestor = By.XPath("(//label[text()='Requestor']/following::div/input)[1]");
        By selectRequestor = By.XPath("(//div[@role='option'])[1]");
        By comboCreatedDate = By.XPath("//label[text()='Created Date']/following::div[3]/button");
        By comboLOB = By.XPath("//label[text()='LOB']/following::div[3]/button");
        By comboLOB1 = By.XPath("(//label[text()='LOB']/following::div[3]/button)[2]");
        By txtEventName = By.XPath("(//label[text()='Event Name']/following::div/input)[1]");
        By comboEventType = By.XPath("(//label[text()='Event Type']/following::div[3]/button)[2]");
        By comboEventFormat = By.XPath("//label[text()='Event Format']/following::div[3]/button");

        By btnApplyFilters = By.XPath("//button[text()='Apply Filter']");
        By btnApplyFiltersApprovalTab = By.XPath("(//button[text()='Apply Filter'])[2]");
        By btnResetFilters = By.XPath("//button[text()='Reset Filter']");
        By btnResetFiltersApprovalTab = By.XPath("(//button[text()='Reset Filter'])[2]");

        By comboRecordsPerPage = By.XPath("//label[contains(text(),'Records per')]/following::div/select");
        By inputPageNo = By.XPath("//input[@title='Go to a Page']");
        By lblTotalPages = By.XPath("//input[@title='Go to a Page']/following::span/b");

        By lblSelectLOBErrorMsg = By.XPath("(//label[text()='LOB'])[1]/following::div[6]");
        By buttonCreateNewExpenseForm = By.XPath("//button[@title='Create New Expense Form']");
        By dropdownLOB = By.XPath("(//label[text()='LOB'])[1]/following::div[3]/button");
        By comboEventTypeNoLOB = By.XPath("(//label[text()='Event Type']/following::div[3]/button)[2]");

        By lblStatus = By.XPath("(//span[text()='Status']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblExpensePreapprovalNumber = By.XPath("(//span[text()='Expense Preapproval Number']/following::div/span/slot/lightning-formatted-text)[1]");

        public bool VerifyIfExpenseRequestPageIsOpenedSuccessfully()
        {
            Thread.Sleep(3000);
            bool result = false;

            if (driver.FindElement(valExpenseRequest).Text=="Expense Request")
            {
                result = true;
            }
            return result;
        }

        public bool VerifyIfMyRequestTabIsDisplayed()
        {
            Thread.Sleep(3000);
            bool result = false;

            if (driver.FindElement(valMyRequestsTab).Text == "My Requests")
            {
                result = true;
            }
            return result;
        }

        public bool VerifyIfRequestsPendingMyApprovalTabIsDisplayed()
        {
            Thread.Sleep(3000);
            bool result = false;

            if (driver.FindElement(valRequestsPendingMyApprovalTab).Text == "Requests Pending My Approval")
            {
                result = true;
            }
            return result;
        }

        public void NavigateToRequestsPendingMyApprovalTab()
        {
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, valRequestsPendingMyApprovalTab, 120);
            driver.FindElement(valRequestsPendingMyApprovalTab).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyIfAllRequestsTabIsDisplayed()
        {
            Thread.Sleep(3000);
            bool result = false;

            if (driver.FindElement(valAllRequestsTab).Text == "All Requests")
            {
                result = true;
            }
            return result;
        }

        public bool VerifyIfCreatedExpenseRequestIsDisplayedUnderMyRequestsTab(string expReqNo)
        {
            bool result = false;

            Thread.Sleep(3000);

            driver.FindElement(txtExpenseRequestNumber).SendKeys(expReqNo);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);

            if(driver.FindElement(By.XPath($"//a[text()='{expReqNo}']")).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyFilterOptionsEventTypeAndEventFormatUponLOBSelection(string LOB, string evType, string evFormat, string expReqNo)
        {
            bool result = false;

            Thread.Sleep(3000);

            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);

            driver.FindElement(comboLOB1).SendKeys(LOB);
            Thread.Sleep(3000);
            driver.FindElement(comboLOB1).SendKeys(Keys.Enter);
            Thread.Sleep(3000);

            string attValue = driver.FindElement(comboEventType).GetAttribute("class");
            if (attValue == "slds-combobox__input slds-input_faux slds-combobox__input-value")
            {
                driver.FindElement(comboEventType).SendKeys(evType);
                Thread.Sleep(2000);
                driver.FindElement(comboEventType).SendKeys(Keys.Enter);
                Thread.Sleep(2000);

                string attValue1 = driver.FindElement(comboEventFormat).GetAttribute("class");
                if (attValue1 == "slds-combobox__input slds-input_faux slds-combobox__input-value")
                {
                    driver.FindElement(comboEventFormat).SendKeys(evFormat);
                    Thread.Sleep(2000);
                    driver.FindElement(comboEventFormat).SendKeys(Keys.Enter);
                    Thread.Sleep(2000);

                    driver.FindElement(txtExpenseRequestNumber).Clear();
                    driver.FindElement(txtExpenseRequestNumber).SendKeys(expReqNo);
                    driver.FindElement(btnApplyFilters).Click();
                    Thread.Sleep(4000);

                    if (driver.FindElement(By.XPath($"//a[text()='{expReqNo}']")).Displayed)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public bool VerifyDefaultValuesInSearchFiltersOnExpenseRequestPage()
        {
            bool result = false;

            string dv1 = driver.FindElement(comboCriteria).GetAttribute("data-value");
            string dv2 = driver.FindElement(comboProductType).GetAttribute("data-value");
            string dv3 = driver.FindElement(comboLOB).GetAttribute("data-value");
            string dv4 = driver.FindElement(comboEventType).GetAttribute("data-value");
            string dv5 = driver.FindElement(comboCreatedDate).GetAttribute("data-value");
            string dv6 = driver.FindElement(comboSubmissionDate).GetAttribute("data-value");
            string dv7 = driver.FindElement(comboEventFormat).GetAttribute("data-value");

            if (dv1=="AND" && dv2 == "--None--" && dv3 == "--None--" && dv4 == "--None--" && dv5 == "--None--" && dv6 == "--None--" && dv7 == "--None--")
            {
                result = true;
            }
            return result;
        }

        public void NavigateToExpenseRequestDetailPageUsingExpensePreapprovalNumberFilter(string expReqNo)
        {
            //driver.FindElement(tabExpenseRequestLWC).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtExpenseRequestNumber).Clear();
            driver.FindElement(txtExpenseRequestNumber).SendKeys(expReqNo);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(4000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            if (driver.FindElement(By.XPath($"//a[text()='{expReqNo}']")).Displayed)
            {
                WebElement element = (WebElement)driver.FindElement(By.XPath($"//a[text()='{expReqNo}']"));
                js.ExecuteScript("arguments[0].click();", element);

                Thread.Sleep(5000);
            }
        }

        public bool NavigateToExpenseRequestDetailPageFromRequestsPendingApprovalTabUsingExpensePreapprovalNumberFilter(string expReqNo, string status)
        {
            Thread.Sleep(3000);
            CustomFunctions.SwitchToWindow(driver, 0);
            Thread.Sleep(3000);

            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            driver.FindElement(txtExpReqNumberApprovalTab).Clear();
            driver.FindElement(txtExpReqNumberApprovalTab).SendKeys(expReqNo);
            driver.FindElement(btnApplyFiltersApprovalTab).Click();
            Thread.Sleep(4000);

            if (driver.FindElement(By.XPath($"//a[text()='{expReqNo}']")).Displayed)
            {
                WebElement element = (WebElement)driver.FindElement(By.XPath($"//a[text()='{expReqNo}']"));
                js.ExecuteScript("arguments[0].click();", element);

                Thread.Sleep(5000);
            }

            CustomFunctions.SwitchToWindow(driver, 1);

            Thread.Sleep(5000);

            //Validate Expense request detail page opened
            WebDriverWaits.WaitUntilEleVisible(driver, lblExpensePreapprovalNumber, 120);
            string expReqPreAppNum = driver.FindElement(lblExpensePreapprovalNumber).Text;
            string expReqStatus = driver.FindElement(lblStatus).Text;

            if (expReqPreAppNum == expReqNo && expReqStatus == status)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyFilterFunctionalityOnSelectionOfSubmissionDate()
        {
            bool result = false;

            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);

            driver.FindElement(comboSubmissionDate).SendKeys("This Year");
            driver.FindElement(comboSubmissionDate).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(4000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            int maxPages = Int32.Parse(driver.FindElement(lblTotalPages).Text);

            string currentYear = (DateTime.Now.Year).ToString();

            for (int k = 1; k <= maxPages; k++)
            {
                //Get no of records
                int recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;

                for (int i = 1; i <= recordCount; i++)
                {
                    string value = driver.FindElement(By.XPath($"(//table/tbody/tr)[{i}]/td[10]")).Text;

                    if (value.Contains(currentYear))
                    {
                        if (i < recordCount)
                        {
                            continue;
                        }
                        else if (i == recordCount && k<maxPages)
                        {
                            driver.FindElement(inputPageNo).Clear();
                            string newPageNo = (k + 1).ToString();
                            driver.FindElement(inputPageNo).SendKeys(newPageNo);
                            driver.FindElement(inputPageNo).SendKeys(Keys.Enter);
                            Thread.Sleep(3000);
                            recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool VerifyFilterFunctionalityOnSelectionOfCreatedDate()
        {
            bool result = false;

            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);

            driver.FindElement(comboCreatedDate).SendKeys("This Year");
            driver.FindElement(comboCreatedDate).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(4000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            int maxPages = Int32.Parse(driver.FindElement(lblTotalPages).Text);

            string currentYear = (DateTime.Now.Year).ToString();

            for (int k = 1; k <= maxPages; k++)
            {
                //Get no of records
                int recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;

                for (int i = 1; i <= recordCount; i++)
                {
                    string value = driver.FindElement(By.XPath($"(//table/tbody/tr)[{i}]/td[3]")).Text;

                    if (value.Contains(currentYear))
                    {
                        if (i < recordCount)
                        {
                            continue;
                        }
                        else if (i == recordCount && k < maxPages)
                        {
                            driver.FindElement(inputPageNo).Clear();
                            string newPageNo = (k + 1).ToString();
                            driver.FindElement(inputPageNo).SendKeys(newPageNo);
                            driver.FindElement(inputPageNo).SendKeys(Keys.Enter);
                            Thread.Sleep(3000);
                            recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool VerifyFilterFunctionalityOnSelectionOfEventName(string eventName)
        {
            bool result = false;

            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtEventName).SendKeys(eventName);
            Thread.Sleep(3000);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(4000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            int maxPages = Int32.Parse(driver.FindElement(lblTotalPages).Text);

            for (int k = 1; k <= maxPages; k++)
            {
                //Get no of records
                int recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;

                for (int i = 1; i <= recordCount; i++)
                {
                    string value = driver.FindElement(By.XPath($"(//table/tbody/tr)[{i}]/th")).Text;

                    if (value==eventName)
                    {
                        if (i < recordCount)
                        {
                            continue;
                        }
                        else if (i == recordCount && k < maxPages)
                        {
                            driver.FindElement(inputPageNo).Clear();
                            string newPageNo = (k + 1).ToString();
                            driver.FindElement(inputPageNo).SendKeys(newPageNo);
                            driver.FindElement(inputPageNo).SendKeys(Keys.Enter);
                            Thread.Sleep(3000);
                            recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool VerifyFilterFunctionalityOnSelectionOfProductType()
        {
            bool result = false;

            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);

            driver.FindElement(comboProductType).SendKeys("CVAS");
            Thread.Sleep(3000);
            driver.FindElement(comboProductType).SendKeys(Keys.Enter);
            Thread.Sleep(3000);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(4000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            if(driver.FindElement(By.XPath("//p[text()='No Records Found']")).Displayed)
            {
                result = true;
            }
            
            driver.FindElement(comboProductType).SendKeys("--None--");
            Thread.Sleep(3000);
            driver.FindElement(comboProductType).SendKeys(Keys.Enter);
            Thread.Sleep(3000);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(4000);

            return result;
        }

        public bool VerifyFilterFunctionalityOnSelectionOfRequestor(string reqName)
        {
            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,100)");
            Thread.Sleep(2000);

            driver.FindElement(txtRequestor).SendKeys(reqName);
            Thread.Sleep(2000);
            driver.FindElement(txtRequestor).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, selectRequestor);
            driver.FindElement(selectRequestor).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(4000);

            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            int maxPages = Int32.Parse(driver.FindElement(lblTotalPages).Text);

            for (int k = 1; k <= maxPages; k++)
            {
                //Get no of records
                int recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;

                for (int i = 1; i <= recordCount; i++)
                {
                    string value = driver.FindElement(By.XPath($"(//table/tbody/tr)[{i}]/td[2]")).Text;

                    if (value == reqName)
                    {
                        if (i < recordCount)
                        {
                            continue;
                        }
                        else if (i == recordCount && k < maxPages)
                        {
                            driver.FindElement(inputPageNo).Clear();
                            string newPageNo = (k + 1).ToString();
                            driver.FindElement(inputPageNo).SendKeys(newPageNo);
                            driver.FindElement(inputPageNo).SendKeys(Keys.Enter);
                            Thread.Sleep(3000);
                            recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool VerifyFilterFunctionalityInCombinationOfCreatedDateWithEventName(string eventName)
        {
            bool result = false;

            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);

            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(comboCreatedDate)).Perform();
            Thread.Sleep(2000);

            //CustomFunctions.MouseOver(driver, comboCreatedDate, 60);
            //Thread.Sleep(2000);

            driver.FindElement(comboCreatedDate).SendKeys("This Year");
            driver.FindElement(comboCreatedDate).SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            driver.FindElement(txtEventName).SendKeys(eventName);
            Thread.Sleep(3000);

            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(4000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            //Get total pages
            int maxPages = Int32.Parse(driver.FindElement(lblTotalPages).Text);

            string currentYear = (DateTime.Now.Year).ToString();

            for (int k = 1; k <= maxPages; k++)
            {
                //Get no of records on a page
                int recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;

                for (int i = 1; i <= recordCount; i++)
                {
                    string value = driver.FindElement(By.XPath($"(//table/tbody/tr)[{i}]/th")).Text;
                    string value1 = driver.FindElement(By.XPath($"(//table/tbody/tr)[{i}]/td[3]")).Text;

                    if (value1.Contains(currentYear) && value==eventName)
                    {
                        if (i < recordCount)
                        {
                            continue;
                        }
                        else if (i == recordCount && k < maxPages)
                        {
                            driver.FindElement(inputPageNo).Clear();
                            string newPageNo = (k + 1).ToString();
                            driver.FindElement(inputPageNo).SendKeys(newPageNo);
                            driver.FindElement(inputPageNo).SendKeys(Keys.Enter);
                            Thread.Sleep(3000);
                            recordCount = driver.FindElements(By.XPath("//table/tbody/tr/th")).Count;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public string GetRequiredFieldErrorUponClickingCreateNewExpenseFormButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, buttonCreateNewExpenseForm, 120);
            driver.FindElement(buttonCreateNewExpenseForm).Click();

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblSelectLOBErrorMsg, 60);

            string result = driver.FindElement(lblSelectLOBErrorMsg).Text;
            return result;
        }

        public bool VerifyEventTypeFieldDisplayedOrNotUponSelectingLOB(string LOB)
        {
            bool result;
            Thread.Sleep(10000);

            driver.Navigate().Refresh();
            Thread.Sleep(5000);

            //Select LOB
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownLOB, 120);
            driver.FindElement(dropdownLOB).SendKeys(LOB);
            Thread.Sleep(4000);
            driver.FindElement(dropdownLOB).SendKeys(Keys.Enter);
            Thread.Sleep(8000);

            result = CustomFunctions.IsElementPresent(driver, comboEventTypeNoLOB);
            
            return result;
        }
    }
}