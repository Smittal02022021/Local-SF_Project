using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.Pages
{
    class AddOpportunityPageWithJson : BaseClass
    {
        By txtOpportunityName = By.Id("Name");
        By txtClient = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D7zoC']");
        By txtSubject = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D80OZ']");
        By comboJobType = By.CssSelector("select[id*= 'hWW']");
        By comboIndustryGroup = By.CssSelector("select[name*= 'VT3']");
        By comboSector = By.CssSelector("select[name*='PI']");
        By comboAdditionalClient = By.CssSelector("select[name*='FmBza']");
        By comboAdditionalSubject = By.CssSelector("select[name*='Bzb']");
        By comboReferralType = By.CssSelector("select[name*='uS']");
        By comboNonPublicInfo = By.CssSelector("select[name*='Bzn']");
        By comboBeneficialOwner = By.CssSelector("select[name*='HERR2']");
        By comboPrimaryOffice = By.CssSelector("select[name*='VIA']");
        By txtLegalEntity = By.XPath("//span[@class='lookupInput']/input[@name='CF00N5A00000M0eg5']");
        By comboDisclosureStatus = By.CssSelector("select[name*='HaP']");
        By btnSave = By.CssSelector("input[value=' Save ']");

        string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        public string AddOpportunities(string file)
        {
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);

            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 40);
            string valOpportunity = CustomFunctions.RandomValue();

            driver.FindElement(txtOpportunityName).SendKeys(valOpportunity);
            driver.FindElement(txtClient).SendKeys(ReadJSONData.data.addOpportunityDetails.client);            
            driver.FindElement(txtSubject).SendKeys(ReadJSONData.data.addOpportunityDetails.subject);
           
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
            driver.FindElement(comboJobType).SendKeys(ReadJSONData.data.addOpportunityDetails.jobType);
            driver.FindElement(comboIndustryGroup).SendKeys(ReadJSONData.data.addOpportunityDetails.industryGroup);
            driver.FindElement(comboSector).SendKeys(ReadJSONData.data.addOpportunityDetails.sector);

            //Additional Client/Subject
            driver.FindElement(comboAdditionalClient).SendKeys(ReadJSONData.data.addOpportunityDetails.additionalClient);
            driver.FindElement(comboAdditionalSubject).SendKeys(ReadJSONData.data.addOpportunityDetails.additionalSubject);

            //Referral Information
            driver.FindElement(comboReferralType).SendKeys(ReadJSONData.data.addOpportunityDetails.referralType);

            //Compliance Section
            driver.FindElement(comboNonPublicInfo).SendKeys(ReadJSONData.data.addOpportunityDetails.nonPublicInfo);
            driver.FindElement(comboBeneficialOwner).SendKeys(ReadJSONData.data.addOpportunityDetails.beneficialOwner);

            //Administration Section
            driver.FindElement(comboPrimaryOffice).SendKeys(ReadJSONData.data.addOpportunityDetails.primaryOffice);
            driver.FindElement(txtLegalEntity).SendKeys(ReadJSONData.data.addOpportunityDetails.legalEntity);
            driver.FindElement(comboDisclosureStatus).SendKeys(ReadJSONData.data.addOpportunityDetails.disclosureStatus);

            //Click Save button
            driver.FindElement(btnSave).Click();
            return valOpportunity;
        }

    }
}

