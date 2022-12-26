using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;

namespace SalesForce_Project.Pages.Company
{
    class CompanyCreatePage : BaseClass
    {
        By lnkCompanies = By.CssSelector("a[title*='Companies Tab']");
        By btnAddCompany = By.CssSelector("td[class='pbButton center'] > input[value='Add Company']");
        By headingCompanyCreate = By.CssSelector("h2[class='mainTitle']");
        By selectedCompanyType = By.CssSelector("select[id*='j_id77'] > option[selected='selected']");
        By txtCompanyName = By.CssSelector("input[id*='AccountName']");
        By comboCompanyCountry = By.CssSelector("select[id*='AccountCountry']");
        By txtCompanyStreet = By.CssSelector("textarea[id*='j_id79']");
        By txtCompanyCity = By.CssSelector("input[id*='AccountCity']");
        By comboCompanyState = By.CssSelector("select[id*='AccountState']");
        By txtCompanyPostalCode = By.CssSelector("input[id*='AccountPostalCode']");
        By btnSave = By.CssSelector("td[class='pbButtonb '] > input[value='Save']");
        By btnSaveIgnoreAlert = By.CssSelector("td[class='pbButton '] > input[value='Save(Ignore Alert)']");
        By errmsgCmpanyName= By.CssSelector("div[class='errorMsg']");
        //By txtZipPostalCode 


        public string GetCreateCompanyPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headingCompanyCreate, 60);
            string headingCompanyCreatePage = driver.FindElement(headingCompanyCreate).Text;
            return headingCompanyCreatePage;
        }

        public string GetSelectedCompanyType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectedCompanyType, 60);
            string selectedCompanyTypeSelected = driver.FindElement(selectedCompanyType).Text;
            return selectedCompanyTypeSelected;
        }

        public void AddCompany(string file, int companyRow)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            // Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", companyRow, 2));
            // Select country 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyCountry, 40);
            driver.FindElement(comboCompanyCountry).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 3));
            // Enter street 
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyStreet, 40);
            driver.FindElement(txtCompanyStreet).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));
            // Enter city
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyCity, 40);
            driver.FindElement(txtCompanyCity).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 5));
            // Select state
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyState, 40);
            driver.FindElement(comboCompanyState).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 6));
            // Enter postal code
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyPostalCode, 40);
            driver.FindElement(txtCompanyPostalCode).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 14));

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        public void ClickSaveIgnoreAlertButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveIgnoreAlert);
            driver.FindElement(btnSaveIgnoreAlert).Click();
        }

        public string errmsgCompanyName()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
            string errmsg = driver.FindElement(errmsgCmpanyName).Text;
            return errmsg;
        }
    }
}