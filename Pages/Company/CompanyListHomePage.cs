using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Company
{
    class CompanyListHomePage : BaseClass
    {
        By lnkCompanyList = By.XPath("//a[normalize-space()='Company Lists']");
        By btnNewCompanyList = By.CssSelector("input[name='new']");
        By txtCompanyListName = By.CssSelector("input[id='Name']");
        By btnSave = By.CssSelector("td[id='topButtonRow'] > input[name='save']");
        By lnkAlphabelC = By.CssSelector("div[id='000000000000000_rolodex'] > a:nth-child(3)");//  000000000000000_rolodex
        By lnkEdit = By.XPath("//span[normalize-space()='CompanyListTest']/../../../preceding-sibling::td[1]/div/a[1]");
        By txtFirstCompanyInList = By.CssSelector("div[id='a2Q5A000000xjFk_Name'] > a >span");
        By lnkCompanyListName = By.XPath("//a[normalize-space()='CompanyListTest']");
        By lnkUpdatedCompanyList = By.XPath("//a[normalize-space()='CompanyListTestNew']");
        By valUpdatedCompanyList = By.CssSelector("div[id='Name_ileinner']");
        By pinIndicator = By.CssSelector("span[id='pinIndicator']");

        
        public void AddCompanyList(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanyList);
            driver.FindElement(lnkCompanyList).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanyList);
            driver.FindElement(btnNewCompanyList).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyListName, 40);
            string companyName = ReadExcelData.ReadData(excelPath, "Company", 1);
            driver.FindElement(txtCompanyListName).SendKeys(companyName);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        public void VerifyPageAndFieldErrorMessage()
        {         
            //WebDriverWaits.WaitUntilEleVisible(driver, pinIndicator);
            //driver.FindElement(pinIndicator).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanyList);
            driver.FindElement(lnkCompanyList).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanyList);
            driver.FindElement(btnNewCompanyList).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        public void NavigateToCompanyListPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanyList);
            driver.FindElement(lnkCompanyList).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkAlphabelC);
            driver.FindElement(lnkAlphabelC).Click();

        }

        public void EditCompanyList(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit);
            driver.FindElement(lnkEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyListName, 40);
            driver.FindElement(txtCompanyListName).Clear();
            driver.FindElement(txtCompanyListName).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        public char GetCompanyListNameFirstCharacter()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstCompanyInList, 60);
            string companyName = driver.FindElement(txtFirstCompanyInList).Text;
            char companyNameFirstChar = companyName[0];
            return companyNameFirstChar;
        }

        public string GetUpdatedCompanyList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkUpdatedCompanyList, 60);
            driver.FindElement(lnkUpdatedCompanyList).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, valUpdatedCompanyList, 60);
            string updatedCompanyList = driver.FindElement(valUpdatedCompanyList).Text;
            return updatedCompanyList;
        }

        public bool ValidateDeletionOfCompanyList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanyList);
            driver.FindElement(lnkCompanyList).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkAlphabelC);
            driver.FindElement(lnkAlphabelC).Click();

            return (CustomFunctions.IsElementPresent(driver, lnkCompanyListName) || CustomFunctions.IsElementPresent(driver, lnkUpdatedCompanyList));
        }

        public bool ValidateNewCreatedCompanyList()
        {
            return (CustomFunctions.IsElementPresent(driver, lnkCompanyListName));
        }

        public bool ValidateAvailabilityOfCompanyListAfterCancel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkAlphabelC);
            driver.FindElement(lnkAlphabelC).Click();
            return (CustomFunctions.IsElementPresent(driver, lnkCompanyListName));
        }
    }

}
