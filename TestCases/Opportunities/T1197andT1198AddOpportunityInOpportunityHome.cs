using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.UtilityFunctions;
using SalesForce_Project.TestData;
using System;
using SalesForce_Project.Pages.Common;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1197andT1198AddOpportunityInOpportunityHome:BaseClass
    {
       ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        SendEmailNotification notification = new SendEmailNotification();


        public static string fileTC1197 = "TC1197andTC1198AddOpportunityInOpportunityHome.xlsx";

       [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void OpportunityHome()
        {
            try
            {
                //Get Test Data file path                
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1197;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function           
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();             
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Calling Opportunity Home function           
                opportunityHome.ClickOpportunity();

                //Validating Title of Opportunity Record Type Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Opportunity: Select Opportunity Record Type ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validate Record Types                        
                Assert.IsTrue(opportunityHome.VerifyRecordTypes(), "Verified that displayed record types are same");
                extentReports.CreateLog("Displayed Record Types are correct ");

                //Validating Record Type Names and Description          
                Assert.IsTrue(opportunityHome.VerifyNamesAndDesc(), "Verified that displayed Names and Descriptions are same");
                extentReports.CreateLog("Names & Description is displayed as expected ");

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
