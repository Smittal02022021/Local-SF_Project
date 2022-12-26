using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.EventExpense
{
    class LVExpenseRequestCreatePage : BaseClass
    {
        By dropdownLOB = By.XPath("(//label[text()='LOB'])[1]/following::div[3]/button");
        By buttonCreateNewExpenseForm = By.XPath("//button[@title='Create New Expense Form']");

        By txtRequestor = By.XPath("//label[text()='Requestor']/following::div[4]/input");
        By selectRequestor = By.XPath("//thead/tr/th[@title='Name']/following::a[8]");
        By lblRequestorErr = By.XPath("(//label[text()='Requestor'])[1]/following::div[7]");

        By txtEventContact = By.XPath("//label[text()='Event Contact']/following::div[5]/input");
        By selectEventContact = By.XPath("//thead/tr/th[@title='Name']/following::a[8]");
        By lblEventContactErr = By.XPath("(//label[text()='Event Contact'])[1]/following::div[8]");

        By txtEventName = By.XPath("//input[@placeholder='Search Opportunities...']");
        By selectEventName = By.XPath("//thead/tr/th[@title='Opportunity Name']/following::a[9]");
        By lblEventErr = By.XPath("(//label[text()='Event'])[1]/following::div[7]");

        By txtStartDate = By.XPath("//input[@name='Start_Date__c']");
        By lblStartDateErr = By.XPath("(//label[text()='Start Date'])[1]/following::div[2]");

        By comboNoOfGuest = By.XPath("//label[text()='Number of guests']/following::div[3]/button");
        By lblNoOfGuestsErr = By.XPath("(//label[text()='Number of guests'])[1]/following::div[6]");

        By txtExpectedAirFareCost = By.XPath("//input[@name='Expected_Airfare_Cost__c']");
        By lblExpectedAirFareCostErr = By.XPath("(//label[text()='Expected Airfare Cost'])[1]/following::div[3]");

        By txtExpectedRegistrationFeeCost = By.XPath("//input[@name='Expected_Registration_Fee__c']");
        By lblExpectedRegistrationFeeCostErr = By.XPath("(//label[text()='Expected Registration Fee'])[1]/following::div[3]");

        By txtExpectedLodgingCost = By.XPath("//input[@name='Expected_Lodging_Cost__c']");
        By lblExpectedLodgingCostErr = By.XPath("(//label[text()='Expected Lodging Cost'])[1]/following::div[3]");

        By txtExpectedFnBCost = By.XPath("//input[@name='Expected_F_B_cost__c']");
        By lblExpectedFnBCostErr = By.XPath("(//label[text()='Expected F&B Cost'])[1]/following::div[3]");

        By txtOtherCost = By.XPath("//input[@name='Any_additional_cost_Specify__c']");
        By lblOtherCostErr = By.XPath("(//label[text()='Other Cost'])[1]/following::div[3]");

        By txtDescOtherCost = By.XPath("//input[@name='Specify__c']");
        By btnSave = By.XPath("//button[@type='submit']");
        By btnCancel = By.XPath("//button[text()='Cancel']");
        By lblErrMsg = By.XPath("//span[@class='toastMessage forceActionsText']");

        string dir = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\";

        public void CreateNewExpenseRequestLWC(string LOB, string file, int userRow)
        {
            Thread.Sleep(3000);
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

            //Select LOB
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownLOB, 120);
            driver.FindElement(dropdownLOB).SendKeys(LOB);
            Thread.Sleep(10000);
            driver.FindElement(dropdownLOB).SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            //Click on Create New Expense Form button
            driver.FindElement(buttonCreateNewExpenseForm).Click();
            Thread.Sleep(5000);

            //Fill all mandatory fields
            driver.FindElement(txtRequestor).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 2));
            Thread.Sleep(5000);
            driver.FindElement(txtRequestor).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectRequestor, 120);
            driver.FindElement(selectRequestor).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtEventContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 3));
            Thread.Sleep(5000);
            driver.FindElement(txtEventContact).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventContact, 120);
            driver.FindElement(selectEventContact).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtEventName).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 4));
            Thread.Sleep(5000);
            driver.FindElement(txtEventName).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventName, 120);
            driver.FindElement(selectEventName).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtStartDate).SendKeys(DateTime.Today.ToString("dd-MMM-yyyy"));
            driver.FindElement(comboNoOfGuest).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 5));
            driver.FindElement(comboNoOfGuest).SendKeys(Keys.Enter);
            driver.FindElement(txtExpectedAirFareCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 6));
            driver.FindElement(txtExpectedRegistrationFeeCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 7));
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 8));
            driver.FindElement(txtExpectedLodgingCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 9));
            driver.FindElement(txtExpectedFnBCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 10));
            driver.FindElement(txtDescOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 11));

            //Click Save btton
            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);
        }

        public bool VerifyRequiredFieldsOnCreateNewExpenseRequestPage(string LOB, string err)
        {
            Thread.Sleep(3000);

            bool result = false;

            //Click on Create New Expense Form button
            driver.FindElement(buttonCreateNewExpenseForm).Click();
            Thread.Sleep(5000);

            //Click Save button
            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);

            //Verify all mandatory fields
            if(driver.FindElement(lblRequestorErr).Text==err && driver.FindElement(lblEventContactErr).Text == err && driver.FindElement(lblEventErr).Text == err && driver.FindElement(lblStartDateErr).Text == err && driver.FindElement(lblNoOfGuestsErr).Text == err)
            {
                if (driver.FindElement(lblExpectedAirFareCostErr).Text == err && driver.FindElement(lblExpectedFnBCostErr).Text == err && driver.FindElement(lblExpectedLodgingCostErr).Text == err && driver.FindElement(lblExpectedRegistrationFeeCostErr).Text == err && driver.FindElement(lblOtherCostErr).Text == err)
                {
                    result = true;
                }
            }
            
            return result;
        }

        public bool VerifyFunctionalityOfExpReqOnClickingCancelButton()
        {
            Thread.Sleep(3000);
            bool result = false;

            //Click Cancel button on Create Expense Request page
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);

            //Click Ok button on the alert box
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);

            result = CustomFunctions.IsElementPresent(driver, buttonCreateNewExpenseForm);
            Thread.Sleep(5000);
            return result;
        }

        public string GetErrorMsgDisplayedIfLoggedUserIsNotSelectedAsREquestorOrDelegateOfRequestor(string LOB, string file, int userRow)
        {
            Thread.Sleep(3000);
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Fill all mandatory fields
            driver.FindElement(txtRequestor).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 14));
            Thread.Sleep(5000);
            driver.FindElement(txtRequestor).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectRequestor, 120);
            driver.FindElement(selectRequestor).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtEventContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 3));
            Thread.Sleep(5000);
            driver.FindElement(txtEventContact).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventContact, 120);
            driver.FindElement(selectEventContact).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtEventName).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 4));
            Thread.Sleep(5000);
            driver.FindElement(txtEventName).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventName, 120);
            driver.FindElement(selectEventName).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtStartDate).SendKeys(DateTime.Today.ToString("dd-MMM-yyyy"));
            driver.FindElement(comboNoOfGuest).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 5));
            driver.FindElement(comboNoOfGuest).SendKeys(Keys.Enter);
            driver.FindElement(txtExpectedAirFareCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 6));
            driver.FindElement(txtExpectedRegistrationFeeCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 7));
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 8));
            driver.FindElement(txtExpectedLodgingCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 9));
            driver.FindElement(txtExpectedFnBCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 10));
            driver.FindElement(txtDescOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 11));

            //Click Save btton
            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblErrMsg, 120);
            string err = driver.FindElement(lblErrMsg).Text;
            return err;
        }
    }
}