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
    class TS01_ValidateERPSection_PostCreationOfOpportunity : BaseClass
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

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Calling functions to validate for all LOBs operation
                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowUsers " + rowUsers);

                for (int row = 2; row <= rowUsers; row++)
                {
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

                    //Login as Standard User and validate the user                
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity",row, 25);
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

                    //Call function to update HL -Internal Team details
                    opportunityDetails.UpdateInternalTeamDetails(ERP);

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(ERP, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Validate ERP section details------     
                    //Validate ERP Submitted To Sync                 
                    string ERPSubmitted = opportunityDetails.GetERPSubmittedToSync();
                    extentReports.CreateLog("ERP Submitted To Sync in ERP section: " + ERPSubmitted + " is displayed ");

                    //Validate ERP ID
                    string ERPID = opportunityDetails.GetERPID();
                    Assert.NotNull(ERPID);
                    extentReports.CreateLog("ERP ID in ERP section: " + ERPID + " is displayed ");

                    //Validate ERP Project Status Code
                    string ERPProjStatusCode = opportunityDetails.GetERPProjStatusCode();
                    Assert.NotNull(ERPProjStatusCode);
                    extentReports.CreateLog("ERP Project Status Code in ERP section: " + ERPProjStatusCode + " is displayed ");

                    //Validate HL Entity
                    string entity = ReadExcelData.ReadData(excelPath, "AddOpportunity", 12);
                    string HLEntity = opportunityDetails.GetHLEntity();
                    Assert.AreEqual(entity, HLEntity);
                    extentReports.CreateLog("HL Entity in ERP section: " + HLEntity + " matches with Legal Entity entered while creating Opportunity ");

                    //Validate ERP HL Entity                 
                    string ERPEntity = opportunityDetails.GetERPHLEntity();
                    Assert.AreEqual(entity, ERPEntity);
                    extentReports.CreateLog("ERP HL Entity in ERP section: " + ERPEntity + " matches with Legal Entity entered while creating Opportunity ");

                    //Validate ERP Legal Entity                 
                    string ERPLegalEntity = opportunityDetails.GetERPLegalEntity();
                    Assert.AreEqual(entity, ERPLegalEntity);
                    extentReports.CreateLog("ERP Legal Entity in ERP section: " + ERPLegalEntity + " matches with Legal Entity entered while creating Opportunity ");

                    //Validate ERP Project Number                
                    string ERPProjectNumber = opportunityDetails.GetERPProjectNumber();
                    Assert.AreEqual(oppNumber, ERPProjectNumber);
                    extentReports.CreateLog("ERP Project Number in ERP section: " + ERPProjectNumber + " matches with Opportunity Number ");

                    //Validate ERP Project Name                
                    string ERPProjectName = opportunityDetails.GetERPProjectName();
                    Assert.AreEqual(oppName + " " + oppNumber, ERPProjectName);
                    extentReports.CreateLog("ERP Project Name in ERP section: " + ERPProjectName + " is combination of both Opportunity name and number ");

                    //Validate ERP LOB
                    string LOB = opportunityDetails.GetLOB();
                    string ERPLOB = opportunityDetails.GetERPLOB();
                    if (LOB.Equals("CF") ||LOB.Equals("FR"))
                    {
                        Assert.AreEqual(LOB, ERPLOB);
                        extentReports.CreateLog("ERP LOB in ERP section: " + ERPLOB + " matches with Opportunity's LOB ");
                    }
                    else
                    {
                        Assert.AreEqual("FA", ERPLOB);
                        extentReports.CreateLog("ERP LOB in ERP section: " + ERPLOB + " matches with Opportunity's LOB ");
                    }
                    //Validate ERP Industry Group
                    string IG = opportunityDetails.GetIndustryGroup();
                    string ERPIG = opportunityDetails.GetERPIndustryGroup();
                    Assert.AreEqual(IG, ERPIG);
                    extentReports.CreateLog("ERP IG in ERP section: " + ERPIG + " matches with Opportunity's IG ");

                    //Validate ERP Last Integration Status
                    string intStatus = opportunityDetails.GetERPIntegrationStatus();
                    Assert.NotNull(intStatus);
                    extentReports.CreateLog("ERP Last Integration Status in ERP section: " + intStatus + " is displayed ");

                    //Validate ERP Last Integration Response Date
                    string resDate = opportunityDetails.GetERPIntegrationResponseDate();
                    Assert.NotNull(resDate);
                    extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + resDate + " is displayed ");

                    //Validate ERP Last Integration Error Description	
                    string error = opportunityDetails.GetERPIntegrationError();
                    extentReports.CreateLog("ERP Last Integration Error Description in ERP section: " + error + " is displayed ");

                    //Click Job Types link 
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity",row, 3);
                    string pageTitle = pages.ClickJobTypes(type);
                    Assert.AreEqual("Job Type Detail", pageTitle);
                    extentReports.CreateLog("Page with title: " + pageTitle + " is displayed upon clicking Job Types link ");

                    //Get value of Product Type, Product Type Code 
                    string prodLine = pages.GetProductLine();
                    string prodCode = pages.GetProductTypeCode();

                    //Validate Product Line
                    opportunityHome.SearchOpportunity(value);
                    string productLine = opportunityDetails.GetERPProductType();
                    Assert.AreEqual(prodLine, productLine);
                    extentReports.CreateLog("Product Type in ERP section: " + productLine + " matches with Product Line in Job Type Detail ");

                    //Validate ERP Product Type Code 15
                    string productCode = opportunityDetails.GetERPProductTypeCode();
                    Assert.AreEqual(prodCode, productCode);
                    extentReports.CreateLog("ERP Product Type Code in ERP section: " + productCode + " matches with Product Type Code in Job Type Detail ");

                    //Log out of standard user 
                    usersLogin.UserLogOut();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                    //Get legal entities details with admin login
                    string title = pages.ClickLegalEntities(entity);
                    Assert.AreEqual("Legal Entity Detail", title);
                    extentReports.CreateLog("Page with title: " + title + " is displayed ");

                    //Get ERP Legal Entity ID,ERP Business Unit ID,ERP Template number,ERP Business Unit,ERP Entity Code,ERP Legislation Code
                    string templateNum = entityDetails.GetERPTemplateNumber();
                    string unitID = entityDetails.GetERPBusinessUnitID();
                    string unit = entityDetails.GetERPBusinessUnit();
                    string entityID = entityDetails.GetERPLegalEntityID();
                    string code = entityDetails.GetERPEntityCode();
                    string legisCode = entityDetails.GetERPLegislationCode();

                    //Search for the contact of the user added in Internal Team member                            
                    contactHome.SearchContactWithExternalContact(ERP);
                    string emailID = contactHome.GetEmailIDOfContact();
                    Console.WriteLine(emailID);

                    //Login as standard user again
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                    //Search for created Opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Validate ERP Template                
                    string ERPTemplate = opportunityDetails.GetERPTemplate();
                    Assert.AreEqual(templateNum, ERPTemplate);
                    extentReports.CreateLog("ERP Template in ERP section: " + ERPTemplate + " matches with template number of Legal Entity: " + entity + " ");

                    //Validate ERP Business Unit ID             
                    string ERPUnitID = opportunityDetails.GetERPBusinessUnitID();
                    Assert.AreEqual(unitID, ERPUnitID);
                    extentReports.CreateLog("ERP Business Unit ID in ERP section: " + ERPUnitID + " matches with ERP Business Unit ID of Legal Entity: " + entity + " ");

                    //Validate ERP Business Unit              
                    string ERPUnit = opportunityDetails.GetERPBusinessUnit();
                    Assert.AreEqual(unit, ERPUnit);
                    extentReports.CreateLog("ERP Business Unit in ERP section: " + ERPUnit + " matches with ERP Business Unit of Legal Entity: " + entity + " ");

                    //Validate ERP Legal Entity ID              
                    string ERPEntityID = opportunityDetails.GetERPLegalEntityID();
                    Assert.AreEqual(entityID, ERPEntityID);
                    extentReports.CreateLog("ERP Legal Entity ID in ERP section: " + ERPEntityID + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");

                    //Validate ERP Entity Code              
                    string ERPEntityCode = opportunityDetails.GetERPEntityCode();
                    Assert.AreEqual(code, ERPEntityCode);
                    extentReports.CreateLog("ERP Entity Code in ERP section: " + ERPEntityCode + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");

                    //Validate ERP Legislation Code             
                    string ERPLegCode = opportunityDetails.GetERPLegCode();
                    Assert.AreEqual(legisCode, ERPLegCode);
                    extentReports.CreateLog("ERP Legislation Code in ERP section: " + ERPLegCode + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");

                    //Validate ERP Principal Manager           
                    string ERPEmailID = opportunityDetails.GetERPEmailID();
                    Assert.AreEqual(emailID, ERPEmailID);
                    string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
                    extentReports.CreateLog("ERP Principal Manager in ERP section: " + ERPEmailID + " matches with email id of contact of Internal team member: " + valStaff + " ");

                    usersLogin.UserLogOut();                    
                }
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

    

