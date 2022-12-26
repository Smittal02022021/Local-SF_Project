using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SalesForce_Project.Pages.Engagement
{
    class EngagementDetailsPage :BaseClass
    {
        By valEngName = By.CssSelector("div[id='Namej_id0_j_id4_ileinner']");
        By valStage = By.CssSelector("td[id*='id0_j_id4_ilecell']>div[id='00Ni000000D7NlWj_id0_j_id4_ileinner']");
        By valRecordType = By.CssSelector("div[id*='RecordType']");
        By valLegalEntity = By.CssSelector("div[id*='eecj']");
        By valHLEntity = By.CssSelector("div[id *= '00Ni000000D96Bbj_id0_j_id4_ileinner']");
        By titleEngPage = By.CssSelector("h1[class='pageType']");
        By btnEdit = By.CssSelector("input[value=' Edit ']");
        By txtEngNum = By.CssSelector("input[name*='D96p8']");
        By btnSave = By.CssSelector("input[name='save']");
        By valEngNum = By.CssSelector("div[id*='D96p8j']");
        By btnPortfolioValuation = By.CssSelector("input[title='Portfolio Valuation']");
        By btnFREngSummary = By.CssSelector("input[value='FR Engagement Summary']");
        By titleFREngSum = By.CssSelector("h2[class='pageDescription']");
        By lblTransType = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By valRevAccural = By.CssSelector("div[id*='saB_body']>table>tbody>tr>th:nth-child(1)");
        By lnkDeleteAccurals = By.CssSelector("div[id*='saB_body']>table>tbody>tr>td>a[title*='Delete']");
        By btnAddRevenueAccurals = By.CssSelector("input[title='Add Revenue Accrual']");
        By txtPeriodAccuredFees = By.CssSelector("input[name*='Ehsau']");
        By txtTotalEstFees = By.CssSelector("div[id*='zPj_id0_j_id4_ileinner']");
        By txtEngName = By.CssSelector("div[id*='Namej_id0_j_id4_ileinner']");
        By valPeriodAccrual = By.CssSelector("div[id*='00Ni000000EhsaB_body']>table>tbody>tr:nth-child(2)>td:nth-child(3)");
        By valTotalEstFee = By.CssSelector("div[id*='hsaB_body']>table>tbody>tr:nth-child(2)>td:nth-child(4)");
        By valPeriodAccrualFee = By.CssSelector("div[id*='FvixUj_id0_j_id4_ileinner']");
        //By valPeriodAccrualFeeFAS = By.CssSelector("div[id*='FnLOWj_id0_j_id4_ileinner']");s
        By txtTotalEstFeesFAS = By.CssSelector("input[id*='00Ni000000FmBzP']");
        By valTotalEstFeesFAS = By.CssSelector("div[id*='00Ni000000FmBzP']");
        By valYearMonth = By.CssSelector("div[id*='hsaB_body']>table>tbody>tr:nth-child(2)>th>a:nth-child(2)");
        By txtStage = By.CssSelector("select[name*='NlW']");
        By lnkEditContact = By.CssSelector("div[id*='cI_body'] > table > tbody > tr > td.actionColumn > a:nth-child(1)");
        By txtContact = By.CssSelector("span>input[id*='OPH']");
        By txtParty = By.CssSelector("select[name*='M0eMS']");
        By valContact = By.CssSelector("div[id*='QcI_body']> table > tbody > tr.dataRow.even.last.first > th>a:nth-child(2)");
        By btnBillingRequest = By.CssSelector("input[value='Billing Request']");
        By msgContact = By.CssSelector("div[id*='id4:j_id6']");
        By btnBackToManagement = By.CssSelector("input[value='Back To Engagement']");
        By titleBillingForm = By.CssSelector("h2[class='mainTitle']");
        By comboAccountingStatus = By.CssSelector("select[id='00Ni000000FF7XF']");
        By comboStage = By.CssSelector("select[id = '00Ni000000D7NlW']");
        By chkExpApplication = By.CssSelector("img[id*='jj_id0_j_id4_chkbox']");
        By valAccountingStatus = By.CssSelector("div[id*='7XFj_id0_j_id4_ileinner']");
        By lnkRevenueMonth = By.CssSelector("div[id*='00Ni000000EhsaB_body'] > table> tbody >tr:nth-child(2) >th >a:nth-child(2)");
        By valRevID = By.CssSelector("div[id='Name_ileinner']");
        By lnkEngagement = By.CssSelector("a[id*='EhsaB']");
        By btnCounterParties = By.CssSelector("td[id*='topButtonRow'] > input[value='Counterparties']");
        By btnAddRevenueAccrual = By.CssSelector("input[value='Add Revenue Accrual']");
        By errorMessage = By.CssSelector("div[id='errorDiv_ep']");
        By tabEngagement = By.CssSelector("a[title*='Engagements Tab - Selected']");
        By comboClientOwnership = By.CssSelector("select[id*='d2R']");
        By txtDebt = By.CssSelector("input[id*='LfH']");
        By valClientOwnership = By.CssSelector("div[id*='d2Rj_id0_j_id4_ileinner']");
        By valTotalDebt = By.CssSelector("div[id*='fHj_id0_j_id4_ileinner']");
        By lnkRecChange = By.CssSelector("div[id*='RecordType'] > a");
        By comboRecType = By.CssSelector("select[id*='p3']");
        By btnContinue = By.CssSelector("input[value='Continue']");
        By txtComments = By.CssSelector("textarea[id*='FlHaO']");
        By lnkFinalReport = By.CssSelector("div:nth-child(13) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By lnkReDisplayRec = By.CssSelector("table > tbody > tr:nth-child(2) > td > a:nth-child(4)");
        By valPOC = By.CssSelector("div[id*='GQj_id0_j_id4_ileinner']");
        By valERPSubmittedToSync = By.CssSelector("div[id*='eQj']");
        By valERPID = By.CssSelector("div[id*='e6j']");
        By valERPHLEntity = By.CssSelector("div[id*='e5j']");
        By valERPLegalEntityId = By.CssSelector("div[id*='eEj']");
        By valERPProjectNumber = By.CssSelector("div[id*='eMj']");
        By valERPProjectName = By.CssSelector("div[id*='eLj']");
        By valLOB = By.CssSelector("div[id*='oEj']");
        By valERPLOB = By.CssSelector("div[id*='e8j']");
        By valIG = By.CssSelector("div[id*='Axj']");
        By valERPIG = By.CssSelector("div[id*='e7j']");
        By valERPLastIntStatus = By.CssSelector("div[id*='eCj']");
        By valERPResponseDate = By.CssSelector("div[id*='eBj']");
        By valERPError = By.CssSelector("div[id*='e9j']");
        By valJobType = By.CssSelector("div[id*='5sj']");
        By valERPProductType = By.CssSelector("div[id*='eej']");
        By valERPProductTypeCode = By.CssSelector("div[id*='eHj']");
        By valERPTemplate = By.CssSelector("div[id*='eUj']");
        By valERPUnitID = By.CssSelector("div[id*='e2j']");
        By valERPUnit = By.CssSelector("div[id*='e3j']");
        By valERPLegalEntityID = By.CssSelector("div[id*='eDj']");
        By valERPEntityCode = By.CssSelector("div[id*='e4j']");
        By valERPLegCode = By.CssSelector("div[id*='eFj']");
        By comboPrimaryOffice = By.CssSelector("select[id*='Lq0']");
        By valPrimaryOffice = By.CssSelector("div[id*='Lq0']");
        By checkERPUpdateDFF = By.CssSelector("div[id*='eWj']>img");
        By comboIG = By.CssSelector("select[id*='6Ax']");
        By valIGCF = By.CssSelector("div[id*='6Ax']");
        By comboSector = By.CssSelector("select[id*='6B7']");
        By valSector = By.CssSelector("div[id*='6B7']");
        By comboJobType = By.CssSelector("select[id*= '65s']");
        By lnkRecordTypeChange = By.CssSelector("div[id*='RecordTypej_id0_j_id4_ileinner'] > a");        
        By comboLOB = By.CssSelector("select[id*='LoE']");
        By lnkSyncDate = By.CssSelector("table > tbody > tr:nth-child(1) > td.dataCol.col02 > span > span > a");
        By rowContract = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>th>a");
        By valERPContractType = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(4)");
        By valERPBusUnit = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(5)");
        By valERPLegalEntity = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(6)");
        By valERPBillPlan = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(7)");
        By valBillTo = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(8)>a");
        By valCompName = By.CssSelector("div[id*='QcI_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(10)>a");
        By valStartDate = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(9)");
        By valIsMain = By.CssSelector("div[id*='cq_body']>table>tbody>tr:nth-child(2)>td:nth-child(11)>img");
        By valContract1 = By.CssSelector("div[id*='ecq_body'] > table > tbody > tr:nth-child(2) > th > a");
        By valContract2 = By.CssSelector("div[id*='ecq_body'] > table > tbody > tr:nth-child(3) > th > a");
        By lnkOpp = By.CssSelector("div[id*='zAzj']>a");
        By btnAdditionalClient = By.CssSelector("input[value*='Additional Client']");
        By btnAdditionalSubject = By.CssSelector("input[value*='Additional Subject']");
        By comboTypes = By.XPath("//select[@name='00Ni000000D9Dbh']/option");
        By valEngClientSubject = By.CssSelector("table[class*='detailList']>tbody>tr:nth-child(1)>td:nth-child(2)>div>span>input");
        By valType = By.CssSelector("select[id*='0Ni000000D9Dbh']> option:nth-child(1)");
        By valTypeSub = By.CssSelector("select[id*='0Ni000000D9Dbh']> option:nth-child(5)");
        By txtClientSubject = By.CssSelector("table[class*='detailList']>tbody>tr:nth-child(2)>td:nth-child(2)>div>span>input");
        By btnCancel = By.CssSelector("input[value='Cancel']");
        By titleEng = By.CssSelector("h2[class='mainTitle']");
        By valDefaultClient = By.CssSelector("div[id*='DbX_body'] > table > tbody > tr:nth-child(2)>th>a");
        By valNewClient = By.CssSelector("div[id*='D9DbX_body'] > table > tbody > tr:nth-child(2)>th>a");
        //By valNewSubject = By.CssSelector("div[id*='aho5_00Ni000000D9DbX_body'] > table > tbody > tr:nth-child(4)>th>a");
        By valDefaultSubject = By.CssSelector("div[id*='DbX_body'] > table > tbody > tr:nth-child(3)>th>a");
        By lnkEditClient = By.CssSelector("div[id*='DbX_body'] > table > tbody > tr.dataRow.even.first > td.actionColumn > a:nth-child(1)");
        By valClientType = By.CssSelector("div[id*='DbX_body']> table > tbody > tr.dataRow.even.first > td:nth-child(3)");
        By comboType = By.CssSelector("select[name*='D9Dbh']");
        By lnkDelClient = By.CssSelector("div[id*='DbX_body'] > table > tbody > tr.dataRow.even.first> td.actionColumn > a:nth-child(2)");
        By valWomenLed = By.CssSelector("div[id *= 'NgVj_id0_j_id4_ileinner']");
        By checkPrimaryContact = By.CssSelector("input[name*='D7OP7']");
        By lnkClient = By.XPath("//*[text()='Client']/.. //td[2]//div//a");
        By lnkContract = By.CssSelector("div[id*='M0ecq_body'] > table > tbody > tr:nth-child(2) > th > a");
        //By lnkContract = By.CssSelector("div[id*='M0ecq_body'] > table > tbody > tr > th > a");
        //-----------------
        By lnkSecondContract = By.CssSelector("div[id*='M0ecq_body'] > table > tbody > tr:nth-child(3) > th > a");
        
        By txtERPLegalEntity = By.CssSelector("input[id*='M0eec'][type='text']");
        By valERPBusinessUnitId = By.CssSelector("div[id*='M0ee2j']");
        By valERPBusinessUnit = By.CssSelector("div[id*='M0ee3j']");
        
        By valERPId = By.CssSelector("div[id*='M0ee6j']");
        
        By valEnggNumberSuffix = By.CssSelector("div[id*='M0eeaj']");
        By valEnggName = By.CssSelector("td[id*='Namej_id0_j_id4']");
        
        By chkboxIsMainContractNewContract = By.CssSelector("div[id*='M0ecq_body'] > table > tbody > tr.dataRow.even.first > td.dataCell.booleanColumn > img");
        By chkboxIsMainContractOldContract = By.CssSelector("div[id*='M0ecq'] > table > tbody > tr.dataRow.odd.last > td.dataCell.booleanColumn > img");
        By valEndDate = By.CssSelector("div[id*='M0ecq'] > table > tbody > tr:nth-child(3) > td:nth-child(10)");
        By valNoOfContract = By.CssSelector("div[id*='M0ecq'] > table > tbody > tr");
        By btnAddOppContact = By.CssSelector("input[name='new_external_team']");
        By comboRole = By.CssSelector("select[name*='D7Qcn']");
        
        By comboParty = By.CssSelector("select[name*='M0eMp']");
        By checkAckBillingContact = By.CssSelector("input[name*='M0jSL']");
        By checkBillingContact = By.CssSelector("input[name*='Gz3dK']");
        By lnkBillTo = By.CssSelector("a[id*='A00000M0ebc']");

        By txtSecWomenled = By.CssSelector("div[id*='Cyy_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedESOP = By.CssSelector("div[id*='Cxi_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedOther = By.CssSelector("div[id*='Bs_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedActivism = By.CssSelector("div[id*='X1_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedFVA = By.CssSelector("div[id*='9E_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedFR = By.CssSelector("div[id*='PL_ep_j_id0_j_id4']>h3");
        By labelWomenLed = By.CssSelector("div:nth-child(35) > table > tbody > tr:nth-child(9) > td:nth-child(1)");
        By labelWomenLedJob = By.CssSelector("div:nth-child(35) > table > tbody > tr:nth-child(8) > td:nth-child(3)");
        By labelWomenLedActivism = By.CssSelector("div:nth-child(35) > table > tbody > tr:nth-child(7) > td:nth-child(1)");
        By labelWomenFVA = By.CssSelector("div:nth-child(29) > table > tbody > tr:nth-child(3) > td:nth-child(1)");
        By labelWomenFR = By.CssSelector("div:nth-child(33) > table > tbody > tr:nth-child(13) > td:nth-child(1)");

        By checkBoxCoExist = By.CssSelector("div[id*='00N6e00000MRVFNj_id0_j_id4_ileinner'] > img");
        By imputCoExist = By.XPath("//input[@id='00N6e00000MRVFN']");

        By btnGo = By.XPath("//input[@type='submit']");
        By btnNewEngagementSector = By.XPath("//input[@value='New Engagement Sector']");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgCoverageSectorDependencies = By.CssSelector("img[alt = 'Coverage Sector Dependencies']");
        By imgCoverageSectorDependencyLookUp = By.XPath("//img[@alt='Coverage Sector Dependency Lookup (New Window)']");
        By txtSearchBox = By.XPath("//input[@title='Go!']/preceding::input[1]");
        By linkCoverageSectorDependencyName = By.XPath("//a[@href='#']");
        By btnSaveEngagementSector = By.XPath("(//input[@title='Save'])[1]");
        By btnDeleteEngagementSector = By.XPath("(//input[@title='Delete'])[1]");
        By valEngagementSectorName = By.XPath("//td[contains(text(),'Engagement Sector')]/following::div[1]");
        By linkEngagementName = By.XPath("(//td[contains(text(),'Engagement')])[2]/../td[2]/div/a");
        By linkSectorName = By.XPath("//*/th[contains(text(),'Engagement Sector')]/following::tr/th/a");
        By txtCoverageType = By.CssSelector("input[id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By txtPrimarySector = By.CssSelector("input[id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By txtSecondarySector = By.CssSelector("input[id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By txtTertiarySector = By.CssSelector("input[id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnApplyFilters = By.XPath("//input[@title='Apply Filters']");
        By linkShowAllResults = By.XPath("//a[contains(text(),'Show all results')]");
        By linkEngagementSector = By.XPath("//*/span[contains(text(),'Engagement Sectors')]");
        By inputCoverageType = By.XPath("//input[@id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By inputPrimarySector = By.XPath("//input[@id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By inputSecondarySector = By.XPath("//input[@id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By inputTertiarySector = By.XPath("//input[@id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnEditCompCoverageSector = By.XPath("//input[@title='Edit']");

        By btnCFEngagementSummary = By.XPath("(//input[@title='CF Engagement Summary (lwc)'])[1]");
        By lblHeaderText = By.XPath("//h1/span[2]");

        public string NavigateToCFEngagementSummaryPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCFEngagementSummary, 120);
            driver.FindElement(btnCFEngagementSummary).Click();

            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(10000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblHeaderText, 140);
            string h1Text = driver.FindElement(lblHeaderText).Text;
            Thread.Sleep(10000);
            return h1Text;
        }

        public bool VerifyIfEngagementSectorQuickLinkIsDisplayed()
        {
            bool result = false;
            if (driver.FindElement(linkEngagementSector).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ClickNewEngagementSectorButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngagementSector, 120);
            driver.FindElement(btnNewEngagementSector).Click();
            Thread.Sleep(2000);
        }

        public void SelectCoverageSectorDependency(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Enter dependency name
            driver.FindElement(txtSearchBox).SendKeys(covSectorDependencyName);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);

            driver.SwitchTo().DefaultContent();

            //Enter results frame & click on the result
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);

            //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public void SelectCoverageSectorDependencyForEngagementSector(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Enter dependency name
            driver.FindElement(txtSearchBox).SendKeys(covSectorDependencyName);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);

            driver.SwitchTo().DefaultContent();

            //Enter results frame & click on the result
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);

            driver.FindElement(linkShowAllResults).Click();
            Thread.Sleep(2000);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);

            //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public void SelectCoverageSectorDependencyByFilters(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter results frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);

            //Apply filters
            driver.FindElement(txtCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));
            driver.FindElement(txtPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));
            driver.FindElement(txtSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));
            driver.FindElement(txtTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);

            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);

            //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public bool VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(string file, string covSectorDependencyName)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Click Edit button on Company Sector detail page 
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditCompCoverageSector, 120);
            driver.FindElement(btnEditCompCoverageSector).Click();
            Thread.Sleep(2000);

            //Click on Coverage Sector Dependency LookUp icon
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Clear Search box
            driver.FindElement(txtSearchBox).Clear();

            driver.SwitchTo().DefaultContent();

            //Enter results frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);

            //Click on Show Filters link
            //driver.FindElement(linkShowFilters).Click();

            //Enter filter values
            driver.FindElement(inputCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));
            driver.FindElement(inputPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));
            driver.FindElement(inputSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));
            driver.FindElement(inputTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));

            //Click on Apply filters button
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);

            if (driver.FindElement(linkCoverageSectorDependencyName).Text == covSectorDependencyName)
            {
                //Select the desired dependency name from the result
                driver.FindElement(linkCoverageSectorDependencyName).Click();
                Thread.Sleep(4000);

                //Switch back to original window
                CustomFunctions.SwitchToWindow(driver, 0);

                result = true;
            }
            return result;
        }

        public void SaveNewEngagementSectorDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveEngagementSector, 120);
            driver.FindElement(btnSaveEngagementSector).Click();
            Thread.Sleep(2000);
        }

        public string GetEngagementSectorName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEngagementSectorName, 120);
            string name = driver.FindElement(valEngagementSectorName).Text;
            return name;
        }

        public void NavigateToEngagementDetailPageFromEngagementSectorDetailPage()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkEngagementName, 120);
            driver.FindElement(linkEngagementName).Click();
            Thread.Sleep(2000);
        }

        public void NavigateToCoverageSectorDependenciesPage()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgCoverageSectorDependencies).Click();
            Thread.Sleep(2000);
        }

        public bool VerifyEngagementSectorAddedToEngagementOrNot(string sectorName)
        {
            Thread.Sleep(5000);
            bool result = false;
            if (driver.FindElement(linkSectorName).Text == sectorName)
            {
                result = true;
            }
            return result;
        }

        public void DeleteEngagementSector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSectorName, 120);
            driver.FindElement(linkSectorName).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteEngagementSector, 120);
            driver.FindElement(btnDeleteEngagementSector).Click();
            Thread.Sleep(2000);

            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(3000);
        }


        //To get name of Page
        public string GetTitle()
        {
            string title = driver.FindElement(titleEngPage).Text;
            return title;
        }

        public string VerifyIfCoExistFieldIsEditableOrNot()
        {
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            string enb = driver.FindElement(imputCoExist).GetAttribute("Type");
            if (enb == "hidden")
            {
                return "CoExist field is not editable ";
            }
            else
            {
                return "CoExist field is editable ";
            }
        }

        public string ValidateIfCoExistFieldIsPresentAndCheckedOrNot()
        {
            if (driver.FindElement(checkBoxCoExist).Displayed)
            {
                string value = driver.FindElement(checkBoxCoExist).GetAttribute("alt");
                if (value == "Not Checked")
                {
                    return "CoExist checkbox is displayed and not-checked";
                }
                else
                {
                    return "CoExist checkbox is displayed and checked";
                }
            }
            else
            {
                return "CoExist checkbox is not displayed";
            }
        }

        public string GetEngName()            
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEngName,110);
            string name = driver.FindElement(valEngName).Text;
            return name;
        }
        
        public string GetStage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStage, 80);
            string stage = driver.FindElement(valStage).Text;
            return stage;
        }

        //Get Engagement Number
        public string GetEngagementNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEngNum, 90);
            string Name = driver.FindElement(valEngNum).Text;
            return Name;
        }

        public string GetRecordType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecordType, 60);
            string recordType = driver.FindElement(valRecordType).Text;
            return recordType;
        }
        public string GetHLEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valHLEntity, 60);
            string HLEntity = driver.FindElement(valHLEntity).Text;
            return HLEntity;
        }

        public string GetWomenLed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valWomenLed, 60);
            string value = driver.FindElement(valWomenLed).Text;
            return value;
        }
        public string GetLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity, 60);
            string HLEntity = driver.FindElement(valHLEntity).Text;
            return HLEntity;
        }
        //To clear Engagement number and save it
        public string ClearEngNumberAndSave()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngNum, 60);
            driver.FindElement(txtEngNum).Clear();
            driver.FindElement(btnSave).Click();
            string engNum = driver.FindElement(valEngNum).Text;
            return engNum;
        }
        //Validate Portfolio Valuation button and click on it
        public void ClickPortfolioValuation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnPortfolioValuation, 90);
            driver.FindElement(btnPortfolioValuation).Click();           
        }

        //To Validate FR Engagement Summary button 
        public string ValidateFREngSummaryButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnFREngSummary, 90);
            string valFREngSum = driver.FindElement(btnFREngSummary).Displayed.ToString();
            return valFREngSum;
        }
        //Click FR Engagement Summary button 
        public string ClickFREngSummaryButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnFREngSummary, 90);
            driver.FindElement(btnFREngSummary).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleFREngSum, 90);
            string title = driver.FindElement(titleFREngSum).Text;
            return title;
        }
        
        //Get value of Total Estimated Fee
        public string GetTotalEstFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalEstFees, 90);
            string Fees = driver.FindElement(txtTotalEstFees).Text;
            return Fees;
        }

        //To update value of Total Estimated Fee
        public void UpdateTotalEstFee(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalEstFeesFAS, 90);
            driver.FindElement(txtTotalEstFeesFAS).Clear();
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtTotalEstFeesFAS).SendKeys(value);
            driver.FindElement(btnSave).Click();
        }

        //Get value of Total Estimated Fee FAS
        public string GetTotalEstFeeFAS()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalEstFeesFAS, 90);
            string Fees = driver.FindElement(valTotalEstFeesFAS).Text;
            return Fees;
        }

        //To get month from created Revenue Accrual record
        public string GetMonthFromRevenueAccrualRecord()
        {
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valYearMonth, 170);
            string value = driver.FindElement(valYearMonth).Text;
            return value;
        }

        //To get value of Total Estimated Fee from created Revenue Accrual record
        public string GetTotalEstFeeFromRevenueAccrualRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalEstFee, 100);
            string value = driver.FindElement(valTotalEstFee).Text;
            return value;
        }

        //Get Engagement Number
        public string GetEngNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngName, 90);
            string Name = driver.FindElement(txtEngName).Text;
            return Name;
        }

        //Delete any existing Revenue Accurals
        public void DeleteExistingAccurals()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevAccural, 100);
            string value = driver.FindElement(valRevAccural).Text;
            if (value.Equals("Action"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteAccurals, 110);
                driver.FindElement(lnkDeleteAccurals).Click();
                Thread.Sleep(3000);
                /* IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                 js.ExecuteScript("window.confirm = function() { return true;}");
                 js.ExecuteScript("arguments[0].click()");*/
                try
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    alert.Accept();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }                
            }
            else
            {
                Console.WriteLine("No existing reveue accurals");
                Thread.Sleep(2000);
            }
        }

        //Click Add Revenue Accurals 
        public string AddNewRevenueAccurals()
        {            
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddRevenueAccurals, 90);
                driver.FindElement(btnAddRevenueAccurals).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtPeriodAccuredFees, 90);
                driver.FindElement(txtPeriodAccuredFees).SendKeys("10");
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valPeriodAccrual, 100);
                string value= driver.FindElement(valPeriodAccrual).Text;
                return value;
        }

        //To get Period Accured Fee Net value from Revenue Accrual record
        public string GetPeriodAccrualFeeNetValue()
        {            
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            string value = driver.FindElement(valPeriodAccrual).Text;
            return value;
        }

        //To get Period Accrued Fees of FAS record 
        public string GetPeriodAccrualValueFAS()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriodAccrualFee, 120);
            string value = driver.FindElement(valPeriodAccrualFee).Text;
            return value;
        }

        //To get value of Period Accrual value from engagement details
        public string GetPeriodAccrualValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriodAccrualFee, 120);            
            string value = driver.FindElement(valPeriodAccrualFee).Text;
            return value;
        }

        //To update stage of Engagement
        public void UpdateStage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 150);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtStage).SendKeys("Retained");
            driver.FindElement(btnSave).Click();
        }

        //To get message when no Revenue Accrual exists
        public string GetRevAccrualMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevAccural, 120);
            string message = driver.FindElement(valRevAccural).Text;
            return message;
        }


        //To update Engagement contact details
        public string UpdateEngContact(string Name,string LOB)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditContact, 70);
            driver.FindElement(lnkEditContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 70);
            driver.FindElement(txtContact).Clear();
            driver.FindElement(txtContact).SendKeys(Name);
            if (LOB.Equals("CF"))
            {
                driver.FindElement(txtParty).SendKeys("Buyer");
                driver.FindElement(btnSave).Click();
            }
            else 
            {
                driver.FindElement(btnSave).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, valContact, 90);
            Thread.Sleep(2000);
            string contact = driver.FindElement(valContact).Text;
            return contact;
        }

        //To click on billing request button
        public void ClickBillingRequestButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBillingRequest, 70);
            driver.FindElement(btnBillingRequest).Click();
        }

        //To get validation message for contact details
        public string GetContactValidationMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgContact, 70);
            string message = driver.FindElement(msgContact).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToManagement, 70);
            driver.FindElement(btnBackToManagement).Click();
            return message;
        }

        //To get Subject from Billing Request Form
        public string GetTitleOfBillingReqForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleBillingForm, 70);
            string title = driver.FindElement(titleBillingForm).Text;
            return title;
        }

        //To update Accounting Status and stage
        public void UpdateAccountingStatusAndStage(string Status, string Stage)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboAccountingStatus).SendKeys(Status);
            driver.FindElement(comboStage).SendKeys(Stage);
            driver.FindElement(btnSave).Click();
        }

        //To get value of Accounting Status
        public string GetAccontingStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAccountingStatus, 90);
            string value = driver.FindElement(valAccountingStatus).Text;
            return value;
        }

        //To check if Available in Expense Application checkbox is checked or not
        public string ValidateIfExpenseApplicationIsChecked()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkExpApplication, 70);
            string value = driver.FindElement(chkExpApplication).Selected.ToString();
            if (value.Equals("True"))
            {
                return "Expense Application checkbox is checked";
            }
            else
            {
                return "Expense Application checkbox is not checked";
            }
        }

        //Get Revenue record number
        public string GetRevenueRecordNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRevenueMonth, 80);
            driver.FindElement(lnkRevenueMonth).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRevID, 80);
            string id = driver.FindElement(valRevID).Text;
            driver.FindElement(lnkEngagement).Click();
            return id;
        }

        //Create new Revenue Accrual record
        public string AddRevenueAccrual()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRevenueAccrual, 80);
            driver.FindElement(btnAddRevenueAccrual).Click();
            driver.FindElement(btnSave).Click();
            string message = driver.FindElement(errorMessage).Text.Replace("\r\n", " ");
            return message;
        }       

        //Click Counterparties button 
        public string ClickCounterpartiesButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCounterParties, 120);
            driver.FindElement(btnCounterParties).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngPage, 90);
            string title = driver.FindElement(titleEngPage).Text;
            return title;
        }
        //Validate the visibility of Portfolio Valuation button
        public string ValidatePortfolioValuationButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnPortfolioValuation).Displayed.ToString();
                Console.WriteLine("Portfolio Valuation button: " + value);
                return "Portfolio Valuation button is displayed";
            }
            catch (Exception)
            {
                return "Portfolio Valuation button is not displayed";
            }
        }

        //Click on Engagement tab
        public void ClickEngagementTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngagement, 80);
            driver.FindElement(tabEngagement).Click();
        }

        //Update Client Ownership and Total Debt
        public string UpdateClientOwnershipAndDebt(string Ownership,string Debt)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboClientOwnership).SendKeys(Ownership);
            //driver.FindElement(txtDebt).Clear();
            //driver.FindElement(txtDebt).SendKeys(Debt);
            driver.FindElement(btnSave).Click();
            string clientOwnership = driver.FindElement(valClientOwnership).Text;
            return clientOwnership;
        }

        //To get Debt
        public string GetTotalDebt()
        {
            string Debt = driver.FindElement(valTotalDebt).Text;
            return Debt;
        }

        //To update Record Type of Engagement
        public string UpdateRecordType(string Type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecChange, 120);
            driver.FindElement(lnkRecChange).Click();
            driver.FindElement(comboRecType).SendKeys(Type);
            driver.FindElement(btnContinue).Click();
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRecordType, 100);
            string value = driver.FindElement(valRecordType).Text;
            return value;
        }

        //To update stage of Engagement
        public string UpdateEngStage(string Stage)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
                driver.FindElement(btnEdit).Click();
                if (Stage.Equals("Dead"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(txtComments).SendKeys("Test Comments");
                }
                else if (Stage.Equals("Opinion Report"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(txtComments).Clear();
                }
                else if (Stage.Equals("Bill/File"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(lnkFinalReport).Click();
                }
                else
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                }
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valStage, 120);
                string value = driver.FindElement(valStage).Text;
                return value;
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
                driver.FindElement(btnEdit).Click();
                if (Stage.Equals("Dead"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(txtComments).SendKeys("Test Comments");
                }
                else if (Stage.Equals("Opinion Report"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(txtComments).Clear();
                }
                else if (Stage.Equals("Bill/File"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(lnkFinalReport).Click();
                }
                else
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                }
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valStage, 120);
                string value = driver.FindElement(valStage).Text;
                return value;
            }
        }

        //To get value of POC
        public string GetPOCValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            string value = driver.FindElement(valPOC).Text;
            return value;
        }

        //Get ERP Submitted To Sync in ERP section
        public string GetERPSubmittedToSync()
        {
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSync, 120);
            string syncDate = driver.FindElement(valERPSubmittedToSync).Text;
            return syncDate;
        }

        //Get ERP ID in ERP section
        public string GetERPID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPID, 80);
            string id = driver.FindElement(valERPID).Text;
            return id;
        }

        //Get ERP HL Entity in ERP section
        public string GetERPHLEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPHLEntity, 80);
            string ERPEntity = driver.FindElement(valERPHLEntity).Text;
            return ERPEntity;
        }

        //Get ERP Legal Entity in ERP section
        public string GetERPLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityId, 80);
            string ERPEntity = driver.FindElement(valERPLegalEntityId).Text;
            return ERPEntity;
        }

       
        //Get ERP Project Number in ERP section
        public string GetERPProjectNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectNumber, 80);
            string Number = driver.FindElement(valERPProjectNumber).Text;
            return Number;
        }

        //Get ERP Project Name in ERP section
        public string GetERPProjectName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectName, 80);
            string Number = driver.FindElement(valERPProjectName).Text;
            return Number;
        }
        //Get LOB
        public string GetLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOB);
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //Get ERP LOB
        public string GetERPLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLOB);
            string LOB = driver.FindElement(valERPLOB).Text;
            return LOB;
        }

        //Get Industry Group
        public string GetIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIG);
            string IG = driver.FindElement(valIG).Text.Substring(0, 3);
            return IG;
        }
        //Get ERP Industry Group
        public string GetERPIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPIG, 80);
            string IG = driver.FindElement(valERPIG).Text;
            return IG;
        }

        //Get ERP Last Integration Status
        public string GetERPIntegrationStatus()
        {
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntStatus, 80);
            string status = driver.FindElement(valERPLastIntStatus).Text;
            return status;
        }

        //Get ERP Last Integration Response Date
        public string GetERPIntegrationResponseDate()
        {
            Thread.Sleep(7000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPResponseDate, 120);
            string date = driver.FindElement(valERPResponseDate).Text;
            return date;
        }

        //Get ERP Last Integration Error Description
        public string GetERPIntegrationError()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPError, 80);
            string error = driver.FindElement(valERPError).Text;
            return error;
        }

        //Get Job Type
        public string GetJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 80);
            string type = driver.FindElement(valJobType).Text;
            return type;
        }

        //Get ERP Product Type
        public string GetERPProductType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductType, 80);
            string type = driver.FindElement(valERPProductType).Text;
            return type;
        }

        //Get ERP Product Type Code
        public string GetERPProductTypeCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductTypeCode, 80);
            string code = driver.FindElement(valERPProductTypeCode).Text;
            return code;
        }

        //Get ERP Template
        public string GetERPTemplate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPTemplate, 80);
            string number = driver.FindElement(valERPTemplate).Text;
            return number;
        }

        //Get ERP Business Unit ID
        public string GetERPBusinessUnitID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnitID, 80);
            string number = driver.FindElement(valERPUnitID).Text;
            return number;
        }
        //Get ERP Business Unit
        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnit, 80);
            string unit = driver.FindElement(valERPUnit).Text;
            return unit;
        }

        //Get ERP Legal Entity ID
        public string GetERPLegalEntityID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityID, 80);
            string id = driver.FindElement(valERPLegalEntityID).Text;
            return id;
        }

        //Get ERP Entity Code
        public string GetERPEntityCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEntityCode, 80);
            string code = driver.FindElement(valERPEntityCode).Text;
            return code;
        }
        //Get ERP Legislation Code
        public string GetERPLegCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegCode, 80);
            string code = driver.FindElement(valERPLegCode).Text;
            return code;
        }

        //Update Primary Office
        public string UpdatePrimaryOffice(string value)
        {
            Thread.Sleep(50000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboPrimaryOffice).SendKeys(value);
            driver.FindElement(btnSave).Click();
            string office = driver.FindElement(valPrimaryOffice).Text;
            return office;
        }
        //Get ERP Update DFF
        public string GetERPUpdateDFFIfChecked()
        {
            //Thread.Sleep(2000);
            //driver.Navigate().Refresh();            
            WebDriverWaits.WaitUntilEleVisible(driver, checkERPUpdateDFF, 100);
            string value = driver.FindElement(checkERPUpdateDFF).GetAttribute("title");
            if (value.Equals("Not Checked"))
            {
                return "Checkbox is not checked";
            }
            else
            {
                return "Checkbox is checked";
            }
        }

        //Update Industry Group
        public string UpdateIndustryGroup(string group)
        {
            Thread.Sleep(50000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboIG).SendKeys(group);
            driver.FindElement(comboSector).SendKeys("Dental");
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valIG, 100);
            string IG = driver.FindElement(valIG).Text;
            return IG;
        }

        //Update Sector
        public string UpdateSector(string sector)
        {
            Thread.Sleep(50000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboSector).SendKeys(sector);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valSector, 100);
            string Sector = driver.FindElement(valSector).Text;
            return Sector;
        }
        //To update Job type for ERP
        public string UpdateJobTypeERP(string jobType)
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
            driver.FindElement(comboJobType).SendKeys(jobType);
            driver.FindElement(btnSave).Click();
            string type = driver.FindElement(valJobType).Text;
            return type;
        }
        //Update Client Ownership
        public string UpdateClientOwnership(string client)
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboClientOwnership).SendKeys(client);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnership, 120);
            string Client = driver.FindElement(valClientOwnership).Text;
            return Client;
        }
        //To update ERP Record Type
        public string UpdateRecordTypeAndLOBERP()
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecordTypeChange, 120);
            driver.FindElement(lnkRecordTypeChange).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 90);
            driver.FindElement(comboRecType).SendKeys("Buyside");
            driver.FindElement(btnContinue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 90);
            driver.FindElement(comboLOB).SendKeys("CF");
            driver.FindElement(comboJobType).SendKeys("Buyside");
            driver.FindElement(btnSave).Click();
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //To schedule ERP Submitted to Sync manually
        public void ScheduleERPSyncManually()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkSyncDate, 90);
            driver.FindElement(lnkSyncDate).Click();
            driver.FindElement(btnSave).Click();
        }

        //To validate if contract is created or not
        public string ValidateContractExists()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, rowContract, 100);
                string id = driver.FindElement(rowContract).Text;
                return id;
            }
            catch(Exception e)
            {
                return "Contract does not exist";
            }
        }

        //Get the type of Contract
        public string GetERPContractType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContractType, 100);
            string type = driver.FindElement(valERPContractType).Text;
            return type;
        }

        //Get Contract ERP Business Unit
        public string GetContractERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusUnit, 100);
            string unit = driver.FindElement(valERPBusUnit).Text;
            return unit;
        }

        //Get Contract ERP Legal Entity Name
        public string GetContractERPLegalEntityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntity, 100);
            string entity = driver.FindElement(valERPLegalEntity).Text;
            return entity;
        }

        //Get Contract ERP Bill Plan
        public string GetContractERPBillPlan()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillPlan, 100);
            string plan = driver.FindElement(valERPBillPlan).Text;
            return plan;
        }

        //Get Contract Bill To
        public string GetContractBillTo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillTo, 100);
            string bill = driver.FindElement(valBillTo).Text;
            return bill;
        }

        //Get Company name of contact
        public string GetCompanyNameOfContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompName, 100);
            string name = driver.FindElement(valCompName).Text;
            return name;
        }

        //Get Contract Start Date
        public string GetContractStartDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStartDate, 100);
            string date = driver.FindElement(valStartDate).Text;
            return date;
        }

        //Get if Main Contract checkbox is checked
        public string GetIfIsMainContractChecked()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIsMain, 100);
            string main = driver.FindElement(valIsMain).GetAttribute("title");
            if (main.Equals("Checked"))
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }

        //Get 1st contract name
        public string Get1stContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract1, 100);
            string name = driver.FindElement(valContract1).Text;
            return name;
        }

        //Get 2nd contract name
        public string Get2ndContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract2, 100);
            string name = driver.FindElement(valContract2).Text;
            return name;
        }

        //Click on Related Opportunity link
        public string ClickRelatedOpportunityLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpp, 100);
            driver.FindElement(lnkOpp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngPage, 100);
            string name = driver.FindElement(titleEngPage).Text;
            return name;
        }

        //Click Additional Client button 
        public string ClickAdditionalClientButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdditionalClient, 120);
            driver.FindElement(btnAdditionalClient).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleFREngSum, 90);
            string title = driver.FindElement(titleFREngSum).Text;
            return title;
        }

        //Validate Type drop down values
        public bool VerifyTypeValues()
        {
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboTypes);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = {"Client","Subject"};
            bool isSame = true;

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

        //Validate Type drop down values
        public bool VerifyTypeValuesForSubject()
        {
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboTypes);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "Client","Counterparty","Equity Holder", "PE Firm","Subject" };
            bool isSame = true;

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

        //Get Engagement value from Engagement Client Subject Edit
        public string GetEngagementFromClientSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEngClientSubject, 120);
            string value = driver.FindElement(valEngClientSubject).GetAttribute("value");
            return value;
        }

        //Get Type value from Engagement Client Subject Edit
        public string GetTypeFromClientSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valType, 120);
            string value = driver.FindElement(valType).Text;
            return value;
        }

        // To validate cancel functionality of Additional client
        public string ValidateCancelFunctionalityOfAdditionalClient(string company)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubject, 80);
            driver.FindElement(txtClientSubject).SendKeys(company);
            driver.FindElement(btnCancel).Click();
            string page = driver.FindElement(titleEng).Text;
            return page;
        }

        //Get default Company name of Additional Client
        public string GetCompanyNameOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultClient, 120);
            string value = driver.FindElement(valDefaultClient).Text;
            return value;
        }

        // To validate save functionality of Additional client
        public string ValidateSaveFunctionalityOfAdditionalClient(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubject, 80);
            driver.FindElement(txtClientSubject).SendKeys(name);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valNewClient, 100);
            string value = driver.FindElement(valNewClient).Text;
            return value;
        }

        //Get type of added additional client record
        public string GetTypeOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientType, 80);           
            string value = driver.FindElement(valClientType).Text;
            return value;
        }

        //Validate Edit functionality of Additional Client
        public void ValidateEditFunctionalityOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditClient, 80);
            driver.FindElement(lnkEditClient).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 80);
            driver.FindElement(comboType).SendKeys("Subject");
            driver.FindElement(btnSave).Click();         
        }

        //Validate Delete functionality of Additional Client
        public string ValidateDeleteFunctionalityOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelClient, 80);
            driver.FindElement(lnkDelClient).Click();
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultClient, 100);
            string name = driver.FindElement(valDefaultClient).Text;
            return name;
        }
        //Get default Company name of Additional Subject
        public string GetCompanyNameOfAdditionalSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultSubject, 120);
            string value = driver.FindElement(valDefaultSubject).Text;
            return value;
        }

        //Click Additional Subject button 
        public string ClickAdditionalSubjectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdditionalSubject, 120);
            driver.FindElement(btnAdditionalSubject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleFREngSum, 90);
            string title = driver.FindElement(titleFREngSum).Text;
            return title;
        }

        //Get Type value from Engagement Client Subject Edit
        public string GetDefaultTypeFromClientSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTypeSub, 120);
            string value = driver.FindElement(valTypeSub).Text;
            return value;
        }

        //Validate Delete functionality of Additional Subject
        public string ValidateDeleteFunctionalityOfAdditionalSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelClient, 80);
            driver.FindElement(lnkDelClient).Click();
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultSubject, 100);
            string name = driver.FindElement(valDefaultSubject).Text;
            return name;
        }

        //----------------------------------------------------

        public int numberOfContractCounts()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNoOfContract);
            int countContracts = driver.FindElements(valNoOfContract).Count;
            return countContracts;
        }

        public string GetEndDate(int tr)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[id*='M0ecq'] > table > tbody > tr:nth-child({tr}) > td:nth-child(10)"));
            string endDate = driver.FindElement(By.CssSelector($"div[id*='M0ecq'] > table > tbody > tr:nth-child({tr}) > td:nth-child(10)")).Text;
            Console.WriteLine("endDate::::" + endDate);
            return endDate;
        }

        public string GetIsMainContractCheckBoxStatus(int tr)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[id*='M0ecq'] > table > tbody > tr:nth-child({tr}) > td.dataCell.booleanColumn > img"));
            string IsMainContractChkBox = driver.FindElement(By.CssSelector($"div[id*='M0ecq'] > table > tbody > tr:nth-child({tr}) > td.dataCell.booleanColumn > img")).GetAttribute("title");
            return IsMainContractChkBox;
        }

       //To update Engagement contact details
        public string UpdateEngContact(string Name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditContact, 70);
            driver.FindElement(lnkEditContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 70);
            driver.FindElement(txtContact).Clear();
            driver.FindElement(txtContact).SendKeys(Name);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valContact, 90);
            Thread.Sleep(2000);
            string contact = driver.FindElement(valContact).Text;
            return contact;
        }

               
        public void ClickClientLink()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkClient, 180);
            driver.FindElement(lnkClient).Click();
            Thread.Sleep(5000);
        }

        public void ClickContractLink(int row)
        {
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 180);
            driver.FindElement(lnkContract).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 1);
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 4);
                driver.Navigate().Refresh();
            }
        }

        public string ClickICOContractLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 180);
            string contractName = driver.FindElement(lnkContract).Text;
            if (contractName.Contains("ICO"))
            {
                driver.FindElement(lnkContract).SendKeys(Keys.Control + Keys.Return);
                if (row.Equals(2))
                {
                    CustomFunctions.SwitchToWindow(driver, 1);
                    driver.Navigate().Refresh();
                }
                if (row.Equals(3))
                {
                    CustomFunctions.SwitchToWindow(driver, 3);
                    driver.Navigate().Refresh();
                }
                else if (row.Equals(4))
                {
                    CustomFunctions.SwitchToWindow(driver, 5);
                    driver.Navigate().Refresh();
                }
                return "ICO contract created";
            }
            else
            {
                return "NO ICO contract created";
            }
        }

        public string ClickICOContractsLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 180);
            string contractName = driver.FindElement(lnkContract).Text;
            if (contractName.Contains("ICO"))
            {
                driver.FindElement(lnkContract).SendKeys(Keys.Control + Keys.Return);
                if (row.Equals(2))
                {
                    CustomFunctions.SwitchToWindow(driver, 1);
                    driver.Navigate().Refresh();
                }
                else if (row.Equals(3))
                {
                    CustomFunctions.SwitchToWindow(driver, 4);
                    driver.Navigate().Refresh();
                }
                else
                {
                    CustomFunctions.SwitchToWindow(driver, 7);
                    driver.Navigate().Refresh();
                }
                return "ICO contract created";
            }
            else
            {
                return "NO ICO contract created";
            }
        }
        public string getLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity, 90);
            string legalEntity = driver.FindElement(valLegalEntity).Text;
            return legalEntity;
        }

        public void ChangeLegalEntity(string legalEntity)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            if (legalEntity.Equals("HL Capital, Inc."))
            {
                driver.FindElement(txtERPLegalEntity).Clear();
                driver.FindElement(txtERPLegalEntity).SendKeys("HL Consulting, Inc.");
            }
            else if (legalEntity.Equals("HL Consulting, Inc."))
            {
                driver.FindElement(txtERPLegalEntity).Clear();
                driver.FindElement(txtERPLegalEntity).SendKeys("HL (China) Ltd");
            }
            else if (legalEntity.Equals("HL (China) Ltd"))
            {
                driver.FindElement(txtERPLegalEntity).Clear();
                driver.FindElement(txtERPLegalEntity).SendKeys("HL (Australia) Pty Ltd");
            }
            driver.FindElement(btnSave).Click();
        }

        public string GetEnggNumberSuffix()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnggNumberSuffix, 90);
            string valueEnggNumberSuffix = driver.FindElement(valEnggNumberSuffix).Text;
            return valueEnggNumberSuffix;
        }


        public void ClickSecondContractLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkSecondContract, 180);
            driver.FindElement(lnkSecondContract).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 1);
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 4);
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
            }
        }

        public void ClickFirstContractLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 180);
            driver.FindElement(lnkSecondContract).SendKeys(Keys.Control + Keys.Return);

            CustomFunctions.SwitchToWindow(driver, 4);
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
        }

        public void ClickClientLinkForContract(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkClient, 180);
            driver.FindElement(lnkClient).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 2);
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 5);
                driver.Navigate().Refresh();
            }
        }

        public void ClickBillToForContract(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBillTo, 180);
            driver.FindElement(lnkBillTo).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 2);
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 5);
                driver.Navigate().Refresh();
            }
        }

        public string GetERPBusinessUnitId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitId, 60);
            string valueERPBusinessUnitId = driver.FindElement(valERPBusinessUnitId).Text;
            return valueERPBusinessUnitId;
        }

               public string GetERPId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPId, 60);
            string valueERPId = driver.FindElement(valERPId).Text;
            return valueERPId;
        }

        public void CreateContact(string file, string contact, string valRecType, string valType, int rowNumber)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContact, 50);
            driver.FindElement(btnAddOppContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);

            driver.FindElement(txtContact).SendKeys(contact);
            driver.FindElement(comboRole).SendKeys(TestData.ReadExcelData.ReadData(excelPath, "AddContact", 2));
            driver.FindElement(comboType).SendKeys(valType);

            string Type = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowNumber, 4);
            if (Type.Equals("Client"))
            {
                driver.FindElement(checkPrimaryContact).Click();
            }
            if (valRecType.Equals("CF"))
            {
                driver.FindElement(comboParty).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 3));
            }
            if (Type.Equals("External"))
            {
                driver.FindElement(checkPrimaryContact).Click();
                driver.FindElement(checkAckBillingContact).Click();
                driver.FindElement(checkBillingContact).Click();
            }
            driver.FindElement(btnSave).Click();
        }



        public string ValidateWomenLedField(string JobType)
        {
            if (JobType.Contains("ESOP Corporate Finance") || JobType.Contains("General Financial Advisory") || JobType.Contains("Real Estate Brokerage") || JobType.Contains("Special Committee Advisory") || JobType.Contains("Strategic Alternatives Study") || JobType.Contains("Take Over Defense"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedJob, 130);
                string value = driver.FindElement(labelWomenLedJob).Text;
                return value;
            }
            else if (JobType.Equals("Activism Advisory"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedActivism, 125);
                string value = driver.FindElement(labelWomenLedActivism).Text;
                return value;
            }
            else if (JobType.Equals("FA - Portfolio-Advis/Consulting") || JobType.Equals("FA - Portfolio-Auto Loans") || JobType.Equals("FA - Portfolio-Auto Struct Prd") || JobType.Equals("FA - Portfolio-Deriv/Risk Mgmt") || JobType.Equals("FA - Portfolio-Diligence/Assets") || JobType.Equals("FA - Portfolio-Funds Transfer") || JobType.Equals("FA - Portfolio-GP interest") || JobType.Equals("FA - Portfolio-Real Estate") || JobType.Equals("FA - Portfolio-Valuation"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenFVA, 125);
                string value = driver.FindElement(labelWomenFVA).Text;
                return value;
            }
            else if (JobType.Equals("Creditor Advisors") || JobType.Equals("Debtor Advisors") || JobType.Equals("DM&A Buyside") || JobType.Equals("DM&A Sellside") || JobType.Equals("Equity Advisors") || JobType.Equals("PBAS") || JobType.Equals("Liability Mgmt") || JobType.Equals("Regulator/Other"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenFR, 125);
                string value = driver.FindElement(labelWomenFR).Text;
                return value;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLed, 130);
                string value = driver.FindElement(labelWomenLed).Text;
                return value;
            }
        }
        //Get section name of Women Led in Engagement details page
        public string GetSectionNameOfWomenLedField(string JobType)
        {
            if (JobType.Equals("Buyside"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenled, 125);
                string value = driver.FindElement(txtSecWomenled).Text;
                return value;
            }
            else if (JobType.Equals("ESOP Corporate Finance") || JobType.Contains("General Financial Advisory") || JobType.Contains("Real Estate Brokerage") || JobType.Contains("Special Committee Advisory") || JobType.Contains("Strategic Alternatives Study") || JobType.Contains("Take Over Defense"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedESOP, 125);
                string value = driver.FindElement(txtSecWomenLedESOP).Text;
                return value;
            }
            else if (JobType.Equals("Activism Advisory"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedActivism, 125);
                string value = driver.FindElement(txtSecWomenLedActivism).Text;
                return value;
            }
            else if (JobType.Equals("FA - Portfolio-Advis/Consulting") || JobType.Equals("FA - Portfolio-Auto Loans") || JobType.Equals("FA - Portfolio-Auto Struct Prd") || JobType.Equals("FA - Portfolio-Deriv/Risk Mgmt") || JobType.Equals("FA - Portfolio-Diligence/Assets") || JobType.Equals("FA - Portfolio-Funds Transfer") || JobType.Equals("FA - Portfolio-GP interest") || JobType.Equals("FA - Portfolio-Real Estate") || JobType.Equals("FA - Portfolio-Valuation"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedFVA, 125);
                string value = driver.FindElement(txtSecWomenLedFVA).Text;
                return value;
            }
            else if (JobType.Equals("Creditor Advisors") || JobType.Equals("Debtor Advisors") || JobType.Equals("DM&A Buyside") || JobType.Equals("DM&A Sellside") || JobType.Equals("Equity Advisors") || JobType.Equals("PBAS") || JobType.Equals("Liability Mgmt") || JobType.Equals("Regulator/Other"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedFR, 125);
                string value = driver.FindElement(txtSecWomenLedFR).Text;
                return value;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedOther, 120);
                string value = driver.FindElement(txtSecWomenLedOther).Text;
                return value;
            }
        }

    }
}

    



