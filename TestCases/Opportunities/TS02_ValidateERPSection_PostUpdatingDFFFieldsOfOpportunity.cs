using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TS02_ValidateERPSection_PostUpdatingDFFFieldsOfOpportunity : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();      
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        RandomPages pages = new RandomPages();
        LegalEntityDetail entityDetails = new LegalEntityDetail();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        ContactHomePage contactHome = new ContactHomePage();

        public static string ERP = "ERPPostCreationOfOpportunity.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateERPSection()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERP;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                 string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Standard User and validate the user                
                  usersLogin.SearchUserAndLogin(valUser);
                  string stdUser = login.ValidateUser();
                  Assert.AreEqual(stdUser.Contains(valUser), true);
                  extentReports.CreateLog("User: " + stdUser + " logged in ");
                                
                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType, ERP);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(ERP);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppName = opportunityDetails.GetOpportunityName();
                string clientName = opportunityDetails.GetClient();
                string subjectName = opportunityDetails.GetSubject();
                string oppNumber = opportunityDetails.GetOppNumber();
                string jobType = opportunityDetails.GetJobType();
                Console.WriteLine(jobType);                

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(ERP, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Get ERP Submitted to Sync and get ERP Update DFF checkbox and ERP Last Integration Response Date
                string ERPSubmitted = opportunityDetails.GetERPSubmittedToSync();
                extentReports.CreateLog("ERP Submitted to Sync before update is: " + ERPSubmitted +" ");

                string valERPUpdateDFF = opportunityDetails.GetERPUpdateDFFIfChecked();
                Assert.AreEqual("Checkbox is not checked", valERPUpdateDFF);
                extentReports.CreateLog("ERP Update DFF " + valERPUpdateDFF +" by default ");
                               
                string ERPResDate = opportunityDetails.GetERPIntegrationResponseDate();                
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResDate + " is displayed ");

                //-----Update Primary office, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status -----
                string updOffice = ReadExcelData.ReadData(excelPath, "DFFUpdates", 1);
                string primaryOffice = opportunityDetails.UpdatePrimaryOffice(updOffice);
                Assert.AreEqual(updOffice, primaryOffice);
                extentReports.CreateLog("Primary Office is updated to " + primaryOffice + " ");

                string valDFFPrimaryOffice = opportunityDetails.GetERPUpdateDFFIfChecked();
                Assert.AreEqual("Checkbox is checked", valDFFPrimaryOffice);
                extentReports.CreateLog("ERP Update DFF " + valDFFPrimaryOffice + " after updating Primary office ");
                                 
                string ERPSubmittedOffice = opportunityDetails.GetERPSubmittedToSync();
                Assert.AreNotEqual(ERPSubmitted, ERPSubmittedOffice);
                extentReports.CreateLog("ERP Submitted to Sync: " + ERPSubmittedOffice+ " ");
                
                string ERPStatusOffice = opportunityDetails.GetERPIntegrationStatus();
                Assert.AreEqual("Success", ERPStatusOffice);
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusOffice + " is displayed ");

                string ERPResOffice = opportunityDetails.GetERPIntegrationResponseDate();
                Assert.AreNotEqual(ERPResDate, ERPResOffice);
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResOffice + " is displayed ");

                //-----Update Industry Group, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----
                string updIG = ReadExcelData.ReadData(excelPath, "DFFUpdates", 2);
                string IG = opportunityDetails.UpdateIndustryGroup(updIG);
                Assert.AreEqual(updIG, IG);
                extentReports.CreateLog("Industry Group is updated to " + IG + " ");

                string valDFFIG = opportunityDetails.GetERPUpdateDFFIfChecked();
                Assert.AreEqual("Checkbox is checked", valDFFIG);
                extentReports.CreateLog("ERP Update DFF " + valDFFIG + " after updating Industry Group ");

                string ERPSubmittedIG = opportunityDetails.GetERPSubmittedToSync();
                Assert.AreNotEqual(ERPSubmittedOffice, ERPSubmittedIG);
                extentReports.CreateLog("ERP Submitted to Sync: " + ERPSubmittedIG + " ");

                string ERPStatusIG = opportunityDetails.GetERPIntegrationStatus();
                Assert.AreEqual("Success", ERPStatusIG);
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed ");

                string ERPResIG = opportunityDetails.GetERPIntegrationResponseDate();
                Assert.AreNotEqual(ERPResOffice, ERPResIG);
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResIG + " is displayed ");

                //-----Update Sector, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----
                string updSector = ReadExcelData.ReadData(excelPath, "DFFUpdates", 3);
                string sector = opportunityDetails.UpdateSector(updSector);
                Assert.AreEqual(updSector, sector);
                extentReports.CreateLog("Sector is updated to " + sector + " ");

                string valDFFSector = opportunityDetails.GetERPUpdateDFFIfChecked();
                Assert.AreEqual("Checkbox is checked", valDFFSector);
                extentReports.CreateLog("ERP Update DFF " + valDFFSector + " after updating Sector ");

                string ERPSubmittedSector = opportunityDetails.GetERPSubmittedToSync();
                Assert.AreNotEqual(ERPSubmittedIG, ERPSubmittedSector);
                extentReports.CreateLog("ERP Submitted to Sync: " + ERPSubmittedSector + " ");

                string ERPStatusSector = opportunityDetails.GetERPIntegrationStatus();
                Assert.AreEqual("Success", ERPStatusSector);
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusSector + " is displayed ");

                string ERPResSector = opportunityDetails.GetERPIntegrationResponseDate();
                Assert.AreNotEqual(ERPResIG, ERPResSector);
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResSector + " is displayed ");

                //-----Update Job Type, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----
                string updType = ReadExcelData.ReadData(excelPath, "DFFUpdates", 4);
                string updJobType = opportunityDetails.UpdateJobTypeERP(updType);
                Assert.AreEqual(updType, updJobType);
                extentReports.CreateLog("Job Type is updated to " + updJobType + " ");

                string valDFFJobType = opportunityDetails.GetERPUpdateDFFIfChecked();
                Assert.AreEqual("Checkbox is checked", valDFFJobType);
                extentReports.CreateLog("ERP Update DFF " + valDFFJobType + " after updating Job Type ");

                string ERPSubmittedJobType = opportunityDetails.GetERPSubmittedToSync();
                Assert.AreNotEqual(ERPSubmittedSector, ERPSubmittedJobType);
                extentReports.CreateLog("ERP Submitted to Sync: " + ERPSubmittedJobType + " ");

                string ERPStatusJobType = opportunityDetails.GetERPIntegrationStatus();
                Assert.AreEqual("Success", ERPStatusJobType);
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusJobType + " is displayed ");

                string ERPResJobType = opportunityDetails.GetERPIntegrationResponseDate();
                Assert.AreNotEqual(ERPResSector, ERPResJobType);
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResJobType + " is displayed ");

                //Validate Product Line by getting from Job Types page              
                string pageTitle = pages.ClickJobTypes(updType);
                Assert.AreEqual("Job Type Detail", pageTitle);
                extentReports.CreateLog("Page with title: " + pageTitle + " is displayed upon clicking Job Types link ");

                string prodLine = pages.GetProductLine();
                string prodCode = pages.GetProductTypeCode();

                opportunityHome.SearchOpportunity(value);
                string productLine = opportunityDetails.GetERPProductType();
                Assert.AreEqual(prodLine, productLine);
                extentReports.CreateLog("Product Type in ERP section: " + productLine + " matches with Product Line in Job Type Detail as per updated Job Type ");

                string productCode = opportunityDetails.GetERPProductTypeCode();
                Assert.AreEqual(prodCode, productCode);
                extentReports.CreateLog("Updated ERP Product Type Code in ERP section: " + productCode + " matches with Product Type Code in Job Type Detail as per updated Job Type ");

                //-----Update Client Ownership, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----
                string updOwnership = ReadExcelData.ReadData(excelPath, "DFFUpdates", 5);
                string clientOwnership = opportunityDetails.UpdateClientOwnership(updOwnership);
                Assert.AreEqual(updOwnership, clientOwnership);
                extentReports.CreateLog("Client Ownership is updated to " + clientOwnership + " ");

                string valDFFClient = opportunityDetails.GetERPUpdateDFFIfChecked();
                Assert.AreEqual("Checkbox is checked", valDFFClient);
                extentReports.CreateLog("ERP Update DFF " + valDFFClient + " after updating Client Ownership ");
                                
                string ERPSubmittedClient = opportunityDetails.GetERPSubmittedToSync();
                Assert.AreNotEqual(ERPSubmittedJobType, ERPSubmittedClient);
                extentReports.CreateLog("ERP Submitted to Sync: " + ERPSubmittedClient + " ");

                string ERPStatusClient = opportunityDetails.GetERPIntegrationStatus();
                Assert.AreEqual("Success", ERPStatusClient);
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusClient + " is displayed ");

                string ERPResClient = opportunityDetails.GetERPIntegrationResponseDate();
                Assert.AreNotEqual(ERPResJobType, ERPResClient);
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResClient + " is displayed ");

                usersLogin.UserLogOut();

                //-----Update LOB and validate ERP Sync Date and ERP Update DFF checkbox-----
                //Search for any existing Opportunity              
                opportunityHome.SearchOpportunity(value);
                extentReports.CreateLog("Created opportunity is opened again ");

                string LOB = opportunityDetails.UpdateRecordTypeAndLOBERP();
                Assert.AreEqual("FVA", LOB);
                extentReports.CreateLog("LOB is updated to " + LOB + " ");

                string valDFFLOB = opportunityDetails.GetERPUpdateDFFIfChecked();
                Assert.AreEqual("Checkbox is checked", valDFFLOB);
                extentReports.CreateLog("ERP Update DFF " + valDFFLOB + " after updating LOB ");

                string ERPSubmittedLOB = opportunityDetails.GetERPSubmittedToSync();
                Assert.AreNotEqual(ERPSubmittedClient, ERPSubmittedLOB);
                extentReports.CreateLog("ERP Submitted to Sync: " + ERPSubmittedLOB + " ");

                string ERPStatusLOB = opportunityDetails.GetERPIntegrationStatus();
                Assert.AreEqual("Success", ERPStatusLOB);
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusLOB + " is displayed ");

                string ERPResLOB = opportunityDetails.GetERPIntegrationResponseDate();
                Assert.AreNotEqual(ERPResClient, ERPResLOB);
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResLOB + " is displayed ");

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

    

