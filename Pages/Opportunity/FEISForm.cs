using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Opportunity
{
    class FEISForm : BaseClass
    {
        By OppName = By.CssSelector("span[id*='id44']");
        By clientComp = By.CssSelector("span[id*='id46']");
        By subjectComp = By.CssSelector("span[id*='id47']");
        By jobType = By.CssSelector("span[id*='id45']");
        By btnSubmit = By.CssSelector("input[id*='btnSubmit']");
        By errorList = By.CssSelector("span[id*='j_id18']>ul");
        By btnCancel = By.CssSelector("input[value='Cancel Submission']");
        By checkToggleTabs = By.Id("toggleTabs");
        By tabList = By.Id("tabsList");
        By txtAmountPaidOnDelivery = By.CssSelector("input[id*='id55']");
        By txtIncrementalFee = By.CssSelector("textarea[name*='id57']");
        By txtTranOverview = By.CssSelector("textarea[id*='descriptionOfTransaction']");
        By comboTransactionType = By.CssSelector("select[id*='TransactionType']");
        By comboLegalStructure = By.CssSelector("select[id*='LegalStructure']");
        By txtTransactionSize = By.CssSelector("input[id*='TransSize']");
        By comboFormOfConsideration = By.CssSelector("select[id*='FormConsider_unselected']> optgroup > option[value='0']");
        By btnFormRightArrow = By.CssSelector("img[id*='FormConsider_right_arrow']");
        By comboAffiliatedParties = By.CssSelector("select[id*='affiliatedParties']");
        By comboPubliclyDisclosed = By.CssSelector("select[id*='isPubliclyDisclosed']");
        By comboRelativeFairness = By.CssSelector("select[id*='fairnessRelativeFairness']");
        By comboFairnessOfTransaction = By.CssSelector("select[id*='fairnessFairnessOrTerms']");
        By comboFairnessConclusion = By.CssSelector("select[id*='fairnessMultipleConclusions']");
        By comboClientCommittee = By.CssSelector("select[id*='fairnessCommitteeOrTrustee']");
        By comboUnusualAttribute = By.CssSelector("select[id*='fairnessUnusualOpinion']");
        By comboRelationshipQuestion1 = By.CssSelector("select[id*='Conflicts3a']");
        By comboRelationshipQuestion2 = By.CssSelector("select[id*='Conflicts35a']");
        By comboRelationshipQuestion3 = By.CssSelector("select[id*='Conflicts4a']");
        By comboRelationshipQuestion4 = By.CssSelector("select[id*='Conflicts5a']");
        By comboOtherOpinion = By.CssSelector("select[id*='shareholderVote']");
        By comboSpecialCommittee = By.CssSelector("select[id*='id306']");
        By titleEmailPage = By.CssSelector("h2[class='mainTitle']");
        By valEmailOppName = By.CssSelector("body[id*='Body_rta_body'] > span:nth-child(9) > span");
        By btnCancelEmail = By.CssSelector("input[value='Cancel']");
        By btnReturntoOpp = By.CssSelector("input[value*='Return to Opportunity']");

        //string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        //Validate Opp Name
        public string ValidateOppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, OppName, 50);
            string valOpp = driver.FindElement(OppName).Text;
            return valOpp;
        }

        //Validate Client Name
        public string ValidateClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, clientComp);
            string valclient = driver.FindElement(clientComp).Text;
            return valclient;
        }
        //Validate Subject Name
        public string ValidateSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, subjectComp);
            string valSubject = driver.FindElement(subjectComp).Text;
            return valSubject;
        }
        //Validate JobType
        public string ValidateJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, jobType);
            string valJobType = driver.FindElement(jobType).Text;
            return valJobType;
        }

        //Fetch validations for mandatory fields
        public string GetFieldsValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmit, 60);
            driver.FindElement(txtTranOverview).Clear();
            driver.FindElement(btnSubmit).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, errorList, 90);
            string errorDetails = driver.FindElement(errorList).Text.Replace("\r\n", ", ").ToString();
            return errorDetails;
        }
        public void ClickCancelAndAcceptAlert()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
        }
        public string ClickToggleAndValidateTabs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkToggleTabs, 80);
            driver.FindElement(checkToggleTabs).Click();
            bool tabsPresent = driver.FindElement(tabList).Displayed;
            if (tabsPresent == true)
            {
                string lblTabslist = driver.FindElement(tabList).Text.Replace("\r\n", ", ").ToString();
                Console.WriteLine(lblTabslist);
                return lblTabslist;
            }
            else
            {
                return "Tabs are not displayed";
            }
        }

        public void EnterDetailsAndClickSubmit(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtAmountPaidOnDelivery, 90);
            driver.FindElement(txtTranOverview).Click();
            driver.SwitchTo().Alert().Accept();

            //General
            driver.FindElement(txtAmountPaidOnDelivery).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 2));
            driver.FindElement(txtIncrementalFee).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 3));

            //Background on Transaction
            driver.FindElement(txtTranOverview).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 4));
            driver.FindElement(comboTransactionType).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 5));
            driver.FindElement(comboLegalStructure).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 6));

            //Form and Amount of Consideration
            driver.FindElement(txtTransactionSize).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 7));
            driver.FindElement(comboFormOfConsideration).Click();
            driver.FindElement(btnFormRightArrow).Click();

            //Affiliated Parties Information
            driver.FindElement(comboAffiliatedParties).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 8));

            //Legal Review Criteria
            driver.FindElement(comboPubliclyDisclosed).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 9));
            driver.FindElement(comboRelativeFairness).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 10));
            driver.FindElement(comboFairnessOfTransaction).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 11));
            driver.FindElement(comboFairnessConclusion).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 12));
            driver.FindElement(comboClientCommittee).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 13));
            driver.FindElement(comboUnusualAttribute).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 14));

            //Relationship Questions
            driver.FindElement(comboRelationshipQuestion1).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 15));
            driver.FindElement(comboRelationshipQuestion2).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 16));
            driver.FindElement(comboRelationshipQuestion3).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 17));
            driver.FindElement(comboRelationshipQuestion4).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 18));

            //Other Opinion Information
            driver.FindElement(comboOtherOpinion).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 19));
            driver.FindElement(comboSpecialCommittee).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 20));

            driver.FindElement(btnSubmit).Click();
        }
        public string ValidateHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleEmailPage, 70);
            string title = driver.FindElement(titleEmailPage).Text;
            Console.WriteLine(title);
            return title;
        }
        public string GetOppName()
        {
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, valEmailOppName, 90);
            string emailSub = driver.FindElement(valEmailOppName).Text;
            Console.WriteLine(emailSub);
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnCancelEmail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 70);
            driver.FindElement(btnReturntoOpp).Click();
            return emailSub;
        }
    }
}




