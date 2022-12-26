using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Engagement
{
    class CFEngagementSummaryPage : BaseClass
    {
        By lblEngagementDynamicsSection = By.XPath("//span[@title='EL / Engagement Dynamics']");
        By lblFeesActualAmount = By.XPath("//h2/span[contains(text(),'Fees')]");
        By lblTransactionFeeCalcActualAmount = By.XPath("//h2/span[contains(text(),'Transaction Fee Calc')]");
        By lblIncentiveStructureActualAmount = By.XPath("//h2/span[contains(text(),'Incentive')]");
        By lblTransactionActualAmount = By.XPath("//h2/span[contains(text(),'Transaction (Actual Amount)')]");
        By lblTotalsActualAmount = By.XPath("//h2/span[contains(text(),'Totals')]");

        By btnSave = By.XPath("//button[@type='submit']");
        By btnCancel = By.XPath("//button[@name='cancel']");

        //Fees (Actual Amount) section elements
        By btnEditFeeDetails = By.XPath("//button[@title='Edit: Retainer_Fees__c']");
        By chkRetainerFees = By.XPath("//input[@name='Is_Retainer_Fee_Creditable__c']");
        By txtRetainerFees = By.XPath("//input[@name='Retainer_Fees__c']");
        By valRetainerFees = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[1]");

        By chkCompletionOfCIM = By.XPath("//input[@name='Is_Completion_of_CIM_Creditable__c']");
        By txtCompletionOfCIM = By.XPath("//input[@name='Completion_Of_CIM__c']");
        By valCompletionOfCIM = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[2]");

        By chkFirstRoundBid = By.XPath("//input[@name='Is_First_Round_Bid_Creditable__c']");
        By txtFirstRoundBid = By.XPath("//input[@name='First_Round_Bid__c']");
        By valFirstRoundBid = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[3]");

        By chkSecondRoundBid = By.XPath("//input[@name='Is_Second_Round_Bid_Creditable__c']");
        By txtSecondRoundBid = By.XPath("//input[@name='Second_Round_Bid__c']");
        By valSecondRoundBid = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[4]");

        By chkLOI = By.XPath("//input[@name='Is_LOI_Creditable__c']");
        By txtLOI = By.XPath("//input[@name='LOI__c']");
        By valLOI = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[5]");

        By chkSignedAgreement = By.XPath("//input[@name='Is_Signed_Agreement_Creditable__c']");
        By txtSignedAgreement = By.XPath("//input[@name='Signed_Agreement__c']");
        By valSignedAgreement = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[6]");

        By btnEditOtherFee = By.XPath("//button[@title='Edit: Other_Fee_Type_01__c']");
        By txtOtherFeeType01 = By.XPath("//input[@name='Other_Fee_Type_01__c']");
        By txtOtherFee01 = By.XPath("//input[@name='Other_Fee_01__c']");
        By chkOtherFee01 = By.XPath("//input[@name='Is_Other_Fee_01_Creditable__c']");
        By valOtherFee01 = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[8]");

        By txtOtherFeeType02 = By.XPath("//input[@name='Other_Fee_Type_02__c']");
        By txtOtherFee02 = By.XPath("//input[@name='Other_Fee_02__c']");
        By chkOtherFee02 = By.XPath("//input[@name='Is_Other_Fee_02_Creditable__c']");
        By valOtherFee02 = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[10]");

        By iconFeesInfo = By.XPath("(//span[text()='Fees (Actual Amount)']/following::div/h2/c-hl-universal-pop-over/div/lightning-icon)[1]");
        By lblFeeHelpText1 = By.XPath("(//span[text()='Fees (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span");
        By lblFeeHelpText2 = By.XPath("(//span[text()='Fees (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span/following::p");

        //Transaction (Actual Amount) section elements
        By btnEditTransactionDetails = By.XPath("//button[@title='Edit: Transaction_Fee_Type__c']");
        By dropdownTransactionType = By.XPath("//label[text()='Transaction Fee Type']/following::*[@name='Transaction_Fee_Type__c']");
        By txtTransactionFee = By.XPath("//input[@name='Transaction_Fee__c']");
        By valTransactionFee = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[12]");

        By iconTransactionFeeInfo = By.XPath("(//span[text()='Transaction Fee']/following::div/button)[1]");
        By lblTransactionFeeHelpText = By.XPath("//div[@class='slds-popover__body']");

        //Transaction Fee Calc (Actual Amount) section elements
        By btnEditTransactionFeeCalcDetails = By.XPath("//button[@title='Edit: Transaction_Value_for_Fee_Calc__c']");
        By txtTransactionValueForFeeCalc = By.XPath("//input[@name='Transaction_Value_for_Fee_Calc__c']");
        By valTransactionValueForFeeCalc = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[13]");

        By iconTransactionValueForFeeCalcInfo = By.XPath("(//span[text()='Transaction Value for Fee Calc (Actual)']/following::div/button)[1]");
        By lblTransactionValueForFeeCalcHelpText = By.XPath("//div[@class='slds-popover__body']");

        //Incentive Structure (Actual Amount) section elements
        By btnEditIncentiveStructureDetails = By.XPath("//button[@title='Edit: First_Ratchet_Percent__c']");

        By txtFirstRatchetPercent = By.XPath("//input[@name='First_Ratchet_Percent__c']");
        By valFirstRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[1]");
        By txtFirstRatchetFromAmount = By.XPath("//input[@name='First_Ratchet_From_Amount__c']");
        By valFirstRatchetFromAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[1]");
        By txtFirstRatchetToAmount = By.XPath("//input[@name='First_Ratchet_To_Amount__c']");
        By valFirstRatchetToAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[2]");

        By txtSecondRatchetPercent = By.XPath("//input[@name='Second_Ratchet_Percent__c']");
        By valSecondRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[2]");
        By txtSecondRatchetFromAmount = By.XPath("//input[@name='Second_Ratchet_From_Amount__c']");
        By valSecondRatchetFromAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[3]");
        By txtSecondRatchetToAmount = By.XPath("//input[@name='Second_Ratchet_To_Amount__c']");
        By valSecondRatchetToAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[4]");

        By txtThirdRatchetPercent = By.XPath("//input[@name='Third_Ratchet_Percent__c']");
        By valThirdRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[3]");
        By txtThirdRatchetFromAmount = By.XPath("//input[@name='Third_Ratchet_From_Amount__c']");
        By valThirdRatchetFromAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[5]");
        By txtThirdRatchetToAmount = By.XPath("//input[@name='Third_Ratchet_To_Amount__c']");
        By valThirdRatchetToAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[6]");

        By txtFourthRatchetPercent = By.XPath("//input[@name='Fourth_Ratchet_Percent__c']");
        By valFourthRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[4]");
        By txtFourthRatchetFromAmount = By.XPath("//input[@name='Fourth_Ratchet_From_Amount__c']");
        By valFourthRatchetFromAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[7]");
        By txtFourthRatchetToAmount = By.XPath("//input[@name='Fourth_Ratchet_To_Amount__c']");
        By valFourthRatchetToAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[8]");

        By txtFinalRatchetPercent = By.XPath("//input[@name='Final_Ratchet_Percent__c']");
        By valFinalRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[5]");
        By txtFinalRatchetAmount = By.XPath("//input[@name='Final_Ratchet_Amount__c']");
        By valFinalRatchetAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[9]");

        By iconIncentiveStructureInfo = By.XPath("(//span[text()='Incentive Structure (Actual Amount)']/following::c-hl-universal-pop-over/div/lightning-icon)[1]");
        By lblIncentiveStructureHelpText1 = By.XPath("(//span[text()='Incentive Structure (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span");
        By lblIncentiveStructureHelpText2 = By.XPath("(//span[text()='Incentive Structure (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span/following::p");

        //Total (Actual Amount) section elements
        By lblTotalFee = By.XPath("(//span[contains(text(),'Total Fee')]/following::div/lightning-formatted-text)[1]");
        By lblTotalCredits = By.XPath("(//span[contains(text(),'Total Credits')]/following::div/lightning-formatted-text)[1]");
        By lblPaymentOnClosing = By.XPath("(//span[contains(text(),'Payment On Closing')]/following::div/lightning-formatted-text)[1]");

        By btnEditSubjectToContingentFees = By.XPath("//button[@title='Edit: Fee_Subject_To_Contingent_Fees__c']");
        By chkSubjectToContingentFee = By.XPath("//input[@name='Fee_Subject_To_Contingent_Fees__c']");

        By iconTotalsInfo = By.XPath("(//span[text()='Totals (Actual Amount)']/following::div/h2/c-hl-universal-pop-over/div/lightning-icon)[1]");
        By lblTotalsHelpText1 = By.XPath("(//span[text()='Totals (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span");
        By lblTotalsHelpText2 = By.XPath("(//span[text()='Totals (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span/following::p");

        By iconTotalCreditsInfo = By.XPath("//span[text()='Total Credits']/following::div/button");
        By lblTotalCreditsHelpText = By.XPath("//div[@class='slds-popover__body']");
        By iconFeeSubjectToContingentFeesInfo = By.XPath("//span[text()='Fee Subject To Contingent Fees']/following::div/button");
        By lblFeeSubjectToContingentFeesHelpText = By.XPath("//*[@class='slds-popover slds-popover_tooltip slds-nubbin_bottom-left slds-rise-from-ground']/div");

        //Error Message elements
        By lblHeaderErrorMsg = By.XPath("//div[@class='slds-notify__content']/h2");
        By lblFirstRatchetFromAmtErrMsg = By.XPath("//label[text()='First Ratchet From Amount']/following::div[2]");
        By lblFirstRatchetToAmtErrMsg = By.XPath("//label[text()='First Ratchet To Amount']/following::div[2]");
        By lblSecondRatchetFromAmtErrMsg = By.XPath("//label[text()='Second Ratchet From Amount']/following::div[2]");
        By lblSecondRatchetToAmtErrMsg = By.XPath("//label[text()='Second Ratchet To Amount']/following::div[2]");
        By lblThirdRatchetFromAmtErrMsg = By.XPath("//label[text()='Third Ratchet From Amount']/following::div[2]");
        By lblThirdRatchetToAmtErrMsg = By.XPath("//label[text()='Third Ratchet To Amount']/following::div[2]");
        By lblFourthRatchetFromAmtErrMsg = By.XPath("//label[text()='Fourth Ratchet From Amount']/following::div[2]");
        By lblFourthRatchetToAmtErrMsg = By.XPath("//label[text()='Fourth Ratchet To Amount']/following::div[2]");
        By lblFinalRatchetAmtErrMsg = By.XPath("//label[text()='Final Ratchet Amount']/following::div[2]");

        //SF Lightening elements
        By btnSearchBox = By.XPath("//button[@aria-label='Search']");
        By txtSearchBox = By.XPath("//label[text()='Search Opportunities and more']/following::div/input");
        By btnAdditionalTabs = By.XPath("//button[@class='slds-button slds-button_icon-border-filled']");
        By linkCFEngSummary = By.XPath("//span[text()='Engagement Summary (CF)']");

        public void ClickEngagementDynamicsSection()
        {
            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(lblEngagementDynamicsSection)).Perform();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblEngagementDynamicsSection, 120);
            driver.FindElement(lblEngagementDynamicsSection).Click();
            Thread.Sleep(10000);
        }

        public bool VerifySectionsExistsUnderEngagementDynamicsSection()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(lblFeesActualAmount)).Perform();

            bool result = false;
            if(driver.FindElement(lblFeesActualAmount).Displayed && driver.FindElement(lblTransactionFeeCalcActualAmount).Displayed && driver.FindElement(lblIncentiveStructureActualAmount).Displayed && driver.FindElement(lblTransactionActualAmount).Displayed && driver.FindElement(lblTotalsActualAmount).Displayed)
            {
                result = true;
            }
            return result;
        }

        public string GetFeeSubjectToContingentFeesHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconFeeSubjectToContingentFeesInfo)).Perform();

            CustomFunctions.MouseOver(driver, iconFeeSubjectToContingentFeesInfo, 60);
            Thread.Sleep(2000);
            string result = driver.FindElement(lblFeeSubjectToContingentFeesHelpText).Text;
            return result;
        }

        public string GetTotalCreditsHelpText()
        {
            CustomFunctions.MouseOver(driver, iconTotalCreditsInfo, 60);
            Thread.Sleep(2000);
            string result = driver.FindElement(lblTotalCreditsHelpText).Text;
            return result;
        }

        public string GetTransactionValueForFeeCalcHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconTransactionValueForFeeCalcInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconTransactionValueForFeeCalcInfo, 60);
            Thread.Sleep(2000);
            string result = driver.FindElement(lblTransactionValueForFeeCalcHelpText).Text;
            return result;
        }

        public string GetTransactionFeeHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconTransactionFeeInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconTransactionFeeInfo, 60);
            Thread.Sleep(2000);
            string result = driver.FindElement(lblTransactionFeeHelpText).Text;
            return result;
        }

        public string GetTotalsHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconTotalsInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconTotalsInfo, 60);
            Thread.Sleep(2000);
            string result1 = driver.FindElement(lblTotalsHelpText1).Text;
            string result2 = driver.FindElement(lblTotalsHelpText2).Text;
            string result = result1 + " " + result2;
            return result;
        }

        public string GetIncentiveStructureHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconIncentiveStructureInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconIncentiveStructureInfo, 60);
            Thread.Sleep(2000);
            string result1 = driver.FindElement(lblIncentiveStructureHelpText1).Text;
            string result2 = driver.FindElement(lblIncentiveStructureHelpText2).Text;
            string result = result1 + " " + result2;
            return result;
        }

        public string GetFeesHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconFeesInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconFeesInfo, 60);
            Thread.Sleep(2000);
            string result1 = driver.FindElement(lblFeeHelpText1).Text;
            string result2 = driver.FindElement(lblFeeHelpText2).Text;
            string result = result1 + " " + result2;
            return result;
        }

        public void EnterFeesUnderFeeSection(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(2000);

            driver.FindElement(btnEditFeeDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtRetainerFees).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 1));
            driver.FindElement(txtCompletionOfCIM).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 2));
            driver.FindElement(txtFirstRoundBid).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 3));
            driver.FindElement(txtSecondRoundBid).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 4));
            driver.FindElement(txtLOI).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 5));
            driver.FindElement(txtSignedAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            driver.FindElement(btnEditOtherFee).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtOtherFee01).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 7));
            driver.FindElement(txtOtherFee02).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 8));
            Thread.Sleep(2000);

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);
        }

        public bool VerifyCalculationForFeeSectionWhenCrediableAreUnchecked()
        {
            bool result = false;

            string retFees = driver.FindElement(valRetainerFees).Text;
            string compOfCIM = driver.FindElement(valCompletionOfCIM).Text;
            string firstRdBid = driver.FindElement(valFirstRoundBid).Text;
            string secondRdBid = driver.FindElement(valSecondRoundBid).Text;
            string loi = driver.FindElement(valLOI).Text;
            string sgAgreement = driver.FindElement(valSignedAgreement).Text;
            string otherFee1 = driver.FindElement(valOtherFee01).Text;
            string otherFee2 = driver.FindElement(valOtherFee02).Text;

            double retainerFees = Convert.ToDouble(retFees.Split(' ')[1].Trim());
            double completionOfCIM = Convert.ToDouble(compOfCIM.Split(' ')[1].Trim());
            double firstRoundBid = Convert.ToDouble(firstRdBid.Split(' ')[1].Trim());
            double secondRoundBid = Convert.ToDouble(secondRdBid.Split(' ')[1].Trim());
            double lOI = Convert.ToDouble(loi.Split(' ')[1].Trim());
            double signedAgreement = Convert.ToDouble(sgAgreement.Split(' ')[1].Trim());
            double otherFee01 = Convert.ToDouble(otherFee1.Split(' ')[1].Trim());
            double otherFee02 = Convert.ToDouble(otherFee2.Split(' ')[1].Trim());

            double cal = retainerFees + completionOfCIM + firstRoundBid + secondRoundBid + lOI + signedAgreement + otherFee01 + otherFee02;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            string totalFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFee = Convert.ToDouble(totalFee.Split(' ')[1].Trim());

            string totalCredits = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCredits = Convert.ToDouble(totalCredits.Split(' ')[1].Trim());

            string totalPaymentOnClosing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosing = Convert.ToDouble(totalPaymentOnClosing.Split(' ')[1].Trim());

            if (actualTotalFee == cal && actualTotalCredits == 0 && actualTotalPaymentOnClosing == actualTotalFee)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyCalculationForFeeSectionWhenCrediableAreChecked()
        {
            bool result = false;

            string retFees = driver.FindElement(valRetainerFees).Text;
            string compOfCIM = driver.FindElement(valCompletionOfCIM).Text;
            string firstRdBid = driver.FindElement(valFirstRoundBid).Text;
            string secondRdBid = driver.FindElement(valSecondRoundBid).Text;
            string loi = driver.FindElement(valLOI).Text;
            string sgAgreement = driver.FindElement(valSignedAgreement).Text;
            string otherFee1 = driver.FindElement(valOtherFee01).Text;
            string otherFee2 = driver.FindElement(valOtherFee02).Text;

            double retainerFees = Convert.ToDouble(retFees.Split(' ')[1].Trim());
            double completionOfCIM = Convert.ToDouble(compOfCIM.Split(' ')[1].Trim());
            double firstRoundBid = Convert.ToDouble(firstRdBid.Split(' ')[1].Trim());
            double secondRoundBid = Convert.ToDouble(secondRdBid.Split(' ')[1].Trim());
            double lOI = Convert.ToDouble(loi.Split(' ')[1].Trim());
            double signedAgreement = Convert.ToDouble(sgAgreement.Split(' ')[1].Trim());
            double otherFee01 = Convert.ToDouble(otherFee1.Split(' ')[1].Trim());
            double otherFee02 = Convert.ToDouble(otherFee2.Split(' ')[1].Trim());

            double cal = retainerFees + completionOfCIM + firstRoundBid + secondRoundBid + lOI + signedAgreement + otherFee01 + otherFee02;

            driver.FindElement(btnEditFeeDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(chkRetainerFees).Click();
            driver.FindElement(chkCompletionOfCIM).Click();
            driver.FindElement(chkFirstRoundBid).Click();
            driver.FindElement(chkSecondRoundBid).Click();
            driver.FindElement(chkLOI).Click();
            driver.FindElement(chkSignedAgreement).Click();

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            driver.FindElement(btnEditOtherFee).Click();
            Thread.Sleep(2000);

            driver.FindElement(chkOtherFee01).Click();
            driver.FindElement(chkOtherFee02).Click();

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            string totalFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFee = Convert.ToDouble(totalFee.Split(' ')[1].Trim());

            string totalCredits = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCredits = Convert.ToDouble(totalCredits.Split(' ')[1].Trim());

            string totalPaymentOnClosing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosing = Convert.ToDouble(totalPaymentOnClosing.Split(' ')[1].Trim());

            if (actualTotalFee == 0 && actualTotalCredits == cal &&  actualTotalPaymentOnClosing == -actualTotalCredits)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyCalculationForTransactionFeeCalcWhenIncentiveStructureIsEmpty(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string totalFeeBeforeEditing = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeBeforeEditing = Convert.ToDouble(totalFeeBeforeEditing.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingBeforeEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingBeforeEditing = Convert.ToDouble(totalPaymentOnClosingBeforeEditing.Split(' ')[1].Trim());

            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(btnEditTransactionFeeCalcDetails)).Perform();
            Thread.Sleep(2000);

            driver.FindElement(btnEditTransactionFeeCalcDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtTransactionValueForFeeCalc).Clear();
            driver.FindElement(txtTransactionValueForFeeCalc).SendKeys(ReadExcelData.ReadData(excelPath, "TransactionFeeCalc", 1));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            string totalFeeAfterEditingTransactionFeeCalc = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (actualTotalFeeBeforeEditing==actualTotalFeeAfterEditing && actualTotalCreditsBeforeEditing==actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyCalculationForTransactionSectionWithDifferentFeeTypes(string file, string transFeeType, int row, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            string totalFeeBeforeEditing = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeBeforeEditing = Convert.ToDouble(totalFeeBeforeEditing.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditTransactionDetails).Click();
            Thread.Sleep(2000);
            driver.FindElement(dropdownTransactionType).SendKeys(transFeeType);
            driver.FindElement(dropdownTransactionType).SendKeys(Keys.Enter);
            driver.FindElement(txtTransactionFee).Clear();
            driver.FindElement(txtTransactionFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Transaction(Actual Amount)", row, 2));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string transFee = driver.FindElement(valTransactionFee).Text;
            double actualTransFee = Convert.ToDouble(transFee.Split(' ')[1].Trim());

            if(transFeeType == "Minimum Fee")
            {
                double actualTotalFee = actualTotalFeeBeforeEditing + actualTransFee;

                string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
                double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

                string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
                double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

                string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
                double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

                if (actualTotalFeeAfterEditing==actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + actualTotalFeeAfterEditing)
                {
                    result = true;
                }
            }
            else if(transFeeType == "Flat Fee")
            {
                double actualTotalFee = actualTransFee;

                string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
                double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

                string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
                double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

                string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
                double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + actualTotalFeeAfterEditing)
                {
                    result = true;
                }
            }

            return result;
        }

        public double GetactualTotalFeeBeforeEditing()
        {
            Thread.Sleep(5000);
            string totalFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFee = Convert.ToDouble(totalFee.Split(' ')[1].Trim());

            return actualTotalFee;
        }

        public double GetactualPaymentClosingFeeBeforeEditing()
        {
            Thread.Sleep(5000);
            string totalPaymentOnClosingFee = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingFee = Convert.ToDouble(totalPaymentOnClosingFee.Split(' ')[1].Trim());

            return actualTotalPaymentOnClosingFee;
        }

        public bool VerifyCalculationForFirstRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFirstRatchetPercent).Clear();
            driver.FindElement(txtFirstRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtFirstRatchetFromAmount).Clear();
            driver.FindElement(txtFirstRatchetFromAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 2));
            driver.FindElement(txtFirstRatchetToAmount).Clear();
            driver.FindElement(txtFirstRatchetToAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 3));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string firstPercentage = driver.FindElement(valFirstRatchetPercent).Text;
            double firstPercentValue = Convert.ToDouble(firstPercentage.Split('%')[0].Trim());
            string firstRacFromAmount = driver.FindElement(valFirstRatchetFromAmount).Text;
            double firstRacFromAmountValue = Convert.ToDouble(firstRacFromAmount.Split(' ')[1].Trim());
            string firstRacToAmount = driver.FindElement(valFirstRatchetToAmount).Text;
            double firstRacToAmountValue = Convert.ToDouble(firstRacToAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (firstRacToAmountValue <= actualtransactionFeeCalc)
            {
                double firstRatchetCalc= (firstRacToAmountValue - firstRacFromAmountValue) * firstPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + firstRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + firstRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double firstRatchetCalc = (actualtransactionFeeCalc - firstRacFromAmountValue) * firstPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + firstRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + firstRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyCalculationForSecondRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtSecondRatchetPercent).Clear();
            driver.FindElement(txtSecondRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtSecondRatchetFromAmount).Clear();
            driver.FindElement(txtSecondRatchetFromAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 4));
            driver.FindElement(txtSecondRatchetToAmount).Clear();
            driver.FindElement(txtSecondRatchetToAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 5));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string secondPercentage = driver.FindElement(valSecondRatchetPercent).Text;
            double secondPercentValue = Convert.ToDouble(secondPercentage.Split('%')[0].Trim());
            string secondRacFromAmount = driver.FindElement(valSecondRatchetFromAmount).Text;
            double secondRacFromAmountValue = Convert.ToDouble(secondRacFromAmount.Split(' ')[1].Trim());
            string secondRacToAmount = driver.FindElement(valSecondRatchetToAmount).Text;
            double secondRacToAmountValue = Convert.ToDouble(secondRacToAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (secondRacToAmountValue <= actualtransactionFeeCalc)
            {
                double secondRatchetCalc = (secondRacToAmountValue - secondRacFromAmountValue) * secondPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + secondRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + secondRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double secondRatchetCalc = (actualtransactionFeeCalc - secondRacFromAmountValue) * secondPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + secondRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + secondRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyCalculationForThirdRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtThirdRatchetPercent).Clear();
            driver.FindElement(txtThirdRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtThirdRatchetFromAmount).Clear();
            driver.FindElement(txtThirdRatchetFromAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 6));
            driver.FindElement(txtThirdRatchetToAmount).Clear();
            driver.FindElement(txtThirdRatchetToAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 7));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string thirdPercentage = driver.FindElement(valThirdRatchetPercent).Text;
            double thirdPercentValue = Convert.ToDouble(thirdPercentage.Split('%')[0].Trim());
            string thirdRacFromAmount = driver.FindElement(valThirdRatchetFromAmount).Text;
            double thirdRacFromAmountValue = Convert.ToDouble(thirdRacFromAmount.Split(' ')[1].Trim());
            string thirdRacToAmount = driver.FindElement(valThirdRatchetToAmount).Text;
            double thirdRacToAmountValue = Convert.ToDouble(thirdRacToAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (thirdRacToAmountValue <= actualtransactionFeeCalc)
            {
                double thirdRatchetCalc = (thirdRacToAmountValue - thirdRacFromAmountValue) * thirdPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + thirdRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + thirdRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double thirdRatchetCalc = (actualtransactionFeeCalc - thirdRacFromAmountValue) * thirdPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + thirdRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + thirdRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyCalculationForFourthRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFourthRatchetPercent).Clear();
            driver.FindElement(txtFourthRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtFourthRatchetFromAmount).Clear();
            driver.FindElement(txtFourthRatchetFromAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 8));
            driver.FindElement(txtFourthRatchetToAmount).Clear();
            driver.FindElement(txtFourthRatchetToAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 9));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string fourthPercentage = driver.FindElement(valFourthRatchetPercent).Text;
            double fourthPercentValue = Convert.ToDouble(fourthPercentage.Split('%')[0].Trim());
            string fourthRacFromAmount = driver.FindElement(valFourthRatchetFromAmount).Text;
            double fourthRacFromAmountValue = Convert.ToDouble(fourthRacFromAmount.Split(' ')[1].Trim());
            string fourthRacToAmount = driver.FindElement(valFourthRatchetToAmount).Text;
            double fourthRacToAmountValue = Convert.ToDouble(fourthRacToAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (fourthRacToAmountValue <= actualtransactionFeeCalc)
            {
                double fourthRatchetCalc = (fourthRacToAmountValue - fourthRacFromAmountValue) * fourthPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + fourthRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + fourthRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double fourthRatchetCalc = (actualtransactionFeeCalc - fourthRacFromAmountValue) * fourthPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + fourthRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + fourthRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyCalculationForFinalRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFinalRatchetPercent).Clear();
            driver.FindElement(txtFinalRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtFinalRatchetAmount).Clear();
            driver.FindElement(txtFinalRatchetAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 10));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string finalPercentage = driver.FindElement(valFinalRatchetPercent).Text;
            double finalPercentValue = Convert.ToDouble(finalPercentage.Split('%')[0].Trim());
            string finalRacAmount = driver.FindElement(valFinalRatchetAmount).Text;
            double finalRacAmountValue = Convert.ToDouble(finalRacAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());


            if (finalRacAmountValue <= actualtransactionFeeCalc)
            {
                double finalRatchetCalc = (actualtransactionFeeCalc - finalRacAmountValue) * finalPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + finalRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + finalRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double finalRatchetCalc = 0;
                if (actualTotalFeeAfterEditing == actualTotalFeeBeforeEditing && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + finalRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyValidationRulesForBlankValuesOfFirstRatchetAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFirstRatchetPercent).Clear();
            driver.FindElement(txtFirstRatchetPercent).SendKeys(ReadExcelData.ReadData(excelPath, "IncentiveStructure", 1));
            
            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string firstRatchetFromErrMsg = driver.FindElement(lblFirstRatchetFromAmtErrMsg).Text;
            string firstRatchetToErrMsg = driver.FindElement(lblFirstRatchetToAmtErrMsg).Text;

            if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && firstRatchetFromErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 2) && firstRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 3))
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesForBlankValuesOfSecondRatchetAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtSecondRatchetPercent).Clear();
            driver.FindElement(txtSecondRatchetPercent).SendKeys(ReadExcelData.ReadData(excelPath, "IncentiveStructure", 1));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string secondRatchetFromErrMsg = driver.FindElement(lblSecondRatchetFromAmtErrMsg).Text;
            string secondRatchetToErrMsg = driver.FindElement(lblSecondRatchetToAmtErrMsg).Text;

            if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && secondRatchetFromErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 2) && secondRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 3))
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesForBlankValuesOfThirdRatchetAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtThirdRatchetPercent).Clear();
            driver.FindElement(txtThirdRatchetPercent).SendKeys(ReadExcelData.ReadData(excelPath, "IncentiveStructure", 1));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string thirdRatchetFromErrMsg = driver.FindElement(lblThirdRatchetFromAmtErrMsg).Text;
            string thirdRatchetToErrMsg = driver.FindElement(lblThirdRatchetToAmtErrMsg).Text;

            if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && thirdRatchetFromErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 2) && thirdRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 3))
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesForBlankValuesOfFourthRatchetAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFourthRatchetPercent).Clear();
            driver.FindElement(txtFourthRatchetPercent).SendKeys(ReadExcelData.ReadData(excelPath, "IncentiveStructure", 1));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string fourthRatchetFromErrMsg = driver.FindElement(lblFourthRatchetFromAmtErrMsg).Text;
            string fourthRatchetToErrMsg = driver.FindElement(lblFourthRatchetToAmtErrMsg).Text;

            if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && fourthRatchetFromErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 2) && fourthRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 3))
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesWhenFirstRatchetFromAmountIsGreaterThanFirstRatchetToAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFirstRatchetFromAmount).Clear();
            driver.FindElement(txtFirstRatchetFromAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 5));
            driver.FindElement(txtFirstRatchetToAmount).Clear();
            driver.FindElement(txtFirstRatchetToAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string firstRacFromAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 5);
            double firstRacFromAmountValue = Convert.ToDouble(firstRacFromAmount);
            string firstRacToAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 6);
            double firstRacToAmountValue = Convert.ToDouble(firstRacToAmount);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string firstRatchetToErrMsg = driver.FindElement(lblFirstRatchetToAmtErrMsg).Text;

            if(firstRacFromAmountValue > firstRacToAmountValue)
            {
                if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && firstRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 4))
                {
                    result = true;
                }
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesWhenSecondRatchetFromAmountIsGreaterThanSecondRatchetToAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtSecondRatchetFromAmount).Clear();
            driver.FindElement(txtSecondRatchetFromAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 5));
            driver.FindElement(txtSecondRatchetToAmount).Clear();
            driver.FindElement(txtSecondRatchetToAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string secondRacFromAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 5);
            double secondRacFromAmountValue = Convert.ToDouble(secondRacFromAmount);
            string secondRacToAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 6);
            double secondRacToAmountValue = Convert.ToDouble(secondRacToAmount);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string secondRatchetToErrMsg = driver.FindElement(lblSecondRatchetToAmtErrMsg).Text;

            if (secondRacFromAmountValue > secondRacToAmountValue)
            {
                if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && secondRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 4))
                {
                    result = true;
                }
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesWhenThirdRatchetFromAmountIsGreaterThanThirdRatchetToAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtThirdRatchetFromAmount).Clear();
            driver.FindElement(txtThirdRatchetFromAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 5));
            driver.FindElement(txtThirdRatchetToAmount).Clear();
            driver.FindElement(txtThirdRatchetToAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string thirdRacFromAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 5);
            double thirdRacFromAmountValue = Convert.ToDouble(thirdRacFromAmount);
            string thirdRacToAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 6);
            double thirdRacToAmountValue = Convert.ToDouble(thirdRacToAmount);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string thirdRatchetToErrMsg = driver.FindElement(lblThirdRatchetToAmtErrMsg).Text;

            if (thirdRacFromAmountValue > thirdRacToAmountValue)
            {
                if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && thirdRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 4))
                {
                    result = true;
                }
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesWhenFourthRatchetFromAmountIsGreaterThanFourthRatchetToAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFourthRatchetFromAmount).Clear();
            driver.FindElement(txtFourthRatchetFromAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 5));
            driver.FindElement(txtFourthRatchetToAmount).Clear();
            driver.FindElement(txtFourthRatchetToAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string fourthRacFromAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 5);
            double fourthRacFromAmountValue = Convert.ToDouble(fourthRacFromAmount);
            string fourthRacToAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 6);
            double fourthRacToAmountValue = Convert.ToDouble(fourthRacToAmount);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string fourthRatchetToErrMsg = driver.FindElement(lblFourthRatchetToAmtErrMsg).Text;

            if (fourthRacFromAmountValue > fourthRacToAmountValue)
            {
                if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && fourthRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 4))
                {
                    result = true;
                }
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public void NavigateToCFEngSummaryPage(string engName)
        {
            Thread.Sleep(10000);
            driver.FindElement(btnSearchBox).Click();
            Thread.Sleep(2000);
            driver.FindElement(txtSearchBox).SendKeys(engName);
            driver.FindElement(txtSearchBox).SendKeys(Keys.Enter);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"(//a[@title='{engName}'])[2]")).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnAdditionalTabs).Click();
            driver.FindElement(linkCFEngSummary).Click();
            Thread.Sleep(3000);
        }

    }
}


