using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T2081_OpportunityManager_VerifyOpportunityWithRecordTypeOppDelAreNotDisplayed : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        CNBCForm form = new CNBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        OpportunityManager oppMgr = new OpportunityManager();


        public static string fileTC2081 = "T1592_ValidationsToBeCompleted.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyOpportunityWithRecordTypeOppDelAreNotDisplayed()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2081;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login again as Standard User
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
                string value = addOpportunity.AddOpportunities(valJobType, fileTC2081);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC2081);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppName = opportunityDetails.GetOppNumber();
                string clientName = opportunityDetails.GetClient();
                string subjectName = opportunityDetails.GetSubject();
                string jobType = opportunityDetails.GetJobType();
                Console.WriteLine(oppName);
               
                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateInternalTeamDetails(fileTC2081);

                //Navigate to Opportunity Manager page and sort the column
                opportunityHome.ClickOppManager();
                oppMgr.SortOppIdCol();

                //Search created opportunity in Opportunity Manager
                string id =oppMgr.SearchOpportunity(oppName);
                Console.WriteLine("id: "+id);
                Assert.AreEqual(oppName,id);
                extentReports.CreateLog("Created opportunity is displayed in Opportunity Manager ");

                //Logout of standard user
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " logged in ");

                //Search created opportunity
                string rec= opportunityHome.SearchOpportunity(opportunityNumber); 
                Assert.AreEqual("Record found",rec);
                extentReports.CreateLog("Created opportunity is found ");

                //Click Opportunity Name, update Record Type                
                opportunityDetails.UpdateRecordType("OPP DEL");              
                opportunityDetails.ClickSaveButton();
                string recType = opportunityDetails.GetRecType();
                Console.WriteLine(recType);
                Assert.AreEqual("OPP DEL [Change]", recType);
                extentReports.CreateLog("Opportunity's record type is updated to: " + recType +" ");

                //Login as Standard User
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Navigate to Opportunity Manager and check if Opportunity is still visible
                opportunityHome.ClickOppManager();
                oppMgr.SortOppIdCol();
                string oppDisplayed = oppMgr.SearchOpportunity(oppName);
                Console.WriteLine("oppDispalyed: " + oppDisplayed);
                Assert.AreEqual("Opportunity does not exist", oppDisplayed);
                extentReports.CreateLog("Created opportunity is not displayed anymore in Opportunity Manager ");

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
