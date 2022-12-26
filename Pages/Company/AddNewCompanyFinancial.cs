using OpenQA.Selenium;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SalesForce_Project.Pages.Company
{
    class AddNewCompanyFinancial : BaseClass
    {
        CompanyHomePage companyHome = new CompanyHomePage();
        By valNewCompanyFinancialTitle = By.CssSelector("h2[class='pageDescription']");
        By valPrefilledSelectedCompany = By.CssSelector("span[class='lookupInput'] > input");
        By comboYear = By.CssSelector("div[class='requiredInput'] > span > select");
        By txtAsOfDate = By.CssSelector("span[class*='dateOnlyInput'] > input");
        By btnSave = By.CssSelector("input[name='save']");
        By drpdwnRecentYear = By.CssSelector("div[class='requiredInput'] > span > select > option:nth-child(2)");
        By btnCancel = By.CssSelector("td[class*='pbButtonb'] > input[name='cancel']");
        By linkEditCompanyFinancial = By.CssSelector("div[id*='ke_body'] > table > tbody > tr:nth-child(2) > td > a:nth-child(1)");
        By errPage = By.CssSelector("span[id='theErrorPage:theError']");

        
        public string GetNewCompanyFinancialHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewCompanyFinancialTitle, 60);
            string headingCompanyDetail = driver.FindElement(valNewCompanyFinancialTitle).Text;
            return headingCompanyDetail;
        }

        public void ClickCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 40);
            driver.FindElement(btnCancel).Click();
        }

        public string GetMostRecentYear(int rows)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[class='requiredInput'] > span > select > option:nth-child({rows})"), 60);
            string mostRecentYear = driver.FindElement(By.CssSelector($"div[class='requiredInput'] > span > select > option:nth-child({rows})")).Text;
            return mostRecentYear;
        }

        public string GetPreFilledSelectedCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrefilledSelectedCompany, 60);
            string preFilledSelectedCompany = driver.FindElement(valPrefilledSelectedCompany).GetAttribute("value");
            return preFilledSelectedCompany;
        }

        public string CreateNewCompanyFinancial(string file,int rows)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            // Enter year
            WebDriverWaits.WaitUntilEleVisible(driver, comboYear, 40);
            driver.FindElement(comboYear).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyFinancial",rows,  1));

            string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            //if(getDate.)
            WebDriverWaits.WaitUntilEleVisible(driver, txtAsOfDate);
            driver.FindElement(txtAsOfDate).SendKeys(getDate);


            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();

            return getDate;
        }

        public string EditCompanyFinancialFirstRecord(string file, string CompanyType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, linkEditCompanyFinancial, 120);
            driver.FindElement(linkEditCompanyFinancial).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            // Enter year
            WebDriverWaits.WaitUntilEleVisible(driver, comboYear, 40);
            driver.FindElement(comboYear).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyFinancial", 2, 3));

            string secondMostRecentYearFromNewFinancialCreateDrpDwn = GetMostRecentYear(2);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();

            return secondMostRecentYearFromNewFinancialCreateDrpDwn;
        }
    }
}
