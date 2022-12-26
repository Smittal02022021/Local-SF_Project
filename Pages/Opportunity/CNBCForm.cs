using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Opportunity
{
    class CNBCForm : BaseClass
    {
        By OppName = By.CssSelector("span[id*='id40:j_id41']");
        By clientComp = By.CssSelector("span[id*='id45']");
        By subjectComp = By.CssSelector("span[id*='id48']");
        By jobType = By.CssSelector("span[id*='id61']");
        By btnSubmit = By.CssSelector("input[id*='j_id33:btnSubmitForReview']");
        By errorList = By.CssSelector("#j_id0\\:CNBCForm\\:j_id2\\:j_id3\\:j_id4\\:0\\:j_id5\\:j_id6\\:j_id18 > ul");
        By btnCancel = By.CssSelector("input[value='Cancel Submission']");
        By checkToggleTabs = By.Id("toggleTabs");
        By tabList = By.Id("tabsList");
        By checkConfirm = By.CssSelector("input[id*='HeadApproval']");
        By txtTranOverview = By.CssSelector("textarea[name*='id98']");
        By txtCurrentStatus = By.CssSelector("textarea[name*='id100']");
        By txtCompDesc = By.CssSelector("textarea[name*='id103']");
        By comboCrossBorder = By.CssSelector("select[name*='InternationalAngle']");
        By comboAsiaAngle = By.CssSelector("select[name*='AsiaAngle']");
        By comboRealEstate = By.CssSelector("select[name*='RealEstateAngle']");
        By txtOwnershipStr = By.CssSelector("textarea[name*='id124']");
        By txtTotalDebt = By.CssSelector("input[name*='TotalDebt']");
        By comboAudit = By.CssSelector("select[name*='FinAudit01']");
        By txtCapRaise = By.CssSelector("input[name*='capRaiseReq']");
        By txtStrExp = By.CssSelector("textarea[name *= 'StructPriceTXT00']");
        By txtRiskFactors = By.CssSelector("textarea[name*='id168']");
        By txtEstFee = By.CssSelector("input[name*='estMinFee']");
        By txtFeeStr = By.CssSelector("textarea[name*= 'id179']");
        By comboLockUps = By.CssSelector("select[name*= 'id183']");
        By comboReferralFee = By.CssSelector("select[name*= 'id188']");
        By comboClient = By.CssSelector("select[id='j_id0:CNBCForm:j_id31:j_id64:Exist']");
        By txtHLComp = By.CssSelector("textarea[name*= 'id75']");
        By comboExistingRel = By.CssSelector("select[name*= 'ExistingRel']");
        By comboResList = By.CssSelector("select[name*= 'RestrictedList']");
        By titleEmailPage = By.CssSelector("h2[class='mainTitle']");
        By valEmailOppName = By.CssSelector("body[id*='Body_rta_body'] > span:nth-child(10) > span");
        By btnCancelEmail = By.CssSelector("input[value='Cancel']");
        By btnReturntoOpp = By.CssSelector("input[value*='Return to Opportunity']");
        By btnReturntoOppCFUser = By.CssSelector("span[id*=':j_id34'] > a");
        By txtAvailProceeds = By.CssSelector("select[id*='UseofProceeds_unselected']> optgroup > option[value='0']");
        By imgArrow = By.CssSelector("img[id*='UseofProceeds_right_arrow']");
        By lblReview = By.CssSelector("label[for*='reviewGrade']");
        By lblReviewAdmin = By.CssSelector("div[id*='210']>div>h3");
        By comboGrade = By.CssSelector("select[id*='reviewGrade']");
        By btnSave = By.CssSelector("input[name*='id33:j_id37']");
        By valGrade = By.CssSelector("select[id*='reviewGrade']>option[selected='selected']");
        By txtNotes = By.CssSelector("textarea[id*='reviewNotes']");
        By txtDateSubmitted = By.CssSelector("input[id*='dateSubmit']");
        By txtReason = By.CssSelector("textarea[id*='reasonWonLost']");
        By txtFeeDiff = By.CssSelector("textarea[id*='feeDiff']");
        By btnEUOverride = By.CssSelector("span[id*='1:j_id33'] > input[id*='euOverride']");
        By btnPDFView = By.XPath("//a[contains(text(),'PDF View')]");
        By btnAttachFile = By.CssSelector("button[type='button']");
        By btnAddFinancials = By.CssSelector("input[id*='newFinancials']");

        //Validate Opp Name
        public string ValidateOppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, OppName, 80);
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

            //Opportunity Overview
            WebDriverWaits.WaitUntilEleVisible(driver, checkConfirm, 90);
            driver.FindElement(checkConfirm).Click();
            driver.FindElement(comboClient).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 19));
            driver.FindElement(txtHLComp).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 20));
            driver.FindElement(txtAvailProceeds).Click();
            driver.FindElement(imgArrow).Click();
            driver.FindElement(comboExistingRel).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 21));

            //Overview and Financials
            driver.FindElement(txtTranOverview).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 3));
            driver.FindElement(txtCurrentStatus).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 4));
            driver.FindElement(txtCompDesc).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 5));
            driver.FindElement(comboCrossBorder).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 6));
            driver.FindElement(comboAsiaAngle).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 7));
            driver.FindElement(comboRealEstate).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 7));
            driver.FindElement(txtOwnershipStr).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 8));
            driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 9));
            driver.FindElement(comboAudit).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 10));
            driver.FindElement(txtCapRaise).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 11));
            driver.FindElement(txtStrExp).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 12));
            driver.FindElement(txtRiskFactors).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 13));

            //Fees
            driver.FindElement(txtEstFee).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 14));
            driver.FindElement(txtFeeStr).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 15));
            driver.FindElement(comboLockUps).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 16));
            driver.FindElement(comboReferralFee).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 17));

            //Administrative
            driver.FindElement(comboResList).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 27));

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
            WebDriverWaits.WaitUntilEleVisible(driver, valEmailOppName, 70);
            string emailSub = driver.FindElement(valEmailOppName).Text;
            Console.WriteLine(emailSub);
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnCancelEmail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 70);
            driver.FindElement(btnReturntoOpp).Click();
            return emailSub;
        }

        //Validate Review section 
        public string ValidateReviewSectionForAdmin()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lblReviewAdmin);
                string txtReview = driver.FindElement(lblReviewAdmin).Text;
                return txtReview;
            }
            catch (Exception e)
            {
                return "No Review section";
            }
        }

        //Validate Review section 
        public string ValidateReviewSection()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lblReview);
                string txtReview = driver.FindElement(lblReview).Text;
                return txtReview;
            }
            catch (Exception e)
            {
                return "No Review section";
            }
        }

        //To validate NBC form is disabled
        public string ValidateIfFormIsEditable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalDebt, 90);
            string value = driver.FindElement(txtTotalDebt).Enabled.ToString();
            if (value.Equals("True"))
            {
                return "Form is editable";
            }
            else
            {
                return "Form is not editable";
            }
        }

        //Save the Grade value
        public void SaveGradeValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboGrade);
            driver.FindElement(comboGrade).SendKeys("A+");
            driver.FindElement(btnSave).Click();
        }

        //Fetch the value of Grade field
        public string GetGradeValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGrade, 80);
            string value = driver.FindElement(valGrade).Text;
            return value;
        }

        //Validate Estimated Fee Field
        public string ValidateEstimatedFeeField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEstFee);
            string value = driver.FindElement(txtEstFee).Enabled.ToString();
            return value;
        }

        //Validate Grade Field
        public string ValidateGradeField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboGrade, 20);
                string value = driver.FindElement(comboGrade).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Grade field";
            }
        }

        //Validate Notes Field
        public string ValidateNotesField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtNotes, 20);
                string value = driver.FindElement(txtNotes).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Notes field";
            }
        }

        //Validate Date Submitted Field
        public string ValidateDateSubmittedField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtDateSubmitted, 20);
                string value = driver.FindElement(txtDateSubmitted).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Date Submitted field";
            }
        }

        //Validate Reason Field
        public string ValidateReasonField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtReason, 20);
                string value = driver.FindElement(txtReason).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Reason field";
            }
        }

        //Validate Fee Differences Field
        public string ValidateFeeDifferencesField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtFeeDiff);
                string value = driver.FindElement(txtFeeDiff).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Fee Differences field";
            }
        }

        //Validate Save NBC button
        public string ValidateSaveNBCButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 20);
                string value = driver.FindElement(btnSave).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Save button";
            }
        }

        //Validate Return To Opportunity button
        public string ValidateReturnToOpportunityButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 20);
            string value = driver.FindElement(btnReturntoOpp).Enabled.ToString();
            return value;
        }

        //Validate Return To Opportunity button
        public string ValidateReturnToOpportunityButtonForCFUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOppCFUser);
            string value = driver.FindElement(btnReturntoOppCFUser).Enabled.ToString();
            return value;
        }
        //Validate EU Override button
        public string ValidateEUOverrideButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEUOverride, 20);
                string value = driver.FindElement(btnEUOverride).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No EU Override button";
            }
        }

        //Validate PDF View button
        public string ValidatePDFViewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnPDFView);
            string value = driver.FindElement(btnPDFView).Enabled.ToString();
            return value;
        }

        //Validate Attach File button
        public string ValidateAttachFileButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAttachFile);
            string value = driver.FindElement(btnAttachFile).Enabled.ToString();
            return value;
        }

        //Validate Add Financials button
        public string ValidateAddFinancialsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddFinancials);
            string value = driver.FindElement(btnAddFinancials).Enabled.ToString();
            return value;
        }

    }
}

