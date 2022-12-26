using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;


namespace SalesForce_Project.Pages.Contact
{
    class ContactEditPage : BaseClass
    {
        By contactEditHeading = By.CssSelector("h2.mainTitle");
        By btnEdit = By.CssSelector("td[id*='topButtonRow'] >input[name='edit']");
        By txtMiddleName = By.CssSelector("input[id='name_middlecon2']");
        By txtMailingStreet = By.CssSelector("textarea[id='con19street']");
        By txtOffice = By.CssSelector("#ep > div.pbBody > div:nth-child(3) > table > tbody > tr:nth-child(7) > td:nth-child(2) > input");
        By txtMailingCity = By.CssSelector("input[id='con19city']");
        By txtMailingState = By.CssSelector("select[id='con19state']");
        By btnSaveAndNew = By.CssSelector("td[id='topButtonRow'] > input[name='save_new']");
        By comboContactStatus = By.CssSelector("select[id='00Ni000000D7LLC']");
        By comboOffice = By.CssSelector("select[id='00Ni000000Fjq9r']");
        By comboPhysicalOffice = By.CssSelector("select[id='00N3100000Gb67T']");
        By txtTitle = By.CssSelector("input[id='con5']");
        By txtDepartment = By.CssSelector("input[id='con6']");
        //By txtHireDate = By.CssSelector("input[id='00N3100000Gb67A']");
        By comboCountry = By.CssSelector("select[id='con19country']");
        By comboLineOfBusiness = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(11) > td:nth-child(2) > span > select");
        By comboLineOfBusinessDistributed = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(9) > td:nth-child(2) > span > select");
        By comboProjectNotifyRole = By.CssSelector("select[title='Project Notify Role - Available']");
        By rightArrow = By.CssSelector("img[id*='67d_right_arrow']");
        By txtHireDate = By.CssSelector("div[class='requiredInput'] > span[class*='dateOnlyInput'] > input");
        By comboMentor = By.CssSelector("span[class='lookupInput'] > select[id*='Gz5Lf']");
        By txtMentor = By.CssSelector("span[class='lookupInput'] > input[id*='Gz5Lf']");
        By lookupCompany = By.CssSelector("a[title='Company Name Lookup (New Window)']");


        By searchFrame = By.CssSelector("frame#searchFrame");
        By resultFrame = By.CssSelector("frame#resultsFrame");
        By txtSearchBox = By.CssSelector("input#lksrch");
        By btnGo = By.CssSelector("input[name=go]");

        By txtSearchResults = By.CssSelector("tr[class='dataRow even first']>th>a");

        By btnSave = By.CssSelector("td[id='topButtonRow'] > input[name='save']");

        By txtErrorMessageCompany = By.CssSelector("#errorDiv_ep");
        By txtDepartureDate = By.CssSelector("input[id='00N3100000Gb671']");
        By txtErrorMessageDepartureDate = By.CssSelector("div[class='errorMsg']");
        By btnCancel = By.CssSelector("input[title='Cancel']");
        By lookupExpenseApprover = By.CssSelector("img[alt='Expense Approver Lookup (New Window)']");
        By radioBtnAllFields = By.CssSelector("input[value=SEARCH_ALL]");
        By txtSearchResultExpenseApprover = By.CssSelector("tr[class='dataRow even last first']>th>a");
        By selectFlagReasonDrpDown = By.CssSelector("select[name='00Ni000000Fk47Y']");
        By selectCurrencyDrpDown = By.CssSelector("select[name='con21']");
        By txtLastName = By.CssSelector("input[name='name_lastcon2']");

        By lookupPrimaryPDC = By.CssSelector("img[alt='Primary PDC Lookup (New Window)']");
        By txtSecondaryPDC = By.CssSelector("input[name='CF00N3100000Gb67h']");
        By txtDealAnnouncementsChangeDate = By.XPath("//td[text()='Deal Announcements Change Date']//following::div[1]");
        By txtEventsChangeDate = By.CssSelector(@"#\30 0Ni000000FZZiNj_id0_j_id1_ileinner");

        By txtGeneralAnnouncementsChangeDate = By.CssSelector(@"#\30 0Ni000000FZZiSj_id0_j_id1_ileinner");
        By txtContentChangeDate = By.CssSelector(@"#\30 0Ni000000FZZmFj_id0_j_id1_ileinner");
        By inputDealAnnouncementsChangeDate = By.CssSelector("input[name='00Ni000000FZZiI']");
        By inputEventsChangeDate = By.CssSelector("input[name='00Ni000000FZZiN']");
        By inputGeneralAnnouncementsChangeDate = By.CssSelector("input[name='00Ni000000FZZiS']");
        By inputContentChangeDate = By.CssSelector("input[name='00Ni000000FZZmF']");
        By txtBadgeFirstName = By.CssSelector(@"#\30 0Ni000000FYRYnj_id0_j_id1_ileinner");
        By txtBadgeLastName = By.CssSelector(@"#\30 0Ni000000FYRYsj_id0_j_id1_ileinner");
        By txtBadgeCompany = By.CssSelector(@"#\30 0Ni000000FYRZ2j_id0_j_id1_ileinner");
        By txtBadgeFullName = By.CssSelector(@"#\30 0Ni000000FYRZ7j_id0_j_id1_ileinner");
        By checkboxCopyFromContactDetail = By.CssSelector(@"#\30 0Ni000000FZTyw");
        By txtGoogleMaps = By.CssSelector("table[class='customLinks']>tbody>tr>td:nth-of-type(1)>a");

        public string GetContactEditHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, contactEditHeading, 60);
            string headingcontactEdit = driver.FindElement(contactEditHeading).Text;
            return headingcontactEdit;
        }

        public bool ValidateOffileFieldEditableForHCMUser()
        {
            bool result = false;
            string val = driver.FindElement(txtOffice).GetAttribute("Type");
            if(val == "hidden")
            {
               result = true;
            }
            return result;
        }

        public void EditContact(string file, int row, int userRow)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", userRow, 1).Equals("Admin"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtMiddleName);
                driver.FindElement(txtMiddleName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 7));

                WebDriverWaits.WaitUntilEleVisible(driver, txtMailingStreet);
                driver.FindElement(txtMailingStreet).Clear();
                driver.FindElement(txtMailingStreet).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 8));

                WebDriverWaits.WaitUntilEleVisible(driver, txtMailingCity);
                driver.FindElement(txtMailingCity).Clear();
                driver.FindElement(txtMailingCity).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 9));

                WebDriverWaits.WaitUntilEleVisible(driver, comboCountry);
                driver.FindElement(comboCountry).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 11));

                WebDriverWaits.WaitUntilEleVisible(driver, txtMailingState);
                driver.FindElement(txtMailingState).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 10));
               //CustomFunctions.SelectByText(driver, driver.FindElement(By.XPath("//select[@id='00N3100000Gb675']")), "None");

            }
            if (ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1).Contains("Houlihan Employee"))
            {
                if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", userRow, 1).Equals("Admin"))
                {
                    CustomFunctions.SelectByText(driver, driver.FindElement(By.XPath("//select[@id='00N3100000Gb675']")), "None");

                    WebDriverWaits.WaitUntilEleVisible(driver, comboContactStatus);
                    driver.FindElement(comboContactStatus).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 12));

                    WebDriverWaits.WaitUntilEleVisible(driver, comboOffice);
                    driver.FindElement(comboOffice).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 13));

                    WebDriverWaits.WaitUntilEleVisible(driver, comboPhysicalOffice);
                    driver.FindElement(comboPhysicalOffice).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 14));

                    WebDriverWaits.WaitUntilEleVisible(driver, txtTitle);
                    driver.FindElement(txtTitle).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 15));

                    WebDriverWaits.WaitUntilEleVisible(driver, comboLineOfBusiness);
                    driver.FindElement(comboLineOfBusiness).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 17));

                    WebDriverWaits.WaitUntilEleVisible(driver, txtDepartment);
                    driver.FindElement(txtDepartment).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 16));

                    string getDates = DateTime.Today.AddDays(-10).ToString("MM/dd/yyyy");
                    WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
                    driver.FindElement(txtHireDate).SendKeys(getDates);
                }
                else
                {
                    string getDate = DateTime.Today.AddDays(-10).ToString("dd/MM/yyyy");
                    WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
                    driver.FindElement(txtHireDate).SendKeys(getDate);
                }
            }
            else if (ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1).Contains("Distribution List"))
            {
                if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", userRow, 1).Equals("Admin"))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, comboLineOfBusinessDistributed);
                    driver.FindElement(comboLineOfBusinessDistributed).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 17));

                    WebDriverWaits.WaitUntilEleVisible(driver, txtDepartment);
                  //  CustomFunctions.SelectByText(driver, driver.FindElement(By.XPath("//select[@id='00N3100000Gb675']")), "None");

                    driver.FindElement(txtDepartment).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 16));
                }
                else
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, txtMentor);
                    //CustomFunctions.SelectByText(driver, driver.FindElement(comboMentor), ReadExcelData.ReadData(excelPath, "Contact", 19));
                    driver.FindElement(txtMentor).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 19));
                }
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveAndNew, 120);
            driver.FindElement(btnSaveAndNew).Click();
        }

        public void EditCompany()
        {


            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lookupCompany, 120);
            driver.FindElement(lookupCompany).Click();

            CustomFunctions.SwitchToWindow(driver, 1);
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));

            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("test");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            // WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);

            driver.SwitchTo().Frame(driver.FindElement(resultFrame));

            driver.FindElement(txtSearchResults).Click();
            CustomFunctions.SwitchToWindow(driver, 0);


        }

        public void ClickSaveBtn()
        {


            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 120);
            driver.FindElement(btnSave).Click();

        }
        public string TxtErrorMessageCompany()
        {
            string text = driver.FindElement(txtErrorMessageCompany).Text;
            return text;
        }

        public void VerifyDepartureDate()
        {
            driver.Navigate().Back();
            /* 
           WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
           driver.FindElement(btnEdit).Click();
           string getDate = DateTime.Today.ToString("dd/MM/yyyy");
             WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
             driver.FindElement(txtHireDate).SendKeys(getDate);*/

            CustomFunctions.SelectByText(driver, driver.FindElement(By.XPath("//select[@id='00N3100000Gb675']")), "None");
            driver.FindElement(txtDepartureDate).SendKeys("08/20/2021");
        }

        public void VerifyDepartureDateforInactiveEmployee()
        {
            driver.FindElement(comboContactStatus).SendKeys("Inactive");
            driver.FindElement(txtDepartureDate).Clear();

        }

        public string TxtErrorMessageDepartureDate()
        {
            string text = driver.FindElement(txtErrorMessageDepartureDate).Text;
            return text;
        }
        public void ClickCancelBtn()
        {
            driver.FindElement(btnCancel).Click();

        }

        public void VerifyExpenseApproverValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(lookupExpenseApprover).Click();

            CustomFunctions.SwitchToWindow(driver, 1);

            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("Inactive");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            driver.FindElement(txtSearchResultExpenseApprover).Click();


            CustomFunctions.SwitchToWindow(driver, 0);

        }


        public void VerifyFlagReasonValidation(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();

            CustomFunctions.SelectByText(driver, driver.FindElement(selectFlagReasonDrpDown), value);

        }


        public void VerifyEmployeeCurrencyValidation(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();

            string getDate = DateTime.Today.AddDays(-10).ToString("dd/MM/yyyy");
            string date = getDate.Replace('-', '/');
            WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
            driver.FindElement(txtHireDate).SendKeys(date);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCurrencyDrpDown), value);
        }

        public void VerifyLastNameValidation()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();

            string getDate = DateTime.Today.AddDays(-10).ToString("dd/MM/yyyy");
            string date = getDate.Replace('-', '/');
            WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
            driver.FindElement(txtHireDate).SendKeys(date);
            driver.FindElement(txtLastName).SendKeys("test");
        }

        public void VerifyIndustryGroupValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, comboLineOfBusiness);
            driver.FindElement(comboLineOfBusiness).SendKeys("CF");

        }

        public void VerifyPDCValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();

            driver.FindElement(lookupPrimaryPDC).Click();


            CustomFunctions.SwitchToWindow(driver, 1);

            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("Inactive");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            driver.FindElement(txtSearchResultExpenseApprover).Click();


            CustomFunctions.SwitchToWindow(driver, 0);


        }
        public void VerifyPrimaryPDCValidation()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtSecondaryPDC, 120);

            driver.FindElement(txtSecondaryPDC).SendKeys("test houlihan");
        }


        public string TxtDealAnnouncementsChangeDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDealAnnouncementsChangeDate, 120);
            string text = driver.FindElement(txtDealAnnouncementsChangeDate).Text;
            return text;
        }

        public string TxtEventsChangeDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEventsChangeDate, 120);
            string text = driver.FindElement(txtEventsChangeDate).Text;
            return text;
        }
        public string TxtGeneralAnnouncementsChangeDate()

        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtGeneralAnnouncementsChangeDate, 120);
            string text = driver.FindElement(txtGeneralAnnouncementsChangeDate).Text;
            return text;
        }
        public string TxtContentChangeDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContentChangeDate, 120);
            string text = driver.FindElement(txtContentChangeDate).Text;
            return text;
        }

      //Edit Subscription Preference Fields
        public void EditSubscriptionPreferenceFields()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            Console.WriteLine(getDate);
            driver.FindElement(inputDealAnnouncementsChangeDate).SendKeys(getDate);
            driver.FindElement(inputEventsChangeDate).SendKeys(getDate);
            driver.FindElement(inputGeneralAnnouncementsChangeDate).SendKeys(getDate);
            driver.FindElement(inputContentChangeDate).SendKeys(getDate);
        }

        //Get Text from First Name Badge
        public string TxtBadgeFirstName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtBadgeFirstName, 120);
            string text = driver.FindElement(txtBadgeFirstName).Text;
            return text;
        }
        //Get text from Last Name Badge
        public string TxtBadgeLastName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtBadgeLastName, 120);
            string text = driver.FindElement(txtBadgeLastName).Text;
            return text;
        }
        //Get Text from Company Badge

        public string TxtBadgeCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtBadgeCompany, 120);
            string text = driver.FindElement(txtBadgeCompany).Text;
            return text;

        }

        
        //Edit Event Badge fields
        public void EditEventBadgeFields()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkboxCopyFromContactDetail, 120);
            driver.FindElement(checkboxCopyFromContactDetail).Click();
        }
        //Verify quick links and Related objects for external contact
        public void ValidateQuickLink(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int RowQuickLinkList = ReadExcelData.GetRowCount(excelPath, "QuickLink");

            for (int row = 1; row < RowQuickLinkList; row++)
            {


                string QuickLinkText = driver.FindElement(By.CssSelector("div[class='listHoverLinks']>a:nth-of-type(" + row + ")>span")).Text;
                Console.WriteLine(QuickLinkText);

                string preSetListExl = ReadExcelData.ReadDataMultipleRows(excelPath, "QuickLink", row + 1, 1);
                Console.WriteLine(preSetListExl);

                if (row == 9)
                {
                }
                else
                {
                    Assert.AreEqual(QuickLinkText, preSetListExl);
                    String cssvalue = driver.FindElement(By.CssSelector("div[class='listHoverLinks']>a:nth-of-type(" + row + ")>span")).GetCssValue("text-decoration");
                    Console.WriteLine(cssvalue);

                    Assert.AreEqual(cssvalue, "underline solid rgb(51, 52, 53)");
                    String attributevalue = driver.FindElement(By.CssSelector("div[class='listHoverLinks']>a:nth-of-type(" + row + ")>span")).GetAttribute("href");
                    Console.WriteLine(attributevalue);
                }
            }
        }

            public string GetCustomLinkText() {
            WebDriverWaits.WaitUntilEleVisible(driver, txtGoogleMaps, 120);
            string text = driver.FindElement(txtGoogleMaps).Text;
            return text;

        }
        public void VerifyGoogleMapsLink() {

            WebDriverWaits.WaitUntilEleVisible(driver, txtGoogleMaps, 120);
            driver.FindElement(txtGoogleMaps).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            string title = driver.Title;
            Assert.AreEqual("Google Maps", title);
            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);
            
            
        }
        

    }
}
