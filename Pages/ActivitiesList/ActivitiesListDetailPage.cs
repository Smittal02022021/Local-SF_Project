using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.ActivitiesList
{
    class ActivitiesListDetailPage : BaseClass
    {
        By tabActivitiesList = By.CssSelector("a[title*='Activities List Tab']");
        By colEventTaskType = By.CssSelector("div[title*='Event/Task Type']");
        By lnkAlphabetC = By.CssSelector("div[id*='rolodex'] > a:nth-child(3)");
        By valEventTaskType = By.CssSelector("div[class='x-grid3-body'] > div[class*='first'] > table > tbody > tr > td:nth-child(6) > div");
        By lnkAlphabetM = By.CssSelector("div[id*='rolodex'] > a:nth-child(13)");
        //By lnkView = By.CssSelector("div[class*='row-first'] table[class='x-grid3-row-table'] tr:nth-child(1) div[id*='00N2g0000018zGy'] > a");
        By lnkView = By.XPath("//div[@class='x-grid3-scroller']/div/div[1]/table/tbody/tr/td[3]/div/a");
        By lnkEditAction = By.CssSelector("div[class*='row-first'] table[class='x-grid3-row-table'] tr:nth-child(1) div[id*='00N2g0000018zGy'] > a:nth-child(2)");
        By lnkDeleteAction = By.CssSelector("div[class*='row-first'] table[class='x-grid3-row-table'] tr:nth-child(1) div[id*='00N2g0000018zGy'] > a:nth-child(3)");
        By drpdwnSelectPreSetTemplate = By.CssSelector("select[id*='listSelect']");
        By columnsList = By.CssSelector("div[id*='ext-gen9'] > div > div > table > thead > tr > td");
        By btnRefresh = By.CssSelector("input[id*='refresh']");
        By btnNewTask = By.CssSelector("input[id*='newtask']");
        By valActivitySubject = By.CssSelector("div[class='x-grid3-body'] > div[class*='first'] > table > tbody > tr > td:nth-child(7) > div");
        By valPrimaryAttendee = By.CssSelector("div[class='x-grid3-body'] > div[class*='first'] > table > tbody > tr > td:nth-child(9) > div");
        By valPrimaryExternalContact = By.CssSelector("div[class='x-grid3-body'] > div[class*='first'] > table > tbody > tr > td:nth-child(10) > div");
        //By lnkPrint = By.CssSelector("div[id*='00N2g0000019GUN'] > a");
        By lnkPrint = By.XPath("//div[@class='x-grid3-scroller']/div/div[1]/table/tbody/tr/td[4]/div/a");
        By lnkCreateNewView = By.CssSelector("div[class='filterLinks'] > a:nth-child(3)");
        By lnkEdit = By.CssSelector("div[class='filterLinks'] > a:nth-child(1)");
        By lnkDelete = By.CssSelector("div[class='filterLinks'] > a:nth-child(2)");
        By txtViewName = By.CssSelector("input[id='fname']");
        By radioMyActivities = By.CssSelector("input[id='fscope1']");
        By drpdwnAdditionalFields = By.CssSelector("select[id='fcol1']");
        By drpdwnOperator = By.CssSelector("select[id='fop1']");
        By drpdwnValue = By.CssSelector("input[id='fval1']");
        By btnSave = By.CssSelector("input[value*='Save']");
        By selectedView = By.CssSelector("select[id*='listSelect'] > option[selected='selected']");
        By valSubject = By.CssSelector("div[id*='GrY12']");
        By lnkContactName = By.CssSelector("div[id*='WHO_NAME'] > a");
        By txtContactName = By.CssSelector("div[id*='WHO_NAME'] > a > span");
        By comboPreSelectTemplate = By.CssSelector("select[id*='j_id0:j_id2_listSelect']");
        By txtExternalContactActivityPage = By.CssSelector("input[id$='inputContactId']");
        By btnEdit = By.CssSelector("input[value='Edit']");
        
        By btnActivitySave = By.CssSelector("td[class='pbButton '] > input:nth-child(1)");
        By tblActivityListRowCount = By.CssSelector("div[id*='ext-gen11'] > div");

        //Click Activities List Tab
        public void ClickActivitiesListTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabActivitiesList);
            driver.FindElement(tabActivitiesList).Click();
        }

        public void SelectFirstPresetTemplate(string file,string preSetTemplate)
        {

            CustomFunctions.SelectByText(driver, driver.FindElement(comboPreSelectTemplate), preSetTemplate);
        }


        //Create New View for activity list
        public void CreateNewView()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCreateNewView);
            driver.FindElement(lnkCreateNewView).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtViewName);
            driver.FindElement(txtViewName).SendKeys("Test_New_View");
            driver.FindElement(radioMyActivities).Click();
            driver.FindElement(drpdwnAdditionalFields).SendKeys("Type");
            driver.FindElement(drpdwnOperator).SendKeys("Call");
            driver.FindElement(btnSave).Click();
        }

        //Update New View for activity list
        public void EditNewView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit);
            driver.FindElement(lnkEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtViewName);
            driver.FindElement(txtViewName).SendKeys("Test_Updated_View");

            driver.FindElement(drpdwnAdditionalFields).SendKeys("Type");
            driver.FindElement(drpdwnOperator).SendKeys("Meeting");
            driver.FindElement(btnSave).Click();
        }

        //Delete New View for activity list
        public void DeleteNewView()
        {
            driver.FindElement(lnkDelete).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Thread.Sleep(1000);
            alert.Accept();
            Thread.Sleep(1000);
        }

        //Verify new task button availability
        public bool NewTaskButtonAvailability()
        {
            bool newTaskButtonAvailable = CustomFunctions.IsElementPresent(driver, btnNewTask);
            return newTaskButtonAvailable;
        }

        //Verify new task button availability
        public bool EditActionLinkAvailability()
        {
            bool newTaskButtonAvailable = CustomFunctions.IsElementPresent(driver, lnkEditAction);
            return newTaskButtonAvailable;
        }
        //Verify new task button availability
        public bool DeleteActionLinkAvailability()
        {
            bool newTaskButtonAvailable = CustomFunctions.IsElementPresent(driver, lnkDeleteAction);
            return newTaskButtonAvailable;
        }

        //Click Refresh button
        public void ClickRefreshButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefresh);
            driver.FindElement(btnRefresh).Click();
        }

        //Click Refresh button
        public void ClickPrintLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPrint);
            driver.FindElement(lnkPrint).Click();
        }
        //Click View Link
        public void ClickViewLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkView);
            Thread.Sleep(3000);
            driver.FindElement(lnkView).Click();
        }

        //Verify Alphabetical Sorting
        public void VerifyAlphabeticalSorting()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, colEventTaskType);
            driver.FindElement(colEventTaskType).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkAlphabetC).Click();
            Thread.Sleep(4000);
        }

        //Click alphabet M link
        public void ClickAlphabetMforSorting()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkAlphabetM);
            driver.FindElement(lnkAlphabetM).Click();
            Thread.Sleep(3000);
        }

        // Get event task type
        public string getEventTaskType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventTaskType, 60);
            string valueEventType = driver.FindElement(valEventTaskType).Text;
            return valueEventType;
        }

        public string getActivitySubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valActivitySubject, 60);
            string valueActivitySubject = driver.FindElement(valActivitySubject).Text;
            return valueActivitySubject;
        }

        public string GetSelectedView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectedView, 60);
            string selectedViewLayout = driver.FindElement(selectedView).Text;
            return selectedViewLayout;
        }

        public string getPrimaryAttendee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryAttendee, 60);
            string valuePrimaryAttendee = driver.FindElement(valPrimaryAttendee).Text;
            return valuePrimaryAttendee;
        }

        public string getPrimaryExternalContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryExternalContact, 60);
            string valuePrimaryExternalContact = driver.FindElement(valPrimaryExternalContact).Text;
            return valuePrimaryExternalContact;
        }

        public void ValidatePreSetTemplate(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IWebElement recordDropdown = driver.FindElement(drpdwnSelectPreSetTemplate);
            SelectElement select = new SelectElement(recordDropdown);
            int RowPreSetTemplateList = ReadExcelData.GetRowCount(excelPath, "ActivityList");

            for (int i = 2; i <= RowPreSetTemplateList; i++)
            {
                IList<IWebElement> options = select.Options;
                IWebElement PReSetTemplateOption = options[i - 2];
                string preSetListExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", i, 1);
                Assert.AreEqual(PReSetTemplateOption.Text, preSetListExl);
            }
        }

      
        public void VerifyFieldsOrColumnsOnActivitiesList(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;         
            //IList<IWebElement> element = driver.FindElements(columnsList);
            int totalRows = ReadExcelData.GetRowCount(excelPath, "ActivityList"); ;

            for (int i = 2; i <= totalRows; i++)
            {
                By columnName = By.CssSelector($"div[id*='ext-gen9'] > div > div > table > thead > tr > td:nth-child({i+1}) > div");
                IWebElement columnNameOfActivityList = driver.FindElement(columnName);

                string valColumnName = columnNameOfActivityList.Text;

                string valColumnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", i, 1);
                Assert.AreEqual(valColumnNameExl, valColumnName);
            }
        }

        public void SelectPreSetTemplate(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(drpdwnSelectPreSetTemplate).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 2));
        }

        public string GetTagNameOfSubject()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, valSubject, 60);
            string valueTagNameOfSubject = driver.FindElement(valSubject).TagName;
            return valueTagNameOfSubject;
        }

        //Click link contact name
        public void ClickContactName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContactName);
            driver.FindElement(lnkContactName).Click();
            
        }

        public string GetContactName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            string contactName = driver.FindElement(txtContactName).Text;
            return contactName;

        }
        public void enterExternalContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit);
            driver.FindElement(btnEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtExternalContactActivityPage, 3000);
            driver.FindElement(txtExternalContactActivityPage).SendKeys("Test External");
           // WebDriverWaits.WaitUntilEleVisible(driver, By.XPath("/html/body/ul[6]/li/a"), 30000);
            //driver.FindElement(By.XPath("/html/body/ul[6]/li/a")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();

        }

        public int GetActivityListCount()
        {
            //Get row count from the activitylist
            IList<IWebElement> element = driver.FindElements(tblActivityListRowCount);
            int totalRows = element.Count;
            return totalRows;
        }

        public bool VerifyAndSelectIfDesiredActivityIsVisible(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Get row count from the activitylist
            IList<IWebElement> element = driver.FindElements(tblActivityListRowCount);
            int totalRows = element.Count;

            bool result = false;
            for (int i=1; i<=totalRows; i++)
            {
                //Get the subject of each activity listed
                string subject = driver.FindElement(By.CssSelector($"div[id*='ext-gen11'] > div:nth-child({i}) > table > tbody > tr > td:nth-child(7) > div")).Text;
                if (subject == ReadExcelData.ReadData(excelPath, "Activity", 2))
                {
                    result = true;
                    driver.FindElement(By.CssSelector($"div[id*='ext-gen11'] > div:nth-child({i}) > table > tbody > tr > td:nth-child(3) > div > a")).Click();
                    break;
                }
            }
            return result;
        }
    }
}