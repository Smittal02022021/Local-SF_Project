using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1967AndT1968_OpportunityComment_ValidationRules_OnlyComplianceUserCanCreateCommentsOfTypeCompliance : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        ValuationPeriods valPeriods = new ValuationPeriods();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC2214 = "T1967AndT1968_OpportunityComment";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ImportPositionsWithoutTeamMembers()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2214;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Search for required Opportunity
                opportunityHome.SearchOpportunity("Rockwood - PV 2021");
                opportunityDetails.AddOppComments("Compliance");
                string message = opportunityDetails.GetCommentsSectionMessage();
                Assert.AreEqual("Error: Only Compliance users can create comments of type Compliance.", message);
                extentReports.CreateLog("Message: " + message + " is displayed with Standard User ");

                //Logout of Standard user and login with Compliance User
                usersLogin.UserLogOut();                
                string valCompUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valCompUser);
                string compUser = login.ValidateUser();
                Assert.AreEqual(compUser.Contains(valCompUser), true);
                extentReports.CreateLog("User: " + compUser + " logged in ");

                //Search for required Opportunity
                opportunityHome.SearchOpportunity("Rockwood - PV 2021");
                opportunityDetails.AddOppComments("Compliance");
                string addedComment = opportunityDetails.GetAddedComment();
                Assert.AreEqual("Testing", addedComment);
                extentReports.CreateLog("Added comment: " + addedComment + " of type compliance is saved successfully with Compliance user ");

                //Delete the added comment
                string comment =opportunityDetails.DeleteAddedOppComments();
                Assert.AreEqual("Opportunity's existing comments are deleted", comment);
                extentReports.CreateLog("Added comment is deleted successfully ");
                
                //Validate any other type of comment with Compliance User
                opportunityDetails.AddOppComments("Internal");
                string message1 = opportunityDetails.GetCommentsSectionMessage();
                Assert.AreEqual("Error: Compliance users can only create Compliance comments.", message1);
                extentReports.CreateLog("Message: " + message1 + " is displayed while adding any other type of comment with Compliance User ");
                               
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
            }
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            usersLogin.UserLogOut();
            usersLogin.UserLogOut();
        }
    }
}

    

