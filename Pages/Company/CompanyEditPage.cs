using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.Pages.Company
{
    class CompanyEditPage : BaseClass
    {
        By comboCompanySubType = By.CssSelector("select[id='acc6']");
        By comboOwnership = By.CssSelector("select[id='acc14']");
        By txtParentCompany = By.CssSelector("input[id='acc3']");
        By selAvailableIndustryFocus = By.CssSelector("select[id*='00Ni000000D9WGg'] > optgroup > option:nth-child(1)");
        By btnArrowRightIndustryFocus = By.CssSelector("img[id='00Ni000000D9WGg_right_arrow']");
        By selAvailableDealPreference = By.CssSelector("select[id*='00Ni000000DvG7n'] > optgroup > option:nth-child(1)");
        By btnArrowRightDealPreference = By.CssSelector("img[id='00Ni000000DvG7n_right_arrow']");
        By selAvailableGeographicalFocus = By.CssSelector("select[id='00Ni000000D9WG2_unselected'] > optgroup > option:nth-child(1)");
        By btnArrowRightGeographicalFocus = By.CssSelector("img[id='00Ni000000D9WG2_right_arrow']");
        By txtDescription = By.CssSelector("textarea[id='acc20']");
        By txtCapIQCompany = By.CssSelector("input[id='CF00Ni000000DvFoM']");
        By btnSave = By.CssSelector("td[id='bottomButtonRow'] > input[value=' Save ']");
        By valEditPageHeading = By.CssSelector("h2[class='mainTitle']");
        By btnEditCompanyDetail = By.CssSelector("td[id='topButtonRowj_id0_j_id1'] > input[name='edit']");
        By txtPhone = By.CssSelector("input[id='acc10']");
        By txtStreet = By.CssSelector("textarea[id='acc17street']");
        By txtCity = By.CssSelector("input[id='acc17city']");
        By comboState = By.CssSelector("select[id='acc17state']");
        By txtZipPostal = By.CssSelector("input[id='acc17zip']");
        By txtCountry = By.CssSelector("select[id='acc17country']");
        By btnEdit = By.CssSelector("td[id*='topButtonRow'] >input[value=' Edit ']");
        By chkBoxHQ = By.CssSelector("input[id='00N3100000GyJM5']");
        By valErrorMsgHQNotTrue = By.CssSelector("td[class='dataCol col02'] > div[class='errorMsg']");
        By valCompanyType = By.XPath("//*[@id='Account.RecordType-_help']/label/../../../td[2]");
        By errMsgChangeCompanyType = By.CssSelector("div[id='errorDiv_ep']");
        By valCompanyName = By.CssSelector("input[id='acc2']");
        By linkDateERPSubmittedToSync = By.XPath("//*[text()='ERP Submitted To Sync']/../../../td[2]/span/span/a");

        
        public string GetCompanyNameEditPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyName, 60);
            string CompanyNameFromEditPage = driver.FindElement(valCompanyName).Text;
            return CompanyNameFromEditPage;
        }
        public void EditCompanyDetails(string file, string companyType)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            if (companyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
            {
                // Enter company sub type
                WebDriverWaits.WaitUntilEleVisible(driver, comboCompanySubType, 40);
                driver.FindElement(comboCompanySubType).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 9));

                //Enter ownership detail
                WebDriverWaits.WaitUntilEleVisible(driver, comboOwnership, 40);
                driver.FindElement(comboOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 10));

                //Enter Parent company
                //WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
                //driver.FindElement(txtParentCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 11));

                // Select value from industry focus
                WebDriverWaits.WaitUntilEleVisible(driver, selAvailableIndustryFocus, 40);
                driver.FindElement(selAvailableIndustryFocus).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightIndustryFocus, 40);
                driver.FindElement(btnArrowRightIndustryFocus).Click();

                // Select value from Deal Preference
                WebDriverWaits.WaitUntilEleVisible(driver, selAvailableDealPreference, 40);
                driver.FindElement(selAvailableDealPreference).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightDealPreference, 40);
                driver.FindElement(btnArrowRightDealPreference).Click();

                // Select value from Geographical Preference
                WebDriverWaits.WaitUntilEleVisible(driver, selAvailableGeographicalFocus, 40);
                driver.FindElement(selAvailableGeographicalFocus).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightGeographicalFocus, 40);
                driver.FindElement(btnArrowRightGeographicalFocus).Click();
            }
            //Enter Description
            WebDriverWaits.WaitUntilEleVisible(driver, txtDescription, 40);
            driver.FindElement(txtDescription).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 12));

            //Enter CapIQ company
            //WebDriverWaits.WaitUntilEleVisible(driver, txtCapIQCompany, 40);
            //driver.FindElement(txtCapIQCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 13));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        //Get heading of company edit page details 
        public string GetCompanyEditPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEditPageHeading, 60);
            string editPageHeading = driver.FindElement(valEditPageHeading).Text;
            return editPageHeading;
        }

        //Edit company detail
        public void UpdateExistingCompanyDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditCompanyDetail);
            driver.FindElement(btnEditCompanyDetail).Click();

            //Enter updated phone number
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhone, 40);
            driver.FindElement(txtPhone).Clear();
            driver.FindElement(txtPhone).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));

            //Enter updated country
            WebDriverWaits.WaitUntilEleVisible(driver, txtCountry, 40);
            driver.FindElement(txtCountry).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 5));

            //Enter updated street 
            WebDriverWaits.WaitUntilEleVisible(driver, txtStreet, 40);
            driver.FindElement(txtStreet).Clear();
            driver.FindElement(txtStreet).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 6));

            //Enter updated city
            WebDriverWaits.WaitUntilEleVisible(driver, txtCity, 40);
            driver.FindElement(txtCity).Clear();
            driver.FindElement(txtCity).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 7));

            //Enter updated state
            WebDriverWaits.WaitUntilEleVisible(driver, comboState, 40);
            driver.FindElement(comboState).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 8));

            //Enter updated zipPostalCode
            WebDriverWaits.WaitUntilEleVisible(driver, txtZipPostal, 40);
            driver.FindElement(txtZipPostal).Clear();
            driver.FindElement(txtZipPostal).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 9));

            //Enter updated description
            WebDriverWaits.WaitUntilEleVisible(driver, txtDescription, 40);
            driver.FindElement(txtDescription).Clear();
            driver.FindElement(txtDescription).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 10));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        public void ValidateUpdatingParentCompanyOnly(string file)
        {
            //Click edit button
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 40);
            driver.FindElement(btnEdit).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string checkBoxValue = driver.FindElement(chkBoxHQ).GetAttribute("checked");
            if (checkBoxValue == null)
            {
                Console.WriteLine("HQ checkbox not checked ");
            }
            else if (checkBoxValue.Equals("true"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkBoxHQ, 40);
                driver.FindElement(chkBoxHQ).Click();
            }
            //Enter Parent company
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
            driver.FindElement(txtParentCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        public void ValidateCheckingHQCheckBoxOnly(string file)
        {
            //Click edit button
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 40);
            driver.FindElement(btnEdit).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //Clear Parent company
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
            driver.FindElement(txtParentCompany).Clear();

            // Click Checkbox HQ
            string checkBoxValue = driver.FindElement(chkBoxHQ).GetAttribute("checked");
            if (checkBoxValue != ("true"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkBoxHQ, 40);
                driver.FindElement(chkBoxHQ).Click();
            }
            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        public void ValidateErrorHQCanBeTrue(string file)
        {
            //Click edit button
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 40);
            driver.FindElement(btnEdit).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            // Click Checkbox HQ
            string checkBoxValue = driver.FindElement(chkBoxHQ).GetAttribute("checked");
            if (checkBoxValue != ("true"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkBoxHQ, 40);
                driver.FindElement(chkBoxHQ).Click();
            }
            //Add Parent company
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
            driver.FindElement(txtParentCompany).Clear();
            driver.FindElement(txtParentCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        //Error msg for HQ no true
        public string GetErrorMsgHQNoTrue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valErrorMsgHQNotTrue, 60);
            string errMsgHQNotTrue = driver.FindElement(valErrorMsgHQNotTrue).Text;
            return errMsgHQNotTrue;
        }

        //Error msg for company record type change
        public string GetErrorMsgForCompanyRecordTypeChange()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, errMsgChangeCompanyType, 60);
            string errMsgOnChangeCompanyType = driver.FindElement(errMsgChangeCompanyType).Text;
            return errMsgOnChangeCompanyType;
        }


        public string GetCompanyType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyType, 60);
            string companyType = driver.FindElement(valCompanyType).Text;
            return companyType;
        }
        public void UpdateERPSubmittedToSyncField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkDateERPSubmittedToSync, 40);
            driver.FindElement(linkDateERPSubmittedToSync).Click();

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }
    }
}
