using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class ContactHomePage : BaseClass
    {
        By lnkContacts = By.CssSelector("a[title*='Contacts Tab']");
        By lnkShowAdvanceSearch = By.Id("searchLabel");
        By txtCompanyName = By.CssSelector("input[id*='companySearch']");
        By txtFirstName = By.CssSelector("input[id*='firstNameSearch']");
        By txtLastName = By.CssSelector("input[id*='lastNameSearch']");
        By txtState = By.CssSelector("input[id*='stateSearch']");
        By txtEmail = By.CssSelector("input[id*='emailSearch']");
        By btnSearch = By.CssSelector("input[id*='btnSearch']");
        By tblResults = By.CssSelector("table[id*='pbtContacts']");
        By tblResultsRowCount = By.CssSelector("table[id*='pbtContacts'] > tbody > tr");

        By matchedResult = By.XPath("//*[contains(@id,':pbtContacts:0:j_id57')]/a");
        By valEmail = By.CssSelector("div[id*='con15j']");

        By lnkHome = By.CssSelector("ul[id='tabBar'] > li[id='home_Tab']");
        By btnAddContact = By.CssSelector("input[name*='j_id29:j_id30']");
        
        By lnkReDisplayRec = By.CssSelector("table > tbody > tr:nth-child(2) > td > a:nth-child(4)");
        
        By btnAddActivity = By.CssSelector("td[class='pbButton center'] > input[value='Add Activity']");
        By comboAcitivityType = By.CssSelector("select[id*='j_id31:eventType']");
        By txtActivitySubject = By.CssSelector("div[class='requiredInput'] > input[id*='j_id57:eventSubject']");
        By btnActivitySave = By.CssSelector("td[class='pbButton '] > input:nth-child(1)");
        By txtAddCompanyDiscussed = By.CssSelector("input[id='j_id0:frmActivityEvent:activityedit:pbsCompaniesDiscussed:j_id181:inputTxtId']");
        By selAddCompanyDiscussed = By.XPath("//*[contains(text(),'Lookup Company')]//ancestor::body//ul[3]");
        By btnReturnToActivityDetail = By.CssSelector("td[class='pbButton '] > input[value='Return']");
        By lnkViewActivity = By.CssSelector("td[class='dataCell top '] > span[id*='pbtActivities:0:j_id17'] >a:nth-child(1)");
        By btnEditActivityDetail = By.CssSelector("td[class='pbButton '] > input[value='Edit']");
        By txtNoActivity = By.CssSelector("span[id*='pbActivityLog:noActivities'] > center > h2");
        By txtInternalMeetingWMentee = By.CssSelector("td[class='data2Col first last'] > span > input[type='text']");
        By selInternalMeetingWMentee = By.XPath("//a[normalize-space()='John Taylor']");
        By btnDeleteActivity = By.CssSelector("td[class='pbButton '] > input[value='Delete']");
        By headingActivityDetail = By.CssSelector("div[class*='tertiaryPalette'] h3");
        By txtActivityType = By.CssSelector("td[class='data2Col  first '] > span");
        By txtActivitySubjectValue = By.CssSelector("span[id*='j_id40:j_id42']");
        By selInternalMentee = By.CssSelector("span[id*='j_id69:0:j_id114']");
        By defaultHLMentor = By.CssSelector("span[id*='j_id78:0:j_id119']");
        By MentorMenteeErrMsg = By.CssSelector("td.messageCell:nth-child(2) > div");
        By txtLookupContact = By.CssSelector("input[id*='j_id142:inputInternalContactId'][type='text']");
        By selInternalMenteeContact = By.CssSelector("td[id*='j_id129:0:j_id135'] > a");
        By btnActivityReturnToContact = By.CssSelector("td[class='pbButton '] > input[value='Return']");
        By lnkActivities = By.CssSelector("span[id='activityList_link']");
        By lnkEditActivity = By.CssSelector("td[class='dataCell top '] > span[id*='pbtActivities:0:j_id18'] a:nth-child(2)");
        By txtContactHeading = By.CssSelector("h2[class='pageDescription']");
        By valDefaultHLAttendee = By.CssSelector("tbody[id*='pbsHLEmployees'] > tr > td:nth-child(3) > span");
        
        string dir = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\";

        public string SearchContactWithExternalContact(string file)
        {
            try
            {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 130);
            driver.FindElement(lnkContacts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
            driver.FindElement(lnkShowAdvanceSearch).Click();          
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
            driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
            driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch (Exception)
            {
                driver.Navigate().Refresh();
                string excelPath = dir + file;
                WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
                driver.FindElement(lnkContacts).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                driver.FindElement(lnkShowAdvanceSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
                WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
        }

        public string SearchContactMultipleRows(string file, int row)
        {
            try
            {
                string excelPath = dir + file;
                WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 130);
                driver.FindElement(lnkContacts).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                driver.FindElement(lnkShowAdvanceSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3));
                WebDriverWaits.WaitUntilEleVisible(driver, txtEmail);
                driver.FindElement(txtEmail).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 5));
                WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);

                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch (Exception)
            {
                driver.Navigate().Refresh();
                string excelPath = dir + file;
                WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
                driver.FindElement(lnkContacts).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                driver.FindElement(lnkShowAdvanceSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3));
                WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
        }

        //Get email id of contact
        public string GetEmailIDOfContact()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valEmail, 100);
                string id = driver.FindElement(valEmail).Text;
                return id;
            }
            catch(Exception)
            {
                driver.Navigate().Refresh();
                WebDriverWaits.WaitUntilEleVisible(driver, valEmail, 100);
                string id = driver.FindElement(valEmail).Text;
                return id;
            }            
        }

        //To Click on Contact tab
        public bool ClickContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts);
            driver.FindElement(lnkContacts).Click();
            return true;
        }

        //To Click on Add Contact tab
        public void ClickAddContact()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnAddContact);
            driver.FindElement(btnAddContact).Click();
        }

        //Function to get contact heading
        public string GetContactPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactHeading, 60);
            string headingContact = driver.FindElement(txtContactHeading).Text;
            return headingContact;
        }

        //Function to search contact
        public string SearchContact(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch, 3000);
            driver.FindElement(lnkShowAdvanceSearch).Click();
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
            driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
            driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 80);
            Thread.Sleep(2000);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                Thread.Sleep(4000);

                if (CustomFunctions.IsElementPresent(driver, lnkReDisplayRec))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                    driver.FindElement(lnkReDisplayRec).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
                    driver.FindElement(lnkContacts).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                    driver.FindElement(lnkShowAdvanceSearch).Click();

                    WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                    driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
                    WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                    driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
                    WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                    driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
                    WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
                    driver.FindElement(btnSearch).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                    Thread.Sleep(6000);

                    string result2 = driver.FindElement(matchedResult).Displayed.ToString();
                    Console.WriteLine("Search Results :" + result2);
                    driver.FindElement(matchedResult).Click();

                }
                return "Record found";
            }

            catch (Exception)
            {
                if (CustomFunctions.IsElementPresent(driver, lnkReDisplayRec))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                    driver.FindElement(lnkReDisplayRec).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
                    driver.FindElement(lnkContacts).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                    driver.FindElement(lnkShowAdvanceSearch).Click();

                    WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                    driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
                    WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                    driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
                    WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                    driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
                    WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
                    driver.FindElement(btnSearch).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                    Thread.Sleep(6000);

                    string result2 = driver.FindElement(matchedResult).Displayed.ToString();
                    Console.WriteLine("Search Results :" + result2);
                    driver.FindElement(matchedResult).Click();

                }
                return "No record found";
            }
        }

        public bool SearchAndValidateDuplicateContactExists(string file, int row)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch, 3000);
            driver.FindElement(lnkShowAdvanceSearch).Click();

            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1));
            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
            driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
            driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row,  3));

            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 80);
            Thread.Sleep(2000);
            driver.FindElement(btnSearch).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);

            //Get row count from the searched results
            IList<IWebElement> element = driver.FindElements(tblResultsRowCount);
            int totalRows = element.Count;

            if (totalRows >= 2)
            {
                result = true;
                driver.FindElement(matchedResult).Displayed.ToString();
                driver.FindElement(matchedResult).Click();
            }
            return result;
        }

        public bool SearchAndValidateDuplicateContactDoNotExists(string file, int row)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch, 3000);
            driver.FindElement(lnkShowAdvanceSearch).Click();

            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1));
            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
            driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
            driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3));

            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 80);
            Thread.Sleep(2000);
            driver.FindElement(btnSearch).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);

            //Get row count from the searched results
            IList<IWebElement> element = driver.FindElements(tblResultsRowCount);
            int totalRows = element.Count;

            if (totalRows == 1)
            {
                result = true;
            }
            return result;

        }


        public string SearchContact(string file, string ContactType)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
            driver.FindElement(lnkShowAdvanceSearch).Click();
            if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 3));
            }
            else if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 3, 1)))
            {

                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 3));
            }
            else if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 4, 1)))
            {

                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 4, 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 4, 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 4, 3));
            }
            else if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 5, 1)))
            {

                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 5, 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 5, 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 5, 3));
            }
            else if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 6, 1)))
            {

                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 6, 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 6, 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 6, 3));
            }
            else if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 7, 1)))
            {

                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 7, 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 7, 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 7, 3));
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
            driver.FindElement(btnSearch).Click();
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

        public string SearchHLAttendee(string HLAttendee)
        {
            IList<string> name = HLAttendee.Split(' ');
            if (name.Count.Equals(3))
            {
                string firstName = name[0];
                string lastName = name[2];
                WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
                driver.FindElement(lnkContacts).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                driver.FindElement(lnkShowAdvanceSearch).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(firstName);
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(lastName);
                WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
            }
            else if (name.Count.Equals(2))
            {
                string firstName = name[0];
                string lastName = name[1];
                WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
                driver.FindElement(lnkContacts).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                driver.FindElement(lnkShowAdvanceSearch).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(firstName);
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(lastName);
                WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
            }
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

        //Delete activity from contact page
        public void DeleteCreatedActivityFromContactHomePage()
        {
            CustomFunctions.MouseOver(driver, lnkActivities);
            driver.SwitchTo().Frame("iframeContentId");

            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditActivity);
            driver.FindElement(lnkEditActivity).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity);
            driver.FindElement(btnDeleteActivity).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(2000);
        }

        public void ValidateMentorMenteeNotSame(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
            driver.FindElement(lnkContacts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity);
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(1000);
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadData(excelPath, "Activity", 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultHLAttendee, 60);
            string HL_Attendee = driver.FindElement(valDefaultHLAttendee).Text;

            WebDriverWaits.WaitUntilEleVisible(driver, txtInternalMeetingWMentee);
            driver.FindElement(txtInternalMeetingWMentee).SendKeys(HL_Attendee);
            Thread.Sleep(4000);
            CustomFunctions.SelectValueFromDropdown(driver, HL_Attendee).Click();

            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
        }

        public string GetMenteeMentorErrorMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, MentorMenteeErrMsg, 60);
            string errorMsg = driver.FindElement(MentorMenteeErrMsg).Text;
            Thread.Sleep(2000);
            return errorMsg;
        }

        public void ValidateAlwaysOneToOneMeeting(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int rowMenteeContacts = ReadExcelData.GetRowCount(excelPath, "MenteeContact");
            for (int row = 2; row <= rowMenteeContacts; row++)
            {
                Thread.Sleep(1000);
                WebDriverWaits.WaitUntilEleVisible(driver, txtLookupContact);
                driver.FindElement(txtLookupContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "MenteeContact", row, 1));
                Thread.Sleep(4000);
                CustomFunctions.SelectValueFromDropdown(driver, ReadExcelData.ReadDataMultipleRows(excelPath, "MenteeContact", row, 1)).Click();
                Thread.Sleep(4000);
                Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "MenteeContact", row, 1), GetSelectedInternalMenteeActivityCreatePage());
            }
        }

        public string SearchContactWithActivityObject(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivityReturnToContact);
            driver.FindElement(btnActivityReturnToContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
            driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
            WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
            driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 4));
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
            driver.FindElement(btnSearch).Click();
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

        public int rowCountsOfMentee()
        {
            return driver.FindElements(By.CssSelector("table[id*='pbsInternalContacts:j_id129']  > tbody > tr")).Count;
        }

        public string GetSelectedInternalMenteeActivityCreatePage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selInternalMenteeContact, 60);
            string activitySelectedMentee = driver.FindElement(selInternalMenteeContact).Text;
            return activitySelectedMentee;
        }



    }
}
