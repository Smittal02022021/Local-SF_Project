using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Engagement
{
    class TS04AndTS07_ValidateERPSectionAndDefaultContract_PostConversionFromOpportunityToEngagement : BaseClass
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
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();

        public static string ERP = "ERPPostCoversionToEngagement1.xlsx";

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
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
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

                    //Update required Opportunity fields for conversion and Internal team details
                    if(valRecordType.Equals("CF"))
                    {
                        opportunityDetails.UpdateReqFieldsForCFConversion(ERP);
                    }
                    else if (valRecordType.Equals("FVA"))
                    {
                        opportunityDetails.UpdateReqFieldsForFVAConversion(ERP);
                    }
                    else
                    {
                        opportunityDetails.UpdateReqFieldsForFRConversion(ERP);
                    }                    
                    opportunityDetails.UpdateInternalTeamDetails(ERP);

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(ERP);
                    if (valRecordType.Equals("CF"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateLog("Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateLog("Conflict Check fields are updated ");
                    }

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Update Total Anticipated Revenue                   
                    if (valRecordType.Equals("FVA"))
                    {
                        opportunityDetails.UpdateTotalAnticipatedRevenueForValidations();                        
                        extentReports.CreateLog("Total Anticipated Revenue fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateLog("Revenue fields need not to be updated ");
                    }                    

                    //Requesting for engagement and validate the success message
                    string msgSuccess = opportunityDetails.ClickRequestEng();
                    Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    //Log out of Standard User
                    usersLogin.UserLogOut();

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 2));
                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 2)), true);
                    extentReports.CreateLog("User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Approve the Opportunity 
                    opportunityDetails.ClickApproveButton();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog("Opportunity is approved ");

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();

                    //Validate the Engagement name in Engagement details page
                    string engName = engagementDetails.GetEngName();
                    Assert.AreEqual(opportunityNumber, engName);
                    extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                    string engNum = engagementDetails.GetEngagementNumber();

                    //Login as Standard User and validate ERP details
                    usersLogin.UserLogOut();
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser2 = login.ValidateUser();
                    Assert.AreEqual(stdUser2.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                    engHome.SearchEngagementWithName(engName);

                    //Validate ERP section details------     
                    //Validate ERP Submitted To Sync                 
                    string ERPSubmitted = engagementDetails.GetERPSubmittedToSync();
                    Assert.NotNull(ERPSubmitted);
                    extentReports.CreateLog("ERP Submitted To Sync in ERP section: " + ERPSubmitted + " is displayed ");

                    //Validate ERP ID
                    string ERPID = engagementDetails.GetERPID();
                    Assert.NotNull(ERPID);
                    extentReports.CreateLog("ERP ID in ERP section: " + ERPID + " is displayed ");
                                       
                    //Validate HL Entity
                    string entity = engagementDetails.GetLegalEntity();
                    string HLEntity = engagementDetails.GetHLEntity();
                    Assert.AreEqual(entity, HLEntity);
                    extentReports.CreateLog("HL Entity in ERP section: " + HLEntity + " matches with Legal Entity of Engagement ");

                    //Validate ERP HL Entity                 
                    string ERPEntity = engagementDetails.GetERPHLEntity();
                    Assert.AreEqual(entity, ERPEntity);
                    extentReports.CreateLog("ERP HL Entity in ERP section: " + ERPEntity + " matches with Legal Entity of Engagement ");

                    //Validate ERP Legal Entity                 
                    string ERPLegalEntity = engagementDetails.GetERPLegalEntity();
                    Assert.AreEqual(entity, ERPLegalEntity);
                    extentReports.CreateLog("ERP Legal Entity in ERP section: " + ERPLegalEntity + " matches with Legal Entity of Engagement ");

                    //Validate ERP Project Number                
                    string ERPProjectNumber = engagementDetails.GetERPProjectNumber();
                    Assert.AreEqual(engNum, ERPProjectNumber);
                    extentReports.CreateLog("ERP Project Number in ERP section: " + ERPProjectNumber + " matches with Engagement Number ");

                    //Validate ERP Project Name                
                    string ERPProjectName = engagementDetails.GetERPProjectName();
                    Assert.AreEqual(engName + " " + engNum, ERPProjectName);
                    extentReports.CreateLog("ERP Project Name in ERP section: " + ERPProjectName + " is combination of both Engagement name and number ");

                    //Validate ERP LOB
                    string LOB = engagementDetails.GetLOB();
                    string ERPLOB = engagementDetails.GetERPLOB();
                    if (LOB.Equals("CF") ||LOB.Equals("FR"))
                    {
                        Assert.AreEqual(LOB, ERPLOB);
                        extentReports.CreateLog("ERP LOB in ERP section: " + ERPLOB + " matches with Engagement's LOB ");
                    }
                    else
                    {
                        Assert.AreEqual("FA", ERPLOB);
                        extentReports.CreateLog("ERP LOB in ERP section: " + ERPLOB + " matches with Engagement's LOB ");
                    }
                    //Validate ERP Industry Group
                    string IG = engagementDetails.GetIndustryGroup();
                    string ERPIG = engagementDetails.GetERPIndustryGroup();
                    Assert.AreEqual(IG, ERPIG);
                    extentReports.CreateLog("ERP IG in ERP section: " + ERPIG + " matches with Engagement's IG ");

                    //Validate ERP Last Integration Status
                    string intStatus = engagementDetails.GetERPIntegrationStatus();
                    Assert.NotNull(intStatus);
                    extentReports.CreateLog("ERP Last Integration Status in ERP section: " + intStatus + " is displayed ");

                    //Validate ERP Last Integration Response Date
                    string resDate = engagementDetails.GetERPIntegrationResponseDate();
                    Assert.NotNull(resDate);
                    extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + resDate + " is displayed ");

                    //Validate ERP Last Integration Error Description	
                    string error = engagementDetails.GetERPIntegrationError();
                    extentReports.CreateLog("ERP Last Integration Error Description in ERP section: " + error + " is displayed ");

                    //Click Job Types link 
                    string type = engagementDetails.GetJobType();
                    string pageTitle = pages.ClickJobTypes(type);
                    Assert.AreEqual("Job Type Detail", pageTitle);
                    extentReports.CreateLog("Page with title: " + pageTitle + " is displayed upon clicking Job Types link ");

                    //Get value of Product Type, Product Type Code 
                    string prodLine = pages.GetProductLine();
                    string prodCode = pages.GetProductTypeCode();

                    //Validate Product Line                   
                    engHome.SearchEngagementWithName(engName);
                    string productLine = engagementDetails.GetERPProductType();
                    Assert.AreEqual(prodLine, productLine);
                    extentReports.CreateLog("Product Type in ERP section: " + productLine + " matches with Product Line in Job Type Detail ");

                    //Validate ERP Product Type Code 15
                    string productCode = engagementDetails.GetERPProductTypeCode();
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
                    string stdUser3 = login.ValidateUser();
                    Assert.AreEqual(stdUser3.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser3 + " logged in ");

                    //Search for converted Engagement
                    engHome.SearchEngagementWithName(engName);

                    //Validate ERP Template                
                    string ERPTemplate = engagementDetails.GetERPTemplate();
                    Assert.AreEqual(templateNum, ERPTemplate);
                    extentReports.CreateLog("ERP Template in ERP section: " + ERPTemplate + " matches with template number of Legal Entity: " + entity + " ");

                    //Validate ERP Business Unit ID             
                    string ERPUnitID = engagementDetails.GetERPBusinessUnitID();
                    Assert.AreEqual(unitID, ERPUnitID);
                    extentReports.CreateLog("ERP Business Unit ID in ERP section: " + ERPUnitID + " matches with ERP Business Unit ID of Legal Entity: " + entity + " ");

                    //Validate ERP Business Unit              
                    string ERPUnit = engagementDetails.GetERPBusinessUnit();
                    Assert.AreEqual(unit, ERPUnit);
                    extentReports.CreateLog("ERP Business Unit in ERP section: " + ERPUnit + " matches with ERP Business Unit of Legal Entity: " + entity + " ");

                    //Validate ERP Legal Entity ID              
                    string ERPEntityID = engagementDetails.GetERPLegalEntityID();
                    Assert.AreEqual(entityID, ERPEntityID);
                    extentReports.CreateLog("ERP Legal Entity ID in ERP section: " + ERPEntityID + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");

                    //Validate ERP Entity Code              
                    string ERPEntityCode = engagementDetails.GetERPEntityCode();
                    Assert.AreEqual(code, ERPEntityCode);
                    extentReports.CreateLog("ERP Entity Code in ERP section: " + ERPEntityCode + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");

                    //Validate ERP Legislation Code             
                    string ERPLegCode = engagementDetails.GetERPLegCode();
                    Assert.AreEqual(legisCode, ERPLegCode);
                    extentReports.CreateLog("ERP Legislation Code in ERP section: " + ERPLegCode + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");

                    //Validate the creation of default contract
                    string contractID = engagementDetails.ValidateContractExists();
                    Assert.AreEqual(contractID,engName);
                    extentReports.CreateLog("Contract with name: "+contractID+" similar to Engagement's name is created upon conversion to engagement ");

                    //Validate ERP Contract Type
                    string contractType = engagementDetails.GetERPContractType();
                    Assert.AreEqual("Engagement", contractType);
                    extentReports.CreateLog("ERP Contract Type: " + contractType + " is displayed in Contract section ");

                    //Validate ERP Business Unit
                    string contractUnit = engagementDetails.GetContractERPBusinessUnit();
                    Assert.AreEqual(unit, contractUnit);
                    extentReports.CreateLog("ERP Business Unit: " + contractUnit + " is displayed in Contract section ");

                    //Validate ERP Legal Entity Name
                    string contractEntity = engagementDetails.GetContractERPLegalEntityName();
                    Assert.AreEqual(entity, contractEntity);
                    extentReports.CreateLog("ERP Legal Entity Name: " + contractEntity + " is displayed in Contract section ");

                    //Validate ERP Bill Plan
                    string contractPlan = engagementDetails.GetContractERPBillPlan();
                    Assert.AreEqual("Bill Plan", contractPlan);
                    extentReports.CreateLog("ERP Bill Plan: " + contractPlan + " is displayed in Contract section ");

                    //Validate Bill To
                    string compName = engagementDetails.GetCompanyNameOfContact();
                    string contractBillTo = engagementDetails.GetContractBillTo();
                    Assert.AreEqual(compName, contractBillTo);
                    extentReports.CreateLog("Bill To: " + contractBillTo + " is displayed same as Engagement contact's company in Contract section ");

                    //Validate Contract Start Date
                    string contractStartDate = engagementDetails.GetContractStartDate();
                    if (LOB.Equals("FR")|| LOB.Equals("FVA"))
                    {
                        Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), contractStartDate);
                        extentReports.CreateLog("Contract Start Date: " + contractStartDate + " is displayed same as current date ");
                    }
                    else
                    {
                        Assert.AreEqual(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), contractStartDate);
                        extentReports.CreateLog("Contract Start Date: " + contractStartDate + " is displayed same as current date ");
                    }
               
                    //Validate Is Main Contract checkbox is checked
                    string mainContract = engagementDetails.GetIfIsMainContractChecked();
                    Assert.AreEqual("Is Main Contract checkbox is checked", mainContract);
                    extentReports.CreateLog(mainContract + " ");

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

    

