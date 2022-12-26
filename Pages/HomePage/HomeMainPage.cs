using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.HomePage
{
    class HomeMainPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        By linkHLReports = By.XPath("//a[normalize-space()='HL Reports']");
        By txtGlobalSearch = By.CssSelector("input[id='phSearchInput']");
        By btnGlobalSearch = By.CssSelector("input[id='phSearchButton']");
        By linkPeople = By.CssSelector("div[class='peopleInfoContent'] > div > a");
        By linkContact = By.CssSelector("#Contact_body > table > tbody > tr:nth-child(2) > th > a");
        By TimeRecordManagerTab = By.XPath("//a[normalize-space()='Time Record Manager']");
        By valPeople = By.CssSelector("span[id='tailBreadcrumbNode']");
        By valUserTitle = By.CssSelector("h1[class='currentStatusUserName'] > a");
        By btnExpandPin = By.CssSelector("span[id='pinIndicator']");
        By imgAllTabs = By.XPath("//li[@id='AllTab_Tab']/a/img");
        By linkMonthlyRevenue = By.XPath("//a[@class='listRelatedObject Custom95Block title']");
        By linkSwitchToLightningExperience = By.XPath("//a[@class='switch-to-lightning']");

        string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        public void NavigateToMonthlyRevenueProcessControlsPage()
        {
            driver.FindElement(imgAllTabs).Click();
            Thread.Sleep(2000);
            driver.FindElement(linkMonthlyRevenue).Click();
            Thread.Sleep(2000);
        }
        public void ClickHLReportLink()
        {
            if (CustomFunctions.IsElementPresent(driver, linkHLReports))
            {

                WebDriverWaits.WaitUntilEleVisible(driver, linkHLReports);
                driver.FindElement(linkHLReports).Click();
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnExpandPin);
                driver.FindElement(btnExpandPin).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, linkHLReports);
                driver.FindElement(linkHLReports).Click();
            }
        }

        public string ReportList(int tableNumber,int trCount)
        {
            IWebElement value = driver.FindElement(By.CssSelector($"div[id='j_id0:j_id1:j_id2:{tableNumber}:j_id3'] > div[class='pbBody'] > table > tbody > tr:nth-child({trCount}) > td:nth-child(1) > a"));
            string a = value.Text;
            return a;
        }

        public string DescriptionList(int tableNumber, int trCount)
        {
            IWebElement value = driver.FindElement(By.CssSelector($"div[id = 'j_id0:j_id1:j_id2:{tableNumber}:j_id3'] > div[class='pbBody'] > table > tbody > tr:nth-child({trCount}) > td:nth-child(2)"));
            string a = value.Text;
            if (a.Equals(""))
            {
                a = null;
            }
            return a;
        }

        public string ContactReportList(int trCount)
        {
            IWebElement value = driver.FindElement(By.CssSelector($"div[id='j_id0:j_id1:j_id2:2:j_id3'] > div[class='pbBody'] > table > tbody > tr:nth-child({trCount}) > td:nth-child(1) > a"));
            string a = value.Text;
            return a;
        }

        public string EngagementDescriptionList(int trCount)
        {
            IWebElement value = driver.FindElement(By.CssSelector($"div[id = 'j_id0:j_id1:j_id2:3:j_id3'] > div[class='pbBody'] > table > tbody > tr:nth-child({trCount}) > td:nth-child(2)"));
            string a = value.Text;
            if (a.Equals(""))
            {
                a = null;
            }
            return a;
        }

        public string EngagementReportList(int trCount)
        {
            IWebElement value = driver.FindElement(By.CssSelector($"div[id='j_id0:j_id1:j_id2:3:j_id3'] > div[class='pbBody'] > table > tbody > tr:nth-child({trCount}) > td:nth-child(1) > a"));
            string a = value.Text;
            return a;
        }

        public string OpportunityReportList(int trCount)
        {
            IWebElement value = driver.FindElement(By.CssSelector($"div[id='j_id0:j_id1:j_id2:5:j_id3'] > div[class='pbBody'] > table > tbody > tr:nth-child({trCount}) > td:nth-child(1) > a"));
            string a = value.Text;
            return a;
        }

        public string ContactDescriptionList(int trCount)
        {
            IWebElement value = driver.FindElement(By.CssSelector($"div[id='j_id0:j_id1:j_id2:2:j_id3'] > div[class='pbBody'] > table > tbody > tr:nth-child({trCount}) > td:nth-child(2)"));
            string a = value.Text;
            if (a.Equals(""))
            {
                a = null;
            }
            return a;
        }

        public IWebElement HLReportsSection(int number)
        {
            IWebElement element = driver.FindElement(By.CssSelector($"div[id='j_id0:j_id1:j_id2:{number}:j_id3'] > div[class='pbBody'] > table"));
            return element;
        }

        /*public Tuple<bool,string> ValidateLinks(IWebElement element)
        {
            IList<IWebElement> links = element.FindElements(By.TagName("a"));
            foreach (IWebElement link in links)
            {
                string url = link.GetAttribute("href");
                bool linkExist = CustomFunctions.AreLinksWorking(url);
                return new Tuple<bool, string>(linkExist,url);
            }
            return new Tuple<bool, string>(false, "abc");
        }
        */

        public void ValidateLinks(IWebElement element,string ReportSet)
        {
            IList<IWebElement> engagementLinks = element.FindElements(By.TagName("a"));
            foreach (IWebElement link in engagementLinks)
            {
                string url = link.GetAttribute("href");
                bool linkExist = CustomFunctions.AreLinksWorking(url);
                Assert.AreEqual(true, linkExist);
                extentReports.CreateLog(ReportSet+" HL Report Link with URL: " + url + " response " + linkExist + " ");
            }
        }

        public string OpportunityDescriptionList(int trCount)
        {
            IWebElement value = driver.FindElement(By.CssSelector($"div[id='j_id0:j_id1:j_id2:5:j_id3'] > div[class='pbBody'] > table > tbody > tr:nth-child({trCount}) > td:nth-child(2)"));
            string a = value.Text;
            if (a.Equals(""))
            {
                a = null;
            }
            return a;
        }

        public void SearchUserByGlobalSearch(string file,string user)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtGlobalSearch);
            driver.FindElement(txtGlobalSearch).SendKeys(user);

            WebDriverWaits.WaitUntilEleVisible(driver, btnGlobalSearch);
            driver.FindElement(btnGlobalSearch).Click();
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkPeople);
            driver.FindElement(linkPeople).Click();
            Thread.Sleep(2000);
        }

        public void SearchContactByGlobalSearch(string file, string contact)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtGlobalSearch);
            driver.FindElement(txtGlobalSearch).SendKeys(contact);

            WebDriverWaits.WaitUntilEleVisible(driver, btnGlobalSearch);
            driver.FindElement(btnGlobalSearch).Click();
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkContact);
            driver.FindElement(linkContact).Click();
            Thread.Sleep(2000);
        }

        public void ClickTimeRecordManagerTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, TimeRecordManagerTab);
            driver.FindElement(TimeRecordManagerTab).Click();
            Thread.Sleep(30000);
        }

        public string GetPeopleOrUserName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPeople, 80);
            string CompanyNameFromDetail = driver.FindElement(valPeople).Text.TrimEnd();
            return CompanyNameFromDetail;
        }

        public string GetUserTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valUserTitle, 80);
            string CompanyNameFromDetail = driver.FindElement(valUserTitle).Text;
            return CompanyNameFromDetail;
        }

        public void SwitchToLightningView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSwitchToLightningExperience, 120);
            driver.FindElement(linkSwitchToLightningExperience).Click();
        }
    }
}