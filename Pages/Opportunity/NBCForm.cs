using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Opportunity
{
    class NBCForm : BaseClass
    {
        By OppName = By.CssSelector("span[id*='id40']");
        By clientComp = By.XPath("//records-highlights-details-item[1]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");
        By subjectComp = By.XPath("//records-highlights-details-item[3]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");
        By jobType = By.XPath("//records-highlights-details-item[5]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");
        By valOppName = By.XPath("//div[2]/h1/slot/records-formula-output/slot/lightning-formatted-text");
        By lnkEditReviewSub = By.XPath("//flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By lnkEditReviewSub2nd = By.XPath("//flexipage-column2[@class='narrow-region-full-width']/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[2]/button");
        By btnSubmit = By.XPath("(//span[@class='slds-checkbox slds-checkbox_standalone']/input)[4]");
        By btnUpdSubmit = By.XPath("(//span[@class='slds-checkbox slds-checkbox_standalone']/input)[5]");
        By chkNextSchCall = By.XPath("(//span[@class='slds-checkbox slds-checkbox_standalone']/input)[3]");
        By btnSave = By.XPath("//li[2]/runtime_platform_actions-action-renderer/runtime_platform_actions-executor-lwc-headless/slot[1]/slot/lightning-button/button");
        By btnClose = By.XPath("//records-record-edit-error-header/lightning-button-icon/button/lightning-primitive-icon");
        By OppOverviewTab = By.XPath("//lightning-tab-bar/ul/li[@title='Opportunity Overview']");
        By FinancialsTab = By.XPath("//lightning-tab-bar/ul/li[@title='Financials']");
        By FeesTab = By.XPath("//lightning-tab-bar/ul/li[@title='Fees']");
        By PitchTab = By.XPath("//lightning-tab-bar/ul/li[@title='Pitch']");
        By FairnessTab = By.XPath("//lightning-tab-bar/ul/li[@title='Fairness/Admin Checklist']");
        By PublicTab = By.XPath("//lightning-tab-bar/ul/li[@title='Public Sensitivity']");
        By HLInternalTab = By.XPath("//lightning-tab-bar/ul/li[@title='HL Internal Team']");

        By lnkEditOppOverviewTab = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By msgTransOverview = By.XPath("//lightning-textarea/label[text()='Transaction Overview']/following::div[3]");
        By msgTotalDebt = By.XPath("//label[text()='Total Debt (MM)']/following::div[2]");
        By msgEstValuation= By.XPath("//label[text()='Estimated Valuation (MM)']/following::div[3]");
        By msgCurrentStatus = By.XPath("//label[text()='Current Status']/following::div[7]");
        By msgValuationExp = By.XPath("//label[text()='Valuation Expectations']/following::div[3]");
        By msgCompanyDesc = By.XPath("//label[text()='Company Description']/following::div[3]");
        By msgRealEstate = By.XPath("//label[text()='Real Estate Angle']/following::div[6]");
        By msgOwnershipStr = By.XPath("//label[text()='Ownership and Capital Structure']/following::div[3]");
        By msgAsiaAngle = By.XPath("//label[text()='Asia Angle']/following::div[6]");
        By msgRiskFact = By.XPath("//label[text()='Risk Factors']/following::div[3]");
        By msgInterAngle = By.XPath("//label[text()='International Angle?']/following::div[6]");

        By msgCapMkt = By.XPath("//label[text()='Capital Markets Consulted']/following::div[7]");
        By msgExistingFin = By.XPath("//label[text()='Existing Financial Arrangement Notes']/following::div[3]");
        By msgFinSubject = By.XPath("//label[text()='Financials Subject to Audit']/following::div[6]");
        By msgNoFin = By.XPath("//label/span[text()='No Financials']/following::div[2]");

        By msgRetainer = By.XPath("//label[text()='Retainer']/following::div[2]");
        By msgRetainerFee = By.XPath("//label[text()='Retainer Fee Creditable ?']/following::div[3]");
        By msgProgressFee = By.XPath("//label[text()='Progress Fee Creditable ?']/following::div[3]");
        By msgTxnFeeType = By.XPath("//label[text()='Transaction Fee Type']/following::div[6]");
        

        By msgWillThere = By.XPath("//label[text()='Will There Be a Pitch?']/following::div[6]");
        By msgHLComp = By.XPath("//label[text()='Houlihan Lokey Competition']/following::div[2]");
        By msgLockups = By.XPath("//label[text()='Lockups on Future M&A or Financing Work']/following::div[6]");
        By msgExistingRel = By.XPath("//label[text()='Existing Relationships']/following::div[6]");
        By msgExistingOrRepeat = By.XPath("//label[text()='Existing or Repeat Client?']/following::div[6]");
        By msgTAS = By.XPath("//label[text()='TAS/Bridge Assistance Benefit?']/following::div[7]");
        By msgOutside = By.XPath("//label[text()='Outside Council']/following::div[2]");

        By msgFairnessOpinion = By.XPath("//label[text()='Fairness Opinion Provided']/following::div[7]");

        By msgA = By.XPath("//label[text()='A']/following::div[6]");
        By msgB = By.XPath("//label[text()='B']/following::div[6]");
        By msgC = By.XPath("//label[text()='C']/following::div[6]");
        By msgD = By.XPath("//label[text()='D']/following::div[6]");
        By msgGroupHead = By.XPath("//label/span[text()='Group Head Approval']/following::div[2]");

        By txtTxnOverview = By.XPath("//lightning-textarea/label[text()='Transaction Overview']/following::textarea[1]");
        By txtTotalDebt = By.XPath("//label[text()='Total Debt (MM)']/following::input[1]");
        By txtEstValuation = By.XPath("//label[text()='Estimated Valuation (MM)']/following::input[1]");
        By btnCurrentStatus = By.XPath("//label[text()='Current Status']/following::button[2]");
        By comboCurrentStatus = By.XPath("//label[text()='Current Status']/following::lightning-base-combobox-item/span[2]/span[text()='Pitched']");
        By txtValuationExp= By.XPath("//label[text()='Valuation Expectations']/following::textarea");
        By txtCompanyDesc = By.XPath("//label[text()='Company Description']/following::textarea[1]");
        By comboRealEstate = By.XPath("//label[text()='Real Estate Angle']/following::button[1]");
        By txtOwnership = By.XPath("//label[text()='Ownership and Capital Structure']/following::textarea[1]");
        By comboAsia = By.XPath("//label[text()='Asia Angle']/following::button[1]");
        By txtRisk = By.XPath("//label[text()='Risk Factors']/following::textarea[1]");
        By comboInternational = By.XPath("(//lightning-base-combobox)[3]");

        By btnCapMkt = By.XPath("//label[text()='Capital Markets Consulted']/following::button[2]");
        By txtExistingFin = By.XPath("//label[text()='Existing Financial Arrangement Notes']/following::textarea[1]");
        By btnFinSubject = By.XPath("(//lightning-base-combobox)[7]");
        By txtFinAuditNotes = By.XPath("//label[text()='Financials Audit Notes']/following::textarea[1]");
        By chkNoFin = By.XPath("//span[text()='No Financials']/following::input[1]");
        By txtNoFinExp = By.XPath("//label[text()='No Financials Explanation']/following::textarea[1]");

        By txtRetainer = By.XPath("//*[@name='Retainer1__c']");
        By txtRetainerFeeCred = By.XPath("//input[@name='Retainer_Creditable__c']");
        By txtProgressFee = By.XPath("//input[@name='Is_Progress_Fee_Creditable__c']");
        By btnTxnFeeType = By.XPath("(//lightning-base-combobox)[8]");
        By txtReferralFee = By.XPath("//label[text()='Referral Fee Owed (MM)']/following::div[1]/input");

        By btnWillThere = By.XPath("//label[text()='Will There Be a Pitch?']/following::button[1]");
        By txtHLComp = By.XPath("//label[text()='Houlihan Lokey Competition']/following::textarea[1]");
        By btnLockups = By.XPath("//label[text()='Lockups on Future M&A or Financing Work']/following::button[1]");
        By btnExistingRel = By.XPath("//label[text()='Existing Relationships']/following::button[1]");
        By btnExistingClient = By.XPath("//label[text()='Existing or Repeat Client?']/following::button[1]");
        By btnTAS = By.XPath("//label[text()='TAS/Bridge Assistance Benefit?']/following::button[2]");
        By txtOutsideCouncil = By.XPath("//label[text()='Outside Council']/following::textarea[1]");

        By btnFairnessOpinion = By.XPath("//label[text()='Fairness Opinion Provided']/following::button[2]");
        By tabAdmin = By.XPath("//*[@id='flexipage_tab8__item']");
        By btnRestricted = By.XPath("//label[text()='Restricted List']/following::button[1]");
        By btnYes1 = By.XPath("(//lightning-base-combobox)[17]");
        By btnYes2 = By.XPath("(//lightning-base-combobox)[18]");
        By btnYes3 = By.XPath("(//lightning-base-combobox)[19]");
        By btnYes4 = By.XPath("(//lightning-base-combobox)[20]");
        By btnYes5 = By.XPath("(//lightning-base-combobox)[21]");

        By btnA = By.XPath("//label[text()='A']/following::button[1]");
        By btnB = By.XPath("//label[text()='B']/following::button[1]");
        By btnC = By.XPath("//label[text()='C']/following::button[1]");
        By btnD = By.XPath("(//lightning-base-combobox)[25]");
        By chkGroupHead = By.XPath("//span[text()='Group Head Approval']/following::input[1]");

        By lblRelOpp = By.XPath("//div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[1]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblTxnOver= By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordRelated_Opportunity_cField1']/slot[1]/following::span[1]");
        By lblCurrentStatus = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordTransaction_Overview__cField']/slot[1]/following::span[1]");
        By lblCompDesc = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordStatus_cField1']/slot[1]/following::span[1]");
        By lblOwnerCapStr = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordCompany_Description__cField']/slot[1]/following::span[1]");
        By lblRiskFact = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordOwnership_and_Capital_Structure__cField']/slot[1]/following::span[1]");
        By lblIntAngle = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordRisk_Factors_cField1']/slot[1]/following::span[1]");
        By lblTotalDebtMM = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblEstimatedValuation = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordTotal_Debt_MM_cField2']/slot[1]/following::span[1]");
        By lblValExp = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordEstimated_Valuation_cField2']/slot[1]/following::span[1]");
        By lblRealEstate = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordValuation_Expectations_cField1']/slot[1]/following::span[1]");
        By lblAsiaAngle = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordReal_Estate_Angle_cField1']/slot[1]/following::span[1]");

        By msgCapMarket = By.XPath("//span[@title='Has the Capital Markets Group been Consulted regarding financing or capital structure?']");
        By lblCapMkt = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblExistFin = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordCapital_Markets_Consulted_cField2']/slot[1]/following::span[1]");
        By msgAboveFin = By.XPath("//span[text()='Have the above financials been subject to an audit?']");
        By lblFinSub = By.XPath("//flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By msgFinAvail = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordFinancials_Subject_to_Audit_cField1']/slot[1]/following::span[1]");
        By lblNoFin = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[4]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblNoFinExp = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordNo_Financials_cField2']/slot[1]/following::span[1]");

        By lblRetainer = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblProgressFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordRetainer1_cField1']/slot[1]/following::span[1]");
        By lblMinFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordProgress_Fee_cField1']/slot[1]/following::span[1]");
        By lblTxnFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordEstimated_Minimum_Fee_cField1']/slot[1]/following::span[1]");
        By lblRetainerFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordTransaction_Fee_Type_cField1']/slot[1]/following::span[1]");
        By lblProgFeeCredit = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordRetainer_Creditable_cField1']/slot[1]/following::span[1]");

        By secPrePitch = By.XPath("//span[@title='Pre-Pitch']");
        By lblWillThere = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblHLComp = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordWill_there_be_a_pitch_cField1']/slot[1]/following::span[1]");
        By lblExistingRel = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordHoulihan_Lokey_Competition_cField1']/slot[1]/following::span[1]");
        By lblExisting = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordExisting_Relationships_cField1']/slot[1]/following::span[1]");
        By lblWhoAre = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordExisting_or_Repeat_Client_cField1']/slot[1]/following::span[1]");
        By lblLockups = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secWould = By.XPath("//span[@title='Would the opportunity benefit from TAS Assistance?']");
        By lblTAS = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field/following::span[1]");
        By secIfKnown = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordTAS_Assistance_Benefit_cField1']/slot[1]/following::span[1]");
        By lblOutside = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secReferral = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordOutsideCouncil_cField1']/slot[1]/following::span[1]");
        By lblRefType = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[4]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblRefSource = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordReferral_Type_cField1']/slot[1]/following::span[1]");

        By secIsPotential = By.XPath("//span[@title='Is there a potential Fairness Opinion component to this assignment?']");
        By lblFairnessOpinion = By.XPath("//flexipage-field[@data-field-id='RecordFairness_Opinion_Provided_cField4']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By secRestricted = By.XPath("//slot/flexipage-tab2[2]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblRestictedList = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secCCInfo = By.XPath("//span[@title='Conflicts Check Information - (the answers to each of these questions must be verified with each member of the deal team)']");
        By lblCCStatus = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflict_Check_Status_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secAreThereAnyPitch = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText6']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl1stYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_2a_Not_Listed_cField3']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secHaveAny1 = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText15']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl2ndYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_3a_Related_to_Transaction_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secHaveAny2 = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText16']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl3rdYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_35a_Related_to_Client_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secHaveAny3 = By.XPath("//flexipage-component2[9]/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl4thYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_4a_Conflict_of_Interest_cField3']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secHaveAny4 = By.XPath("//flexipage-component2[11]/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl5thYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_5a_Other_Conflicts_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By secA = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lblA = By.XPath("//flexipage-field[@data-field-id='RecordA_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secB = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText2']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lblB = By.XPath("//flexipage-field[@data-field-id='RecordB_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secC = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText3']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lblC = By.XPath("//flexipage-field[@data-field-id='RecordC_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secD = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText4']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lblD = By.XPath("//flexipage-field[@data-field-id='RecordD_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secPlsConfirm = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection15']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblGroupHead = By.XPath("//flexipage-field[@data-field-id='RecordHead_Approval_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By lblStaff = By.XPath("//tr[2]/td/div/label");
        By btnSaveITTeam = By.XPath("/html/body/span[2]/form/div[1]/div/div[1]/table/tbody/tr/td[2]/span/input[1]");       
        By btnReturnToOpp =By.XPath("/html/body/span[2]/form/div[1]/div/div[1]/table/tbody/tr/td[2]/span/input[2]");
        By btnRoleDef =    By.XPath("/html/body/span[2]/form/div[1]/div/div[1]/table/tbody/tr/td[2]/span/input[3]");

        By btnSubmitForReview = By.XPath("//lightning-button/button[text()='Submit for Review']");
        By valOppNum = By.XPath("//html/body/p[10]/span");
        By errorList = By.CssSelector("#j_id0\\:NBCForm\\:j_id2\\:j_id3\\:j_id4\\:0\\:j_id5\\:j_id6\\:j_id18 > ul");
        By btnCancel = By.CssSelector("input[value='Cancel Submission']");
        By checkToggleTabs = By.Id("toggleTabs");
        By tabList = By.Id("tabsList");
        By comboFinancialOpinion = By.CssSelector("select[id*='MajoritySale']");
        By checkConfirm = By.CssSelector("input[id*='HeadApproval']");
        By txtTranOverview = By.CssSelector("textarea[name*='id78']");
        By txtHLCompPG = By.CssSelector("textarea[name*='id75']");
        //By txtCurrentStatus = By.CssSelector("textarea[name*='id80']");
        //By txtCompDesc = By.CssSelector("textarea[name*='id82']");
        By comboCrossBorder = By.CssSelector("select[name*='InternationalAngle']");
        By comboAsiaAngle = By.CssSelector("select[name*='AsiaAngle']");
        //By comboRealEstate = By.CssSelector("select[name*='RealEstateAngle']");
        By txtOwnershipStr = By.CssSelector("textarea[name*='id104']");
        //By txtTotalDebt = By.CssSelector("input[name*='TotalDebt']");
        By comboAudit = By.CssSelector("select[name*='FinAudit01']");
        By txtEstVal = By.CssSelector("input[name*='estValu']");
        //By txtValExp = By.CssSelector("textarea[name *= 'id153']");
        By txtRiskFactors = By.CssSelector("textarea[name*='id157']");
        By txtEstFee = By.CssSelector("input[name*='estMinFee']");
        By txtFeeStr = By.CssSelector("textarea[name*= 'id247']");
        By comboLockUps = By.CssSelector("select[name*= 'id251']");
        By comboReferralFee = By.CssSelector("select[name*= 'id256']");
        By comboPitch = By.CssSelector("select[name*= 'Pitch00']");
        By comboClient = By.CssSelector("select[id='j_id0:NBCForm:j_id31:j_id260:Exist']");
        //By txtHLComp = By.CssSelector("textarea[name*= 'id273']");
        By comboExistingRel = By.CssSelector("select[name*= 'ExistingRel']");
        By comboTASAssist = By.CssSelector("select[name*= 'TAS00']");
        //By txtOutsideCouncil = By.CssSelector("textarea[name*= 'OutsideCouncil']");
        //By comboCapMkt = By.CssSelector("select[name*= 'id311']");
        By txtSum = By.CssSelector("textarea[name*= 'id314']");
        By comboFairness = By.CssSelector("select[name*= 'Fairness']");
        By comboResList = By.CssSelector("select[name*= 'RestrictedList']");
        By comboCCStatus = By.CssSelector("select[name*= 'Conflicts2a']");
        By comboCCStatus1 = By.CssSelector("select[name*= 'Conflicts3a']");
        By comboCCStatus2 = By.CssSelector("select[name*= 'Conflicts35a']");
        By comboCCStatus3 = By.CssSelector("select[name*= 'Conflicts4a']");
        By comboCCStatus4 = By.CssSelector("select[name*= 'Conflicts5a']");
        By titleEmailPage = By.XPath("//div[@class='pbHeader']/table/tbody/tr/td/h2[@class='mainTitle']");
        By valEmailOppName = By.CssSelector("body[id*='Body_rta_body'] > span:nth-child(10) > span");
        By btnCancelEmail = By.XPath("//div[@class='pbHeader']/table/tbody/tr/td/input[@value='Cancel']");
        By btnReturntoOpp = By.CssSelector("input[value*='Return to Opportunity']");
        By btnReturntoOppCFUser = By.CssSelector("span[id*=':j_id34'] > a");
        By statusCC = By.CssSelector("div[id*='tabs-6']>div>div[class='pbSubsection']>table>tbody>tr:nth-child(4)>td>span>table>tbody>tr>td:nth-child(2)>span");
        By valFinOption = By.CssSelector("select[id*='MajoritySale']>option[selected='selected']");
        By txtComment = By.CssSelector("span[id*='MajoritySale01']");
        By tblSuggestedFee = By.CssSelector("span[id*='j_id163'] >table");
        By btnEUOverride = By.CssSelector("span[id*='1:j_id33'] > input[id*='euOverride']");
        By txtEUComment = By.CssSelector("tr[id *= 'MajoritySale02'] > td");
        By tblEUFee = By.CssSelector("span[id*='j_id218'] > table");
        By lblReview = By.CssSelector("div[id*='tabs-7']>div>div>h3");
        By comboGrade = By.CssSelector("select[id*='reviewGrade']");
        By valGrade = By.CssSelector("select[id*='reviewGrade']>option[selected='selected']");
        By txtNotes = By.CssSelector("textarea[id*='reviewNotes']");
        By txtDateSubmitted = By.CssSelector("input[id*='dateSubmit']");
        By txtReason = By.CssSelector("textarea[id*='reasonWonLost']");
        By txtFeeDiff = By.CssSelector("textarea[id*='feeDiff']");
        By btnPDFView = By.XPath("//a[contains(text(),'PDF View')]");
        By btnAttachFile = By.CssSelector("button[type='button']");
        By btnAddFinancials = By.CssSelector("input[id*='newFinancials']");
        //Validate Opp Name
        public string ValidateOppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppName, 50);
            string valOpp = driver.FindElement(valOppName).Text;
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

        //Click Opportunity Overview tab
        public string ClickOpportunityOverview()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, OppOverviewTab,80);
            driver.FindElement(OppOverviewTab).Click();
            string valTab = driver.FindElement(OppOverviewTab).Text;
            return valTab;
        }

        //Click Financials tab
        public string ClickFinancialsTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, FinancialsTab, 80);
            driver.FindElement(FinancialsTab).Click();
            string valTab = driver.FindElement(FinancialsTab).Text;
            return valTab;
        }

        //Click Fees tab
        public string ClickFeesTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, FeesTab, 80);
            driver.FindElement(FeesTab).Click();
            string valTab = driver.FindElement(FeesTab).Text;
            return valTab;
        }

        //Click Pitch tab
        public string ClickPitchTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, PitchTab, 80);
            driver.FindElement(PitchTab).Click();
            string valTab = driver.FindElement(PitchTab).Text;
            return valTab;
        }

        //Click Fairness/Admin Checklist tab
        public string ClickFairnessAdminChecklistTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, FairnessTab, 80);
            driver.FindElement(FairnessTab).Click();
            string valTab = driver.FindElement(FairnessTab).Text;
            return valTab;
        }

        //Click Administrative tab
        public string ClickAdministrativeTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabAdmin, 150);
            driver.FindElement(tabAdmin).Click();
            string valTab = driver.FindElement(tabAdmin).Text;
            return valTab;
        }

        //Click Public Sensitivity tab
        public string ClickPublicSensitivityTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, PublicTab, 80);
            driver.FindElement(PublicTab).Click();
            string valTab = driver.FindElement(PublicTab).Text;
            return valTab;
        }

        //Click HL Internal Team tab
        public string ClickHLInternalTeamTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, HLInternalTab, 80);
            driver.FindElement(HLInternalTab).Click();
            string valTab = driver.FindElement(HLInternalTab).Text;
            return valTab;
        }

        //Click Review Submission button
        public void ClickReviewSubmission()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditReviewSub, 100);
            driver.FindElement(lnkEditReviewSub).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,50)");
            Thread.Sleep(6000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnSubmit, 110);
            driver.FindElement(btnSubmit).Click();
            Console.WriteLine("Submit clicked once");            
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
        }

        //Update Review Submission and Referral Fee Owned
        public void UpdateReviewSubmissionAndUpdateReferralFee()
        {
            Thread.Sleep(2000);            
            driver.FindElement(btnUpdSubmit).Click();
            Console.WriteLine("Submit clicked once");
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,120)");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditReviewSub2nd, 100);
            driver.FindElement(lnkEditReviewSub2nd).Click();
            Thread.Sleep(6000);            
            driver.FindElement(btnUpdSubmit).Click();
            Thread.Sleep(3000);
            //driver.FindElement(btnUpdSubmit).Click();
            //Thread.Sleep(4000);
            driver.FindElement(FeesTab).Click();
            Thread.Sleep(3000);           
            js.ExecuteScript("window.scrollTo(0,450)");
            WebDriverWaits.WaitUntilEleVisible(driver, txtReferralFee, 200);
            driver.FindElement(txtReferralFee).SendKeys("10");
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-550)");
            Thread.Sleep(3000);
            driver.FindElement(chkNextSchCall).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
        }

        //Update Administrative tab
        public void UpdateAdministrativeTab(string file)
        {
            Thread.Sleep(2000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm", 2);
            driver.FindElement(btnYes1).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[17]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(3000);
            js.ExecuteScript("window.scrollTo(0,600)");
            driver.FindElement(btnYes2).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[18]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,800)");
            driver.FindElement(btnYes3).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[19]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,1050)");
            driver.FindElement(btnYes4).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[20]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,1300)");
            driver.FindElement(btnYes5).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[21]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();            
        }

        //Click Submit button
        public string ClickSubmitButton()
        {
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-2500)");
            Thread.Sleep(4000);           
            driver.FindElement(btnSubmitForReview).Click();
            Thread.Sleep(9000);
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string title = driver.FindElement(titleEmailPage).Text;
            return title;
        }

        public string GetOpportunityName()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@class='cke_wysiwyg_frame cke_reset']")));
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valOppNum, 112);
            string emailSub = driver.FindElement(valOppNum).Text;
            Console.WriteLine(emailSub);
            driver.SwitchTo().DefaultContent();
            driver.Close();
            Thread.Sleep(5000);
            return emailSub;
        }

        //Get Validations of Opportunity Overview tab
        public string GetFieldsValidationsOfOppOverview()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgTransOverview, 130);
            string value =driver.FindElement(msgTransOverview).Text;
            return value;
        }

        //Get Validation of Total Debt
        public string GetValidationOfTotalDebt()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, msgTotalDebt, 80);
            string value = driver.FindElement(msgTotalDebt).Text;
            return value;
        }

        //Get Validation of Estimated Valuation
        public string GetValidationOfEstVal()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEstValuation, 80);
            string value = driver.FindElement(msgEstValuation).Text;
            return value;
        }

        //Get Validation of Current Status
        public string GetValidationOfCurrentStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgCurrentStatus, 80);
            string value = driver.FindElement(msgCurrentStatus).Text;
            return value;
        }

        //Get Validation of Valuation Expectations
        public string GetValidationOfValuationExp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgValuationExp, 80);
            string value = driver.FindElement(msgValuationExp).Text;
            return value;
        }

        //Get Validation of Company Description
        public string GetValidationOfCompDesc()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgCompanyDesc, 80);
            string value = driver.FindElement(msgCompanyDesc).Text;
            return value;
        }

        //Get Validation of Real Estate Angle
        public string GetValidationOfRealEstateAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgRealEstate, 80);
            string value = driver.FindElement(msgRealEstate).Text;
            return value;
        }

        //Get Validation of Ownership and Capital Structure
        public string GetValidationOfOwnershipAndCapStr()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgOwnershipStr, 80);
            string value = driver.FindElement(msgOwnershipStr).Text;
            return value;
        }
        //Get Validation of Asia Angle
        public string GetValidationOfAsiaAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgAsiaAngle, 80);
            string value = driver.FindElement(msgAsiaAngle).Text;
            return value;
        }
         
        //Get Validation of Risk Factors
        public string GetValidationOfRiskFactors()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgRiskFact, 80);
            string value = driver.FindElement(msgRiskFact).Text;
            return value;
        }
        //Get Validation of International Angle
        public string GetValidationOfInternationalAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgInterAngle, 80);
            string value = driver.FindElement(msgInterAngle).Text;
            return value;
        }

        //Get Validation of Capital Markets Consulted
        public string GetValidationOfCapMarketConsulted()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgCapMkt, 80);
            string value = driver.FindElement(msgCapMkt).Text;
            return value;
        }

        //Get Validation of Existing Financial Arrangement Notes
        public string GetValidationOfExistingFin()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgExistingFin, 80);
            string value = driver.FindElement(msgExistingFin).Text;
            return value;
        }

        //Get Validation of Financials Subject to Audit
        public string GetValidationOfFinancialsSubject()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgFinSubject, 80);
            string value = driver.FindElement(msgFinSubject).Text;
            return value;
        }

        //Get Validation of No Financials 
        public string GetValidationOfNoFinancials()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgNoFin, 80);
            string value = driver.FindElement(msgNoFin).Text;
            return value;
        }

        //Get Validation of Retainer 
        public string GetValidationOfRetainer()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgRetainer, 80);
            string value = driver.FindElement(msgRetainer).Text;
            return value;
        }

        //Get Validation of Retainer Fee 
        public string GetValidationOfRetainerFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgRetainerFee, 80);
            string value = driver.FindElement(msgRetainerFee).Text;
            return value;
        }

        //Get Validation of Progress Fee 
        public string GetValidationOfProgressFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgProgressFee, 80);
            string value = driver.FindElement(msgProgressFee).Text;
            return value;
        }

        //Get Validation of Transaction Fee 
        public string GetValidationOfTxnFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgTxnFeeType, 80);
            string value = driver.FindElement(msgTxnFeeType).Text;
            return value;
        }

        //Get Validation of Will There Be a Pitch 
        public string GetValidationOfPitch()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgWillThere, 80);
            string value = driver.FindElement(msgWillThere).Text;
            return value;
        }

        //Get Validation of HL Competition 
        public string GetValidationOfHLComp()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLComp, 80);
            string value = driver.FindElement(msgHLComp).Text;
            return value;
        }

        //Get Validation of Lockups on Future M&A or Financing Work
        public string GetValidationOfLockups()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgLockups, 80);
            string value = driver.FindElement(msgLockups).Text;
            return value;
        }
        //Get Validation of Existing Relationships
        public string GetValidationOfExistingRel()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgExistingRel, 80);
            string value = driver.FindElement(msgExistingRel).Text;
            return value;
        }

        //Get Validation of Existing or Repeat Client
        public string GetValidationOfExistingOrRepeatClient()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgExistingOrRepeat, 80);
            string value = driver.FindElement(msgExistingOrRepeat).Text;
            return value;
        }

        //Get Validation of TAS/Bridge Assistance Benefit
        public string GetValidationOfTASBridgeAssist()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgTAS, 80);
            string value = driver.FindElement(msgTAS).Text;
            return value;
        }

        //Get Validation of Outside Council
        public string GetValidationOfOutsideCouncil()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgOutside, 80);
            string value = driver.FindElement(msgOutside).Text;
            return value;
        }

        //Get Validation of Fairness Opinion
        public string GetValidationOfFairnessOpinion()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgFairnessOpinion, 80);
            string value = driver.FindElement(msgFairnessOpinion).Text;
            return value;
        }

        //Get Validation of A
        public string GetValidationOfA()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgA, 80);
            string value = driver.FindElement(msgA).Text;
            return value;
        }

        //Get Validation of B
        public string GetValidationOfB()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgB, 80);
            string value = driver.FindElement(msgB).Text;
            return value;
        }
        //Get Validation of C
        public string GetValidationOfC()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgC, 80);
            string value = driver.FindElement(msgC).Text;
            return value;
        }
        //Get Validation of D
        public string GetValidationOfD()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgD, 80);
            string value = driver.FindElement(msgD).Text;
            return value;
        }

        //Get Validation of Group Head Approval
        public string GetValidationOfGroupHeadApproval()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgGroupHead, 80);
            string value = driver.FindElement(msgGroupHead).Text;
            return value;
        }

        //Save all required details in Opportunity Overview tab
        public void SaveAllReqFieldsInOppOverview(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtTxnOverview).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 3));
            driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 9));
            driver.FindElement(txtEstValuation).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 11));
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,80)");
            Thread.Sleep(4000);
            driver.FindElement(txtValuationExp).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 12));
            driver.FindElement(btnCurrentStatus).Click();
            string name = ReadExcelData.ReadData(excelPath, "NBCForm", 4);
            driver.FindElement(By.XPath("//label[text()='Current Status']/following::lightning-base-combobox-item/span[2]/span[text()='" + name + "']")).Click();
            driver.FindElement(txtCompanyDesc).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 5));
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(4000);
            string real = ReadExcelData.ReadData(excelPath, "NBCForm", 7);
            driver.FindElement(comboRealEstate).Click();
            driver.FindElement(By.XPath("//label[text()='Real Estate Angle']/following::lightning-base-combobox-item/span[2]/span[text()='" + real + "']")).Click();
            driver.FindElement(txtOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 8));
            js.ExecuteScript("window.scrollTo(0,380)");
            Thread.Sleep(4000);
            driver.FindElement(comboAsia).Click();
            driver.FindElement(By.XPath("//label[text()='Asia Angle']/following::lightning-base-combobox-item/span[2]/span[text()='" + real + "']")).Click();
            driver.FindElement(txtRisk).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 13));
            js.ExecuteScript("window.scrollTo(0,450)");
            Console.WriteLine("about to enter int angle ");
            Thread.Sleep(5000);
            driver.FindElement(comboInternational).Click();
            driver.FindElement(comboInternational).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='International Angle?']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-200)"); 
        }

        //Save all required details in Financial Overview tab
        public void SaveAllReqFieldsInFinancialsOverview(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;            
            driver.FindElement(btnCapMkt).Click();
            string name = ReadExcelData.ReadData(excelPath, "NBCForm", 7);
            driver.FindElement(By.XPath("//label[text()='Capital Markets Consulted']/following::div[6]/lightning-base-combobox-item/span[2]/span[text()='"+name+"']")).Click();
            driver.FindElement(txtExistingFin).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 3));
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(4000);            
            driver.FindElement(btnFinSubject).Click();
            driver.FindElement(btnFinSubject).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='Financials Subject to Audit']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtFinAuditNotes).SendKeys("Testing");
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(4000);
            driver.FindElement(chkNoFin).Click();
            driver.FindElement(chkNoFin).Click();
            Thread.Sleep(5000);
            js.ExecuteScript("window.scrollTo(0,250)");
            driver.FindElement(txtNoFinExp).SendKeys("Testing");
            Thread.Sleep(3000);              
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Save all required details in Fees tab
        public void SaveAllReqFieldsInFees(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 9);
            driver.FindElement(txtRetainer).SendKeys(fee);
            driver.FindElement(txtRetainerFeeCred).SendKeys(fee);
            driver.FindElement(txtProgressFee).SendKeys(fee);
            js.ExecuteScript("window.scrollTo(0,350)");
            driver.FindElement(btnTxnFeeType).Click();           
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//label[text()='Transaction Fee Type']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Save all required details in Pitch tab
        public void SaveAllReqFieldsInPitch(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string text = ReadExcelData.ReadData(excelPath, "NBCForm", 3);
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 10);
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm",2);
            driver.FindElement(btnWillThere).Click();           
            driver.FindElement(By.XPath("//label[text()='Will There Be a Pitch?']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + fee + "']")).Click();
            driver.FindElement(txtHLComp).SendKeys(text);
            driver.FindElement(btnLockups).Click();
            driver.FindElement(By.XPath("//label[text()='Lockups on Future M&A or Financing Work']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(3000);
            driver.FindElement(btnExistingRel).Click();           
            driver.FindElement(By.XPath("(//lightning-base-combobox)[10]/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            driver.FindElement(btnExistingClient).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[11]/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            js.ExecuteScript("window.scrollTo(0,350)");
            driver.FindElement(btnTAS).Click();
            driver.FindElement(By.XPath("//label[text()='TAS/Bridge Assistance Benefit?']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + fee + "']")).Click();
            driver.FindElement(txtOutsideCouncil).SendKeys(text);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Save all required details in Fairness/AdminChecklist tab
        public void SaveAllReqFieldsInFairnessAdminChecklist(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;           
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm", 2);
            driver.FindElement(btnFairnessOpinion).Click();
            driver.FindElement(By.XPath("//label[text()='Fairness Opinion Provided']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            driver.FindElement(tabAdmin).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnRestricted).Click();
            driver.FindElement(By.XPath("//label[text()='Restricted List']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Save all required details in Public Senstivity tab
        public void SaveAllReqFieldsInPublicSenstivity(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm", 2);
            driver.FindElement(btnA).Click();
            driver.FindElement(By.XPath("//label[text()='A']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            js.ExecuteScript("window.scrollTo(0,280)");
            driver.FindElement(btnB).Click();
            driver.FindElement(By.XPath("//label[text()='B']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(0,400)");
            driver.FindElement(btnC).Click();
            driver.FindElement(By.XPath("//label[text()='C']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(3000);
            js.ExecuteScript("window.scrollTo(0,600)");
            Thread.Sleep(2000);
            driver.FindElement(btnD).Click();
            driver.FindElement(By.XPath("//label[text()='D']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(3000);
            js.ExecuteScript("window.scrollTo(0,800)");
            Thread.Sleep(3000);
            driver.FindElement(chkGroupHead).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Fetch the label of Related Opportunity
        public string GetLabelRelatedOpportunity()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblRelOpp, 100);
            string text = driver.FindElement(lblRelOpp).Text;
            return text;
        }

        //Fetch the label of Transaction Overview
        public string GetLabelTxnOverview()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTxnOver, 100);
            string text = driver.FindElement(lblTxnOver).Text;
            return text;
        }
        //Fetch the label of Current Status
        public string GetLabelCurrentStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCurrentStatus, 100);
            string text = driver.FindElement(lblCurrentStatus).Text;
            return text;
        }
        //Fetch the label of Company Description
        public string GetLabelCompDesc()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompDesc, 100);
            string text = driver.FindElement(lblCompDesc).Text;
            return text;
        }
        //Fetch the label of Ownership and Capital Sructure
        public string GetLabelOnwershipStructure()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOwnerCapStr, 100);
            string text = driver.FindElement(lblOwnerCapStr).Text;
            return text;
        }

        //Fetch the label of Risk Factors
        public string GetLabelRiskFactors()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRiskFact, 100);
            string text = driver.FindElement(lblRiskFact).Text;
            return text;
        }

        //Fetch the label of International Angle
        public string GetLabelIntAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblIntAngle, 100);
            string text = driver.FindElement(lblIntAngle).Text;
            return text;
        }

        //Fetch the label of Total Debt (MM)
        public string GetLabelTotalDebtMM()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTotalDebtMM, 100);
            string text = driver.FindElement(lblTotalDebtMM).Text;
            return text;
        }

        //Fetch the label of Estimated Valuation
        public string GetLabelExtVal()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblEstimatedValuation, 100);
            string text = driver.FindElement(lblEstimatedValuation).Text;
            return text;
        }

        //Fetch the label of Valuation Expectations
        public string GetLabelValExp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblValExp, 100);
            string text = driver.FindElement(lblValExp).Text;
            return text;
        }

        //Fetch the label of Real Estate Angle
        public string GetLabelRealEstAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRealEstate, 100);
            string text = driver.FindElement(lblRealEstate).Text;
            return text;
        }

        //Fetch the label of Asia Angle
        public string GetLabelAsiaAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblAsiaAngle, 100);
            string text = driver.FindElement(lblAsiaAngle).Text;
            return text;
        }

        //Fetch message of Capital Market
        public string GetMessageCapitalMarket()
            {
            
            WebDriverWaits.WaitUntilEleVisible(driver, msgCapMarket, 100);
            string text = driver.FindElement(msgCapMarket).Text;
            return text;
            }

        //Fetch the label of Capital Market Consulted
        public string GetLabelCapMktConsult()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCapMkt, 100);
            string text = driver.FindElement(lblCapMkt).Text;
            return text;
        }

        //Fetch the label of Existing Financial Arrangement Notes
        public string GetLabelExistingFinNotes()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistFin, 100);
            string text = driver.FindElement(lblExistFin).Text;
            return text;
        }

        //Fetch message of Above Financials
        public string GetMessageFinSubToAudit()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, msgAboveFin, 100);
            string text = driver.FindElement(msgAboveFin).Text;
            return text;
        }

        //Fetch the label of Financials Subject to Audit
        public string GetLabelFinSubToAudit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinSub, 100);
            string text = driver.FindElement(lblFinSub).Text;
            return text;
        }

        //Fetch message of Financials Unavailable
        public string GetMessageFinUnavailable()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, msgFinAvail, 100);
            string text = driver.FindElement(msgFinAvail).Text;
            return text;
        }

        //Fetch the label of No Financials 
        public string GetLabelNoFin()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNoFin, 100);
            string text = driver.FindElement(lblNoFin).Text;
            return text;
        }

        //Fetch the label of No Financials Explanation
        public string GetLabelNoFinExp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNoFinExp, 100);
            string text = driver.FindElement(lblNoFinExp).Text;
            return text;
        }

        //Fetch the label of Retainer
        public string GetLabelRetainer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRetainer, 100);
            string text = driver.FindElement(lblRetainer).Text;
            return text;
        }

        //Fetch the label of Progress Fee MM
        public string GetLabelProgressFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProgressFee, 100);
            string text = driver.FindElement(lblProgressFee).Text;
            return text;
        }

        //Fetch the label of Minimum Fee MM
        public string GetLabelMinimumFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMinFee, 100);
            string text = driver.FindElement(lblMinFee).Text;
            return text;
        }

        //Fetch the label of Transaction Fee Type
        public string GetLabelTxnFeeType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTxnFee, 100);
            string text = driver.FindElement(lblTxnFee).Text;
            return text;
        }

        //Fetch the label of Retainer Fee Creditable
        public string GetLabelRetainerFeeCred()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRetainerFee, 100);
            string text = driver.FindElement(lblRetainerFee).Text;
            return text;
        }

        //Fetch the label of Progress Fee Creditable
        public string GetLabelProgressFeeCred()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProgFeeCredit, 100);
            string text = driver.FindElement(lblProgFeeCredit).Text;
            return text;
        }

        //Fetch the label of Pre Pitch section
        public string GetLabelPrePitch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secPrePitch, 100);
            string text = driver.FindElement(secPrePitch).Text;
            return text;
        }

        //Fetch the label of Will There Be a Pitch?
        public string GetLabelWillThereBePitch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblWillThere, 100);
            string text = driver.FindElement(lblWillThere).Text;
            return text;
        }

        //Fetch the label of Houlihan Lokey Competition
        public string GetLabelHLComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblHLComp, 100);
            string text = driver.FindElement(lblHLComp).Text;
            return text;
        }

        //Fetch the label of Existing Relationships
        public string GetLabelExistingRel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistingRel, 100);
            string text = driver.FindElement(lblExistingRel).Text;
            return text;
        }

        //Fetch the label of Existing or Repeat Client?
        public string GetLabelExistingOrRepeatClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExisting, 100);
            string text = driver.FindElement(lblExisting).Text;
            return text;
        }

        //Fetch the label of Who Are The Key Decision-Makers?
        public string GetLabelWhoAreTheKeyDecisionMakers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblWhoAre, 100);
            string text = driver.FindElement(lblWhoAre).Text;
            return text;
        }

        //Fetch the label of Lockups on Future M&A or Financing Work
        public string GetLabelLockupsOnFutureMA()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblLockups, 150);
            string text = driver.FindElement(lblLockups).Text;
            return text;
        }

        //Fetch the label of If known, identify the name(s) of the client’s outside counsel and/or other advisors (If any)
        public string GetLabelIfKnown()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secIfKnown, 100);
            string text = driver.FindElement(secIfKnown).Text;
            return text;
        }

        //Fetch the label of Outside Council
        public string GetLabelOutsideCouncil()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOutside, 100);
            string text = driver.FindElement(lblOutside).Text;
            return text;
        }

        //Fetch the label of Referral
        public string GetLabelReferral()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secReferral, 100);
            string text = driver.FindElement(secReferral).Text;
            return text;
        }

        //Fetch the label of Referral Type
        public string GetLabelReferralType()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblRefType, 130);
            string text = driver.FindElement(lblRefType).Text;
            return text;
        }

        //Fetch the label of Referral Source
        public string GetLabelReferralSource()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRefSource, 100);
            string text = driver.FindElement(lblRefSource).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-500)");
            return text;
        }

        //Fetch the label of Section Is there a potential Fairness Opinion component to this assignment?
        public string GetLabelIsTherePotentialFairness()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secIsPotential, 100);
            string text = driver.FindElement(secIsPotential).Text;
            return text;
        }

        //Fetch the label of Fairness Opinion Provided
        public string GetLabelFairnessOpinionProvided()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFairnessOpinion, 100);
            string text = driver.FindElement(lblFairnessOpinion).Text;
            return text;
        }

        //Fetch the label of Restricted List Information section
        public string GetLabelRestrictedListInformation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secRestricted, 100);
            string text = driver.FindElement(secRestricted).Text;
            return text;
        }

        //Fetch the label of Restricted List 
        public string GetLabelRestrictedList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRestictedList, 100);
            string text = driver.FindElement(lblRestictedList).Text;
            return text;
        }

        //Fetch the label of CC Information section
        public string GetLabelCCInformation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secCCInfo, 100);
            string text = driver.FindElement(secCCInfo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of CC Status
        public string GetLabelCCStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCCStatus, 100);
            string text = driver.FindElement(lblCCStatus).Text;           
            return text;
        }

        //Fetch the label of Are There any Pitch section
        public string GetLabelAreThereAnyPitch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secAreThereAnyPitch, 100);
            string text = driver.FindElement(secAreThereAnyPitch).Text;
            return text;
        }

        //Fetch the label of 1st YES/NO
        public string GetLabel1stYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl1stYesNo, 100);
            string text = driver.FindElement(lbl1stYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of 1st Have any of deal team section
        public string GetLabelRespectiveAffiliates()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHaveAny1, 100);
            string text = driver.FindElement(secHaveAny1).Text;
            return text;
        }

        //Fetch the label of 2nd YES/NO
        public string GetLabel2ndYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl2ndYesNo, 100);
            string text = driver.FindElement(lbl2ndYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of 2nd Have any of deal team section
        public string GetLabelRespectiveMgmt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHaveAny2, 100);
            string text = driver.FindElement(secHaveAny2).Text;
            return text;
        }

        //Fetch the label of 3rd YES/NO
        public string GetLabel3rdYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl3rdYesNo, 100);
            string text = driver.FindElement(lbl3rdYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,750)");
            Thread.Sleep(4000);
            return text;
        }

        //Fetch the label of 3rd Have any of deal team section
        public string GetLabelConflictOfInterest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHaveAny3, 100);
            string text = driver.FindElement(secHaveAny3).Text;
            return text;
        }

        //Fetch the label of 4th YES/NO
        public string GetLabel4thYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl4thYesNo, 100);
            string text = driver.FindElement(lbl4thYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of perception of a conflict of interest?
        public string GetLabelPerceptionOfConflictOfInterest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHaveAny4, 100);
            string text = driver.FindElement(secHaveAny4).Text;
            return text;
        }

        //Fetch the label of 4th YES/NO
        public string GetLabel5thYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl5thYesNo, 100);
            string text = driver.FindElement(lbl5thYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-1000)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the section A
        public string GetMessageOfA()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secA, 100);
            string text = driver.FindElement(secA).Text;
            return text;
        }

        //Fetch the label of A
        public string GetLabelOfA()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblA, 100);
            string text = driver.FindElement(lblA).Text;           
            return text;
        }

        //Fetch the section B
        public string GetMessageOfB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secB, 100);
            string text = driver.FindElement(secB).Text;
            return text;
        }

        //Fetch the label of B
        public string GetLabelOfB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblB, 100);
            string text = driver.FindElement(lblB).Text;
            return text;
        }

        //Fetch the section C
        public string GetMessageOfC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secC, 100);
            string text = driver.FindElement(secC).Text;
            return text;
        }

        //Fetch the label of C
        public string GetLabelOfC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblC, 100);
            string text = driver.FindElement(lblC).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(3000);
            return text;
        }
        //Fetch the section D
        public string GetMessageOfD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secD, 100);
            string text = driver.FindElement(secD).Text;
            return text;
        }

        //Fetch the label of D
        public string GetLabelOfD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblD, 100);
            string text = driver.FindElement(lblD).Text;
            return text;
        }

        //Fetch the section of Confirmation
        public string GetMessageOfConfirmation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secPlsConfirm, 100);
            string text = driver.FindElement(secPlsConfirm).Text;
            return text;
        }

        //Fetch the label of Group Head Approval
        public string GetLabelOfGroupHeadApproval()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblGroupHead, 100);
            string text = driver.FindElement(lblGroupHead).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-850)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of Staff
        public string GetLabelOfStaff()
        {
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("(//div[@class='content iframe-parent'])[2]/iframe[contains(@name,'vfFrameId_')]")));
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblStaff, 150);
            string text = driver.FindElement(lblStaff).Text;
            driver.SwitchTo().DefaultContent();
            return text;
        }

        //Validate Save button
        public string ValidateSaveButton()
        {
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("(//div[@class='content iframe-parent'])[2]/iframe[contains(@name,'vfFrameId_')]")));
            Thread.Sleep(4000);
            Console.WriteLine("entered into frame ");                             
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveITTeam, 130);
            string text = driver.FindElement(btnSaveITTeam).Text;
            driver.SwitchTo().DefaultContent();
            return text;
        }

        //Validate Return To Opportunity button
        public string ValidateReturnToOppButton()
        {
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("(//div[@class='content iframe-parent'])[2]/iframe[contains(@name,'vfFrameId_')]")));
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 100);
            string text = driver.FindElement(btnReturnToOpp).Text;
            driver.SwitchTo().DefaultContent();
            return text;
        }

        //Validate Role Definitions button
        public string ValidateRoleDefButton()
        {
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("(//div[@class='content iframe-parent'])[2]/iframe[contains(@name,'vfFrameId_')]")));
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnRoleDef, 100);
            string text = driver.FindElement(btnRoleDef).Text;
            driver.SwitchTo().DefaultContent();
            return text;
        }


        //Fetch validations for mandatory fields
        public string GetFieldsValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmit, 80);            
            driver.FindElement(btnSubmit).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();            
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
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancialOpinion, 90);
            driver.FindElement(comboFinancialOpinion).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 2));
            driver.FindElement(checkConfirm).Click();

            //Overview and Financials
            driver.FindElement(txtTranOverview).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 3));
            //driver.FindElement(txtCurrentStatus).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 4));
            //driver.FindElement(txtCompDesc).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 5));
            driver.FindElement(comboCrossBorder).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 6));
            driver.FindElement(comboAsiaAngle).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 7));
            driver.FindElement(comboRealEstate).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 7));
            driver.FindElement(txtOwnershipStr).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 8));
            driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 9));
            driver.FindElement(comboAudit).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 10));
            driver.FindElement(txtEstVal).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 11));
           // driver.FindElement(txtValExp).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 12));
            driver.FindElement(txtRiskFactors).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 13));

            //Fees
            driver.FindElement(txtEstFee).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 14));
            driver.FindElement(txtFeeStr).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 15));
            driver.FindElement(comboLockUps).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 16));
            driver.FindElement(comboReferralFee).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 17));

            //Pre-Pitch
            driver.FindElement(comboPitch).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 18));
            driver.FindElement(comboClient).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 19));
            driver.FindElement(txtHLComp).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 20));
            driver.FindElement(comboExistingRel).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 21));
            driver.FindElement(comboTASAssist).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 22));
            //driver.FindElement(txtOutsideCouncil).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 23));

            //Financing Checklist            
            //driver.FindElement(comboCapMkt).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 24));
            driver.FindElement(txtSum).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 25));

            //Fairness Checklist
            driver.FindElement(comboFairness).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 26));
            Console.WriteLine("comboFairness");

            //Administrative
            driver.FindElement(comboResList).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 27));
            //driver.FindElement(comboCCStatus).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 28));
            driver.FindElement(comboCCStatus1).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 28));
            driver.FindElement(comboCCStatus2).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 29));
            driver.FindElement(comboCCStatus3).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 30));
            driver.FindElement(comboCCStatus4).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 31));

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
            WebDriverWaits.WaitUntilEleVisible(driver, valEmailOppName, 112);
            string emailSub = driver.FindElement(valEmailOppName).Text;
            Console.WriteLine(emailSub);
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnCancelEmail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 70);
            driver.FindElement(btnReturntoOpp).Click();
            return emailSub;
        }

        public string GetCCStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, statusCC, 90);
            string status = driver.FindElement(statusCC).Text;
            return status;
        }     

        //Update FinancialOption
        public string UpdateFinancialOption(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancialOpinion, 80);
            driver.FindElement(comboFinancialOpinion).SendKeys(value);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valFinOption, 80);
            string valFin = driver.FindElement(valFinOption).Text;
            return valFin;
        }

        //Get Role Text
        public string GetRoleText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtComment, 80);
            string txtValue = driver.FindElement(txtComment).Text;
            return txtValue;
        }

        //Get Suggested Fees
        public string GetSuggestedFees()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tblSuggestedFee, 100);
            string txtFees = driver.FindElement(tblSuggestedFee).Text.Replace("\r\n", " ");
            return txtFees;
        }

        //Click EU Override
        public void ClickEUOverrideButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEUOverride, 80);
            driver.FindElement(btnEUOverride).Click();
        }

        //Get EU Override Text
        public string GetEUOverrideText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEUComment, 80);
            string txtValue = driver.FindElement(txtEUComment).Text.Replace("\r\n", " ");
            return txtValue;
        }

        //Get EU Fees
        public string GetEUFees()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tblEUFee, 80);
            string txtFees = driver.FindElement(tblEUFee).Text.Replace("\r\n", " ");
            return txtFees;
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
            catch(Exception e)
            {
                return "No Review section";
            }
            
        }

        //To validate NBC form is disabled
        public string ValidateIfFormIsEditable()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalDebt, 60);
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

        //To validate NBC form is disabled
        public string ValidateIfFormIsEditableForPG()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTranOverview, 60);
            string value = driver.FindElement(txtTranOverview).Enabled.ToString();

            if (value.Equals("True"))
            {
                return "Form is editable";
            }
            else
            {
                return "Form is not editable";
            }
        }

        //To validate NBC form is disabled
        public string ValidateIfCNBCFormIsEditableForPG()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLCompPG, 60);
            string value = driver.FindElement(txtHLCompPG).Enabled.ToString();

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
                WebDriverWaits.WaitUntilEleVisible(driver, comboGrade, 10);
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
                WebDriverWaits.WaitUntilEleVisible(driver, txtNotes, 10);
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
                WebDriverWaits.WaitUntilEleVisible(driver, txtDateSubmitted,10);
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
                WebDriverWaits.WaitUntilEleVisible(driver, txtReason, 10);
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
                WebDriverWaits.WaitUntilEleVisible(driver, txtFeeDiff,10);
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
                WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 10);
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
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 10);
            string value = driver.FindElement(btnReturntoOpp).Enabled.ToString();
            return value;
        }

        //Validate Return To Opportunity button
        public string ValidateReturnToOpportunityButtonForCFUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOppCFUser,10);
            string value = driver.FindElement(btnReturntoOppCFUser).Enabled.ToString();
            return value;
        }
        //Validate EU Override button
        public string ValidateEUOverrideButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEUOverride,10);
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
            WebDriverWaits.WaitUntilEleVisible(driver, btnPDFView,10);
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


