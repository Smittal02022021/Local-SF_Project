using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Company
{
    class CompanyListCreatePage : BaseClass
    {

        By valPageLevelError = By.CssSelector("div[id='errorDiv_ep']");
        By valFieldLevelError = By.CssSelector("div[class='errorMsg']");
        By txtCompanyListName = By.CssSelector("input[id='Name']");
        By btnCancel = By.CssSelector("input[name='cancel']");

       
        public string GetPageLevelError()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPageLevelError, 60);
            string pageLevelError = driver.FindElement(valPageLevelError).Text;
            return pageLevelError;
        }

        public string GetFieldLevelError()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFieldLevelError, 60);
            string fieldLevelError = driver.FindElement(valFieldLevelError).Text;
            return fieldLevelError;
        }

        public void VerifyCancelFunctionality(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyListName, 40);
            string companyName = ReadExcelData.ReadData(excelPath, "Company", 1);
            driver.FindElement(txtCompanyListName).SendKeys(companyName);

            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();
        }
    }
}
