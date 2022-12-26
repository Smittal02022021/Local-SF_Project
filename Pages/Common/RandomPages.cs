using OpenQA.Selenium;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace SalesForce_Project.Pages.Common
{
    class RandomPages :BaseClass
    {
        By linkExpenseRequest = By.XPath("//a[text() = 'Expense Request']");
        By linkExpenseNumber = By.CssSelector("tbody[id*='pbtableId1:tb']>tr>td[id*='j_id142'] > a");
        By titleNewExpense = By.XPath("//h2[text() = 'New Expense Request']");
        By btnClone = By.CssSelector("input[value='Clone']");
        By linkDBCompanyRecords = By.XPath("//a[text() = 'D&B Company Records']");
        By btnAllList = By.Id("AllTab_Tab");
        By linkRowSelection = By.CssSelector("div[id*='FaE_Name'] > a");
        By linkDBContactRecords = By.XPath("//a[text() = 'D&B Contact Records']");
        By linkRowSelectionDB = By.CssSelector("div[id*='xdu_Name'] > a");
        By btnGo = By.CssSelector("input[title='Go!']");
        By titleCoverageTeam = By.CssSelector("h2[class='mainTitle']");
        By linkDBContactRec = By.CssSelector("div[id*='p_Name'] > a");
        By linkLegalEntities = By.XPath("//a[text() = 'Legal Entities']");
        By linkLegalEntityName = By.XPath("//th/a[text() = 'HL Capital, Inc.']");
        By linkJobTypes = By.XPath("//a[text() = 'Job Types']");
        By valProductLine = By.CssSelector("table > tbody > tr:nth-child(21) > td:nth-child(2)");
        By valProductTypeCode = By.CssSelector("table > tbody > tr:nth-child(22) > td:nth-child(2)");
        By valJobTypeNames = By.CssSelector("div[id*='_Name']>a>span");
        By valJobTypes =By.CssSelector("div[class*='-row']>table>tbody>tr>td:nth-child(3)");
        By valProdLines = By.CssSelector("div[class*='-row']>table>tbody>tr>td:nth-child(4)");
        By valBlank = By.CssSelector("div[id*='wN2_00Ni000000G8Xmo']");

        //To click Expense Request Tab
        public string ClickExpenseRequestTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkExpenseRequest, 120);
            driver.FindElement(linkExpenseRequest).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleNewExpense, 100);
            string title = driver.FindElement(titleNewExpense).Text;
            return title;
        }

        //To Click Expense Number
        public string ClickExpenseNumberandValidateCloneButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkExpenseNumber, 100);
            driver.FindElement(linkExpenseNumber).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
            try
            {
                string value = driver.FindElement(btnClone).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Clone button is displayed";
                }
                else
                {
                    return "Clone button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Clone button is not displayed";
            }
        }

        //To close additional opened tab
        public void CloseAdditionalTab()
        {
            driver.Close();
            Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        //To click D&B Company Records
        public string ClickDBCompanyRecords()
        {
            driver.Navigate().Refresh();            
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkDBCompanyRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 90);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkRowSelection, 90);
            driver.FindElement(linkRowSelection).Click();
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        //To click D&B Contact Records
        public string ClickDBContactRecords()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkDBContactRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo,100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkDBContactRec, 110);
            driver.FindElement(linkDBContactRec).Click();
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        //To click Legal Entities 
        public string ClickLegalEntities(string name)
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 150);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkLegalEntities).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 120);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div/a/span[text() = '" + name + "']")).Click();           
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        //To Get Job Types 
        public string ClickJobTypes(string name)
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkJobTypes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000); 
            driver.FindElement(By.XPath("//div/a/span[text() = '" + name + "']")).Click();           
            WebDriverWaits.WaitUntilEleVisible(driver, titleCoverageTeam, 110);
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }
       
        //To Validate Job Types 
        public bool ValidateJobTypes()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkJobTypes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> jobTypes = driver.FindElements(valJobTypes);
            var actualValue = jobTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = {"Activism Advisory", "Board Advisory Services (BAS)", "Buyside", "Buyside & Financing Advisory", "Collateral Valuation", "Compensation/Formula Analysis", "Consulting", "Corporate Alliances", "Creditor Advisors","CVAS - Forensic Services","CVAS - FV Opinion","CVAS - Goodwill or Asset Impairment", "CVAS - Pre-Acq Val'n Cons", "CVAS - Purchase Price Allocation", "CVAS - SFAS 123R/409A Stock, Option Valuation", "CVAS - SFAS 133 Derivatives, Liabilities Valuation", "CVAS - Sovereign Advisory", "CVAS - Tangible Asset Valuation", "CVAS - Tax Valuation", "CVAS - Transfer Pricing", "Debt Advisory", "Debt Capital Markets", "Debtor Advisors", "Discretionary Advisory", "DM&A Buyside", "DM&A Sellside","DRC - Ad Valorem Services","DRC - Appointed Arbitrator/Mediator","DRC - Contract Compliance","DRC - Exp Cons-Arbitrat'n","DRC - Exp Cons-Bankruptcy","DRC - Exp Cons-Litigation","DRC - Exp Cons-Mediation","DRC - Exp Cons-Pre-Complt","DRC - Exp Wit-Arbitration","DRC - Exp Wit-Bankruptcy","DRC - Exp Wit-Litigation","DRC - Exp Wit-Mediation","DRC - Exp Wit-Pre-Complnt","DRC - Post Transaction Dispute","Equity Advisors","Equity Capital Markets","ESOP Advisory","ESOP Capital Partnership","ESOP Corporate Finance","ESOP Fairness","ESOP Update","Estate & Gift Tax","FA - Fund Opinions-Fairness","FA - Fund Opinions-Non-Fairness","FA - Fund Opinions-Valuation","FA - Portfolio - SPAC","FA - Portfolio LIBOR Advisory","FA - Portfolio-Advis/Consulting","FA - Portfolio-Auto Loans","FA - Portfolio-Auto Struct Prd","FA - Portfolio-BDC/Direct Lending","FA - Portfolio-Deriv/Risk Mgmt","FA - Portfolio-Diligence/Assets","FA - Portfolio-Funds Transfer","FA - Portfolio-GP interest","FA - Portfolio-Real Estate","FA - Portfolio-Valuation","Fairness","FAS - Administrative","Financing","FMV Non-Transaction Based Opinion","FMV Transaction Based Opinion","General Financial Advisory","Going Private","Illiquid Financial Assets","Income Deposit Securities","InSource","Liability Management","Liability Mgmt","Litigation","Merger","Negotiated Fairness","Partners","PBAS","Portfolio Acquisition","Private Funds: GP Advisory", "Private Funds: GP Stake Sale", "Private Funds: Primary Advisory", "Private Funds: Secondary Advisory", "Real Estate Brokerage","Regulator/Other","Securities Design","Sellside","Solvency","Sovereign Restructuring","Special Committee Advisory","Special Situations","Special Situations Buyside","Special Situations Sellside","Strategic Alternatives Study","Strategic Consulting","Syndicated Finance","T+IP - Damages","T+IP - Expert Report","Take Over Defense","TAS - Accounting and Financial Reporting Advisory","TAS - Due Diligence-Buyside","TAS - Due Diligence-Sellside","TAS - Lender Services","TAS - Tax","Tech+IP - Buyside","Tech+IP - Patent Acquisition Support","Tech+IP - Patent Sales","Tech+IP - Strategic Advisory","Tech+IP - Tech+IP Sales","Tech+IP - Valuation"};
            bool isSame = true;

            List<string> difference = actualValue.Except(expectedValue).ToList();
            foreach (var value in difference)
            {
                Console.WriteLine(value);                
            }           

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
             


        //To get Job Types 
        public void GetJobTypes()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkJobTypes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> type = driver.FindElements(valJobTypes);
            //Console.WriteLine("NUMBER OF ROWS IN THIS TABLE = " + type.Count);
            int row_num = 1;           
            foreach (IWebElement element in type)
            {
                Console.WriteLine(element.Text);
                row_num++;               
            }
        }

        //To get Product Line 
        public void GetProductLines()
        {            
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> type = driver.FindElements(valProdLines);
            Console.WriteLine("NUMBER OF ROWS IN THIS TABLE = " + type.Count);
            
            int row_num = 1; 
            
                foreach (IWebElement element in type)
                {
                Console.WriteLine("row# "+row_num+element.Text);                
                row_num++;
                }               
              }

        //To Validate Job Types 
        public bool ValidateProductLines()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkJobTypes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);            
            IReadOnlyCollection<IWebElement> prodLines = driver.FindElements(valProdLines);
            var actualValue = prodLines.Select(x => x.Text).ToArray();
            string[] expectedValue = {"Advisory","Transaction Opinions","Buyside","Capital Markets", "Other", "Other", "Other", "Advisory", "Financial Restructuring - Creditor / Debtor","CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "Capital Markets", "Capital Markets", "Financial Restructuring - Creditor / Debtor", "Advisory", "Financial Restructuring - Other", "Financial Restructuring - Other", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Financial Restructuring - Other", "Capital Markets", "Other", "Other", "Advisory", "Transaction Opinions", "Other", "Dispute", "Fund Opinions", "Fund Opinions", "Fund Opinions", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Transaction Opinions", " ","Capital Markets", "Other", "Transaction Opinions", "Advisory", "Advisory", "Advisory", "Other", "Other", "Capital Markets", "Financial Restructuring - Other", "Dispute", "Sellside", "Advisory", "Capital Markets", "Financial Restructuring - Other", " ","Private Funds Advisory", "Private Funds Advisory", "Private Funds Advisory", "Private Funds Advisory", "Advisory", "Financial Restructuring - Other", "Other", "Sellside", "Transaction Opinions"," ", "Advisory", "Advisory", "Buyside", "Sellside", "Advisory", "Strategic Consulting", "Capital Markets", "Tech+IP Advisory", "Tech+IP Advisory", "Advisory", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "Advisory", "Tech+IP Advisory", "Advisory", "Tech+IP Advisory", "Advisory", "Tech+IP Advisory"};

            bool isSame = true;

            List<string> difference = actualValue.Except(expectedValue).ToList();
            //int row_num = 1;
            foreach (var value in difference)
            {
               Console.WriteLine(value);               
            }            

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }                
            }
            return isSame;
        }

        public string GetBlankValue()
        {
            string value = driver.FindElement(valBlank).Text;
            return value;
        }

             
                    

        //Get the value of Product Line
        public string GetProductLine()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProductLine, 110);
            string value = driver.FindElement(valProductLine).Text;
            return value;
        }

        //Get the value of Product Type Code
        public string GetProductTypeCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProductTypeCode, 110);
            string value = driver.FindElement(valProductTypeCode).Text;
            return value;
        }
    }
}
