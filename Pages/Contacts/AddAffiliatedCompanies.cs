using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;


namespace SalesForce_Project.Pages.Contact
{
    class AddAffiliatedCompanies : BaseClass
    {
        By headingNewAffiliate = By.CssSelector("h2[class='pageDescription']");
        By btnSave = By.CssSelector("td[id='topButtonRow'] > input[value=' Save ']");
        By btnCancel = By.CssSelector("td[id='topButtonRow'] > input[value='Cancel']");
        By valPageLevelError = By.CssSelector("div[id='errorDiv_ep']");
        By txtCompanyName = By.CssSelector("input[id='CF00Ni000000D735b']");
        By txtContactName = By.CssSelector("input[id='CF00Ni000000D735q']");
        By comboAffiliationStatus = By.CssSelector("select[id='00Ni000000D737m']");
        By comboAffiliationType = By.CssSelector("select[name='00Ni000000D737h']");
        By affiliationNotes = By.CssSelector("textarea[id='00Ni000000D737w']");
        By linkEditAffiliationCompany = By.CssSelector("div[id*='00Ni000000D735q_body'] > table  > tbody > tr:nth-child(2) > td[class='actionColumn'] > a:nth-child(1)");
        By btnSaveAndNew = By.CssSelector("td[id='topButtonRow'] > input[name='save_new']");

        
        //FUnction to get new affiliation heading
        public string GetNewAffliationCompaniesHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headingNewAffiliate, 60);
            string headingNewAffliation = driver.FindElement(headingNewAffiliate).Text;
            return headingNewAffliation;
        }

        // To identify required tags/mandatory fields in Contact Create page
        public IWebElement AddAffilationRequiredTag(string tagName,string fieldName)
        {
            IWebElement element = driver.FindElement(By.XPath($"//{tagName}[@id='{fieldName}']/../..//div"));
            return element;// driver.FindElement(By.XPath($"//input[@id='{fieldName}')]/../..//div"));
        }

        // Validate mandatory fields on new affilation companies page
        public bool ValidateMandatoryFields()
        {
            return AddAffilationRequiredTag("input","CF00Ni000000D735b").GetAttribute("class").Contains("requiredBlock") &&
            AddAffilationRequiredTag("input","CF00Ni000000D735q").GetAttribute("class").Contains("requiredBlock") &&
            AddAffilationRequiredTag("select", "00Ni000000D737m").GetAttribute("class").Contains("requiredBlock") &&
            AddAffilationRequiredTag("select", "00Ni000000D737h").GetAttribute("class").Contains("requiredBlock");
        }

        public void ClickSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        public void ClickSaveAndNewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveAndNew);
            driver.FindElement(btnSaveAndNew).Click();
        }
        public string GetPageLevelError()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPageLevelError, 60);
            string pageLevelError = driver.FindElement(valPageLevelError).Text;
            return pageLevelError;
        }

        public void EnterNewAffilationCompaniesDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName, 40);
            driver.FindElement(txtContactName).Clear();
            driver.FindElement(txtContactName).SendKeys(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, comboAffiliationStatus, 40);
            driver.FindElement(comboAffiliationStatus).SendKeys(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 3));
            WebDriverWaits.WaitUntilEleVisible(driver, comboAffiliationType, 40);
            driver.FindElement(comboAffiliationType).SendKeys(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 4));
            WebDriverWaits.WaitUntilEleVisible(driver, affiliationNotes, 40);
            driver.FindElement(affiliationNotes).SendKeys(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 5));
                       
        }

        public void EditNewAffilationCompaniesDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, linkEditAffiliationCompany, 40);
            driver.FindElement(linkEditAffiliationCompany).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, comboAffiliationStatus, 40);
            driver.FindElement(comboAffiliationStatus).SendKeys(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, comboAffiliationType, 40);
            driver.FindElement(comboAffiliationType).SendKeys(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, affiliationNotes, 40);
            driver.FindElement(affiliationNotes).Clear();
            driver.FindElement(affiliationNotes).SendKeys(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 3));
        }

        public void ClickCancelButton()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();
        }

        public string GetCompanyNameText()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            return driver.FindElement(txtCompanyName).Text;
        }
        public string GetContactNameText()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName, 40);
            return driver.FindElement(txtContactName).Text;
        }
    }
}
