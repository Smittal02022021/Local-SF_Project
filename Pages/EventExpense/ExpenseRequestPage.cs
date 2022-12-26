using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Linq;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class ExpenseRequestPage : BaseClass
    {       

        By lnkExpenseRequest = By.CssSelector("a[title='Expense Request Tab']");
        By btnCreateNewExpenseForm = By.CssSelector("input[value*='Create New Expense Form']");
        By msgLOB = By.CssSelector("div[id*='id33:j_id35']");
        By comboLOB = By.CssSelector("select[id*='j_id60:lob']");
        By comboEventType = By.CssSelector("select[id*='j_id65:j_id68']");
        By btnSave =By.CssSelector("input[name*='j_id61']");    
        By msgRequestor = By.CssSelector("div[id*='j_id40']");
        By txtRequestor = By.CssSelector("div[class='requiredInput'] >input[id*='id64']");
        By lblRequestorInfo = By.CssSelector("div[class*='first tertiaryPalette']>h3");
        By btnReturnToExpense = By.CssSelector("input[value*='Back To Expense Request List']");
        By lblNewExpRequest = By.XPath("//h2[text() = 'New Expense Request']");
        By lnkEdit = By.CssSelector("tbody[id*='j_id0'] >tr:first-child >td[id*='id129'] >a");
        By btnCancel = By.CssSelector("input[value*='Cancel']");
        By btnSubmitForApproval = By.CssSelector("input[value*='Submit for Approval']");
        By msgErrorList = By.CssSelector("div[class*='message errorM3']>table>tbody>tr:nth-child(2)>td>span>ul");
        By comboProductType = By.CssSelector("select[id*='j_id67']");
        By txtEventContact = By.CssSelector("input[id='j_id0:form1:pb1:j_id63:j_id66']");
        By txtEventName = By.CssSelector("input[id*='j_id72']");
        By txtCity = By.CssSelector("input[id*='j_id76']");
        By valStartDate = By.CssSelector("div[class='pbSubsection']>table>tbody>tr:nth-child(2)>td>span>span[class='dateFormat']>a");
        By comboEventFormat = By.CssSelector("select[id*='formatid']");
        By comboNumberOfGuests = By.CssSelector("select[id*='j_id97']");
        By txtExpectedTravelCost = By.CssSelector("input[id*='j_id196:etc']");
        By txtOtherCost = By.CssSelector("input[id*='j_id196:aacs']");
        By txtExpectedFBCost = By.CssSelector("input[id*='j_id196:efbc']");
        By txtDescOfOtherCost = By.CssSelector("input[id*='specifyFieldId']");
        By txtHLIntOppName = By.CssSelector("input[id='j_id0:form1:pb1:j_id196:j_id205']");
        By txtListofTeamMembers = By.CssSelector("span + input[id*='j_id218:inputContactId3']");
        By valStatus = By.CssSelector("span[id*='id73']");
        By comboMarketingSupport = By.CssSelector("select[name*='Marketingsupport']");
        By txtDescMarketing = By.CssSelector("input[name*='Marketingsupport']");
        By btnEdit = By.CssSelector("input[value='Edit']");
        By txtEndDate = By.CssSelector("input[name*='id80']");
        By valEndDate = By.CssSelector("div[class='pbSubsection']>table>tbody>tr:nth-child(3)>td>span>span[class='dateFormat']>a");
        By msgOtherCost = By.CssSelector("div[id*='id39']");
        By txtEvalDate = By.CssSelector("input[id*='id192']");
        By valEvalDate = By.CssSelector("div[class='pbSubsection']>table>tbody>tr:nth-child(1)>td>span>span[class='dateFormat']>a");

        string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        public void ClickExpenseRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkExpenseRequest);
            driver.FindElement(lnkExpenseRequest).Click();

        }
        //To Click on Create New Expense Form button
        public void ClickCreateNewExpenseForm()
        {
            //Calling wait function-- Create New Expense Form
            WebDriverWaits.WaitUntilEleVisible(driver, btnCreateNewExpenseForm, 70);
            driver.FindElement(btnCreateNewExpenseForm).Click();
        }

        //To validate LOB validation
        public string ValidateLOBMessage()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, msgLOB, 90);
            string message = driver.FindElement(msgLOB).Text.Replace("\r\n", " ");
            Console.WriteLine(message);
            return message;
        }

        //To validate Event Type validation
        public string ValidateEventTypeMessage(string value)
        {        
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 90);
            driver.FindElement(comboLOB).SendKeys(value);
            driver.FindElement(btnCreateNewExpenseForm).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgLOB, 90);
            string message = driver.FindElement(msgLOB).Text.Replace("\r\n", " ");
            return message;
        }

        //To validate Requestor/Delegate validation
        public string ValidateRequestorMessage(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboEventType, 90);
            driver.FindElement(comboEventType).SendKeys(type);
            driver.FindElement(btnCreateNewExpenseForm).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 90);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgRequestor, 90);
            string message = driver.FindElement(msgRequestor).Text.Replace("\r\n", " ");
            return message;
        }

        //To save Requestor/Delegate details
        public string SaveRequestorDetails(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtRequestor, 90);
            driver.FindElement(txtRequestor).SendKeys(name);            
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblRequestorInfo, 80);
            string label = driver.FindElement(lblRequestorInfo).Text;
            return label;
        }      

        //To click Back To Expense Request List button 
        public string ClickBackAndValidatePage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToExpense, 90);
            driver.FindElement(btnReturnToExpense).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewExpRequest, 80);
            string label = driver.FindElement(lblNewExpRequest).Text;
            return label;
        }

        //Validate Edit functionality
        public string ValidateEditFeature()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 90);
            driver.FindElement(lnkEdit).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblRequestorInfo, 80);
            string label = driver.FindElement(lblRequestorInfo).Text;
            return label;
        }

        //Validate cancel functionality
        public string ValidateCancelFeature()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 90);
            driver.FindElement(btnCancel).Click();
            driver.SwitchTo().Alert().Dismiss();
            driver.FindElement(btnCancel).Click();
            driver.SwitchTo().Alert().Accept();            
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewExpRequest, 80);
            string label = driver.FindElement(lblNewExpRequest).Text;
            return label;
        }

        //To click submit without entering all required fields
        public string ClickSubmitWithoutMandatoryFields()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 90);
            driver.FindElement(lnkEdit).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForApproval, 70);
            driver.FindElement(btnSubmitForApproval).Click();
            string errorList = driver.FindElement(msgErrorList).Text.Replace("\r\n", ", ").ToString();
            return errorList;
        }

        //To navigate back to Return to Expense page
        public void ClickReturnToExpense()
        {
            driver.FindElement(btnReturnToExpense).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 90);
            driver.FindElement(lnkEdit).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }


        //To save all details of Event Expense form
        public void SaveAllValuesOfEventExpense(string file)
        {
            string excelPath = dir + file;           
            Thread.Sleep(2000);
            driver.FindElement(comboProductType).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 4));
            string valEventContact = ReadExcelData.ReadData(excelPath, "EventExp", 5);
            driver.FindElement(txtEventContact).SendKeys(valEventContact);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithXpath(valEventContact);
            driver.FindElement(txtEventName).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 6));
            driver.FindElement(txtCity).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 7));
            driver.FindElement(valStartDate).Click();
            driver.FindElement(comboEventFormat).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 8));
            driver.FindElement(comboNumberOfGuests).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 9));
            driver.FindElement(txtExpectedTravelCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 10));
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 11));
            driver.FindElement(txtExpectedFBCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 12));
            driver.FindElement(txtDescOfOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 13));
            string valHLOppName = ReadExcelData.ReadData(excelPath, "EventExp", 14);
            driver.FindElement(txtHLIntOppName).SendKeys(valHLOppName);
            Thread.Sleep(3000);
            //CustomFunctions.SelectValueWithXpath(valHLOppName);
            string valTeamMembers = ReadExcelData.ReadData(excelPath, "EventExp", 15);
            Console.WriteLine(valTeamMembers);
            driver.FindElement(txtListofTeamMembers).SendKeys(valTeamMembers);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//a[contains(@class, 'ui-corner-all')]/ b")).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();
        }
        public void SubmitEventExpenseRequest()
        { 
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForApproval, 90);
            driver.FindElement(btnSubmitForApproval).Click();            
        }

        //To get value of Status
        public string GetRequestStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 90);
            string status = driver.FindElement(valStatus).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToExpense, 90);
            driver.FindElement(btnReturnToExpense).Click();
            return status;
        }

        //To select Marketing Support 
        public string SelectMarketingSupport(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboMarketingSupport, 90);
            driver.FindElement(comboMarketingSupport).SendKeys(value);
            Thread.Sleep(5000);
            string val = driver.FindElement(txtDescMarketing).Enabled.ToString();
            return val;
        }

        //To enter the value of description of Marketing Support
        public void EnterMarketingSupportDesc()
        {
            driver.FindElement(txtDescMarketing).SendKeys("Test Description");
        }

        //To click on edit button
        public void ClickEditButton()
        {
            WebDriverWaits.WaitUntilClickable(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
        }

        //To enter End Date value
        public void EnterEndDate(string value)
        {
            WebDriverWaits.WaitUntilClickable(driver, txtEndDate);
            driver.FindElement(txtEndDate).SendKeys(value);
            driver.FindElement(btnSave).Click();
        }

        //To enter End Date link
        public void ClickEndDateLink()
        {
            WebDriverWaits.WaitUntilClickable(driver, valEndDate);
            driver.FindElement(valEndDate).Click();
            driver.FindElement(btnSave).Click();
        }

        //To get End Date validations
        public string GetEndDateValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgOtherCost, 70);
            string validations = driver.FindElement(msgOtherCost).Text.Replace("\r\n", " ");
            return validations;
        }

        //Clear Other Cost and Description of Other Cost value
        public void ClearCostValuesAndSave()
        {
            WebDriverWaits.WaitUntilClickable(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherCost);
            driver.FindElement(txtOtherCost).Clear();
            driver.FindElement(txtDescOfOtherCost).Clear();
            driver.FindElement(btnSave).Click();
        }

        //To get Other cost validations
        public string GetOtherCostValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgOtherCost);
            string validation = driver.FindElement(msgOtherCost).Text.Replace("\r\n", " ");
            return validation;
        }

        //Enter Other cost value and Save
        public string EnterOtherCostAndSave(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherCost);
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 11));
            driver.FindElement(btnSave).Click();
            driver.FindElement(btnSubmitForApproval).Click();
            string validation = driver.FindElement(msgOtherCost).Text.Replace("\r\n", " ");
            return validation;
        }

        //Enter Description of Other cost value and Save
        public string EnterDescofOtherCostAndSave(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtDescOfOtherCost);
            driver.FindElement(txtDescOfOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 13));
            driver.FindElement(txtEvalDate).Clear();
            driver.FindElement(txtEvalDate).SendKeys("11/11/2020");
            driver.FindElement(btnSave).Click();
            driver.FindElement(btnSubmitForApproval).Click();
            string errorList = driver.FindElement(msgErrorList).Text.Replace("\r\n", ", ").ToString();
            return errorList;
        }

        //Update the value of Evaluation date
        public void UpdateEvalDateAndSubmit()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, valEvalDate);            
            driver.FindElement(valEvalDate).Click();
            driver.FindElement(btnSave).Click();
            driver.FindElement(btnSubmitForApproval).Click();            
        }
    }
}

