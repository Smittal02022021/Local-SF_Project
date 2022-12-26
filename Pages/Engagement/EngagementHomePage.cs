using OpenQA.Selenium;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class EngagementHomePage : BaseClass
    {
        By lnkEngagements = By.CssSelector("a[title*='Engagements Tab']");
        By linkAdvancedSearch = By.CssSelector("span[id='searchLabel']");
        By btnSearch = By.CssSelector("input[name*='btnSearch']");
        By tblResults = By.CssSelector("table[id*='pbtEngagements']");
        By matchedResult = By.XPath("//*[contains(@id,':pbtEngagements:0:j_id57')]/a");
        By comboJobType = By.CssSelector("select[name*='jobTypeSearch']");
        By comboLOB = By.CssSelector("select[name*='obSearch']");
        By comboStage = By.CssSelector("select[name*='stageSearch']");
        By lnkEngageManager = By.XPath("//a[contains(text(),'Engagement Manager')]");
        By titleEngageManager = By.CssSelector("label[id*='PageTitle']");
        By txtEngageName = By.CssSelector("input[name*='nameSearch']");
        By txtErrorMessage = By.CssSelector("span[id*=':theError']");


        //To Click on Engagement tab
        public void ClickEngagement()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements);
            driver.FindElement(lnkEngagements).Click();
        }

        //To Search Engagement with Job Type
        public string SearchEngagementWithJobType(string jobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 120);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkAdvancedSearch);
            driver.FindElement(linkAdvancedSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
            driver.FindElement(comboJobType).SendKeys(jobType);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(5000);
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

        //To Search Engagement with LOB
        public string SearchEngagementWithLOB(string LOB, string stage)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 90);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkAdvancedSearch);
            driver.FindElement(linkAdvancedSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB);
            driver.FindElement(comboLOB).SendKeys(LOB);
            driver.FindElement(comboStage).SendKeys(stage);
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

        //To click on Engagement Manager Link
        public string ClickEngageManager()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngageManager, 100);
            driver.FindElement(lnkEngageManager).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngageManager, 70);
            string title = driver.FindElement(titleEngageManager).Text;
            return title;
        }

        //To Search with Engagement Name
        public string SearchEngagementWithName(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 150);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngageName,80);
            driver.FindElement(txtEngageName).SendKeys(name);           
            driver.FindElement(btnSearch).Click();                       
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();                
                return "Record found";
            }                   
           
            catch(Exception)
            {
                driver.Navigate().Refresh();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 110);
                driver.FindElement(lnkEngagements).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngageName, 80);
                driver.FindElement(txtEngageName).SendKeys(name);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                driver.FindElement(matchedResult).Click();               
                return "Error page found or no record found";               
            }           
        }
    }
}


