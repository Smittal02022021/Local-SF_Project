using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.EventExpense
{
    class ExpenseRequestCreatePage : BaseClass
    {
        By lnkExpenseRequest = By.CssSelector("a[title='Expense Request Tab']");
        By comboLOB = By.CssSelector("select[id*='j_id60:lob']");
        By comboEventType = By.CssSelector("select[id*='j_id65:j_id68']");
        By btnCreateNewExpenseForm = By.CssSelector("input[type='submit']");
        By txtRequestor = By.CssSelector("input[class='required'][id*='j_id63:j_id64']");
        By txtEventContact = By.CssSelector("input[class='required'][id*='j_id63:j_id66']");
        By comboProductType = By.CssSelector("select[class='required'][id*='j_id63:j_id67']");
        By txtEventName = By.CssSelector("input[id*='j_id71:j_id72']");
        By txtEventCity = By.CssSelector("input[id*='j_id71:j_id76']");
        By txtStartDate = By.CssSelector("input[id*='j_id71:j_id78']");
        By comboEventFormat = By.CssSelector("select[id*='j_id86:eventformatid']");
        By comboNoOfGuest = By.CssSelector("select[id*='pb3:j_id97']");
        By selAvailableTargetAudience = By.CssSelector("select[id*='j_id107_unselected'] > optgroup > option:nth-child(1)");
        By btnArrowRightTargetAudience = By.CssSelector("img[id*='j_id107_right_arrow']");
        By txtPotentialGuest = By.CssSelector("input[id*='inputContactId2'][type='text']");
        By drpdwnPotentialGuest = By.XPath("/html/body/ul[2]");
        By txtExpectedTravelCost = By.CssSelector("input[id*='j_id196:etc']");
        By txtExpectedFnBCost = By.CssSelector("input[id*='j_id196:efbc']");
        By txtOtherCost = By.CssSelector("input[id*='j_id196:aacs']");
        By txtDescOtherCost = By.CssSelector("input[id*='j_id198:specifyFieldId']");
        By txtHLInternalOpportunityName = By.CssSelector("input[class='required'][id*='j_id196:j_id205']");
        By txtTeamMember = By.CssSelector("input[id*='j_id218:inputContactId3'][type='text']");
        By drpdwnListTeamMember = By.XPath("/html/body/ul[3]");
        By btnSave = By.CssSelector("input[name*='bottom:j_id61']");
       
        public void CreateExpenseRequest(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, lnkExpenseRequest, 120);
            driver.FindElement(lnkExpenseRequest).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB);
            driver.FindElement(comboLOB).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, comboEventType);
            driver.FindElement(comboEventType).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, btnCreateNewExpenseForm);
            driver.FindElement(btnCreateNewExpenseForm).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtRequestor);
            driver.FindElement(txtRequestor).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 3));
            WebDriverWaits.WaitUntilEleVisible(driver, txtEventContact);
            driver.FindElement(txtEventContact).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, comboProductType);
            driver.FindElement(comboProductType).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 5));

            WebDriverWaits.WaitUntilEleVisible(driver, txtEventName);
            driver.FindElement(txtEventName).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 6));

            WebDriverWaits.WaitUntilEleVisible(driver, txtEventCity);
            driver.FindElement(txtEventCity).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 7));

            string getDate = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtStartDate);
            driver.FindElement(txtStartDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, comboEventFormat);
            driver.FindElement(comboEventFormat).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 8));

            WebDriverWaits.WaitUntilEleVisible(driver, comboNoOfGuest);
            driver.FindElement(comboNoOfGuest).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 9));
            WebDriverWaits.WaitUntilEleVisible(driver, selAvailableTargetAudience);
            driver.FindElement(selAvailableTargetAudience).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightTargetAudience);
            driver.FindElement(btnArrowRightTargetAudience).Click();

            string valPotentialGuest = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 16);
            Thread.Sleep(3000);
            driver.FindElement(txtPotentialGuest).SendKeys(valPotentialGuest);
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, drpdwnPotentialGuest, valPotentialGuest);
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtExpectedTravelCost);
            driver.FindElement(txtExpectedTravelCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 10));

            WebDriverWaits.WaitUntilEleVisible(driver, txtExpectedFnBCost);
            driver.FindElement(txtExpectedFnBCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 11));

            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherCost);
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 12));

            WebDriverWaits.WaitUntilEleVisible(driver, txtDescOtherCost);
            driver.FindElement(txtDescOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 13));

            WebDriverWaits.WaitUntilEleVisible(driver, txtHLInternalOpportunityName);
            driver.FindElement(txtHLInternalOpportunityName).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 14));
            
            string valTeamMember = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 15);
            Thread.Sleep(3000);
            driver.FindElement(txtTeamMember).SendKeys(valTeamMember);
            Thread.Sleep(4000);
            CustomFunctions.SelectValueWithoutSelect(driver, drpdwnListTeamMember, valTeamMember);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }    
    }
}