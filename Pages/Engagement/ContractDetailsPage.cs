using OpenQA.Selenium;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Engagement
{
    class ContractDetailsPage : BaseClass
    {
        By valERPSubmittedToSync = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(1) > td.dataCol.col02");
        By btnEdit = By.CssSelector("td[id*='topButtonRow'] > input[title='Edit']");
        By valERPSubmittedToSyncICO = By.XPath("//*[@id='ep']/div[2]/div[4]/table/tbody/tr[1]/td[2]");
        By valERPLegalEntity = By.CssSelector("div[id*='M0ecL'] >a");
        By valERPLegalEntityICO = By.CssSelector("a[id*='M0ecL']");
        By valERPLegalEntityName = By.CssSelector("div[id*='M0ecM']");
        By valERPLegalEntityNameICO = By.XPath("//*[@id='ep']/div[2]/div[4]/table/tbody/tr[3]/td[2]");
        By valERPBusinessUnit = By.CssSelector("div[id*='M0ec3']");
        By valERPBusinessUnitICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(4) > td.dataCol.col02");
        By valERPBusinessUnitId = By.CssSelector("div[id*='M0ec2']");
        By valERPBusinessUnitIdICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(6) > td.dataCol.col02");
        By valERPExpenditureTypeName = By.CssSelector("div[id*='M0ecB']");
        By valERPExpenditureTypeNameICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(7) > td.dataCol.col02");
        By valERPProjectNumber = By.CssSelector("div[id*='M0ecY']");
        By valERPProjectNumberICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(8) > td.dataCol.col02");
        By valERPOrganization = By.CssSelector("div[id*='M0ecU']");
        By valERPOrganizationICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(9) > td.dataCol.col02");
        By valERPContractNumber = By.CssSelector("div[id*='M0ec4']");
        By valERPContractNumberICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(2) > td:nth-child(4)");
        By valERPId = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(3) > td:nth-child(4)");
        By valERPProjectId = By.CssSelector("div[id*='M0ecX']");
        By valERPProjectIdICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(4) > td:nth-child(4)");
        By valERPLastIntegrationResponseDate = By.CssSelector("div[id*='M0ecI']");
        By valERPLastIntRespDateICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(11) > td:nth-child(4)");
        By valERPLastIntegrationStatus = By.CssSelector("div[id*='M0ecJ']");
        By valERPLastIntegrationStatusICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(12) > td:nth-child(4)");
        By valBillTo = By.CssSelector("div[id*='M0ebc'] > a");
        By valBillToICO = By.CssSelector("a[id*='M0ebc']");
        By valBillingContact = By.CssSelector("div[id*='M0ebh'] > a");
        By valBillingContactICO = By.CssSelector("a[id*='M0ebh']");
        By valERPBillToCustomerId = By.CssSelector("div[id*='M0ebx']");
        By valERPBillToCustomerIdICO = By.CssSelector("#ep > div.pbBody > div:nth-child(8) > table > tbody > tr:nth-child(3) > td.dataCol.col02");
        By valERPBillToAddressId = By.CssSelector("div[id*='M0ebv']");
        By valERPBillToAddressIdICO = By.CssSelector("#ep > div.pbBody > div:nth-child(8) > table > tbody > tr:nth-child(4) > td.dataCol.col02");
        By valERPBillToContactId = By.CssSelector("div[id*='M0ebw']");
        By valERPBillToContactIdICO = By.CssSelector("#ep > div.pbBody > div:nth-child(8) > table > tbody > tr:nth-child(5) > td.dataCol.col02");
        By valShipTo = By.CssSelector("div[id*='M0ed5'] > a");
        By valShipToICO = By.CssSelector("a[id*='M0ed5']");
        By valERPShipToCustomerId = By.CssSelector("div[id*='M0ecg']");
        By valERPShipToCustomerIdICO = By.CssSelector("#ep > div.pbBody > div:nth-child(8) > table > tbody > tr:nth-child(2) > td:nth-child(4)");
        By valERPShipToAddressId = By.CssSelector("div[id*='M0ecf']");
        By valERPShipToAddressIdICO = By.CssSelector("#ep > div.pbBody > div:nth-child(8) > table > tbody > tr:nth-child(3) > td:nth-child(4)");
        By valERPShipToAccountNumber = By.CssSelector("div[id*='M0ece']");
        By valERPShipToAccountNumberICO = By.CssSelector("#ep > div.pbBody > div:nth-child(8) > table > tbody > tr:nth-child(4) > td:nth-child(4)");
        By valERPExpenditureBusinessUnit = By.CssSelector("div[id*='M0ec9']");
        By valERPExpenditureBusinessUnitICO = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(5) > td.dataCol.col02");
        By valERPTaskNumber = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(5) > td:nth-child(4)");
        By valERPTaskName = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(6) > td:nth-child(4)");
        By valERPTaskDescription = By.CssSelector("#ep > div.pbBody > div:nth-child(4) > table > tbody > tr:nth-child(7) > td:nth-child(4)");
        By lnkERPSubmittedToSyncDate = By.CssSelector("#ep > div.pbBody > div:nth-child(5) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By btnSave = By.CssSelector("td[id='topButtonRow'] > input[title='Save']");
        //By valERPId

        public string GetERPSubmittedToSync()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSync, 120);
            string valueERPSubmittedToSync = driver.FindElement(valERPSubmittedToSync).Text;
            return valueERPSubmittedToSync;
        }

        public string GetERPSubmittedToSyncICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSyncICO, 60);
            string valueERPSubmittedToSyncICO = driver.FindElement(valERPSubmittedToSyncICO).Text;
            return valueERPSubmittedToSyncICO;
        }


        public string GetERPTaskNumber()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valERPTaskNumber, 60);
            string ERPTaskNumber = driver.FindElement(valERPTaskNumber).Text;
            return ERPTaskNumber;
        }

        public string GetERPTaskName()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valERPTaskName, 60);
            string ERPTaskName = driver.FindElement(valERPTaskName).Text;
            return ERPTaskName;
        }

        public string GetERPTaskDesc()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valERPTaskDescription, 60);
            string ERPTaskDescription = driver.FindElement(valERPTaskDescription).Text;
            return ERPTaskDescription;
        }

        public string GetERPTaskValue(string LegalEntityName,string revenueAllcation)
        {
            Thread.Sleep(2000);
            if (LegalEntityName.Equals("HL Financial Advisors, Inc."))
            {
                return "ICO - 020 - 1";
            }
            else
            if (LegalEntityName.Equals("HL EMEA, LLP") && revenueAllcation.Equals("LO-QM"))
            {
                return "ICO - 520 - 1";
            }
            else 
                if (LegalEntityName.Equals("HL EMEA, LLP"))
                {
                return "ICO - 420 - 1";
            }
            else
            if (LegalEntityName.Contains("HL Consulting"))
            {
                return "ICO - 030 - 1";
            }
            else
            if (LegalEntityName.Contains("HL Capital"))
            {
                return "ICO - 030 - 1";
            }
            return null;
        }


        public string GetERPExpenditureBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPExpenditureBusinessUnit, 60);
            string ERPExpenditureBusinessUnit = driver.FindElement(valERPExpenditureBusinessUnit).Text;
            return ERPExpenditureBusinessUnit;
        }

        public string GetERPExpenditureBusinessUnitICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPExpenditureBusinessUnitICO, 60);
            string ERPExpenditureBusinessUnit = driver.FindElement(valERPExpenditureBusinessUnitICO).Text;
            return ERPExpenditureBusinessUnit;
        }

        public string GetERPLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntity, 60);
            string valueERPLegalEntity = driver.FindElement(valERPLegalEntity).Text;
            return valueERPLegalEntity;
        }

        public string GetERPLegalEntityICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityICO, 60);
            string valueERPLegalEntity = driver.FindElement(valERPLegalEntityICO).Text;
            return valueERPLegalEntity;
        }

        public string GetLegalEntityBasedOnLOB(string LOB)
        {
            if (LOB.Equals("FVA"))
            {
                return "HL Financial Advisors, Inc.";
            }
            else
            {
                return "HL Capital, Inc.";
            }
        }

        public string GetERPBusinessUnitICO(string LegalEntityName)
        {
            if (LegalEntityName.Equals("HL Financial Advisors, Inc."))
            {
                return "USD US";
            }
            else
            {
                return "USD US";
            }
        }

        public string GetERPBusinessUnitIdICO(string LegalEntityName)
        {
            if (LegalEntityName.Equals("HL Financial Advisors, Inc."))
            {
                return "300000002779895";
            }
            else
            {
                return "300000002779895";
            }
        }

        public string GetERPLegalEntityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityName, 60);
            string valueERPLegalEntityName = driver.FindElement(valERPLegalEntityName).Text;
            return valueERPLegalEntityName;
        }

        public string GetERPLegalEntityNameICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityNameICO, 60);
            string valueERPLegalEntityNameICO = driver.FindElement(valERPLegalEntityNameICO).Text;
            return valueERPLegalEntityNameICO;
        }


        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnit, 60);
            string valueERPBusinessUnit = driver.FindElement(valERPBusinessUnit).Text;
            return valueERPBusinessUnit;
        }

        public string GetERPBusinessUnitICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitICO, 60);
            string valueERPBusinessUnit = driver.FindElement(valERPBusinessUnitICO).Text;
            return valueERPBusinessUnit;
        }

        public string GetERPBusinessUnitId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitId, 60);
            string valueERPBusinessUnitId = driver.FindElement(valERPBusinessUnitId).Text;
            return valueERPBusinessUnitId;
        }
        public string GetERPBusinessUnitIdICOContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitIdICO, 80);
            string valueERPBusinessUnitId = driver.FindElement(valERPBusinessUnitIdICO).Text;
            return valueERPBusinessUnitId;
        }


        public string GetERPExpenditureTypeName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPExpenditureTypeName, 60);
            string valueERPExpenditureTypeName = driver.FindElement(valERPExpenditureTypeName).Text;
            return valueERPExpenditureTypeName;
        }

        public string GetERPExpenditureTypeNameICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPExpenditureTypeNameICO, 60);
            string valueERPExpenditureTypeName = driver.FindElement(valERPExpenditureTypeNameICO).Text;
            return valueERPExpenditureTypeName;
        }

        public string GetERPProjectNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectNumber, 60);
            string valueERPProjectNumber = driver.FindElement(valERPProjectNumber).Text;
            return valueERPProjectNumber;
        }


        public string GetERPProjectNumberICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectNumberICO, 60);
            string valueERPProjectNumber = driver.FindElement(valERPProjectNumberICO).Text;
            return valueERPProjectNumber;
        }

        public string GetERPOrganization()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPOrganization, 60);
            string valueERPOrganization = driver.FindElement(valERPOrganization).Text;
            return valueERPOrganization;
        }

        public string GetERPOrganizationICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPOrganizationICO, 60);
            string valueERPOrganization = driver.FindElement(valERPOrganizationICO).Text;
            return valueERPOrganization;
        }

        public bool GetERPContractNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContractNumberICO, 60);
            //string valueERPContractNumber = driver.FindElement(valERPContractNumber).Text;
            bool contractNumberVisible = CustomFunctions.IsElementPresent(driver, valERPContractNumberICO);
            return contractNumberVisible;
        }

        public string GetERPId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPId, 60);
            string valueERPId = driver.FindElement(valERPId).Text;
            return valueERPId;
        }

        public string GetERPTaskNumberICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPId, 60);
            string valueERPId = driver.FindElement(valERPId).Text;
            return valueERPId;
        }

        public bool GetERPIdContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPId, 60);
            bool valueERPId = CustomFunctions.IsElementPresent(driver, valERPId);
            return valueERPId;
        }

        public string GetERPProjectId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectId, 60);
            string valueERPProjectId = driver.FindElement(valERPProjectId).Text;
            return valueERPProjectId;
        }

        public string GetERPProjectIdICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectIdICO, 60);
            string valueERPProjectId = driver.FindElement(valERPProjectIdICO).Text;
            return valueERPProjectId;
        }

        public string GetERPLastIntegrationResponseDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationResponseDate, 60);
            string valueERPLastIntegrationResponseDate = driver.FindElement(valERPLastIntegrationResponseDate).Text;
            return valueERPLastIntegrationResponseDate;
        }
        public string GetERPLastIntegrationResponseDateICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntRespDateICO, 60);
            string valueERPLastIntegrationResponseDate = driver.FindElement(valERPLastIntRespDateICO).Text;
            return valueERPLastIntegrationResponseDate;
        }



        public string GetERPLastIntegrationStatus()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationStatus, 120);
            string valueERPLastIntegrationStatus = driver.FindElement(valERPLastIntegrationStatus).Text;
            return valueERPLastIntegrationStatus;
        }

        public string GetERPLastIntegrationStatusICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationStatusICO, 60);
            string valueERPLastIntegrationStatus = driver.FindElement(valERPLastIntegrationStatusICO).Text;
            return valueERPLastIntegrationStatus;
        }

        public string GetBillTo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillTo, 60);
            string valueBillTo = driver.FindElement(valBillTo).Text;
            return valueBillTo;
        }

        public string GetBillToICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillToICO, 60);
            string valueBillTo = driver.FindElement(valBillToICO).Text;
            return valueBillTo;
        }

        public void ClickClientLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valBillToICO, 180);
            driver.FindElement(valBillToICO).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 2);
                driver.Navigate().Refresh();
            }
            else if (row.Equals(3))
            {
                Thread.Sleep(8000);
               CustomFunctions.SwitchToWindow(driver, 4);
                driver.Navigate().Refresh();
            }
            else{

                CustomFunctions.SwitchToWindow(driver, 6);
                driver.Navigate().Refresh();
            }
        }

        public void ClickClientLinks(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valBillToICO, 180);
            driver.FindElement(valBillToICO).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 2);
                driver.Navigate().Refresh();
            }
            else if (row.Equals(3))
            {
                CustomFunctions.SwitchToWindow(driver, 4);
                driver.Navigate().Refresh();
            }
            else
            {

                CustomFunctions.SwitchToWindow(driver, 8);
                driver.Navigate().Refresh();
            }
        }

        //Get Billing contact
        public string GetBillingContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillingContact, 60);
            string valueBillingContact = driver.FindElement(valBillingContact).Text;
            return valueBillingContact;
        }

        public string GetBillingContactICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillingContactICO, 60);
            string valueBillingContact = driver.FindElement(valBillingContactICO).Text;
            return valueBillingContact;
        }

        public string GetBillToCustomerId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToCustomerId, 60);
            string valueERPBillToCustomerId = driver.FindElement(valERPBillToCustomerId).Text;
            return valueERPBillToCustomerId;
        }
        public string GetBillToCustomerIdICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToCustomerIdICO, 60);
            string valueERPBillToCustomerId = driver.FindElement(valERPBillToCustomerIdICO).Text;
            return valueERPBillToCustomerId;
        }

        public string GetERPBillToAddressId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToAddressId, 60);
            string valueERPBillToAddressId = driver.FindElement(valERPBillToAddressId).Text;
            return valueERPBillToAddressId;
        }

        public string GetERPBillToAddressIdICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToAddressIdICO, 60);
            string valueERPBillToAddressId = driver.FindElement(valERPBillToAddressIdICO).Text;
            return valueERPBillToAddressId;
        }

        public string GetERPBillToContactId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToContactId, 60);
            string valueERPBillToContactId = driver.FindElement(valERPBillToContactId).Text;
            return valueERPBillToContactId;
        }

        public string GetERPBillToContactIdICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToContactIdICO, 60);
            string valueERPBillToContactId = driver.FindElement(valERPBillToContactIdICO).Text;
            return valueERPBillToContactId;
        }

        public string GetShipTo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valShipTo, 60);
            string valueShipTo = driver.FindElement(valShipTo).Text;
            return valueShipTo;
        }

        public string GetShipToICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valShipToICO, 60);
            string valueShipTo = driver.FindElement(valShipToICO).Text;
            return valueShipTo;
        }

        public string GetERPShipToCustomerId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToCustomerId, 60);
            string valueERPShipToCustomerId = driver.FindElement(valERPShipToCustomerId).Text;
            return valueERPShipToCustomerId;
        }

        public string GetERPShipToCustomerIdICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToCustomerIdICO, 60);
            string valueERPShipToCustomerId = driver.FindElement(valERPShipToCustomerIdICO).Text;
            return valueERPShipToCustomerId;
        }


        public string GetERPShipToAddressId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToAddressId, 60);
            string valueERPShipToAddressId = driver.FindElement(valERPShipToAddressId).Text;
            return valueERPShipToAddressId;
        }

        public string GetERPShipToAddressIdICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToAddressIdICO, 60);
            string valueERPShipToAddressId = driver.FindElement(valERPShipToAddressIdICO).Text;
            return valueERPShipToAddressId;
        }

        public string GetERPShipToAccountNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToAccountNumber, 60);
            string valueERPShipToAccountNumber = driver.FindElement(valERPShipToAccountNumber).Text;
            return valueERPShipToAccountNumber;
        }

        public string GetERPShipToAccountNumberICO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToAccountNumberICO, 60);
            string valueERPShipToAccountNumber = driver.FindElement(valERPShipToAccountNumberICO).Text;
            return valueERPShipToAccountNumber;
        }

        public void UpdateERPSubmittedToSyncManually()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(lnkERPSubmittedToSyncDate).Click();
            driver.FindElement(btnSave).Click();
        }

    }
}