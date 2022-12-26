using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.TimeRecordManager
{
    class RateSheetManagementPage : BaseClass
    {
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgTitleRateSheet = By.CssSelector("img[alt = 'Title Rate Sheets']");
        By btnGo = By.CssSelector("input[title='Go!']");
        By tableRateSheet = By.XPath("//table[@class='x-grid3-row-table']/tbody/tr");
        By linkFilterByLetterT = By.XPath("//a[@class='listItem']/span[text()='T']");
        By tabRateSheetManagement = By.CssSelector("li[id*='ratesheet'] > a");
        By comboEngagement = By.CssSelector("select[class*='engagementPicker']");
        By comboEngagementOptions = By.CssSelector("select[class*='engagementPicker'] > option[value*='13aH7IQAU']");
        By comboSelectRateSheet = By.XPath("//div[contains(text(),'Add Record')]/following::div/div/div/select");
        By txtRateSheetFromDate = By.CssSelector("div[class='slds-card__body'] > div[class='slds-grid'] > div:nth-child(2) > div > input");
        By txtRateSheetToDate = By.CssSelector("div[class='slds-card__body'] > div[class='slds-grid'] > div:nth-child(3) > div > input");
        By btnAddRateSheet = By.XPath("//span[text()='Add']");
        By valRateSheetRecord = By.CssSelector("div[class*='slds-table'] > tr > td:nth-child(1)");
        By btnCrossDeleteRecord = By.CssSelector("td[class*='slds-cell-shrink'] > button[class*='slds-button']");
        By tabBillingPreparation = By.CssSelector("li[id*='billing'] > a");
        By txtBillingAmt = By.CssSelector("#tab-billing > div > div.slds-table.slds-table--striped > tr > td:nth-child(5) > span.uiOutputNumber");
        By valDetailList = By.XPath("//*/td[contains(text(),'Intern/Financial')]");

        public void EnterRateSheet(string engagement,string rateSheet)
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
            driver.FindElement(tabRateSheetManagement).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
            WebDriverWaits.WaitUntilEleVisible(driver, comboEngagementOptions, 220);
            
            driver.FindElement(comboEngagement).SendKeys(engagement);
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectRateSheet);
            driver.FindElement(comboSelectRateSheet).SendKeys(rateSheet);
            Thread.Sleep(2000);

            string getFromDate = DateTime.Now.ToString("MMM dd, yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRateSheetFromDate);
            driver.FindElement(txtRateSheetFromDate).Clear();
            driver.FindElement(txtRateSheetFromDate).SendKeys(getFromDate);

            string getToDate = DateTime.Now.AddDays(+7).ToString("MMM dd, yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRateSheetToDate);
            driver.FindElement(txtRateSheetToDate).Clear();
            driver.FindElement(txtRateSheetToDate).SendKeys(getToDate);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRateSheet);
            driver.FindElement(btnAddRateSheet).Click();
            Thread.Sleep(5000);
        }

        //TO get selected rate sheet
        public string GetSelectedRateSheet()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRateSheetRecord, 80);
            string selectedRateSheet = driver.FindElement(valRateSheetRecord).Text;
            return selectedRateSheet;
        }

        public void DeleteRateSheet(string engagementName)
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
            driver.FindElement(tabRateSheetManagement).Click();
            Thread.Sleep(10000);

            WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
            driver.FindElement(comboEngagement).SendKeys(engagementName);
            Thread.Sleep(10000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnCrossDeleteRecord);
            driver.FindElement(btnCrossDeleteRecord).Click();
            Thread.Sleep(5000);
            
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);
        }

        public string GetBillingAmountFromBillingPreparationTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabBillingPreparation);
            driver.FindElement(tabBillingPreparation).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtBillingAmt,120);
            string BillingAmt = driver.FindElement(txtBillingAmt).Text;
            return BillingAmt;
        }

        public string GetDefaultRateForAssociateAsPerRateSheet(string rateSheet)
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgTitleRateSheet).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            driver.FindElement(linkFilterByLetterT).Click();
            Thread.Sleep(2000);

            //Get Row Count
            IList<IWebElement> element = driver.FindElements(tableRateSheet);
            int rowCount = element.Count;
            string rate = "";
            for (int p = 1; p <= rowCount; p++)
            {
                By titleRateSheetName = By.CssSelector($"table[class='x-grid3-row-table'] > tbody > tr:nth-child({p}) > td:nth-child(3) > div > a > span");
                IWebElement rateSheetName = driver.FindElement(titleRateSheetName);

                string sheet = rateSheetName.Text;
                if (sheet.Equals(rateSheet))
                {
                    driver.FindElement(titleRateSheetName).Click();
                    Thread.Sleep(5000);

                    By associateRate = By.XPath("//*[contains(text(),'Associate Rate')]/following::td[1]");
                    IWebElement associateRatePerHour = driver.FindElement(associateRate);

                    rate = associateRatePerHour.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return rate;
        }

        public string GetDefaultRateForOutsourcedContractorAsPerRateSheet(string rateSheet)
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgTitleRateSheet).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            driver.FindElement(linkFilterByLetterT).Click();
            Thread.Sleep(2000);

            //Get Row Count
            IList<IWebElement> element = driver.FindElements(tableRateSheet);
            int rowCount = element.Count;
            string rate = "";
            for (int p = 1; p <= rowCount; p++)
            {
                By titleRateSheetName = By.CssSelector($"table[class='x-grid3-row-table'] > tbody > tr:nth-child({p}) > td:nth-child(3) > div > a > span");
                IWebElement rateSheetName = driver.FindElement(titleRateSheetName);

                string sheet = rateSheetName.Text;
                if (sheet.Equals(rateSheet))
                {
                    driver.FindElement(titleRateSheetName).Click();
                    Thread.Sleep(5000);

                    By outsourcedContractorRate = By.XPath("//*[contains(text(),'Outsourced Contractor Rate')]/following::td[1]");
                    IWebElement outsourcedContractorRatePerHour = driver.FindElement(outsourcedContractorRate);

                    rate = outsourcedContractorRatePerHour.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return rate;
        }

        public string GetDefaultRateForInternAndFinancialAnalyst(string rateSheet)
        {
            string rate = "";
            if (driver.Title.Equals("Title Rate Sheet: " + rateSheet + " ~ Salesforce - Unlimited Edition"))
            {
                string internRatePerHour = driver.FindElement(By.XPath("//*/td[contains(text(),'Intern/Financial')]/following::td")).Text;
                rate = internRatePerHour.Split(' ')[1].Trim();
            }
            return rate;
        }

        public double GetDefaultRateAsPerRole(string role)
        {
            string ratePerHour = driver.FindElement(By.XPath($"//*[text()='{role}']/following::td")).Text;
            double rate = Convert.ToDouble(ratePerHour.Split(' ')[1].Trim());
            return rate;
        }

        public void ClickNewTitleRateSheet(string rateSheet)
        {
            //Get Row Count
            IList<IWebElement> element = driver.FindElements(tableRateSheet);
            int rowCount = element.Count;
            for (int p = 1; p <= rowCount; p++)
            {
                By titleRateSheetName = By.CssSelector($"div[class='x-grid3-body'] > div:nth-child({p}) > table > tbody > tr > td:nth-child(3) > div > a > span");
                IWebElement rateSheetName = driver.FindElement(titleRateSheetName);

                string sheet = rateSheetName.Text;
                if (sheet.Equals(rateSheet))
                {
                    driver.FindElement(titleRateSheetName).Click();
                    break;
                }
            }
        }

        public void NavigateToTitleRateSheetsPage()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgTitleRateSheet).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
        }

        public bool VerifyNewTitle(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, valDetailList, 120);
            string listName = driver.FindElement(valDetailList).Text;
            if (ReadExcelData.ReadData(excelPath, "RateSheetManagement", 1).Equals(listName))
            {
                result = true;
            }
            
            return result;
        }
    }
}
