using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.Pages.TimeRecordManager;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.TimeRecordManager
{
    class T2309_Engagement_TimeTracking_AddTimeEntriesToOpportunityBeforeAndAfterConversionToEngagementAsSupervisor : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();

        public static string fileTC2309 = "T2309_AddTimeEntriesToOpportunityBeforeAndAfterConversionToEngagementAsSupervisor.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AddTimeEntriesToOpportunityBeforeAndAfterConversionToEngagementAsSupervisor()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2309;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);

                //Login as Standard User profile and validate the user
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
                string value = addOpportunity.AddOpportunities(valJobType, fileTC2309);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity : " + value + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC2309);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTC2309, valContact, valRecordType, valContactType,2);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForConversion(fileTC2309);
                opportunityDetails.UpdateInternalTeamDetails(fileTC2309);

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC2309);
                //opportunityDetails.UpdateNBCApproval();
                extentReports.CreateLog("Conflict Check and NBC fields are updated ");

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Requesting for engagement and validate the success message
                string msgSuccess = opportunityDetails.ClickRequestEng();
                Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                //Log out of Standard User
                usersLogin.UserLogOut();

                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Approve the Opportunity 
                opportunityDetails.ClickApproveButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Opportunity is approved ");

                usersLogin.UserLogOut();
                extentReports.CreateLog("Logout from CAO user ");

                string supervisorUser = ReadExcelData.ReadData(excelPath, "Users", 3);
                //Login as supervisor User
                usersLogin.SearchUserAndLogin(supervisorUser);
                string supUser = login.ValidateUser();
                Assert.AreEqual(supUser.Contains(supervisorUser), true);
                extentReports.CreateLog("User: " + supUser + " logged in ");

                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();
                string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitle();
                string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 5);
                Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                extentReports.CreateLog("Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");

                // Enter weekly time entry
                string selectProject = "O - " + opportunityNumber + "-FVA";
                timeEntry.EnterWeeklyEntryMatrix(selectProject, fileTC2309);
                string sundayTime = timeEntry.GetSundayTimeEntry();
                string sundayTimeExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 2);
                Assert.AreEqual(sundayTime, sundayTimeExl);
                extentReports.CreateLog("Time Entry value: " + sundayTime + " is maching with entered value ");

                //Enter summary log details
                string selectProjectInSummary = "Opportunity" + " " + opportunityNumber + "-FVA";
                timeEntry.EnterSummaryLogs(selectProjectInSummary, fileTC2309);
                timeEntry.ClickAddButton();
                extentReports.CreateLog("Summary Log Entry with opportunity is created successfully ");

                //Logout from time record manager staff
                usersLogin.UserLogOut();
                extentReports.CreateLog("LogOut from Supervisor user role ");

                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUsers = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUsers + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Calling function to convert to Engagement
                opportunityDetails.ClickConvertToEng();

                //Validate the Engagement name in Engagement details page
                string engName = engagementDetails.GetEngName();
                Assert.AreEqual(opportunityNumber, engName);
                extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                usersLogin.UserLogOut();
                //Login as supervisor User
                usersLogin.SearchUserAndLogin(supervisorUser);
                string supUsers = login.ValidateUser();
                Assert.AreEqual(supUsers.Contains(supervisorUser), true);
                extentReports.CreateLog("User: " + supUsers + " logged in ");

                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();
                string timeRecordManagersTitle = timeEntry.GetTimeRecordManagerTitle();
                Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagersTitle);
                extentReports.CreateLog("Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");

                //Verify Opportunity name is displayed under project
                Assert.IsTrue(timeEntry.VerifyProjectDisableAfterOppToEngagementConversion());
                extentReports.CreateLog("Verified Opportunity name is displayed under project and it is non-editable ");

                // Verify weekly time entry opportunity project is replaced by engagement
                string selectEngagementProject = "E - " + opportunityNumber + "-FVA";

                //Verify opportunity project is not available in weekly time entry
                string opportunityProjectWeekly = timeEntry.VerifyEngagementProjectExist(selectProject);
                Assert.AreEqual("Project not present", opportunityProjectWeekly);
                extentReports.CreateLog("Verified Opportunity name is removed from the Project selection list of Weekly Entry Matrix ");

                //Verify engagement project is present in weekly time entry
                string engagementProjectWeekly = timeEntry.VerifyEngagementProjectExist(selectEngagementProject);
                Assert.AreEqual("Project is present", engagementProjectWeekly);
                extentReports.CreateLog("Verified Engagement project is visible in Project selection list of Weekly Entry Matrix  ");

                // Verify summary logs tab opportunity project is replaced by engagement
                string selectEngagementProjectInSummary = "Engagement" + " " + opportunityNumber + "-FVA";
                timeEntry.GoToSummaryLogs();

                //Verify opportunity project in summary log
                string opportunityProjectSummary = timeEntry.VerifyEngagementProjectExistInLogs(selectProjectInSummary);
                Assert.AreEqual("Project not present", opportunityProjectSummary);
                extentReports.CreateLog("Verified Opportunity name is removed from the Project selection list of Summary Log ");

                //Verify engagement project in summary log
                string engagementProjectSummary = timeEntry.VerifyEngagementProjectExistInLogs(selectEngagementProjectInSummary);
                Assert.AreEqual("Project is present", engagementProjectSummary);
                extentReports.CreateLog("Verified  Engagement name is visible in Project selection list of Summary Log ");

                // Verify detail logs tab opportunity project is replaced by engagement
                timeEntry.GoToDetailLogs();

                //Verify opportunity project in detail log
                string opportunitytProjectDetailSummary = timeEntry.VerifyEngagementProjectExistInLogs(selectProjectInSummary);
                Assert.AreEqual("Project not present", opportunitytProjectDetailSummary);
                extentReports.CreateLog("Verified Opportunity name is removed from the Project selection list of Detail Log ");

                //Verify engagement project in detail log
                string engagementProjectDetailSummary = timeEntry.VerifyEngagementProjectExistInLogs(selectEngagementProjectInSummary);
                Assert.AreEqual("Project is present", engagementProjectDetailSummary);
                extentReports.CreateLog("Verified  Engagement project is visible in project selection list of Detail Log ");

                //Clean up records
                timeEntry.RemoveRecordFromDetailLogs();
                extentReports.CreateLog("Records are deleted successfully ");

                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();                
                driver.Quit();
            }
        }
    }
}