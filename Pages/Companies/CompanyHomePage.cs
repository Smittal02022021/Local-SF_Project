using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class CompanyHomePage : BaseClass
    {
        By lnkCompanies = By.CssSelector("a[title*='Companies Tab']");
        By txtCompanyName = By.CssSelector("input[id*='j_id37:nameSearch']");
        By btnCompanySearch = By.CssSelector("div[class='searchButtonPanel'] > center > input[value='Search']");
        By tblResults = By.CssSelector("table[id*='pbtCompanies']");
        By matchedResult = By.CssSelector("td[id*=':pbtCompanies:0:j_id68'] a");
              
        
        By CompanyHomePageHeading = By.CssSelector("h2[class='pageDescription']");
        By btnAddCompany = By.CssSelector("td[class='pbButton center'] > input[value='Add Company']");
        By errPage = By.CssSelector("span[id='theErrorPage:theError']");

        string dir = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\";

        // To Search Company
        public string SearchCompany(string file, string CompanyType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies, 120);
            driver.FindElement(lnkCompanies).Click();
            string excelPath = dir + file;
            if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2));
            }
            else if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 2));
            }
            else if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 4, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 4, 2));
            }
            // 4th Company Type
            else if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 5, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 5, 2));
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnCompanySearch);
            Thread.Sleep(2000);
            driver.FindElement(btnCompanySearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }
        
        // Click add company button
        public void ClickAddCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies);
            driver.FindElement(lnkCompanies).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCompany, 120);
            driver.FindElement(btnAddCompany).Click();
        }

        // Search Company from Tableau downloaded sheet
        public string SearchTableauCompany(string file, int row1)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies, 120);
            driver.FindElement(lnkCompanies).Click();

            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Sheet 1", row1, 4));
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnCompanySearch);
            Thread.Sleep(2000);
            driver.FindElement(btnCompanySearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                WebDriverWaits.WaitForPageToLoad(driver, 60);
                //return result;
            }
            catch (Exception)
            {
                //return "No record found";
            }
            return ReadExcelData.ReadDataMultipleRows(excelPath, "Sheet 1", row1, 4);
        }
    }
}
