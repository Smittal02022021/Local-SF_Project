using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TC2362_TMTC0003198_VerifyModificationOfLOBValueForGivenOpportunity : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        FEISForm form = new FEISForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        SendEmailNotification notification = new SendEmailNotification();

        public static string fileTC2362 = "T2362_TMTC0003198_VerifyModificationOfLOBValueForGivenOpportunity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyModificationOfLOBValueForGivenOpportunity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2362;
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
                string value = addOpportunity.AddOpportunities(valJobType, fileTC2362);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC2362);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTC2362, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateReqFieldsForFVAConversion(fileTC2362);
                opportunityDetails.UpdateInternalTeamDetails(fileTC2362);                                             

                //Log out of standard user and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                //Search for same Opportunity and click on NBC form
                string rec = opportunityHome.SearchOpportunity(opportunityNumber);
                Assert.AreEqual("Record found", rec);
                extentReports.CreateLog("Same opportunity is found ");

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC2362);
                extentReports.CreateLog("Conflict Check fields are updated ");

                //Update Record Type to CF, LOB to CF, Job Type to Negotiated Fairness and validate Job Type and LOB                
                string lob= opportunityDetails.UpdateRecordTypeAndLOB();
                Assert.AreEqual("CF", lob);
                string jobType = opportunityDetails.GetJobType();
                Assert.AreEqual("Negotiated Fairness", jobType);
                extentReports.CreateLog("LOB and Job Type are updated to " +lob +" and " +jobType +" ");

                //Login as Standard User and search for same opportunity
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");
                opportunityHome.SearchOpportunity(opportunityNumber);

                //Update additional fields i.e Estimated Fees and Fairness Opinion Component
                opportunityDetails.AddEstFeesWithAdmin(fileTC2362);

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

                //Approve the Opportunity and convert to Engagement
                opportunityDetails.ClickApproveButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Opportunity is approved ");
                opportunityDetails.ClickConvertToEng();
                extentReports.CreateLog("Opportunity is converted to engagement ");

                //Get Record Type of Engagement 
                string engJobType = engagementDetails.GetRecordType();
                Console.WriteLine("engJobType " + engJobType);
                Assert.AreEqual("Fairness (CF)", engJobType);
                extentReports.CreateLog("Record Type of Engagement is displayed as " +engJobType + " ");
                usersLogin.UserLogOut();                
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
            }
        }
        [OneTimeTearDown]
        public void ExtentClose()
        {
            try
            {
                extent.Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
            driver.Quit();
            notification.SendOutlookEmailWithTestExecutionReport("Test Execution Report - Opportunity");
        }
    }
}
