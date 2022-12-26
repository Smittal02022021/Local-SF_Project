using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1683AndT2074_OpportunityDetails_HLInternalTeam_EditAndUpdateAndClientSubjectAsPrimary : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        FEISForm form = new FEISForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1683 = "T1683_HLInternalTeam_EditAndUpdate";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void HLInternalTeam_EditAndUpdate()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1683;
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
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

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
                string value = addOpportunity.AddOpportunities(valJobType, fileTC1683);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1683);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Validate if Primary checkbox is checked for added Client and Subject company
                string valueClient = opportunityDetails.GetPrimaryCheckboxOfClientCompany();
                Assert.AreEqual("Checked", valueClient);
                extentReports.CreateLog("Primary checkbox corresponding to added Client Company is " +valueClient + " ");

                string valueSubject = opportunityDetails.GetPrimaryCheckboxOfSubjectCompany();
                Assert.AreEqual("Checked", valueSubject);
                extentReports.CreateLog("Primary checkbox corresponding to added Subject Company is " + valueSubject+ " ");

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateInternalTeamDetails(fileTC1683);              
                extentReports.CreateLog("User is added in the HL Internal Team members ");

                //Validate if user still exists in deal
                string userExist = opportunityDetails.ValidateUserIfExists();
                Console.WriteLine("userExist: " + userExist);
                Assert.AreEqual("User exists", userExist);
                extentReports.CreateLog(userExist + " in the deal ");

                //Validate if Opportunity exists under My Opportunities          
                string rec = opportunityHome.SearchMyOpportunities(opportunityNumber);
                Assert.AreEqual("Record found", rec);
                extentReports.CreateLog("Opportunity is displayed in My Opportunities of the user ");

                //Call fnction to remove the user and it's assigned roles
                string msgSave = opportunityDetails.RemoveUserFromITTeam();
                Console.WriteLine("msgSave: " +msgSave);
                Assert.AreEqual("Success: Staff Roles Updated.", msgSave);
                extentReports.CreateLog(msgSave + " is displayed upon removing user from HL Internal Team members ");

                //Validate if user still exists in deal
                string userExists = opportunityDetails.ValidateUserIfExists();
                Console.WriteLine("userExists: " + userExists);
                Assert.AreEqual("User does not exist", userExists);
                extentReports.CreateLog(userExists + " anymore in the deal ");

                //Validate if Opportunity exists under My Opportunities 
                driver.Navigate().Refresh();
                Thread.Sleep(3000);
                string recFound = opportunityHome.SearchMyOpportunities(opportunityNumber);
                Assert.AreEqual("No record found", recFound);
                extentReports.CreateLog("Opportunity is not displayed in My Opportunities of the user ");

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

