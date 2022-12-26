using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Engagement
{
    class AddCounterparty : BaseClass
    {

        By btnAddCounterparties = By.CssSelector(".pbButton >table>tbody>tr>td> input[value='Add Counterparties']");
        By btnDel = By.Id("sf_filter_remove_btn_1");
        By btnAddRow = By.Id("sf_filter_add_btn_and");
        By comboCity = By.Id("sf_filter_field_1");
        By txtCityName = By.Id("sf_value_1");
        By btnSearch = By.CssSelector("td>#search_btn");
        By checkCity = By.Id("thePage:theForm3:pbResult:tickTable:0:myCheckbox");
        By btnAddRec = By.CssSelector("input[value*='Add Selected Records To ']");
        By msgSuccess = By.CssSelector("div[class*='messageText']");
        By msgSuccessContact = By.CssSelector("div[id*='j_id8:j_id10']");
        By btnBack = By.Id("back_btn");
        By lnkDetails = By.CssSelector(".view_record__c > a");
        By btnAddEngCounterPartyÇontact = By.CssSelector("input[value='New Engagement Counterparty Contact']");
        By checkName = By.CssSelector("tbody[id*='pbtableId2:tb'] > tr:nth-child(1) > td:nth-child(1)");
        By btnSave = By.CssSelector("input[value='Save']");
        By titleCPSearch = By.CssSelector("div[class*='pbSubheader']>h3");
        By btnTableBack = By.CssSelector("input[name*='PanelId:j_id103']");
        By lnkEngagement = By.CssSelector("a[id*='lookupa']");
        By btnCounterParties = By.CssSelector("td[id*='topButtonRow'] > input[value='Counterparties']");
        By checkRec = By.XPath("//*[@id='dtable']/div[1]/div[2]/div/div/div[1]/input[1]");
        By titlePage = By.CssSelector("h1[class='pageType']");
        By btnDelete = By.CssSelector("input[value='Delete']");
        By msgText = By.CssSelector("span[id*=':f']> div");
        By txtEngage = By.CssSelector("span>input[name*='id64:0:j_id66']");
        By btnSearchEng = By.CssSelector("td>#search_btn2");
        By checkRow = By.CssSelector("#dtable > div.fix-column > div.tbody > div > div > div:nth-child(1) > input.targetCheck");
        By lblExistingEng = By.CssSelector("h3:nth-child(3) > a");
        By btnNewBid = By.CssSelector("input[value='New Bid']");
        By lnkDate = By.CssSelector("span.dateFormat > a");
        By btnCancel = By.CssSelector("td#topButtonRow > input[value='Cancel']");
        By msgNoRec = By.CssSelector("div[id*='3_body'] > table >tbody > tr >th");
        By btnBidSave = By.CssSelector("input[value=' Save ']");
        By valBidDate = By.CssSelector("td[class*='DateElement']");
        By lnkBidEdit = By.XPath("//a[text()='Edit']");
        By txtDate = By.CssSelector("input[id*='FlXO1']");
        By lnkBidDel = By.XPath("//a[text()='Del']");


        //To Click Counterparties button
        public string ClickAddCounterpartiesbutton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterparties, 60);
            driver.FindElement(btnAddCounterparties).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 60);
            string title = driver.FindElement(titlePage).Text;
            return title;
        }


        //To validate Counterparties button
        public string AddCounterparties()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnDel, 90);
                driver.FindElement(btnDel).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddRow, 60);
                driver.FindElement(btnAddRow).Click();
                driver.FindElement(comboCity).SendKeys("Company Name");
                driver.FindElement(txtCityName).SendKeys("Test");
                driver.FindElement(btnSearch).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, checkCity, 120);
                driver.FindElement(checkCity).Click();
                driver.FindElement(btnAddRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 100);
                string message = driver.FindElement(msgSuccess).Text;
                driver.FindElement(btnBack).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, lnkDetails, 120);
                driver.FindElement(lnkDetails).Click();
                return message;
            }
            catch (Exception)
            {
                driver.FindElement(lnkDetails).Click();
                return "Record is already displayed";
            }
        }


        public void ClickDetailsLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDetails, 70);
            driver.FindElement(lnkDetails).Click();
        }

        public string ClickAddCounterPartyContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEngCounterPartyÇontact, 80);
            driver.FindElement(btnAddEngCounterPartyÇontact).Click();
            string title = driver.FindElement(titleCPSearch).Text;
            return title;
        }

        // To validate cancel functionality of CP Contact
        public string ValidateCancelFunctionalityOFCPContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkName, 100);
            driver.FindElement(checkName).Click();
            driver.FindElement(btnSave).Click();
            driver.SwitchTo().Alert().Dismiss();
            try
            {
                string message = driver.FindElement(msgSuccess).Text;
                return message;
            }
            catch (Exception)
            {
                return "Record is not addded";
            }
        }

        //To validate Please select at least one contact before save Message
        public string ValidateErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkName, 100);
            driver.FindElement(checkName).Click();
            driver.FindElement(btnSave).Click();
            driver.SwitchTo().Alert().Accept();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 80);
                string message = driver.FindElement(msgSuccess).Text.Replace("\r\n", " ");
                return message;
            }
            catch (Exception)
            {
                return "Message is not displayed";
            }
        }

        // To validate Save functionality of CP Contact
        public string SaveCounterpartyContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkName, 80);
            driver.FindElement(checkName).Click();
            driver.FindElement(btnSave).Click();
            driver.SwitchTo().Alert().Accept();
            try
            {
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessContact, 150);
                Thread.Sleep(3000);
                string message = driver.FindElement(msgSuccessContact).Text.Replace("\r\n", " ");
                WebDriverWaits.WaitUntilEleVisible(driver, btnTableBack, 100);
                driver.FindElement(btnTableBack).Click();
                return message;
            }
            catch (Exception)
            {
                return "Record is not addded";
            }
        }

        //Delete the added Counterparty
        public string DeleteAddedCP()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagement, 80);
            driver.FindElement(lnkEngagement).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCounterParties, 80);
            driver.FindElement(btnCounterParties).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRec, 80);
            driver.FindElement(checkRec).Click();
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgText, 120);
            string message = driver.FindElement(msgText).Text;
            return message;
        }

        //Add company from existing Engagement
        public string AddCompanyFromExistingEng(string Name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistingEng, 80);
            driver.FindElement(lblExistingEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngage, 80);
            driver.FindElement(txtEngage).SendKeys(Name);
            driver.FindElement(btnSearchEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkCity, 80);
            driver.FindElement(checkCity).Click();
            driver.FindElement(btnAddRec).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 80);
            string message = driver.FindElement(msgSuccess).Text;
            return message;
        }

        //Validate page after clicking back button
        public string ClickBackAndGetTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBack, 60);
            driver.FindElement(btnBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 60);
            string title = driver.FindElement(titlePage).Text;
            return title;
        }


        //Validate if company get added
        public string ValidateAddedCompanyExists()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 80);
            string value = driver.FindElement(checkRow).Displayed.ToString();
            return value;
        }

        //Delete the added company
        public string DeleteAddedCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 80);
            driver.FindElement(checkRow).Click();
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(4000);
            string text = driver.FindElement(msgText).Text;
            return text;
        }

        //Click on New Bid and validate page 

        public string ClickNewBidAndValidatePage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBid, 80);
            driver.FindElement(btnNewBid).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 80);
            string text = driver.FindElement(titlePage).Text;
            return text;
        }

        // To validate cancel functionality of Bid
        public string ValidateCancelFunctionalityOFBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDate, 80);
            driver.FindElement(lnkDate).Click();
            driver.FindElement(btnCancel).Click();
            string page = driver.FindElement(titlePage).Text;
            return page;
        }

        // To validate No Records message
        public string ValidateNoRecordsMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgNoRec, 80);
            string message = driver.FindElement(msgNoRec).Text;
            return message;
        }

        //Save Bid details
        public string SaveBidDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDate, 80);
            driver.FindElement(lnkDate).Click();
            driver.FindElement(btnBidSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valBidDate, 100);
            string date = driver.FindElement(valBidDate).Text;
            return date;
        }

        // To validate edit functionality of Bid
        public string ValidateEditFunctionalityOFBid(string date)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBidEdit, 80);
            driver.FindElement(lnkBidEdit).Click();
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(date);
            driver.FindElement(btnBidSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valBidDate, 100);
            string valDate = driver.FindElement(valBidDate).Text;
            return valDate;
        }

        // To validate delete functionality of Bid
        public string ValidateDeleteFunctionalityOFBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBidDel, 80);
            driver.FindElement(lnkBidDel).Click();
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, msgNoRec, 100);
            string message = driver.FindElement(msgNoRec).Text;
            return message;
        }


    }
}
