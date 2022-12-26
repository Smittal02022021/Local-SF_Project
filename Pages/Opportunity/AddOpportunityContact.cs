using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;

namespace SalesForce_Project.Pages.Opportunity
{
    class AddOpportunityContact : BaseClass
    {
        By btnAddOppContact = By.CssSelector("input[name='new_external_team']");
        By btnSave = By.CssSelector("input[name='save']");
        By txtContact = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D7Ns3']");
        By comboRole = By.CssSelector("select[name*='D7OOx']");
        By comboType = By.CssSelector("select[name*='D7OAq']");
        By checkPrimaryContact = By.CssSelector("input[name*='D7Nro']");
        By comboParty = By.CssSelector("select[name*='M0eMp']");
        By checkAckBillingContact = By.CssSelector("input[name*='M0jSN']");
        By checkBillingContact = By.CssSelector("input[name*='Gz3dL']");

        //string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        public void CreateContact(string file, string contact, string valRecType, string valType)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContact, 70);
            driver.FindElement(btnAddOppContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);

            driver.FindElement(txtContact).SendKeys(contact);
            driver.FindElement(comboRole).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 2));
            driver.FindElement(comboType).SendKeys(valType);
            driver.FindElement(checkPrimaryContact).Click();
            if (valRecType.Equals("CF"))
            {
                driver.FindElement(comboParty).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 3));
            }
            driver.FindElement(checkAckBillingContact).Click();
            driver.FindElement(checkBillingContact).Click();
            driver.FindElement(btnSave).Click();
        }
        public void CreateContact(string file, string contact, string valRecType, string valType, int rowNumber)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContact, 50);
            driver.FindElement(btnAddOppContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);

            driver.FindElement(txtContact).SendKeys(contact);
            driver.FindElement(comboRole).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 2));
            driver.FindElement(comboType).SendKeys(valType);

            string Type = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowNumber, 4);
            if (Type.Equals("Client"))
            {
                driver.FindElement(checkPrimaryContact).Click();
            }
            if (valRecType.Equals("CF"))
            {
                driver.FindElement(comboParty).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 3));
            }
            if (Type.Equals("External"))
            {
                driver.FindElement(checkPrimaryContact).Click();
                driver.FindElement(checkAckBillingContact).Click();
                driver.FindElement(checkBillingContact).Click();
            }
            driver.FindElement(btnSave).Click();
        }
    }
}


