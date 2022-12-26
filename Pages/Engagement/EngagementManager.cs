using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Opportunity
{
    class EngagementManager :BaseClass
    {
        By btnResetFilters = By.CssSelector("input[value='Reset Filters']");
        By comboShowRec = By.CssSelector("select[name*='engagementOptions']");
        By btnApplyFilters = By.CssSelector("input[value='Apply Filters']");       
        By linkEngageName = By.CssSelector("td>span[id*=':j_id181:0:j_id183:2:j_id208'] > a");
        By titleEngageDetail = By.XPath("//h2[contains(text(),'Engagement Detail')]");       
        By comboRecType = By.CssSelector("select[name*='engagementrecordtype']");
        By comboStage = By.CssSelector("select[name*=':0:j_id183:6:j_id188']");
        By txtTotalEstFee = By.CssSelector("input[name*=':0:j_id183:8:j_id186']");
        By txtTotalEstFeeFAS = By.CssSelector("input[name*=':0:j_id183:8:j_id186']");
        By linkFRExtraColEngageName = By.CssSelector("span[id*='181:0:j_id183:1:j_id208']");
        By txtEngageNumber = By.CssSelector("input[name*=':inputTxtEngNum']");
        By txtActualMonthlyFee = By.CssSelector("input[name*=':0:j_id183:4:j_id186']");
        By txtActualTxnFee = By.CssSelector("input[name*=':0:j_id183:5:j_id186']");
        By linkFREngageName = By.CssSelector("span[id*='181:0:j_id183:2:j_id208']");
        By txtPerAccFees = By.CssSelector("input[name*=':0:j_id183:8:j_id186']");
        By comboStageField = By.CssSelector("select[id*='engagementStageOptions']");
        By colActualMonthlyFee = By.CssSelector("td[id*='Actual_Monthly_Fee__c_a095A000013t']>span>span");       
        By colActualTxnFee = By.CssSelector("td[id*='Actual_Transaction_Fee__c_a095A000013t']>span>span");
 
        //string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        //To click on Engagement Name
        public string ClickEngageName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkEngageName,150);
            Thread.Sleep(4000);
            driver.FindElement(linkEngageName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngageDetail, 80);
            string title = driver.FindElement(titleEngageDetail).Text;
            return title;
        }

        //To select show records as FAS/CF
        public void SelectShowRecords(string Value)
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnResetFilters, 60);
            //driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboShowRec, 60);
            driver.FindElement(comboShowRec).SendKeys(Value);
            WebDriverWaits.WaitUntilEleVisible(driver, btnApplyFilters, 180);
            Thread.Sleep(4000);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(7000);
        }    

        //To reset filters with LOB - FAS for Revenue Accrual
        public void ResetFiltersForRevAccrual(string value)
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 80);
            driver.FindElement(comboRecType).SendKeys(value);
            WebDriverWaits.WaitUntilEleVisible(driver, btnApplyFilters, 150);
            Thread.Sleep(4000);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(7000);
        }

        //To update stage value of a record
        public void UpdateStageValue(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboStage, 60);
            driver.FindElement(comboStage).SendKeys(value);
            driver.FindElement(comboRecType).Click();         
        }

        //To update Total Est Fee of a record
        public void UpdateTotalEstFee(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalEstFee, 60);
            driver.FindElement(txtTotalEstFee).Clear();
            driver.FindElement(txtTotalEstFee).SendKeys(value);
            driver.FindElement(comboRecType).Click();
        }

        //To update Total Est Fee of a record with FAS CAO user
        public void UpdateTotalEstFeeWithCAOUser(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalEstFeeFAS, 60);
            driver.FindElement(txtTotalEstFeeFAS).Clear();
            driver.FindElement(txtTotalEstFeeFAS).SendKeys(value);
            driver.FindElement(comboRecType).Click();
        }

        //To click on FR Engagement Name when FR specific columns are added
        public string ClickFREngagementName()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkFRExtraColEngageName, 140);
            driver.FindElement(linkFRExtraColEngageName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngageDetail, 80);
            string title = driver.FindElement(titleEngageDetail).Text;
            return title;
        }

        //To update Actual Monthly Fee and Actual Transaction Fee value
        public void UpdateActualMonthlyAndTxnFeesValue(string actualFee, string txnFee)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtActualMonthlyFee, 60);
            driver.FindElement(txtActualMonthlyFee).Clear();
            driver.FindElement(txtActualTxnFee).Clear();
            driver.FindElement(txtActualMonthlyFee).SendKeys(actualFee);
            driver.FindElement(txtActualTxnFee).SendKeys(txnFee);
            Thread.Sleep(3000);
        }

        //To validate if Actual Monthly Fee is enabled
        public string ValidateIfActualMonthlyFeeIsEnabled()
        {
           WebDriverWaits.WaitUntilEleVisible(driver, colActualMonthlyFee, 60);
           if (driver.FindElement(colActualMonthlyFee).Displayed)
            {
                return "Actual Monthly Fee column is read only";
            }
            else
            {
                return "Actual Monthly Fee column is enabled";
            }           
        }

        //To validate if Actual Transaction Fee is enabled
        public string ValidateIfActualTxnFeeIsEnabled()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, colActualTxnFee, 60);
            if (driver.FindElement(colActualTxnFee).Displayed)
            {
                return "Actual Transaction Fee column is read only";
            }
            else
            {
                return "Actual Transaction Fee column is enabled";
            }
        }

        //Search engagement by enagegement number
        public void SearchByEngNumber(string number)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnResetFilters, 60);
            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngageNumber, 60);
            driver.FindElement(txtEngageNumber).SendKeys(number);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(7000);
        }

        //To click on FR Engagement Name
        public string ClickFREngageName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkFREngageName, 110);
            driver.FindElement(linkFREngageName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngageDetail, 80);
            string title = driver.FindElement(titleEngageDetail).Text;
            return title;
        }
        //To update Period Accrual Fees with any negative value
        public void UpdateRevAccrualFeesValue(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtPerAccFees, 60);
            driver.FindElement(txtPerAccFees).Clear();
            driver.FindElement(txtPerAccFees).SendKeys(value);
            //driver.FindElement(txtTotalEstFeeFAS).SendKeys(Keys.Tab);
            Thread.Sleep(2000);
        }

        //To reset filters with LOB - FR 
        public void ResetFiltersForFR(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboStageField);
            driver.FindElement(comboStageField).SendKeys(value);
            WebDriverWaits.WaitUntilEleVisible(driver, btnApplyFilters,120);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(7000);
        }


    }
}
