using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
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
        By btnSearch = By.CssSelector("input[id*='btnSearch']");
        By tblResults = By.CssSelector("table[id*='pbtContacts']");
        By matchedResult = By.XPath("//*[contains(@id,':pbtContacts:0:j_id57')]/a");
        By valEmail = By.CssSelector("div[id*='con15j']");

        string dir = @"C:\Users\vkumar0427\source\repos\SalesForce_Project\TestData\";

        public string SearchContactWithExternalContact(string file)
        {
            try
            {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 130);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(lnkContacts));

                //driver.FindElement(lnkContacts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(lnkShowAdvanceSearch));

               // driver.FindElement(lnkShowAdvanceSearch).Click();          
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
            driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
            driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnSearch));

                //driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(matchedResult));

               // driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch (Exception)
            {
                driver.Navigate().Refresh();
                string excelPath = dir + file;
                WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 120);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(lnkContacts));

               // driver.FindElement(lnkContacts).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowAdvanceSearch);
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(lnkShowAdvanceSearch));

                driver.FindElement(lnkShowAdvanceSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
                WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName);
                driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLastName);
                driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));
                WebDriverWaits.WaitUntilEleVisible(driver, btnSearch);
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnSearch));

                //driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(matchedResult));

                //driver.FindElement(matchedResult).Click();
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
    }
}