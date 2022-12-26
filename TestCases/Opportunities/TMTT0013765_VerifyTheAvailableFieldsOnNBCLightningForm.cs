using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TMTT0013765_VerifyTheAvailableFieldsOnNBCLightningForm: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        NBCForm form = new NBCForm();
        CNBCForm cnbc = new CNBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1232 = "T1232_NBCFormSubmitforReview.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void NBCFormSubmitforReview()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1232;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType, fileTC1232);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1232);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppNum = opportunityDetails.GetOppNumber();
                string clientName = opportunityDetails.GetClient();
                string subjectName = opportunityDetails.GetSubject();
                string jobType = opportunityDetails.GetJobType();
                Console.WriteLine(jobType);

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateInternalTeamDetails(fileTC1232);

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC1232);
                opportunityDetails.UpdateCCOnly();

                //Login as Standard User and validate the user
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Click on NBC page and validate title of page
                string title = opportunityDetails.ClickNBCFormL();
                Assert.AreEqual("Public Sensitivity", title);
                extentReports.CreateLog("NBC Form page is displayed with default tab : " + title + " ");

                //Click Opportunity Overview tab and validate its available fields
                string txtOppOverview = form.ClickOpportunityOverview();
                Assert.AreEqual("Opportunity Overview", txtOppOverview);
                extentReports.CreateLog("Tab with name " + txtOppOverview + " is displayed upon clicking the tab. ");
                
                string txtRelOpp = form.GetLabelRelatedOpportunity();                
                Assert.AreEqual("Related Opportunity", txtRelOpp);
                extentReports.CreateLog("Field with name: " + txtRelOpp + " is displayed ");

                string txtTxnOver = form.GetLabelTxnOverview();               
                Assert.AreEqual("Transaction Overview", txtTxnOver);
                extentReports.CreateLog("Field with name: " + txtTxnOver + " is displayed ");

                string txtCurrentStatus = form.GetLabelCurrentStatus();
                Assert.AreEqual("Current Status", txtCurrentStatus);
                extentReports.CreateLog("Field with name: " + txtCurrentStatus + " is displayed ");

                string txtCompDesc = form.GetLabelCompDesc();
                Assert.AreEqual("Company Description", txtCompDesc);
                extentReports.CreateLog("Field with name: " + txtCompDesc + " is displayed ");

                string txtOwnershipStr = form.GetLabelOnwershipStructure();
                Assert.AreEqual("Ownership and Capital Structure", txtOwnershipStr);
                extentReports.CreateLog("Field with name: " + txtOwnershipStr + " is displayed ");

                string txtRiskFact = form.GetLabelRiskFactors();
                Assert.AreEqual("Risk Factors", txtRiskFact);
                extentReports.CreateLog("Field with name: " + txtRiskFact + " is displayed ");

                string txtIntAngle = form.GetLabelIntAngle();
                Assert.AreEqual("International Angle?", txtIntAngle);
                extentReports.CreateLog("Field with name: " + txtIntAngle + " is displayed ");

                string txtTotalDebt = form.GetLabelTotalDebtMM();
                Assert.AreEqual("Total Debt (MM)", txtTotalDebt);
                extentReports.CreateLog("Field with name: " + txtTotalDebt + " is displayed ");

                string txtExtVal = form.GetLabelExtVal();
                Assert.AreEqual("Estimated Valuation (MM)", txtExtVal);
                extentReports.CreateLog("Field with name: " + txtExtVal + " is displayed ");

                string txtValExp = form.GetLabelValExp();
                Assert.AreEqual("Valuation Expectations", txtValExp);
                extentReports.CreateLog("Field with name: " + txtValExp + " is displayed ");

                string txtRealEst = form.GetLabelRealEstAngle();
                Assert.AreEqual("Real Estate Angle", txtRealEst);
                extentReports.CreateLog("Field with name: " + txtRealEst + " is displayed ");

                string txtAsiaAngle = form.GetLabelAsiaAngle();
                Assert.AreEqual("Asia Angle", txtAsiaAngle);
                extentReports.CreateLog("Field with name: " + txtAsiaAngle + " is displayed ");

                //Click Financials tab and validate its available fields
                string txtFinancials = form.ClickFinancialsTab();
                Assert.AreEqual("Financials", txtFinancials);
                extentReports.CreateLog("Tab with name " + txtFinancials + " is displayed upon clicking Financials tab. ");

                string msgCapMkt = form.GetMessageCapitalMarket();
                Assert.AreEqual("Has the Capital Markets Group been Consulted regarding financing or capital structure?", msgCapMkt);
                extentReports.CreateLog("Message : " + msgCapMkt + " is displayed ");

                string txtCapMkt = form.GetLabelCapMktConsult();
                Assert.AreEqual("Capital Markets Consulted", txtCapMkt);
                extentReports.CreateLog("Field with name: " + txtCapMkt + " is displayed ");

                string txtFinNotes = form.GetLabelExistingFinNotes();
                Assert.AreEqual("Existing Financial Arrangement Notes", txtFinNotes);
                extentReports.CreateLog("Field with name: " + txtFinNotes + " is displayed ");

                string msgAboveFin = form.GetMessageFinSubToAudit();
                Assert.AreEqual("Have the above financials been subject to an audit?", msgAboveFin);
                extentReports.CreateLog("Message : " + msgAboveFin + " is displayed ");

                string txtFinSub = form.GetLabelFinSubToAudit();
                Assert.AreEqual("Financials Subject to Audit", txtFinSub);
                extentReports.CreateLog("Field with name: " + txtFinSub + " is displayed ");

                string msgFinUnavail = form.GetMessageFinUnavailable();
                Assert.AreEqual("Financials Unavailable", msgFinUnavail);
                extentReports.CreateLog("Message : " + msgFinUnavail + " is displayed ");

                string txtNoFin = form.GetLabelNoFin();
                Assert.AreEqual("No Financials", txtNoFin);
                extentReports.CreateLog("Field with name: " + txtNoFin + " is displayed ");

                string txtNoFinExp = form.GetLabelNoFinExp();
                Assert.AreEqual("No Financials Explanation", txtNoFinExp);
                extentReports.CreateLog("Field with name: " + txtNoFinExp + " is displayed ");                

                //Click Fees tab and validate its mandatory validations 
                string txtFees = form.ClickFeesTab();
                Assert.AreEqual("Fees", txtFees);
                extentReports.CreateLog("Tab with name " + txtFees + " is displayed upon clicking Fees tab. ");

                string txtRetainer = form.GetLabelRetainer();
                Assert.AreEqual("Retainer", txtRetainer);
                extentReports.CreateLog("Field with name: " + txtRetainer + " is displayed ");

                string txtProgFee = form.GetLabelProgressFee();
                Assert.AreEqual("Progress Fee (MM)", txtProgFee);
                extentReports.CreateLog("Field with name: " + txtProgFee + " is displayed ");

                string txtMinFee = form.GetLabelMinimumFee();
                Assert.AreEqual("Minimum Fee (MM)", txtMinFee);
                extentReports.CreateLog("Field with name: " + txtMinFee + " is displayed ");

                string txtTxnFee = form.GetLabelTxnFeeType();
                Assert.AreEqual("Transaction Fee Type", txtTxnFee);
                extentReports.CreateLog("Field with name: " + txtTxnFee + " is displayed ");

                string txtRetainerFee = form.GetLabelRetainerFeeCred();
                Assert.AreEqual("Retainer Fee Creditable ?", txtRetainerFee);
                extentReports.CreateLog("Field with name: " + txtRetainerFee + " is displayed ");

                string txtProgressFee = form.GetLabelProgressFeeCred();
                Assert.AreEqual("Progress Fee Creditable ?", txtProgressFee);
                extentReports.CreateLog("Field with name: " + txtProgressFee + " is displayed ");
                               
                //Click Pitch tab and validate its mandatory validations 
                string txtPitch = form.ClickPitchTab();
                Assert.AreEqual("Pitch", txtPitch);
                extentReports.CreateLog("Tab with name " + txtPitch + " is displayed upon clicking Pitch tab. ");

                string txtPre = form.GetLabelPrePitch();
                Assert.AreEqual("Pre-Pitch", txtPre);
                extentReports.CreateLog("Section with name: " + txtPre + " is displayed ");

                string txtWillThere = form.GetLabelWillThereBePitch();
                Assert.AreEqual("Will There Be a Pitch?", txtWillThere);
                extentReports.CreateLog("Field with name: " + txtWillThere + " is displayed ");

                string txtHLComp = form.GetLabelHLComp();
                Assert.AreEqual("Houlihan Lokey Competition", txtHLComp);
                extentReports.CreateLog("Field with name: " + txtHLComp + " is displayed ");

                string txtExistingRel = form.GetLabelExistingRel();
                Assert.AreEqual("Existing Relationships", txtExistingRel);
                extentReports.CreateLog("Field with name: " + txtExistingRel + " is displayed ");

                string txtExisting= form.GetLabelExistingOrRepeatClient();
                Assert.AreEqual("Existing or Repeat Client?", txtExisting);
                extentReports.CreateLog("Field with name: " + txtExisting + " is displayed ");

                string txtWhoAre = form.GetLabelWhoAreTheKeyDecisionMakers();
                Assert.AreEqual("Who Are The Key Decision-Makers?", txtWhoAre);
                extentReports.CreateLog("Field with name: " + txtWhoAre + " is displayed ");

                string txtLockups = form.GetLabelLockupsOnFutureMA();
                Assert.AreEqual("Lockups on Future M&A or Financing Work", txtLockups);
                extentReports.CreateLog("Field with name: " + txtLockups + " is displayed ");

                string txtIfKnown = form.GetLabelIfKnown();
                Assert.AreEqual("If known, identify the name(s) of the client’s outside counsel and/or other advisors (If any):", txtIfKnown);
                extentReports.CreateLog("Section with name: " + txtIfKnown + " is displayed ");

                string txtOutide = form.GetLabelOutsideCouncil();
                Assert.AreEqual("Outside Council", txtOutide);
                extentReports.CreateLog("Field with name: " + txtOutide + " is displayed ");

                string txtReferral = form.GetLabelReferral();
                Assert.AreEqual("Referral", txtReferral);
                extentReports.CreateLog("Section with name: " + txtReferral + " is displayed ");

                string txtRefType = form.GetLabelReferralType();
                Assert.AreEqual("Referral Type", txtRefType);
                extentReports.CreateLog("Field with name: " + txtRefType + " is displayed ");

                string txtRefSource= form.GetLabelReferralSource();
                Assert.AreEqual("Referral Source", txtRefSource);
                extentReports.CreateLog("Field with name: " + txtRefSource + " is displayed ");
                
                //Click Fairness/Admin Checklist tab and validate its mandatory validations 
                string txtFairness = form.ClickFairnessAdminChecklistTab();
                Assert.AreEqual("Fairness/Admin Checklist", txtFairness);
                extentReports.CreateLog("Tab with name " + txtFairness + " is displayed upon clicking Fairness/Admin Checklist tab. ");

                string txtPotential = form.GetLabelIsTherePotentialFairness();
                Assert.AreEqual("Is there a potential Fairness Opinion component to this assignment?", txtPotential);
                extentReports.CreateLog("Section with name: " + txtPotential + " is displayed ");

                string txtFairnessOpinion = form.GetLabelFairnessOpinionProvided();
                Assert.AreEqual("Fairness Opinion Provided", txtFairnessOpinion);
                extentReports.CreateLog("Field with name: " + txtFairnessOpinion + " is displayed ");
                                
                string txtAdministrative = form.ClickAdministrativeTab();
                Assert.AreEqual("Administrative", txtAdministrative);
                extentReports.CreateLog("Tab with name " + txtAdministrative + " is displayed upon clicking Administrative tab. ");

                string txtRestrictedInfo = form.GetLabelRestrictedListInformation();
                Assert.AreEqual("Restricted List Information", txtRestrictedInfo);
                extentReports.CreateLog("Section with name: " + txtRestrictedInfo + " is displayed ");

                string txtRestrictedList = form.GetLabelRestrictedList();
                Assert.AreEqual("Restricted List", txtRestrictedList);
                extentReports.CreateLog("Field with name: " + txtRestrictedList + " is displayed ");

                string txtCCInfo = form.GetLabelCCInformation();
                Assert.AreEqual("Conflicts Check Information - (the answers to each of these questions must be verified with each member of the deal team)", txtCCInfo);
                extentReports.CreateLog("Section with name: " + txtCCInfo + " is displayed ");

                string txtCCStatus = form.GetLabelCCStatus();
                Assert.AreEqual("Conflict Check Status", txtCCStatus);
                extentReports.CreateLog("Field with name: " + txtCCStatus + " is displayed ");

                string txtAnyPitch = form.GetLabelAreThereAnyPitch();
                Assert.AreEqual("Are there any pitches to, or engagements by, the potential client or, if known, the likely counterparty(ies), that are not listed in the completed conflicts check report?", txtAnyPitch);
                extentReports.CreateLog("Section with name: " + txtAnyPitch + " is displayed ");

                string txt1stYESNO = form.GetLabel1stYesNo();
                Assert.AreEqual("Yes/No", txt1stYESNO);
                extentReports.CreateLog("Field with name: " + txt1stYESNO + " is displayed ");

                string txtAffiliates = form.GetLabelRespectiveAffiliates();
                Assert.AreEqual("Have any of the deal team members had, or do any of them currently have, any direct or indirect financial interests in or exposures to, the potential transaction, any related transactions, the potential client or, if known, the likely counterparty(ies) (or any of their respective affiliates)?", txtAffiliates);
                extentReports.CreateLog("Section with name: " + txtAffiliates + " is displayed ");

                string txt2ndYESNO = form.GetLabel2ndYesNo();
                Assert.AreEqual("Yes/No", txt2ndYESNO);
                extentReports.CreateLog("Field with name: " + txt2ndYESNO + " is displayed ");

                string txtMgmt = form.GetLabelRespectiveMgmt();
                Assert.AreEqual("Have any of the deal team members had, or do any of them currently have, any business, employment, family or other similar relationships with the potential client or, if known, the likely counterparty(ies) (or any of their respective affiliates or management teams)?", txtMgmt);
                extentReports.CreateLog("Section with name: " + txtMgmt + " is displayed ");

                string txt3rdYESNO = form.GetLabel3rdYesNo();
                Assert.AreEqual("Yes/No", txt3rdYESNO);
                extentReports.CreateLog("Field with name: " + txt3rdYESNO + " is displayed ");

                string txtConflict = form.GetLabelConflictOfInterest();
                Assert.AreEqual("Have any of the deal team members (or to their knowledge, any other Houlihan Lokey employees) (a) had any discussions (including, but not limited to, any pitches) with any likely counterparty(ies) (or any of their respective affiliates) regarding a potential transaction with the potential client, or (b) had any discussions with such counterparty(ies) (or any of their respective affiliates) that might create the perception of a conflict of interest?", txtConflict);
                extentReports.CreateLog("Section with name: " + txtConflict + " is displayed ");

                string txt4thYESNO = form.GetLabel4thYesNo();
                Assert.AreEqual("Yes/No", txt4thYESNO);
                extentReports.CreateLog("Field with name: " + txt4thYESNO + " is displayed ");

                string txtCOI = form.GetLabelPerceptionOfConflictOfInterest();
                Assert.AreEqual("Are any of the deal team members aware of any relationships or discussions not specified above that might create the perception of a conflict of interest?", txtCOI);
                extentReports.CreateLog("Section with name: " + txtCOI + " is displayed ");

                string txt5thYESNO = form.GetLabel5thYesNo();
                Assert.AreEqual("Yes/No", txt5thYESNO);
                extentReports.CreateLog("Field with name: " + txt5thYESNO + " is displayed ");

                //Click Public Sensitivity tab and validate all its available fields

                string txtPublic = form.ClickPublicSensitivityTab();
                Assert.AreEqual("Public Sensitivity", txtPublic);
                extentReports.CreateLog("Tab with name " + txtPublic + " is displayed upon clicking Public Sensitivity tab. ");

                string txtAMsg = form.GetMessageOfA();
                Assert.AreEqual("A. Is this an engagement by a company with publicly held securities, where the scope of the engagement may cover a transaction involving the sale, merger or other disposition of all or a substantial part of the company's equity, assets or business?", txtAMsg);
                extentReports.CreateLog("Section with name: " + txtAMsg + " is displayed ");

                string txtA = form.GetLabelOfA();
                Assert.AreEqual("A", txtA);
                extentReports.CreateLog("Field with name: " + txtA + " is displayed ");

                string txtBMsg = form.GetMessageOfB();
                Assert.AreEqual("B. Is this an engagement by a company with publicly held securities, where the scope of the engagement may cover an acquisition of another company or asset representing a material portion (approximately 10% or more)?", txtBMsg);
                extentReports.CreateLog("Section with name: " + txtBMsg + " is displayed ");

                string txtB = form.GetLabelOfB();
                Assert.AreEqual("B", txtB);
                extentReports.CreateLog("Field with name: " + txtB + " is displayed ");

                string txtCMsg = form.GetMessageOfC();
                Assert.AreEqual("C. Is this engagement by a company (whether privately or publicly held), where the scope of the engagement may cover an acquisition of all or a substantial part of the equity, assets or business of another company with publicly held securities?", txtCMsg);
                extentReports.CreateLog("Section with name: " + txtCMsg + " is displayed ");

                string txtC = form.GetLabelOfC();
                Assert.AreEqual("C", txtC);
                extentReports.CreateLog("Field with name: " + txtC + " is displayed ");

                string txtDMsg = form.GetMessageOfD();
                Assert.AreEqual("D. Or is this engagement by a company in connection with which we may be requested to provide a fairness or other financial opinion? ", txtDMsg);
                extentReports.CreateLog("Section with name: " + txtDMsg + " is displayed ");

                string txtD = form.GetLabelOfD();
                Assert.AreEqual("D", txtD);
                extentReports.CreateLog("Field with name: " + txtD + " is displayed ");

                string secConfirm = form.GetMessageOfConfirmation();
                Assert.AreEqual("Please confirm that a group head has approved prior to submitting to the committee", secConfirm);
                extentReports.CreateLog("Section with name: " + secConfirm + " is displayed ");

                string txtConfirm = form.GetLabelOfGroupHeadApproval();
                Assert.AreEqual("Group Head Approval", txtConfirm);
                extentReports.CreateLog("Field with name: " + txtConfirm + " is displayed ");

                //Click HL Internal Team tab and validate its available fields 
                string txtHLInt = form.ClickHLInternalTeamTab();
                Assert.AreEqual("HL Internal Team", txtHLInt);
                extentReports.CreateLog("Tab with name " + txtHLInt + " is displayed upon clicking HL Internal Team tab. ");

                string txtStaff = form.GetLabelOfStaff();                                                                                                                                                                                    
                Assert.AreEqual("Staff:", txtStaff);
                extentReports.CreateLog("Field with name: " + txtStaff + " is displayed ");

                string txtSave = form.ValidateSaveButton();
                Assert.AreEqual("Save", txtSave);
                extentReports.CreateLog("Button with name: " + txtSave + " is displayed ");

                string txtReturn = form.ValidateReturnToOppButton();
                Assert.AreEqual("Return To Opportunity", txtReturn);
                extentReports.CreateLog("Button with name: " + txtReturn + " is displayed ");

                string txtRole = form.ValidateRoleDefButton();
                Assert.AreEqual("Role Definitions", txtRole);
                extentReports.CreateLog("Button with name: " + txtRole + " is displayed ");

                usersLogin.UserLogOut();            
                usersLogin.UserLogOut();
                driver.Quit();
        }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }                
    }
}
}

    

