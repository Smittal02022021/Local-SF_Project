using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.Pages
{
    class AddOpportunityByAdmin : BaseClass
    {
        By txtOpportunityName = By.Id("Name");
        By txtClient = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D7zoC']");
        By txtSubject = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D80OZ']");
        By comboRecordType = By.CssSelector("select[name='00Ni000000D8hW2']");
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
        By txtRetainer = By.CssSelector("input[name*='TdF']");
        By txtMonthlyFee = By.CssSelector("input[name*='FmBzi']");
        By lnkPitchDateFAS = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(6) > td:nth-child(4) > span > span > a");
        By lnkPitchDate = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By lnkTrialExp = By.CssSelector("div:nth-child(15) > table > tbody > tr:nth-child(2) > td.dataCol.col02 > span > span > a");
        By txtContingentFee = By.CssSelector("input[name*='FkGE9']");
        By checkNBCApproved = By.CssSelector("input[name='00Ni000000FmBzh']");
        By lnkValuationDate = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(7) > td:nth-child(4) > span > span > a");
        By lnkDateEngaged = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(9) > td:nth-child(4) > span > span > a");
        By lnkDateEngagedCF = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By chkInternalTeamPrompt = By.CssSelector("input[name*='FnLTz']");
        By chkEngTeamAssembled = By.CssSelector("input[name*='FkmpJ']");
        By comboFairnessOpinion = By.CssSelector("select[name*='GbaZ7']");
        By comboClientOwnership = By.CssSelector("select[name*='M0d2T']");
        By comboSubjectOwnership = By.CssSelector("select[name*='M0d2U']");
        By comboTombstonePermission = By.CssSelector("select[name='00N5A00000GzTIZ']");
        By txtSICCode = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000G9CKu']");
        By txtReferralContact = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D80Oo']");
        By txtOppDesc = By.CssSelector("textarea[name*='D80Oy']");
        By comboConfAgreement = By.CssSelector("select[name*='D8war']");
        By lnkDateCASigned = By.CssSelector("div:nth-child(19) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By lnkDateCASignedFAS = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By lnkDateCAExpires = By.CssSelector("div:nth-child(19) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By lnkDateCAExpiresFAS = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By lnkOutcomeDate = By.CssSelector(" div:nth-child(21) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By lnkOutcomeDateFAS = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By comboOutcome = By.CssSelector("select[name*='D8hIa']");
        By txtMarketCap = By.CssSelector("input[name*='D80P4']");
        By lnkEstClosedDate = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(3) > td.dataCol.col02 > span > span > a");
        By txtFee = By.CssSelector("input[name*='FmBzg']");
        By lnkDateCASignedFR = By.CssSelector("div:nth-child(17) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By lnkDateCAExpiresFR = By.CssSelector("div:nth-child(17) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By lnkOutcomeDateFR = By.CssSelector(" div:nth-child(19) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By lnkDateEngagedFR = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(2) > td.dataCol.col02 > span > span > a");
        By txtTotalDebt = By.CssSelector("input[name*='DwfqW']");
        By txtTotalDebtHL = By.CssSelector("input[name*='4yQD']");
        By comboEMEAInitiatives = By.CssSelector("span>select[name*='MR']");
        By txtClientDesc = By.CssSelector("textarea[name*='4yQ7']");
        By chkDebtConfirmed = By.CssSelector("input[name*='4yQC']");
        By comboLegalAdvisorComp = By.CssSelector("span>select[name*='4yQA']");
        By comboLegalAdvisorHL = By.CssSelector("span>select[name*='4yQB']");
        By comboEUSecurities = By.CssSelector("span>select[name*='4yQ8']");
        By lnkEstimatedClosedDateFR = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(3) > td:nth-child(4) > span > span > a");
        By btnSave = By.CssSelector("input[value=' Save ']");

        //string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";
        public string AddOpportunities(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);

            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 40);
            string valOpportunity = CustomFunctions.RandomValue();

            driver.FindElement(txtOpportunityName).SendKeys(valOpportunity);
            //driver.FindElement(txtClient).SendKeys(ReadJSONData.data.addOpportunityDetails.client);            
            driver.FindElement(txtClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1));
            driver.FindElement(txtSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2));

            if (driver.FindElement(comboRecordType).Text.Contains("CF"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
                driver.FindElement(comboJobType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 3));
                driver.FindElement(lnkPitchDate).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(lnkTrialExp).Click();
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(lnkDateCASigned).Click();
                driver.FindElement(lnkDateCAExpires).Click();
                driver.FindElement(lnkOutcomeDate).Click();
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(lnkDateEngagedCF).Click();
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(checkNBCApproved).Click();
            }
            else if (driver.FindElement(comboRecordType).Text.Contains("FAS"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
                driver.FindElement(comboJobType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 26));
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(lnkDateEngaged).Click();
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                driver.FindElement(lnkOutcomeDateFAS).Click();
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
                driver.FindElement(comboJobType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 3));
                driver.FindElement(lnkPitchDate).Click();
                driver.FindElement(comboEMEAInitiatives).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 29));
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(lnkDateEngagedFR).Click();
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
            }

            driver.FindElement(comboIndustryGroup).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 4));
            driver.FindElement(comboSector).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 5));
            driver.FindElement(comboClientOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 18));
            driver.FindElement(comboSubjectOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 19));
            driver.FindElement(txtSICCode).SendKeys("9999");

            //Additional Client/Subject
            driver.FindElement(comboAdditionalClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 6));
            driver.FindElement(comboAdditionalSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 7));

            //Opportunity Description
            driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Referral Information
            driver.FindElement(comboReferralType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 8));
            driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));

            //Estimated Fees
            driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));

            //Restricted List/Compliance Section
            driver.FindElement(comboNonPublicInfo).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 9));
            driver.FindElement(comboBeneficialOwner).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 10));

            //Legal Matters section
            driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

            //Conflicts check section
            driver.FindElement(comboOutcome).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 24));

            //Administration Section
            driver.FindElement(comboPrimaryOffice).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 11));
            driver.FindElement(txtLegalEntity).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 12));
            driver.FindElement(comboDisclosureStatus).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 13));

            //System Administration section
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(chkEngTeamAssembled).Click();

            //Click Save button
            driver.FindElement(btnSave).Click();
            return valOpportunity;
        }
    }
}


