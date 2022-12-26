using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class LoginPage : BaseClass
    {
        By txtUserName = By.Id("username");
        By txtPassWord = By.Id("password");
        By btnLogin = By.Id("Login");
        By loggedUser = By.XPath("//span[@id='userNavLabel']");
        By loggedUserLightningView = By.XPath("//header[@id='oneHeader']/div/div/span");
        By imgProfile = By.CssSelector("div[class*='profileTrigger ']>span[class='uiImage']");
        By lnkSwitchToClassic = By.XPath("//a[text()='Switch to Salesforce Classic']");
        By userIcon = By.CssSelector("div[class*='profileTrigger'] > span[class='uiImage']");
        By linkSalesforceClassic = By.XPath("//a[normalize-space()='Switch to Salesforce Classic']");

        public void LoginApplication()
        {
           
            driver.FindElement(txtUserName).SendKeys(ReadJSONData.data.authentication.username);
            Console.WriteLine(ReadJSONData.data.authentication.username);
            driver.FindElement(txtPassWord).SendKeys(ReadJSONData.data.authentication.password);
            driver.FindElement(btnLogin).Click();

            Thread.Sleep(2000);
            string url = driver.Url;
            try
            {
                if (url.Contains("lightning.force.com/lightning/n/DNBoptimizer__Data_Stewardship1"))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, imgProfile, 150);
                    driver.FindElement(imgProfile).Click();

                    WebDriverWaits.WaitUntilEleVisible(driver, lnkSwitchToClassic, 120);
                    driver.FindElement(lnkSwitchToClassic).Click();
                }
                else
                {
                    Console.WriteLine("No switch required ");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No switch required ");
            }
        }
        public string ValidateUser()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver,loggedUser,140);
            IWebElement loggedUserName = driver.FindElement(loggedUser);
            return loggedUserName.Text;
        }

        public bool ValidateUserLightningView(string file, int userRow)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, loggedUserLightningView, 140);
            IWebElement loggedUserName = driver.FindElement(loggedUserLightningView);
            if(loggedUserName.Text.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", userRow, 1)))
            {
                result = true;
            }
            return result;
        }

        public void HandleSalesforceLightningPage()
        {
            WebDriverWaits.WaitForPageToLoad(driver, 15);
            Thread.Sleep(10000);
            string url = driver.Url;
            if (url.Contains("lightning.force.com/lightning/n/DNBoptimizer__Data_Stewardship1"))
            {
                //Thread.Sleep(15000);
                WebDriverWaits.WaitUntilEleVisible(driver, userIcon, 40);
                driver.FindElement(userIcon).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, linkSalesforceClassic, 40);
                driver.FindElement(linkSalesforceClassic).Click();
            }
        }

        public void LoginAsExpenseRequestApprover(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtUserName).SendKeys(ReadExcelData.ReadData(excelPath, "Approver", 1));
            driver.FindElement(txtPassWord).SendKeys(ReadExcelData.ReadData(excelPath, "Approver", 2));
            driver.FindElement(btnLogin).Click();
            Thread.Sleep(10000);
        }

        public void LoginAsFirstLevelExpenseRequest(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtUserName).SendKeys(ReadExcelData.ReadData(excelPath, "FirstLevelApprover", 1));
            driver.FindElement(txtPassWord).SendKeys(ReadExcelData.ReadData(excelPath, "FirstLevelApprover", 2));
            driver.FindElement(btnLogin).Click();
        }
    }
}
