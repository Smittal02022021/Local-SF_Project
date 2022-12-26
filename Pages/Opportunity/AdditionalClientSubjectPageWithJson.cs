using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;
//using System.Windows.Forms;

namespace SalesForce_Project.Pages
{
    class AdditionalClientSubjectPageWithJson : BaseClass
    {
        By btnContinue = By.CssSelector("input[value='Continue']");
        By winAdditionalClientSubject = By.CssSelector("span[class='ui-dialog-title']");
        By winAdditionalClient = By.Id("ui-id-2");
        By txtSearch = By.CssSelector("input[name*= 'txtSearch']");
        By btnGo = By.CssSelector("input[name*= 'btnGo']");
        By checkCompany = By.CssSelector("input[name*= 'id50']");
        By btnAddSelected = By.CssSelector("input[value= 'Add Selected']");
        By msgSuccess = By.XPath("//div[contains(text(),'Company Added To Additional Clients')]");
        By btnClose = By.XPath("//span[contains(text(),'Add Additional Client(s)')] /following-sibling::button");
        By btnSubClose = By.XPath("//span[contains(text(),'Add Additional Subject(s)')] /following-sibling::button");
        By msgSubject = By.XPath("//div[contains(text(),'Company Added To Additional Subjects')]");
        By additionalCompany = By.XPath("//*[contains(@id,'pbAdditionalClients')] / tr/td/span/a[contains(text(),'Del')]");
        By additionalSubject = By.XPath("//*[contains(@id,'pbAdditionalSubjects')] / tr/td/span/a[contains(text(),'Del')]");
        By comboClientInterest = By.CssSelector("select[name*='hasAdverseClients']");
        By btnSaveClose = By.CssSelector("input[value='Save & Close']");
        By btnAddClient = By.Id("newClient");
        By btnAddSubject = By.Id("newSubject");
        By titleHLTeam = By.CssSelector("h2[class='mainTitle']");
        By txtStaff = By.CssSelector("input[placeholder*='Begin Typing Name']");
        By checkInitiator = By.CssSelector("input[name*='internalTeam:j_id73:0:j_id75']");
        By btnSave = By.CssSelector("input[value='Save']");
        By listStaff = By.XPath("/html/body/ul");
        By btnReturnToOppor = By.CssSelector("input[value='Return To Opportunity']");
        By linkCompanyName = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[4]/th/a");
        By linkSubjectName = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[5]/th/a");

        //public static string fileTC1649 = "T1649_AdditionalClientSubjectRequired.xlsx";
        //string excelPath = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\" + fileTC1649;

        public void ClickContinue()
        {
            //Calling wait function--Continue button     
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Submit();
        }

        public string ValidateAdditionalClientSubjectTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, winAdditionalClientSubject);
            IWebElement titleAdditionalClientSubject = driver.FindElement(winAdditionalClientSubject);
            return titleAdditionalClientSubject.Text;
        }
        public void ClickAddClient()
        {
            Thread.Sleep(3000);
            driver.SwitchTo().Frame(4);
            driver.FindElement(btnAddClient).Click();
        }

        public string ValidateAdditionalClientTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, winAdditionalClient, 40);
            IWebElement titleAdditionalClient = driver.FindElement(winAdditionalClient);
            return titleAdditionalClient.Text;
        }
        //To add additional Client
        public void AddAdditionalClient()
        {
            driver.SwitchTo().Frame(0);           
            driver.FindElement(txtSearch).SendKeys(ReadJSONData.data.additionalClientSubject.search);
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();

            //Calling wait function -- to load search results
            WebDriverWaits.WaitUntilEleVisible(driver, checkCompany, 50);
            driver.FindElement(checkCompany).Click();
            driver.FindElement(btnAddSelected).Click();
        }
        //To validate message while adding additional Client
        public string ValidateMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess);
            return driver.FindElement(msgSuccess).Text;
        }
        //To validate additional Client added in Table
        public string ValidateTableDetails()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(4);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, additionalCompany, 50);
            if (driver.FindElement(additionalCompany).Displayed)
                return "True";
            else
                return "False";
        }

        //To add additional subject
        public void AddAdditionalSubject()
        {
            driver.FindElement(btnAddSubject).Click();
            driver.SwitchTo().Frame(0);
            driver.FindElement(txtSearch).SendKeys(ReadJSONData.data.additionalClientSubject.search);
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, checkCompany, 50);
            driver.FindElement(checkCompany).Click();
            driver.FindElement(btnAddSelected).Click();
        }
        //To validate message while adding additional subject
        public string ValidateSubjectMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSubject);
            return driver.FindElement(msgSubject).Text;
        }
        //To validate additional Subject in Table
        public string ValidateSubjectTableDetails()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(4);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubClose);
            driver.FindElement(btnSubClose).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, additionalSubject, 50);
            if (driver.FindElement(additionalSubject).Displayed)
                return "True";
            else
                return "False";
        }
        //To select interests of client and Save value
        public void selectClientInterest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver,comboClientInterest);
            IWebElement comboInterest = driver.FindElement(comboClientInterest);
            CustomFunctions.SelectByValue(driver, comboInterest, ReadJSONData.data.additionalClientSubject.clientInterest);
            driver.FindElement(btnSaveClose).Click();
        }
        //To validate HL Internal Page title
        public string ValidateInternalTeamTitle()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, titleHLTeam, 40);
            IWebElement titleHLInternalTeam = driver.FindElement(titleHLTeam);
            return titleHLInternalTeam.Text;
        }
        //To enter team member details
        public void EnterStaffDetails()
        {
            Thread.Sleep(3000);
            string valStaff = ReadJSONData.data.additionalClientSubject.staff;
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 50);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }
        //To validate additional added client in Additional Clients/Subjects section
        public string VaidateAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyName, 40);
            string valCompany = driver.FindElement(linkCompanyName).Text;
            return valCompany;
        }
        //To validate additional added subject in additional Clients/Subjects section
        public string VaidateAddedSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSubjectName, 40);
            string valSubject = driver.FindElement(linkSubjectName).Text;
            return valSubject;
        }


    }
}

