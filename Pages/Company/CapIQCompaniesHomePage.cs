using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Company
{
    class CapIQCompaniesHomePage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By lnkCapIQCompanies = By.XPath("//a[normalize-space()='CapIQ Companies']");
        By drpdwnCapIQCompaniesView = By.CssSelector("select[id*='fcf']");
        By selectedCapIQCompaniesView = By.CssSelector("select[id='fcf'] > option[selected='selected']");
        By btnGoSearchCapIQCompanies = By.CssSelector("input[name='go']");
        By lnkOther = By.CssSelector("div[id*='00Bi00000062GLG'] >a:nth-child(27)");
        By lnkCapIQCompany = By.CssSelector("div[id*='a036w0000007ZJe'] > a > span");
        By btnAddSalesforceCompany = By.CssSelector("td[id='topButtonRow'] > input[value='Add Salesforce Company']");
        By lblCompanyEditPage = By.XPath("//h2[normalize-space()='Create a New Salesforce Company from CapIQ Data']");
        By lnkCapIQCompanyName = By.XPath("//a[normalize-space()='!A Test CapIQ Company']");
        By valCompanyType = By.CssSelector("option[value='012i0000000tEhFAAU']");
        By valCompanyName = By.CssSelector("input[id*='j_id28:j_id36']");
        By valCompanyCity = By.CssSelector("input[id*='j_id28:j_id39']");
        By valCompanyCountry = By.CssSelector("input[id*='j_id28:j_id42']");
        By valCompanyPhone = By.CssSelector("input[id*='j_id28:j_id43']");
        By valTickerSymbol = By.CssSelector("input[id*='j_id28:j_id47']");
        By valCapIQCompany = By.CssSelector("span[id*='j_id28:j_id48']");//("span[class='lookupInput'] > input[id*='j_id28:j_id48']");
        By valCompanyDesc = By.CssSelector("textarea[id*='j_id28:j_id46']");
        By valCompanyStreet = By.CssSelector("textarea[id*='j_id28:j_id38']");
        By btnCreate = By.CssSelector("td[class='pbButtonb '] > input[value='Create']");
        By lblCompanyDetail = By.XPath("//h2[normalize-space()='Company Detail']");
        By valCompanyNameFromDetail = By.CssSelector("div[id*='acc2j_id0']");
        By relationshipCapIQCompany = By.XPath("//*[text()='CapIQ Information']/..//following-sibling::div[1]//*[text()='CapIQ Company']/..//div/a");
        By lnkCompanies = By.CssSelector("a[title*='Companies Tab']");
        By txtCompanyName = By.CssSelector("input[id*='j_id37:nameSearch']");
        By btnCompanySearch = By.CssSelector("div[class='searchButtonPanel'] > center > input[value='Search']");
        By matchedResult = By.CssSelector("td[id*=':pbtCompanies:0:j_id68'] a");
        By btnDeleteCompany = By.CssSelector("td[id*='topButtonRow'] > input[value='Delete']");
        By CapIQList = By.CssSelector("div[class='listBody']");
        By lblCapIQCompanyDetail = By.CssSelector("h2[class='mainTitle']");
        By tblResults = By.CssSelector("div[class='x-panel-bwrap']");
        By txtPotentialMatch = By.CssSelector("div[class='messageText']");
        By btnCancelAndBack = By.CssSelector("td[class*='pbButtonb'] > input[value='Cancel and Back']");
        
        // To Search for CapIQ Company
        public string SearchCapIQCompany(string companyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, shwAllTab, 120);
            driver.FindElement(shwAllTab).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkCapIQCompanies, 120);
            driver.FindElement(lnkCapIQCompanies).Click();
            string CapIQCompaniesViewOption = "All";
            SelectElement element = new SelectElement(driver.FindElement(drpdwnCapIQCompaniesView));
            string selectedCapIQOption = element.SelectedOption.Text;
                        
            if (selectedCapIQOption.Equals(CapIQCompaniesViewOption))
            {
                IWebElement GoSearchBtn = driver.FindElement(btnGoSearchCapIQCompanies);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", GoSearchBtn);
                Thread.Sleep(5000);
            }
            else
            {
                CustomFunctions.SelectByText(driver, driver.FindElement(drpdwnCapIQCompaniesView), CapIQCompaniesViewOption);
                Thread.Sleep(2000);
            }

            try
            {
                WebDriverWaits.WaitFluent(driver, tblResults, 300);
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, lnkCapIQCompanyName, 200);
                CustomFunctions.ActionClicks(driver, lnkCapIQCompanyName);                
                Thread.Sleep(2000);
                return "Record found";
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                return "No record found";
            }
        }

        // Click Add salesforce company
        public void ClickAddSalesforceCompanyButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddSalesforceCompany, 120);
            driver.FindElement(btnAddSalesforceCompany).Click();
        }

        // To get company edit page label
        public string GetCompanyEditLabel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompanyEditPage, 60);
            string headingCompanyEdit = driver.FindElement(lblCompanyEditPage).Text;
            return headingCompanyEdit;            
        }

        // To get company edit page label
        public string GetpotentialMatchText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtPotentialMatch, 60);
            string potentialMatchText = driver.FindElement(txtPotentialMatch).Text;
            return potentialMatchText;
        }
        
        // To Validate the basic information of CapIQ company is copied to Salesforce Company create page
        public void ValidateBasicInfoCopiedOfCapIQToSalesforceComp()
        {
            CustomFunctions.isAttributePresent(driver, driver.FindElement(valCompanyType),"value");
            CustomFunctions.isAttributePresent(driver, driver.FindElement(valCompanyName),"value");
            CustomFunctions.isTextPresent(driver, driver.FindElement(valCompanyStreet));
            CustomFunctions.isAttributePresent(driver, driver.FindElement(valCompanyCity),"value");
            CustomFunctions.isAttributePresent(driver, driver.FindElement(valCompanyCountry), "value");
            CustomFunctions.isAttributePresent(driver, driver.FindElement(valCompanyPhone), "value");
            CustomFunctions.isTextPresent(driver, driver.FindElement(valCompanyDesc));
            CustomFunctions.isAttributePresent(driver, driver.FindElement(valTickerSymbol), "value");
            CustomFunctions.isAttributePresent(driver, driver.FindElement(valCapIQCompany), "value");        

        }

        // To Click create button
        public void ClickCreateButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCreate, 120);
            driver.FindElement(btnCreate).Click();
            Thread.Sleep(2000);
        }

        public void ClickCancelAndBackButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelAndBack, 120);
            driver.FindElement(btnCancelAndBack).Click();
            Thread.Sleep(2000);            

        }
        // To get company detail page heading
        public string GetCompanyDetailHeading()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, lblCompanyDetail, 60);
            string headingCompanyDetail = driver.FindElement(lblCompanyDetail).Text;
            return headingCompanyDetail;
        }

        // To get capIQ company detail page heading
        public string GetCapIQCompanyDetailHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCapIQCompanyDetail, 60);
            string headingCapIQCompanyDetail = driver.FindElement(lblCapIQCompanyDetail).Text;
            return headingCapIQCompanyDetail;
        }

        // To validate and get salesforce company name created
        public string ValidateAndGetCompanyName()
        {
            CustomFunctions.isTextPresent(driver, driver.FindElement(valCompanyNameFromDetail));
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyNameFromDetail, 60);
            string CompanyNameFromDetail = driver.FindElement(valCompanyNameFromDetail).Text.Split('[')[0].Trim();
            return CompanyNameFromDetail;
        }
    
        /* --- To Validate relationship is established after creating salesforce company with CapIQ company
         under CapIQ company information of company detail page -- */
        public bool ValidateRelationshipSalesforceCompanyWCapIQCompany()
        {
            return CustomFunctions.IsElementPresent(driver, relationshipCapIQCompany);
        }

        public bool ValidateCapIQCompanyList()
        {
            return CustomFunctions.IsElementPresent(driver, CapIQList);
        }        
    }
}