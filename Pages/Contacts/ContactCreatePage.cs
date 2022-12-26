using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.Pages.Contact
{
    class ContactCreatePage : BaseClass
    {
        By btnSave = By.CssSelector("div.pbHeader input[value='Save']");
        By btnSaveIgnoreAlert = By.CssSelector("td[class='pbButton '] > input[value='Save (Ignore Alert)']");
        By btnSaveAndNewIgnoreAlert = By.CssSelector("td[class='pbButton '] > input[value='Save and New (Ignore Alert)']");
        By btnCancel = By.CssSelector("td[class='pbButton '] > input[value='Cancel']");
        By lblDuplicateContactWarningMessage = By.CssSelector("span[id='contactNewPage:NewContactForm:j_id0:noof']");
        By popUpLookup = By.CssSelector("a[title='Company Name Lookup (New Window)'] > img");
        By txtSearchBox = By.XPath("//*[contains(@id,'txtSearch')]");
        By lblSearchBox = By.CssSelector("label[for*='formId:txtSearch']");
        By btnGo = By.CssSelector("input[id*='btnGo']");
        By selFirstOption = By.CssSelector("td[id*='tblResults:0:j_id49'] > a");//("tr[class='dataRow even first'] > td:first-child a");
        By firstName = By.CssSelector("input[id *='FirstName']");
        By lastName = By.CssSelector("input[id*='LastName']");
        By comboSalutation = By.CssSelector("select[id*='Salutation']");
        By txtEmail = By.CssSelector("input[id*='pgBlockSectionAcctInfo:Email']");
        By txtPhone = By.CssSelector("input[id*='pgBlockSectionAcctInfo:Phone']");
        By contactDetailsHeading = By.CssSelector("h2.mainTitle");
        By btnContinue = By.CssSelector("input[title='Continue']");
        By tabNewCompany = By.CssSelector("td[id*='tabTwo_lbl']");
        By comboCompanyType = By.CssSelector("td[class='data2Col  first '] > select");
        By txtCompanyName = By.CssSelector("div[class='requiredInput'] > input");
        By btnSaveNewCompany = By.CssSelector("div[class='pbBottomButtons'] > table > tbody > tr > td[class='pbButtonb '] > input");
        By comboCountrySelect = By.CssSelector("td[class='data2Col '] > select[id*='newAccountSection:AccountCountry']");
        By btnNewContact = By.CssSelector("input[name='newContact']");
        By txtContactTitle = By.CssSelector("input[id*='Title']");
        By txtPostalCode = By.CssSelector("input[id*='MailingPostalCode']");
        By txtCity = By.CssSelector("input[id*='MailingCity']");
        By txtStreet = By.CssSelector("textArea[id*='MailingStreet']");
        By selSalutation = By.CssSelector("select[id$='Salutation']");
        By selSelectRecordType = By.CssSelector("select[id='p3']");
        By txtGender = By.CssSelector("div[id='00Ni000000D7z8Uj_id0_j_id1_ileinner']");

        
        // To identify required tags/mandatory fields in Contact Create page
        public IWebElement ContactInformationRequiredTag(string fieldName)
        {
            return driver.FindElement(By.XPath($"//input[contains(@id, '{fieldName}')]/..//div"));
        }
         
        //To Click save button
        public void ClickSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        //To Click Cancel button
        public void ClickCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();
        }

        public void ClickSaveIgnoreAlertButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveIgnoreAlert);
            driver.FindElement(btnSaveIgnoreAlert).Click();
        }

        public bool ValidateIfSaveAndCancelButtonExists()
        {
            bool result = false;
            if (driver.FindElement(btnSaveIgnoreAlert).Displayed && driver.FindElement(btnSaveAndNewIgnoreAlert).Displayed && driver.FindElement(btnCancel).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool ValidateDuplicateContactWarningMessage()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, lblDuplicateContactWarningMessage);
            string message = driver.FindElement(lblDuplicateContactWarningMessage).Text;
            if (message.Contains("Click Cancel if duplicate exists. Click Save (Ignore Alert) to create new contact record."))
            {
                result = true;
            }
            return result;
        }

        //To Create Contact 
        public void CreateContact(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            //Click lookup 
            CustomFunctions.ActionClicks(driver, popUpLookup, 20);
            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);

            string excelPath = dir + file;
            // Enter value in search box
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchBox);
            driver.FindElement(txtSearchBox).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
            //Click on Go button
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            // Select first option
            WebDriverWaits.WaitUntilEleVisible(driver, selFirstOption);
            CustomFunctions.ActionClicks(driver, selFirstOption);
            // Switch back to default window
            CustomFunctions.SwitchToWindow(driver, 0);
            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, firstName, 40);
            driver.FindElement(firstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, lastName, 40);
            driver.FindElement(lastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 40);
            driver.FindElement(txtEmail).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 5));
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhone, 40);
            driver.FindElement(txtPhone).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 6));

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();           

        }

        //To Create Contact 
        public void CreateContact(string file,int row)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            //Click lookup 
            CustomFunctions.ActionClicks(driver, popUpLookup, 20);
            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);

            string excelPath = dir + file;
            // Enter value in search box
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchBox);
            driver.FindElement(txtSearchBox).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact",row, 1));
            //Click on Go button
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            // Select first option
            WebDriverWaits.WaitUntilEleVisible(driver, selFirstOption);
            CustomFunctions.ActionClicks(driver, selFirstOption);
            // Switch back to default window
            CustomFunctions.SwitchToWindow(driver, 0);
            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, firstName, 40);
            driver.FindElement(firstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));
            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, lastName, 40);
            driver.FindElement(lastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3));
            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 40);
            driver.FindElement(txtEmail).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact",row, 5));
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhone, 40);
            driver.FindElement(txtPhone).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact",row, 6));

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        //To Create Contact 
        public void CreateContactWithAddressDetails(string file, int row)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            //Click lookup 
            CustomFunctions.ActionClicks(driver, popUpLookup, 20);
            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);

            string excelPath = dir + file;
            // Enter value in search box
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchBox);
            driver.FindElement(txtSearchBox).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1));
            //Click on Go button
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            // Select first option
            WebDriverWaits.WaitUntilEleVisible(driver, selFirstOption);
            CustomFunctions.ActionClicks(driver, selFirstOption);
            // Switch back to default window
            CustomFunctions.SwitchToWindow(driver, 0);
            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, firstName, 40);
            driver.FindElement(firstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));
            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, lastName, 40);
            driver.FindElement(lastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3));
            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 40);
            driver.FindElement(txtEmail).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 5));
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhone, 40);
            driver.FindElement(txtPhone).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 6));

            //Enter PostalCode
            WebDriverWaits.WaitUntilEleVisible(driver, txtPostalCode, 40);
            driver.FindElement(txtPostalCode).Clear();
            driver.FindElement(txtPostalCode).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 11));

            //Enter City
            WebDriverWaits.WaitUntilEleVisible(driver, txtCity, 40);
            driver.FindElement(txtCity).Clear();
            driver.FindElement(txtCity).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 10));

            //Enter Street
            WebDriverWaits.WaitUntilEleVisible(driver, txtStreet, 40);
            driver.FindElement(txtStreet).Clear();
            driver.FindElement(txtStreet).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 9));

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        //Get heading of contact details page
        public string GetContactDetailsHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, contactDetailsHeading, 60);
            string headingcontactDetail = driver.FindElement(contactDetailsHeading).Text;
            return headingcontactDetail;
        }

        //Get label of search box in second window
        public string GetSearchBoxLabel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSearchBox, 60);
            string headingcontactDetail = driver.FindElement(lblSearchBox).Text;
            return headingcontactDetail;
        }

        // Validate mandatory fields on create contact page
        public bool ValidateMandatoryFields()
        {
            return ContactInformationRequiredTag("FirstName").GetAttribute("class").Contains("requiredBlock") &&
            ContactInformationRequiredTag("LastName").GetAttribute("class").Contains("requiredBlock") &&
            ContactInformationRequiredTag("Account").GetAttribute("class").Contains("requiredBlock");
        }

        // To create contact by adding a new company
        public void CreateContactByAddingNewCompany(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            /* WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
             driver.FindElement(btnContinue).Click();*/
            CustomFunctions.ActionClicks(driver, popUpLookup, 20);
            CustomFunctions.SwitchToWindow(driver, 1);
            // 
            WebDriverWaits.WaitUntilEleVisible(driver, tabNewCompany);
            driver.FindElement(tabNewCompany).Click();
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyType, 40);
            driver.FindElement(comboCompanyType).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, comboCountrySelect, 40);
            driver.FindElement(comboCountrySelect).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 5));

            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveNewCompany);
            driver.FindElement(btnSaveNewCompany).Click();
            CustomFunctions.SwitchToWindow(driver, 0);
            
            WebDriverWaits.WaitUntilEleVisible(driver, firstName, 40);
            driver.FindElement(firstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, lastName, 40);
            driver.FindElement(lastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        //To Create Contact 
        public void CreateContactFromCompany(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //Click new contact button

            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContact);
            driver.FindElement(btnNewContact).Click();
            
            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, firstName, 40);
            driver.FindElement(firstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));
            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, lastName, 40);
            driver.FindElement(lastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3));
            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 40);
            driver.FindElement(txtEmail).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 5));
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhone, 40);
            driver.FindElement(txtPhone).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 6));
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactTitle, 40);
            driver.FindElement(txtContactTitle).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 8));

            WebDriverWaits.WaitUntilEleVisible(driver, txtPostalCode, 40);
            driver.FindElement(txtPostalCode).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 9));
                      
            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        //To Create Contact 
        public void CreateContactFromCompany(string file, string FirstName,string LastName, string email, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //Click new contact button

            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContact);
            driver.FindElement(btnNewContact).Click();

            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, firstName, 40);
            driver.FindElement(firstName).SendKeys(FirstName);
            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, lastName, 40);
            driver.FindElement(lastName).SendKeys(LastName);
            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 40);
            driver.FindElement(txtEmail).SendKeys(email);
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhone, 40);
            driver.FindElement(txtPhone).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 6));
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactTitle, 40);
            driver.FindElement(txtContactTitle).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 8));

            WebDriverWaits.WaitUntilEleVisible(driver, txtPostalCode, 40);
            driver.FindElement(txtPostalCode).Clear();
            driver.FindElement(txtPostalCode).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 9));

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }




        //To Create Contact with salutation
        public void CreateContactwithSalutation(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string contactType = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 7);
            CustomFunctions.SelectByText(driver, driver.FindElement(selSelectRecordType), contactType);

            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Click();
            //Click lookup 
            CustomFunctions.ActionClicks(driver, popUpLookup, 20);
            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);

          
            // Enter value in search box
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchBox);
            driver.FindElement(txtSearchBox).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1));
            //Click on Go button
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            // Select first option
            WebDriverWaits.WaitUntilEleVisible(driver, selFirstOption);
            CustomFunctions.ActionClicks(driver, selFirstOption);
            // Switch back to default window
            CustomFunctions.SwitchToWindow(driver, 0);
            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, firstName, 40);
            driver.FindElement(firstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));
            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, lastName, 40);
            driver.FindElement(lastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3));
            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 40);
            driver.FindElement(txtEmail).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 5));
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhone, 40);
            driver.FindElement(txtPhone).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 6));

            //Enter Salutation
            WebDriverWaits.WaitUntilEleVisible(driver, selSalutation, 40);
         
            CustomFunctions.SelectByText(driver, driver.FindElement(selSalutation), ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 8));
           
            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        public string GetGender()

        {
            string GenderText = driver.FindElement(txtGender).Text;
          return GenderText;
        }
    }
}